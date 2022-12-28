using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : ObjectSuperClass
{
    private StateMachine EnemyState; //���ꂼ��̃X�e�[�g(class)�̊Ǘ�
    private const float signPrefubDestroyTime = 3.0f; //�U���\���I�u�W�F�N�g�̔j������
    private Tween EnemyTweener;
    public Tween _EnemyTweener
    {
        get { return EnemyTweener; }
        set { EnemyTweener = value; }
    }
    [Header("Enemy�A�j���[�^�[�擾")]
    [SerializeField, Tooltip("�A�j���[�^�[")] Animator EnemyAnimator;
    public Animator _EnemyAnimator
    {
        get { return EnemyAnimator; }
    }
    [Header("EnemyAttack�X�e�[�g�֘A")]
    [SerializeField, Tooltip("�U���\���I�u�W�F�N�g")] GameObject AttackSignsPrefubs;
    public GameObject _AttackSignsPrefubs
    {
        get { return AttackSignsPrefubs; } 
    }
    [SerializeField, Tooltip("�U�����鎞��SE")] AudioClip AttackSE;
    public AudioClip _AttackSE
    {
        get { return AttackSE; }
    }

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        //AttackFromBack���n�܂�܂ł̓X�e�[�g��Run�ɂ���
        EnemyState = new StateMachine(new EnemyStateRun()); 
    }

    /// <summary>
    /// ���݂�����薈�t���[���Ăяo�����֐�
    /// </summary>
    void Update()
    {
        //���݂̃X�e�[�g�𖈃t���[���Ăяo��
        EnemyState.Update(this.gameObject);
    }

    /// <summary>
    /// �����蔻��̏����֐�
    /// </summary>
    /// <param name="other"> �Փ˔���</param>
    protected void OnTriggerEnter(Collider Other)
    {
        // EnemyAttack�����Ă���I�u�W�F�N�g�ɏՓ˂�����X�e�[�g��AttackFromBack�ɕύX
        if (Other.CompareTag("EnemyAttack"))
        { 
            EnemyState = new StateMachine(new EnemyStateAttackFromBack());
        }
    }
    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="_disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool Disposing)
    {
        if (this.isDisposed_)
        {
            // ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(Disposing);
    }

    /// <summary>
    /// �U���\���̐���
    /// </summary>
    /// <param name="SignPrefub">�U���\���I�u�W�F�N�g</param>
    /// <param name="EnemyTransform">Enemy�̈ʒu�擾</param>
    public void CreateSignPrefub(GameObject SignPrefub, Transform EnemyTransform)
    {
        //�U���\���I�u�W�F�N�g�𐶐�����ʒu�ݒ�
        var InstansPositon = new Vector3(EnemyTransform.position.x, EnemyTransform.position.y + 0.1f, EnemyTransform.position.z + 6);
        //�U���\���I�u�W�F�N�g����
        GameObject instanceObject = Instantiate(SignPrefub, InstansPositon, EnemyTransform.rotation);
        DOVirtual.DelayedCall(signPrefubDestroyTime, () =>
        {
            //�I�u�W�F�N�g�j��
            Destroy(instanceObject);
        });
    }
}
