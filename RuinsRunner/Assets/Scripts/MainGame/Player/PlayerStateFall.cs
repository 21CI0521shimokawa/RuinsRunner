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
        playerController_.animator_.ResetTrigger("StateFall");
    }

    bool Action()
    {

        //���ɗ������畜�A
        if(playerController_.FallIntoHole())
        {
            playerController_.Damage();

            playerController_.gameObject.GetComponent<Rigidbody>().position = new Vector3(0, 10, playerController_.transform.position.z/*playerController_.GetPositionZ()*/);
        }


        return playerController_.OnGround();
    }
}
