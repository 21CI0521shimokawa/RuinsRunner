using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateLanding : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateLanding");

        PlayAudio.PlaySE(playerController_.landingSE);
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
        return StateTimeCount >= 0.7f;
    }

}
