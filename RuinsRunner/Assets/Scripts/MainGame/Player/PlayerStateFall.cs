using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStateFall 
    : StateBase
    , IEditingCameraPositon
{
    PlayerController playerController_;
    GameObject PlayerObject;
    GameObject CameraObject;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateFall");
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        CameraObject = GameObject.FindGameObjectWithTag("MainCamera");
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
        DoEditingCameraPositon(CameraObject);
        playerController_.animator_.ResetTrigger("StateFall");
    }

    bool Action()
    {

        //ŒŠ‚É—Ž‚¿‚½‚ç•œ‹A
        if(playerController_.FallIntoHole())
        {
            playerController_.Damage();

            playerController_.gameObject.GetComponent<Rigidbody>().position = new Vector3(0, 10, playerController_.transform.position.z/*playerController_.GetPositionZ()*/);
        }


        return playerController_.OnGround();
    }
    public void DoEditingCameraPositon(GameObject CameraObject)
    {
        var NewPositonZ =PlayerObject.transform.position.z-6;
        CameraObject.transform.DOMoveZ(NewPositonZ, 1f);
    }
}
