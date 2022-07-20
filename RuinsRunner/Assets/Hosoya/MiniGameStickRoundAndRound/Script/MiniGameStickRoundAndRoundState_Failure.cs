using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundState_Failure : StateBase
{
    MiniGameStickRoundAndRoundManager manager_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("RoundAndRoundManager");
        manager_ = managerGameObject.GetComponent<MiniGameStickRoundAndRoundManager>();

        PlayAudio.PlaySE(manager_.failureSE);
        MiniGameStickRoundAndRoundManager.isFailure = true;

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