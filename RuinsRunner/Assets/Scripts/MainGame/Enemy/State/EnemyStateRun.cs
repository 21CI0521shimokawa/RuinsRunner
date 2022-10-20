using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    StateBase NextState; //���̃X�e�[�g(RunState)
    EnemyController EnemyController; //Enemy�̐e�N���X���擾
    private Rigidbody EnemyRigidBody; //Enemy�̍��̎擾
    private Transform FollowTargetTransform; ////�U������ΏۃI�u�W�F�N�g�̃g�����X�t�H�[���擾

    /// <summary>
    /// �X�e�[�g���ύX����čŏ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateInitialize()
    {
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemy�̐e�N���X�擾
        EnemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>(); //Enemy�̍��̎擾
        FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //�^�[�Q�b�g(Player)�̃g�����X�t�H�[���擾
        EnemyController._EnemyAnimator.SetTrigger("Run"); //Run�A�j���[�V�����Đ�
        NextState = this;
    }

    /// <summary>
    /// �X�e�[�g���ύX�����܂Ŗ��t���[���Ă΂��֐�
    /// </summary>
    /// <param name="EnemygameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject EnemygameObject)
    {
        return NextState;
    }

    /// <summary>
    /// �X�e�[�g���I��鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateFinalize()
    {
        //�X�^�u
    }
}
