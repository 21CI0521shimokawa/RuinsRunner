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
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;

        #region 走っていないように見せる移動処理
        enemyController.GetComponent<Transform>().position += Vector3.back * MainGameConst.moveSpeed * Time.deltaTime;
        #endregion
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
