using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController
    : ObjectSuperClass
{
    StateMachine EnemyState;

    [Header("Enemy�A�j���[�^�[�擾")]
    [SerializeField] Animator EnemyAnimator;
    public Animator _EnemyAnimator
    {
        get { return EnemyAnimator; }
    }
    [Header("EnemyAttack�X�e�[�g�֘A")]
    [SerializeField] GameObject AttackSignsPrefubs;
    public GameObject _AttackSignsPrefubs
    {
        get { return AttackSignsPrefubs; } 
    }
    [SerializeField] AudioClip AttackSE;
    public AudioClip _AttackSE
    {
        get { return AttackSE; }
    }

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        EnemyState = new StateMachine(new EnemyStateRun()); //AttackFromBack���n�܂�܂ł̓X�e�[�g��Run�ɂ���
    }

    /// <summary>
    /// ���݂�����薈�t���[���Ăяo�����֐�
    /// </summary>
    void Update()
    {
        EnemyState.Update(this.gameObject); //���݂̃X�e�[�g�𖈃t���[���Ăяo��
    }

    /// <summary>
    /// �����蔻��̏����֐�
    /// </summary>
    /// <param name="other"> �Փ˔���</param>
    protected void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("EnemyAttack")) //EnemyAttack�����Ă���I�u�W�F�N�g�ɏՓ˂�����X�e�[�g��AttackFromBack�ɕύX
        { 
            EnemyState = new StateMachine(new EnemyStateAttackFromBack());
        }
    }
    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="_disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        this.isDisposed_ = true; // Dispose�ς݂��L�^
        base.Dispose(_disposing); // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
    }

    /// <summary>
    /// �U���\���̐���
    /// </summary>
    /// <param name="SignPrefub">�U���\���I�u�W�F�N�g</param>
    /// <param name="EnemyTransform">Enemy�̈ʒu�擾</param>
    public void CreateSignPrefub(GameObject SignPrefub, Transform EnemyTransform)
    {
        var InstansPositon = new Vector3(EnemyTransform.position.x, EnemyTransform.position.y + 0.1f, EnemyTransform.position.z + 6); //�U���\���I�u�W�F�N�g�𐶐�����ʒu�ݒ�
        GameObject InstanceObject = Instantiate(SignPrefub, InstansPositon, EnemyTransform.rotation); //�U���\���I�u�W�F�N�g����
        DOVirtual.DelayedCall(3.0f, () =>
        {
            Destroy(InstanceObject); //�I�u�W�F�N�g�j��
        });
    }
}
