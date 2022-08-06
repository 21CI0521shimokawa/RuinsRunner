using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateKnockBack : StateBase
{
    EnemyController enemyController;

    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemyController.EnemyAnimator.SetTrigger("KnockBack");
        //���܂���ꂽ�̂ŃX�R�A���Z
        StaticInterfaceManager.UpdateScore(300);
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;

        #region �����Ă��Ȃ��悤�Ɍ�����ړ�����
        enemyController.GetComponent<Transform>().position += Vector3.back * MainGameConst.moveSpeed * Time.deltaTime;
        #endregion
        #region Run�X�e�[�g�ڍs
        if (Action())
        {
            NextState = new EnemyStateRun();
        }
        #endregion

        return NextState;

    }
    public override void StateFinalize()
    {
    }
    bool Action()
    {
        return StateTimeCount >= 1.0f;
    }
}
