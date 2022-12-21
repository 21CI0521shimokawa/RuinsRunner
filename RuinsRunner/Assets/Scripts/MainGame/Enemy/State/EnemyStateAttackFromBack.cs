using System;
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

    private enum attackFromBackState { IDOL, PREPARATION, ATTACK, BUCK, END }; //�X�e�[�g�ݒ�
    private attackFromBackState state;
    EnemyController enemyController; //Enemy�̐e�N���X�擾
    GameObject enemy; //Enemy�I�u�W�F�N�g�擾
    GameObject camera; //Camera�I�u�W�F�N�g�擾
    Transform followTargetTransform; //�U������ΏۃI�u�W�F�N�g�̃g�����X�t�H�[���擾
    private float defaultEnemyPositon; //�f�t�H���g��Enemy�̈ʒu(����)
    private int nowMiniGameCount; //���݂̃~�j�Q�[����
    private const int maxMiniGameCont = 2; //�~�j�Q�[���񐔐ݒ�
    private const float collisionThrottleTime = 5.0f; //�����蔻����{��
    private const float setDelayTime = 1.5f; //Z���ɓːi���鎞�ɑ҂���
    private const float movePrerationXSpeed = 3f; //�U������O�̉����̈ړ�����
    private const float moveAttackZSpeed = 1f; //�ΏۃI�u�W�F�N�g�ɓːi����ۂ̃X�s�[�h
    private const float moveReturnZSpeed = 0.5f; //�ːi���Ă���f�t�H���g�̈ʒu�ɖ߂�ۂ̃X�s�[�h

    /// <summary>
    /// �X�e�[�g���ύX����čŏ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateInitialize()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy"); //Enemy�I�u�W�F�N�g�擾
        camera = GameObject.FindGameObjectWithTag("MainCamera"); //���C���J�����I�u�W�F�N�g�擾
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemy�̐e�N���X�擾
        followTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //�^�[�Q�b�g(Player)�̃g�����X�t�H�[���擾
        enemyController._EnemyAnimator.SetTrigger("Chase");
        nowMiniGameCount = 0;
        defaultEnemyPositon = enemy.transform.position.z; //Enemy�̃f�t�H���g�ʒu���擾
        state = attackFromBackState.PREPARATION;
        StaticInterfaceManager.DoEnemyAttackMove(camera); //CameraController��DoEnemyFromBackMove�Ăяo��
        StaticInterfaceManager.StopConcentrationLineEffect(); //Player���������Ă���W�����G�t�F�N�g���~
        MovePreration(enemy);//�X�e�[�g��PREPARATION��������Ă΂��֐�
        MoveAttack(enemy); //�X�e�[�g��ATTACK��������Ă΂��֐�
        BackMove(enemy); //�X�e�[�g��BUCK��������Ă΂��֐�
    }

    /// <summary>
    /// �X�e�[�g���ύX�����܂Ŗ��t���[���Ă΂��֐�
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        //NowMiniGameCount��2�ɂȂ����炱�̃~�j�Q�[���I��
        if (state == attackFromBackState.END)
        {
            //�X�e�[�g��Run��Ԃɖ߂�
            nextState = new EnemyStateRun();
            //�}�b�v������ʏ�ɖ߂�
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>().endEnemyAttack = true;
        }
        return nextState;
    }

    /// <summary>
    /// �X�e�[�g���I��鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateFinalize()
    {
        //�J�������f�t�H���g�̈ʒu�ɖ߂�
        StaticInterfaceManager.DoReturnCameraMove(camera);
        //�O��������ł��钹�������Q�[���J�n
        StaticInterfaceManager.AvoidGameStart();
    }

    /// <summary>
    /// X���̉����^���֐�(�t�F�[�Y1)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void MovePreration(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.PREPARATION)
            .Subscribe(_ =>
            {
                //���[�v����񐔐ݒ�
                int prerationTime = UnityEngine.Random.Range(2, 3);

                //�ړ�����
                enemyController._EnemyTweener = enemyGameObject.transform.DOPath
                (
                    new[]
                    {
                        //�E�Ɉړ�
                        new Vector3(3.0f,enemyGameObject.transform.position.y,enemyGameObject.transform.position.z),
                        //���Ɉړ�
                        new Vector3(-3.0f,enemyGameObject.transform.position.y,enemyGameObject.transform.position.z),
                    },
                    //�R�b�Ԃŉ�������
                    movePrerationXSpeed,
                    //�C�[�W���O�^�C�v���w��
                    PathType.Linear
                 //�ŏ��̈ʒu�ɖ߂�prerationTime�����[�v������
                 ).SetOptions(true).SetLoops(prerationTime);

                //�ŏ��̈ʒu(�f�t�H���g��X�n�_�ɖ߂�)
                enemyController._EnemyTweener
                     .OnComplete(() =>
                     {
                         //�U���\������
                         enemyController.CreateSignPrefub(enemyController._AttackSignsPrefubs, enemyGameObject.transform);
                         //�X�e�[�g��Attack�ɕύX
                         state = attackFromBackState.ATTACK;
                     });
            });
    }

    /// <summary>
    /// Z���Ɉړ�����U���֐�(�t�F�[�Y2)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void MoveAttack(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.ATTACK)
            .Subscribe(_ =>
            {
                //�����蔻������{
                HitProcessingWithPlayer(enemyGameObject);
                //Enemy���ːi����ʒu���w��
                Vector3 enemyNewPositon = followTargetTransform.position;
                //Enemy�̈ړ�������setDelay���҂��Ă���s��
                enemyController._EnemyTweener = enemyGameObject.transform.DOMoveZ(enemyNewPositon.z, moveAttackZSpeed).SetDelay(setDelayTime);
                enemyController._EnemyTweener
                     .OnStart(() =>
                     {
                         //Attack�A�j���[�V�����Đ�
                         enemyController._EnemyAnimator.SetTrigger("Attack");
                         //�����̃G�t�F�N�g���Đ�s
                         StaticInterfaceManager.PlayEnemyStormEffect();
                         //�U��SE���Đ�
                         PlayAudio.PlaySE(enemyController._AttackSE);
                     })
                     .OnComplete(() =>
                     {
                         //�G�t�F�N�g���~
                         StaticInterfaceManager.StopEnemyStormEffect();
                         //�X�e�[�g��BUCK�ɕύX
                         state = attackFromBackState.BUCK;
                     });
            });
    }

    /// <summary>
    /// �U����ʏ펞�̈ʒu�ɖ߂�(�t�F�[�Y3)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void BackMove(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.BUCK)
            .Subscribe(_ =>
            {
                 enemyGameObject.transform.DOMoveZ(defaultEnemyPositon, moveReturnZSpeed) //defaultEnemyPositon��MoveReturnZSpeed�̑����Ŗ߂�
            �@.OnComplete(() =>
             {
                 nowMiniGameCount++; //�~�j�Q�[���J�E���g��1���₷
                 state = attackFromBackState.PREPARATION;
                 if (nowMiniGameCount >= maxMiniGameCont) //NowMiniGameCount��2�ȏゾ������X�e�[�g��END�ɕύX
                 {
                     state = attackFromBackState.END;
                 }
             });
            });
    }
    /// <summary>
    /// Player�Ƃ̓����蔻��̏����֐�
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void HitProcessingWithPlayer(GameObject enemyGameObject)
    {
        enemyGameObject.OnTriggerEnterAsObservable()
            //�Փ˂����I�u�W�F�N�g�̃^�O��
            .Select(collison => collison.tag)
             //"Player"��������
            .Where(tag => tag == "Player")
            //��x��������collisionThrottleTime���X���[����
            .ThrottleFirst(TimeSpan.FromSeconds(collisionThrottleTime))
            .Subscribe(collision =>
            {
                //�X�R�A���|100����
                StaticInterfaceManager.UpdateScore(-100);
                //�v���C���̃X�e�[�g��PlayerStateStumble�ɕύX
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3);
            });
    }
}
