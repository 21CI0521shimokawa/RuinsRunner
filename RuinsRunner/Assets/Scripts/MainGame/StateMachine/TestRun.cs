using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�e�X�g�N���X�iRun�j
public class TestRun : StateBase
{
    public override void StateInitialize()
    {
        Debug.Log("RunInitialize");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //Debug.Log("RunUpdate�J�n");
        StateBase nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestNormal();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            //Debug.Log("���݂�State:" + this.StateName);
            //Debug.Log("��O��State:" + this.PreviousStateName);
        }

        //Debug.Log("RunUpdate�I��");
        return nextState;
    }

    public override void StateFinalize()
    {
        Debug.Log("RunFinalize");
    }
}
