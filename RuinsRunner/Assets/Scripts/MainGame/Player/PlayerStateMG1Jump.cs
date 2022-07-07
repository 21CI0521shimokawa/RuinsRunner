using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMG1Jump : StateBase
{
    StateBase nextState;
    PlayerController playerController_;
    Rigidbody rigidBody_;
    float jumpPower_ = 5.0f;
    bool isJumped;
    //加える速度のベース値
    float speed_ = 20f;
    //移動速度の最大、最小
    float maxSpeed_ = 8f;
    float minSpeed_ = -8f;
    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateMG1Jump");
        nextState = this;
        rigidBody_ = playerController_.GetComponent<Rigidbody>();
        isJumped = false;
        Debug.Log(this.ToString() + "に入った");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //1フレーム目だったらジャンプ
        if (StateFlameCount == 1)
        {
            Jump(gameObject);
            return this;
        }

        //飛ぶ前だったら遷移しない
        if (!isJumped)
        {
            //飛んだか判定
            if (!playerController_.OnGround())
                isJumped = true;
            else
                return this;
        }

        //状態遷移
        //優先度低-------------------------------------------------------------------

        //待機状態
        if (playerController_.OnGround() && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Idle();

        //走る状態
        if (playerController_.OnGround() && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Run();

        //落下状態
        if (!playerController_.OnGround() && rigidBody_.velocity.y < 0)
            nextState = new PlayerStateMG1Fall();

        //優先度高-------------------------------------------------------------------
        return nextState;
    }
    public override void StateFinalize()
    {
        playerController_.animator_.ResetTrigger("StateMG1Jump");
    }
    void Jump(GameObject _gameobject)
    {
        rigidBody_.AddForce(Vector3.up * jumpPower_, ForceMode.Impulse);
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
        if (moveVec.x < 0)
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

            if (adjustedSpeed * adjustedSpeed > 0.01f)
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
