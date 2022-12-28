using System;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BirdController : ObjectSuperClass
{
    [Header("���̐ݒ�")]
    [SerializeField, Tooltip("���̈ړ����鎞��")] float birdSpeed;
    [SerializeField, Tooltip("���̈ړ��ړI�n�ݒ�")] float birdDestinationPositonZ;
    [SerializeField, Tooltip("�U���ΏۈȊO�̒��̃I�u�W�F�N�g�j�����鎞��")] float destroyTime;
    [SerializeField, Tooltip("���̑ҋ@����")] float delayTime;
    [SerializeField, Tooltip("�C�[�W���O�ݒ�")] Ease birdEaseType;
    [SerializeField, Tooltip("���̓����蔻��")] BoxCollider boxCollider;
    private bool isAnimEnd = false; //�U���Ώۂ̒��ȊO�������痣��Ă����A�j���[�V�������Đ������邽�߂̕ϐ�
    private const float collisionThrottleTime = 3.0f; //�����蔻���ϐ����X���[����
    [Header("�A�j���[�^�[")]
    [SerializeField, Tooltip("�A�j���[�^�[�擾")] Animator animator;
    [Header("�O���A�g")]
    public bool IsAttack = false; //AvoidGameController�Ŏg�p��

    /// <summary>I
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        //Player�^�O�����Ă���I�u�W�F�N�g�ƏՓ˔���
        HitProcessingWithPlayer();
        //���̋����̌Ăяo��
        BirdMove();
    }

    /// <summary>
    /// ���̓����̊֐�
    /// </summary>
    private void BirdMove()
    {
        //delayTime���҂��Ă���ړ��J�n���C�[�W���O�^�C�v���w��
        var tween = transform.DOMoveZ(birdDestinationPositonZ, birdSpeed).SetDelay(delayTime).SetEase(birdEaseType);

        tween
           .OnStart(() =>
           {
               if (!IsAttack)
               {
                   boxCollider.enabled = false;
                   DestroyAfterSecond(destroyTime).Forget();
               }
           })
           .OnUpdate(() =>
           {
               //�U���ΏۈȊO�̒��������痣���
               if (!this.IsAttack)
               {
                   //�U���ΏۈȊO�̒��������痣���
                   Leave();
               }
               else
               {
                   //�U���Ώۂ̒���������U���J�n
                   Attack();
               }
           })
           .OnComplete(() =>
           {
               //�ړ�����������I�u�W�F�N�g��j��
               Destroy(this.gameObject);
           });
    }

    /// <summary>
    /// Player�Ƃ̓����蔻��̏����֐�
    /// </summary>
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnterAsObservable()
            //�Փ˂����^�O��
            .Select(collison => collison.tag)
             //"Player"��������
            .Where(tag => tag == "Player")
             //��x��������collisionThrottleTime���X���[����
            .ThrottleFirst(TimeSpan.FromSeconds(collisionThrottleTime))
            .Subscribe(collision =>
            {
                //�X�R�A��-100����
                StaticInterfaceManager.UpdateScore(-100);
                //�v���C���̃X�e�[�g��PlayerStateStumble�ɕύX
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3);
                //Player�^�O�����Ă���I�u�W�F�N�g�ɏՓ˂����璹�I�u�W�F�N�g��j��
                Destroy(gameObject);
            });
    }

    /// <summary>
    /// ���̗���鏈��
    /// </summary>
    private void Leave()
    {
        if (isAnimEnd) return;
        animator.SetBool("Leave", true);
        isAnimEnd = true;
    }

    /// <summary>
    /// ���̍U�����鏈��
    /// </summary>
    private void Attack()
    {
        if (this.transform.position.z < 15 && !isAnimEnd)
        {
            animator.SetBool("Attack", true);
            isAnimEnd = true;
        }
    }

    /// <summary>
    /// ����destroyTime�Ŕj��
    /// </summary>
    /// <param name="destroyTime">�j�����鎞��</param>
    /// <returns></returns>
    private async UniTask DestroyAfterSecond(float destroyTime)
    {
        //�j�����鎞�ԕ��҂�
        await UniTask.Delay(TimeSpan.FromSeconds(destroyTime));
        //�I�u�W�F�N�g��j��
        Destroy(gameObject);
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(disposing);
    }
}
