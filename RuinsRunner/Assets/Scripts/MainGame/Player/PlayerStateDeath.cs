using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDeath : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateDeath");

        MoveLooksLikeRunning.Set_isRunning(false);   //ˆÚ“®’âŽ~
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if (Action())
        {
            nextState = new PlayerStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {

    }


    bool Action()
    {
        return false;
    }

}