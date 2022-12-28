using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    StateBase nextState; //���̃X�e�[�g(RunState)
    EnemyController enemyController; //Enemy�̐e�N���X���擾
    private Rigidbody enemyRigidBody; //Enemy�̍��̎擾
    private Transform followTargetTransform; ////�U������ΏۃI�u�W�F�N�g�̃g�����X�t�H�[���擾

    /// <summary>
    /// �X�e�[�g���ύX����čŏ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemy�̐e�N���X�擾
        enemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>(); //Enemy�̍��̎擾
        followTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //�^�[�Q�b�g(Player)�̃g�����X�t�H�[���擾
        enemyController._EnemyAnimator.SetTrigger("Run"); //Run�A�j���[�V�����Đ�
        nextState = this;
    }

    /// <summary>
    /// �X�e�[�g���ύX�����܂Ŗ��t���[���Ă΂��֐�
    /// </summary>
    /// <param name="EnemygameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject EnemygameObject)
    {
        return nextState;
    }

    /// <summary>
    /// �X�e�[�g���I��鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    public override void StateFinalize()
    {
        //�X�^�u
    }
}
