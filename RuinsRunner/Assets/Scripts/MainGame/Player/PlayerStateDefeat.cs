using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDefeat : StateBase
{
    PlayerController playerController;

    protected override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.animator.SetTrigger("StateDefeat");
    }

    protected override StateMachine StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if(Action())
        {
            nextState = new PlayerStateRun();
        }

        return nextState;
    }

    protected override void StateFinalize()
    {
        playerController.Defert_Attack = false;
    }

    bool Action()
    {
        if(StateTimeCount >= 0.2f)
        {
            playerController.Defert_Attack = true;
        }

        return StateTimeCount >= 1.0f;
    }
}
