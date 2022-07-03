using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQTEState_GameClear : StateBase
{
    MiniGameQTEManager manager_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("QTEManager");
        manager_ = managerGameObject.GetComponent<MiniGameQTEManager>();

        MiniGameQTEManager.isGameClear = true;

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
