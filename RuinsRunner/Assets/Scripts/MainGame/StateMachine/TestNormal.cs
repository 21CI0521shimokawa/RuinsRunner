using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//テストクラス（Normal）
public class TestNormal : StateBase
{
    override protected void StateInitialize()
    {
        Debug.Log("NormalInitialize");
    }

    override protected StateMachine StateUpdate(GameObject gameObject)
    {
        //Debug.Log("NormalUpdate開始");
        StateMachine nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestRun();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            Debug.Log("現在のState:" + this.StateName);
            Debug.Log("一つ前のState:" + this.PreviousStateName);
        }

        //Debug.Log("NormalUpdate終了");
        return nextState;
    }

    protected override void StateFinalize()
    {
        Debug.Log("NormalFinalize");
    }
}
