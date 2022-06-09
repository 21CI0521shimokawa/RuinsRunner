using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//テストクラス（Run）
public class TestRun : StateMachine
{
    override protected void StateInitialize()
    {
        Debug.Log("RunInitialize");
    }

    override protected StateMachine StateUpdate()
    {
        //Debug.Log("RunUpdate開始");
        StateMachine nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestNormal();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            Debug.Log("現在のState:" + this.StateName);
            Debug.Log("一つ前のState:" + this.PreviousStateName);
        }

        //Debug.Log("RunUpdate終了");
        return nextState;
    }

    protected override void StateFinalize()
    {
        Debug.Log("RunFinalize");
    }
}
