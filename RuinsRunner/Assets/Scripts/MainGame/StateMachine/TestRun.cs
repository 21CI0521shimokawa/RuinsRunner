using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�e�X�g�N���X�iRun�j
public class TestRun : StateMachine
{
    override protected void StateInitialize()
    {
        Debug.Log("RunInitialize");
    }

    override protected StateMachine StateUpdate()
    {
        //Debug.Log("RunUpdate�J�n");
        StateMachine nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestNormal();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            Debug.Log("���݂�State:" + this.StateName);
            Debug.Log("��O��State:" + this.PreviousStateName);
        }

        //Debug.Log("RunUpdate�I��");
        return nextState;
    }

    protected override void StateFinalize()
    {
        Debug.Log("RunFinalize");
    }
}
