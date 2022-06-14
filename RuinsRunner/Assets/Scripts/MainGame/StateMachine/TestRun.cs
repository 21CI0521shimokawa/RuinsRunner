using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//テストクラス（Run）
public class TestRun : StateBase
{
    public override void StateInitialize()
    {
        Debug.Log("RunInitialize");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //Debug.Log("RunUpdate開始");
        StateBase nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestNormal();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            //Debug.Log("現在のState:" + this.StateName);
            //Debug.Log("一つ前のState:" + this.PreviousStateName);
        }

        //Debug.Log("RunUpdate終了");
        return nextState;
    }

    public override void StateFinalize()
    {
        Debug.Log("RunFinalize");
    }
}
