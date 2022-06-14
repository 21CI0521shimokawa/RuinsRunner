using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�e�X�g�N���X�iNormal�j
public class TestNormal : StateBase
{
    public override void StateInitialize()
    {
        Debug.Log("NormalInitialize");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //Debug.Log("NormalUpdate�J�n");
        StateBase nextState = this;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new TestRun();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(this.StateTimeCount);

            //Debug.Log("���݂�State:" + this.StateName);
            //Debug.Log("��O��State:" + this.PreviousStateName);
        }

        //Debug.Log("NormalUpdate�I��");
        return nextState;
    }

    public override void StateFinalize()
    {
        Debug.Log("NormalFinalize");
    }
}
