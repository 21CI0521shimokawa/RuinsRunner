using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundState_GameClear : StateBase
{
    MiniGameStickRoundAndRoundManager manager_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("RoundAndRoundManager");
        manager_ = managerGameObject.GetComponent<MiniGameStickRoundAndRoundManager>();

        PlayAudio.PlaySE(manager_.successSE);
        MiniGameStickRoundAndRoundManager.isGameClear = true;

        Debug.Log("クリア！！");
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
