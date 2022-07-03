using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMG1Run : StateBase
{
    StateBase nextState;
    PlayerController playerController_;
    Rigidbody rigidBody_;
    //加える速度のベース値
    float speed_ = 20f;
    //移動速度の最大、最小
    float maxSpeed_ = 8f;
    float minSpeed_ = -8f;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateMG1Run");
        nextState = this;
        rigidBody_ = playerController_.GetComponent<Rigidbody>();
        Debug.Log(this.ToString() + "に入った");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //左右移動
        SideMove();

        //状態遷移
        //優先度低-------------------------------------------------------------------

        //待機状態
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Idle();

        //ジャンプ状態
        if (Input.GetKeyDown(KeyCode.W))
            nextState =  new PlayerStateMG1Jump();

        //落下状態
        if (!playerController_.OnGround())
            nextState = new PlayerStateMG1Fall();

        //優先度高-------------------------------------------------------------------

        return nextState;
    }
    public override void StateFinalize()
    {

    }
    //左右移動
    void SideMove()
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        //入力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = speed_;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -speed_;
        }
        else
        {
            moveVec.x = 0;
        }

        //回転
        if (moveVec.x > 0)
            playerController_.transform.localEulerAngles = new Vector3(0, 90, 0);
        if(moveVec.x < 0)
            playerController_.transform.localEulerAngles = new Vector3(0, -90, 0);

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                //調整された加速力で力を加える
                rigidBody_.AddForce(new Vector3(moveVec.x, 0, 0));
                //速度制限
                rigidBody_.velocity = new Vector3(Mathf.Clamp(rigidBody_.velocity.x, minSpeed_, maxSpeed_), rigidBody_.velocity.y, rigidBody_.velocity.z);
            }
        }
        else
        {
            float currentSpeed = -rigidBody_.velocity.x;
            float adjustedSpeed = currentSpeed * 0.5f;

            if(adjustedSpeed * adjustedSpeed > 0.01f)
            {
                rigidBody_.AddForce(new Vector3(adjustedSpeed, 0, 0));
            }
            else
            {
                rigidBody_.velocity = new Vector3(0, 0, 0);
            }
        }
    }

}