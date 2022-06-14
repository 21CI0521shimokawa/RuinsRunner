using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//テストクラス（Normal）
public class TestNormal : StateBase
{
    public override void StateInitialize()
    {
        Debug.Log("NormalInitialize");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //Debug.Log("NormalUpdate開始");
        StateBase nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestRun();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            //Debug.Log("現在のState:" + this.StateName);
            //Debug.Log("一つ前のState:" + this.PreviousStateName);
        }

        //Debug.Log("NormalUpdate終了");
        return nextState;
    }

    public override void StateFinalize()
    {
        Debug.Log("NormalFinalize");
    }
}
