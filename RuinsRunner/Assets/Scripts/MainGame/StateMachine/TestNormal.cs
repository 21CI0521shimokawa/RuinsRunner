using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�e�X�g�N���X�iNormal�j
public class TestNormal : StateBase
{
    override protected void StateInitialize()
    {
        Debug.Log("NormalInitialize");
    }

    override protected StateMachine StateUpdate(GameObject gameObject)
    {
        //Debug.Log("NormalUpdate�J�n");
        StateMachine nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestRun();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            Debug.Log("���݂�State:" + this.StateName);
            Debug.Log("��O��State:" + this.PreviousStateName);
        }

        //Debug.Log("NormalUpdate�I��");
        return nextState;
    }

    protected override void StateFinalize()
    {
        Debug.Log("NormalFinalize");
    }
}
