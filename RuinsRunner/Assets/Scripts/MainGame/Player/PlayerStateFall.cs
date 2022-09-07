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

    bool isFallIntoFall_; //���ɗ������ĕ��A�������ǂ���

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //playerController_.animator_.SetTrigger("StateFall");
        //1�t���[�������g���K�[���Z�b�g����
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

        //���ɗ�����܂ł̓v���C�������ɉ����Ȃ��痎��������悤�Ɂi���Ɉ���������s��h�~�j
        if(!isFallIntoFall_)
        {
            Vector3 newVec = playerController_.transform.position;
            newVec += Vector3.back * (MainGameConst.moveSpeed * MoveLooksLikeRunning.moveMagnification * 0.8f) * Time.deltaTime;

            //����艺�ɂ���Ȃ牺�����Ɉ�������i���Ɉ���������s��h�~�j
            if (playerController_.transform.position.y < 0.0f)
            {
                //newVec.y -= 10.0f * Time.deltaTime;
                playerController_.rigidbody_.AddForce(Vector3.down * 20.0f * Time.deltaTime, ForceMode.VelocityChange);
            }

            playerController_.rigidbody_.MovePosition(newVec);
        }

        //���ɗ������畜�A
        if(playerController_.FallIntoHole())
        {
            playerController_.Damage();

            playerController_.gameObject.GetComponent<Rigidbody>().position = new Vector3(0, 10, playerController_.GetPositionZ());
            playerController_.rigidbody_.velocity = new Vector3(0.0f, playerController_.rigidbody_.velocity.y, 0.0f);

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
