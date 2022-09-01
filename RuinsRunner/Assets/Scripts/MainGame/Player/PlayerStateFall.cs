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
    //GameObject CameraObject;

    bool isFallIntoFall_; //穴に落下して復帰したかどうか

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //playerController_.animator_.SetTrigger("StateFall");
        //1フレームだけトリガーをセットする
        playerController_.animator_.SetTriggerOneFrame("StateFall");
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        //CameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        isFallIntoFall_ = false;
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

        if(isFallIntoFall_)
        {
            playerController_.LostCoin(5);
        }
    }

    bool Action()
    {

        //穴に落ちたら復帰
        if(playerController_.FallIntoHole())
        {
            playerController_.Damage();

            playerController_.gameObject.GetComponent<Rigidbody>().position = new Vector3(0, 10, playerController_.GetPositionZ());

            isFallIntoFall_ = true;

            //DoEditingCameraPositon(CameraObject);
        }


        return playerController_.OnGround();
    }
    public void DoEditingCameraPositon(GameObject CameraObject)
    {
        var NewPositonZ =PlayerObject.transform.position.z-6;
        CameraObject.transform.DOMoveZ(NewPositonZ, 1f);
    }
}
