using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class BirdController : ObjectSuperClass
{
    [Header("���̐ݒ�")]
    [SerializeField,Tooltip("���̈ړ����鎞��")] float BirdSpeed;
    [SerializeField,Tooltip("���̈ړ��ړI�n�ݒ�")] float BirdDestinationPositonZ;
    [SerializeField,Tooltip("�U���ΏۈȊO�̒��̃I�u�W�F�N�g�j�����鎞��")] float DestroyTime;
    [SerializeField,Tooltip("���̑ҋ@����")] float DelayTime;
    [SerializeField,Tooltip("�C�[�W���O�ݒ�")] Ease BirdEaseType;
    [SerializeField,Tooltip("���̓����蔻��")] BoxCollider boxCollider;
    bool isAnimEnd = false;
    [Header("�A�j���[�^�[")] 
    [SerializeField,Tooltip("�A�j���[�^�[�擾")] Animator animator;
    [Header("�O���A�g")]
    public bool isAttack = false; //AvoidGameController�Ŏg�p��

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        HitProcessingWithPlayer(); //Player�^�O�����Ă���I�u�W�F�N�g�ƏՓ˔���
        BirdMove(); //���̋����̌Ăяo��
    }

    /// <summary>
    /// ���̓����̊֐�
    /// </summary>
    private void BirdMove()
    {
        this.transform.DOMoveZ(BirdDestinationPositonZ, BirdSpeed) //BirdSpeed�̑����ňړ�����
            .SetDelay(DelayTime)
            .OnStart(() =>
            {
                if (!isAttack)
                {
                    boxCollider.enabled = false;
                    StartCoroutine(DestroyAfterSecond(DestroyTime));
                }
            })
            .OnUpdate(() =>
            {
                if (!this.isAttack) //�U���ΏۈȊO�̒��������痣���
                {
                    Leave(); //�U���ΏۈȊO�̒��������痣���
                }
                else
                {
                    Attack(); //�U���Ώۂ̒���������U���J�n
                }
            })
            .OnComplete(() =>
            {
                Destroy(this.gameObject); //�ړ�����������I�u�W�F�N�g��j��
            })
            .SetEase(BirdEaseType); //�C�[�W���O�^�C�v�ݒ�
    }

    /// <summary>
    /// Player�Ƃ̓����蔻��̏����֐�
    /// </summary>
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnterAsObservable()
            .Select(collison => collison.tag) //�Փ˂����I�u�W�F�N�g�̃^�O��
            .Where(tag => tag == "Player") //"Player"��������
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100); //�X�R�A��-100����

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3); //�v���C���̃X�e�[�g��PlayerStateStumble�ɕύX

                Destroy(gameObject); //Player�^�O�����Ă���I�u�W�F�N�g�ɏՓ˂����璹�I�u�W�F�N�g��j��
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
        if(this.transform.position.z < 15 && !isAnimEnd)
        {
            animator.SetBool("Attack", true);
            isAnimEnd=true;
        }
    }

    /// <summary>
    /// ����destroyTime�Ŕj��
    /// </summary>
    /// <param name="destroyTime">�j�����鎞��</param>
    /// <returns></returns>
    IEnumerator DestroyAfterSecond(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject); //�I�u�W�F�N�g��j��
        yield return null;
    }
}
