using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQTEState_Failure : StateBase
{
    MiniGameQTEManager manager_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("QTEManager");
        manager_ = managerGameObject.GetComponent<MiniGameQTEManager>();

        PlayAudio.PlaySE(manager_.failureSE);
        MiniGameQTEManager.isFailure = true;

        Debug.Log("é∏îsÅc");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        return nextState;
    }

    public override void StateFinalize()
    {

    }
}
