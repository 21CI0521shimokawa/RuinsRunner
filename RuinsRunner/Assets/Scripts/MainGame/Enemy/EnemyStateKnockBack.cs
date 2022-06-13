using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateKnockBack : StateBase
{
    EnemyController enemyController;

    protected override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemyController.EnemyAnimator.SetTrigger("KnockBack");
    }
    protected override StateMachine StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;
        #region Runステート移行
        if(Action())
        {
            NextState = new EnemyStateRun();
        }
        #endregion

        return NextState;

    }
    protected override void StateFinalize()
    {
        enemyController._IsSqueeze = false;
    }
    bool Action()
    {
        if (StateTimeCount >= 0.2f)
        {
            enemyController._IsSqueeze = true;
        }

        return StateTimeCount >= 1.0f;
    }
}
