using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFall : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateFall");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if(Action())
        {
            if(StateTimeCount >= 0.3f)
            {
                nextState = new PlayerStateLanding();
            }
            else
            {
                nextState = new PlayerStateRun();
            }
        }

        return nextState;
    }

    public override void StateFinalize()
    {

    }

    bool Action()
    {

        //ŒŠ‚É—Ž‚¿‚½‚ç•œ‹A
        if(playerController_.FallIntoHole())
        {
            for (int i = 0; i < 2; ++i)
            {
                playerController_.Damage();
            }

            playerController_.gameObject.GetComponent<Rigidbody>().position = new Vector3(0, 10, playerController_.GetPositionZ());
        }


        return playerController_.OnGround();
    }
}
