using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateCatch : StateBase
{
    EnemyController enemyController;

    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemyController.EnemyAnimator.SetTrigger("Catch");
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;
        #region Runステート移行
        if (Action())
        {
            NextState = new EnemyStateRun();
        }
        #endregion

        return NextState;

    }
    public override void StateFinalize()
    {
        enemyController._IsCatch = false;
    }
    bool Action()
    {
        return StateTimeCount >= 1.0f;
    }
}
