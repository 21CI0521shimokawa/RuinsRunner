using System.Collections;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Toolkit;
using UniRx.Triggers;
using DG.Tweening;
public class EnemyStateAttackFromBack : StateBase
{

    private enum AttackFromBackState { IDOL, PREPARATION, ATTACK, BUCK, END }; //�X�e�[�g�ݒ�
    private AttackFromBackState State; 
    EnemyController EnemyController; //Enemy�̐e�N���X�擾
    GameObject Enemy; //Enemy�I�u�W�F�N�g�擾
    GameObject Camera; //Camera�I�u�W�F�N�g�擾
    Transform FollowTargetTransform; //�U������ΏۃI�u�W�F�N�g�̃g�����X�t�H�[���擾
    private float DefaultEnemyPositon; //�f�t�H���g��Enemy�̈ʒu(����)
    private int NowMiniGameCount; //���݂̃~�j�Q�[����
    private const int MaxMiniGameCont = 2; //�~�j�Q�[���񐔐ݒ�
    private const int CollisionDetectionImplement = 1; //�����蔻����{��
    private const float SetDelayTime = 1.5f; //Z���ɓːi���鎞�ɑ҂���
    private const float MovePrerationXSpeed = 3f; //�U������O�̉����̈ړ�����
    private const float MoveAttackZSpeed = 1f; //�ΏۃI�u�W�F�N�g�ɓːi����ۂ̃X�s�[�h
    private const float MoveReturnZSpeed = 0.5f; //�ːi���Ă���f�t�H���g�̈ʒu�ɖ߂�ۂ̃X�s�[�h

    /// <summary>
    /// �X�e�[�g���ύX����čŏ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateInitialize()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy"); //Enemy�I�u�W�F�N�g�擾
        Camera = GameObject.FindGameObjectWithTag("MainCamera"); //���C���J�����I�u�W�F�N�g�擾
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemy�̐e�N���X�擾
        FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //�^�[�Q�b�g(Player)�̃g�����X�t�H�[���擾
        EnemyController._EnemyAnimator.SetTrigger("Chase");
        NowMiniGameCount = 0;
        DefaultEnemyPositon = Enemy.transform.position.z; //Enemy�̃f�t�H���g�ʒu���擾
        State = AttackFromBackState.PREPARATION;
        StaticInterfaceManager.DoEnemyAttackMove(Camera); //CameraController��DoEnemyFromBackMove�Ăяo��
        StaticInterfaceManager.StopConcentrationLineEffect(); //Player���������Ă���W�����G�t�F�N�g���~
        MovePreration(Enemy);//�X�e�[�g��PREPARATION��������Ă΂��֐�
        MoveAttack(Enemy); //�X�e�[�g��ATTACK��������Ă΂��֐�
        BackMove(Enemy); //�X�e�[�g��BUCK��������Ă΂��֐�
    }

    /// <summary>
    /// �X�e�[�g���ύX�����܂Ŗ��t���[���Ă΂��֐�
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;
        if (State == AttackFromBackState.END) //NowMiniGameCount��2�ɂȂ����炱�̃~�j�Q�[���I��
        {
            NextState = new EnemyStateRun(); //�X�e�[�g��Run��Ԃɖ߂�
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>().endEnemyAttack = true; //�}�b�v������ʏ�ɖ߂�
        }
        return NextState;
    }

    /// <summary>
    /// �X�e�[�g���I��鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateFinalize()
    {
        StaticInterfaceManager.DoReturnCameraMove(Camera); //�J�������f�t�H���g�̈ʒu�ɖ߂�
        StaticInterfaceManager.AvoidGameStart(); //�O��������ł��钹�������Q�[���J�n
    }
    
    /// <summary>
    /// X���̉����^���֐�(�t�F�[�Y1)
    /// </summary>
    /// <param name="EnemygameObject"></param>
    private void MovePreration(GameObject EnemygameObject)
    {
        EnemygameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.PREPARATION)
            .Subscribe(_ =>
            {
                var PrerationTime = Random.Range(2, 3); //���[�v����񐔐ݒ�
                EnemygameObject.transform.DOPath //�ړ�����ʒu�ݒ�
                    (
                    new[]
                    {
              new Vector3(3.0f,EnemygameObject.transform.position.y,EnemygameObject.transform.position.z), //�E�Ɉړ�
              new Vector3(-3.0f,EnemygameObject.transform.position.y,EnemygameObject.transform.position.z), //���Ɉړ�
                    },
                    MovePrerationXSpeed, PathType.Linear //�R�b�Ԃŉ�������
                    ).SetOptions(true) //�ŏ��̈ʒu(�f�t�H���g��X�n�_�ɖ߂�)
                     .SetLoops(PrerationTime) //�O�񃋁[�v
                     .OnComplete(() =>
                     {
                         EnemyController.CreateSignPrefub(EnemyController._AttackSignsPrefubs, EnemygameObject.transform); //�U���\������
                         State = AttackFromBackState.ATTACK; //�X�e�[�g��Attack�ɕύX
                     });
            });
    }

    /// <summary>
    /// Z���Ɉړ�����U���֐�(�t�F�[�Y2)
    /// </summary>
    /// <param name="EnemygameObject"></param>
    private void MoveAttack(GameObject EnemygameObject)
    {//�������@�ύX�\��
        EnemygameObject.ObserveEveryValueChanged(x => State)
           .Where(x => State == AttackFromBackState.ATTACK)
            .Subscribe(_ =>
            {
                HitProcessingWithPlayer(EnemygameObject); //�����蔻����X�e�[�g��Attack�̎��̂݋���
                var EnemyNewPositon = FollowTargetTransform.position; //Enemy���ːi����ʒu���w��
                EnemygameObject.transform.DOMoveZ(EnemyNewPositon.z, MoveAttackZSpeed)
            .SetDelay(SetDelayTime)
            .OnStart(() =>
            {
                EnemyController._EnemyAnimator.SetTrigger("Attack"); //Attack�A�j���[�V�����Đ�
                StaticInterfaceManager.PlayEnemyStormEffect(); //�����̃G�t�F�N�g���Đ�
                PlayAudio.PlaySE(EnemyController._AttackSE); //�U��SE���Đ�
            })
             .OnComplete(() =>
             {
                 StaticInterfaceManager.StopEnemyStormEffect(); //�G�t�F�N�g���~
                 State = AttackFromBackState.BUCK; //�X�e�[�g��BUCK�ɕύX
             });
            });
    }

    /// <summary>
    /// �U����ʏ펞�̈ʒu�ɖ߂�(�t�F�[�Y3)
    /// </summary>
    /// <param name="gameObject"></param>
    private void BackMove(GameObject gameObject)
    {
        gameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.BUCK)
            .Subscribe(_ =>
            {
                gameObject.transform.DOMoveZ(DefaultEnemyPositon, MoveReturnZSpeed) //DefaultEnemyPositon��MoveReturnZSpeed�̑����Ŗ߂�
                .OnComplete(() =>
                {
                    NowMiniGameCount++; //�~�j�Q�[���J�E���g��1���₷
                    State = AttackFromBackState.PREPARATION;
                    if (NowMiniGameCount >= MaxMiniGameCont) //NowMiniGameCount��2�ȏゾ������X�e�[�g��END�ɕύX
                    {
                        State = AttackFromBackState.END;
                    }
                });
            });
    }
    /// <summary>
    /// Player�Ƃ̓����蔻��̏����֐�
    /// </summary>
    /// <param name="gameObject"></param>
    private void HitProcessingWithPlayer(GameObject gameObject)
    {
        gameObject.OnTriggerEnterAsObservable()
            .Select(collison => collison.tag) //�Փ˂����I�u�W�F�N�g�̃^�O��
            .Where(tag => tag == "Player") //"Player"��������
            .Take(CollisionDetectionImplement) //���̂ݎ��s
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100); //�X�R�A���|100����
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3); //�v���C���̃X�e�[�g��PlayerStateStumble�ɕύX
            });
    }
}
