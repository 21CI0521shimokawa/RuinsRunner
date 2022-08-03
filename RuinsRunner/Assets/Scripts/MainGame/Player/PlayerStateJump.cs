using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    PlayerController playerController_;
    Rigidbody rigidbody_;

    [SerializeField] float sideMoveSpeed_;   //左右移動速度
    float jumpPower_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateJump");

        rigidbody_ = playerController_.GetComponent<Rigidbody>();

        jumpPower_ = 6.0f;
        sideMoveSpeed_ = 5;
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //横移動
        SideMove(gameObject);

        if (Action(gameObject))
        {
            nextState = new PlayerStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {

    }

    bool Action(GameObject _gameobject)
    {
        Vector3 playerPosition = _gameobject.transform.position;

        if(StateFlameCount == 1)
        {
            PlayAudio.PlaySE(playerController_.jumpSE);
            rigidbody_.AddForce(Vector3.up * jumpPower_, ForceMode.Impulse);
        }

        _gameobject.transform.position = playerPosition;

        return StateTimeCount >= 1.0;
    }

    //左右移動
    void SideMove(GameObject _gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        #region 旧
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    moveVec.x = speed_;
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveVec.x = -speed_;
        //}
        //else
        //{
        //    moveVec.x = 0;
        //}
        #endregion

        float gamepadStickLX = ControllerManager.GetGamepadStickL().x;

        moveVec.x += gamepadStickLX > 0.9f ? sideMoveSpeed_ : 0;
        moveVec.x += gamepadStickLX < -0.9f ? -sideMoveSpeed_ : 0;

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                rigidbody_.velocity = new Vector3(moveVec.x, rigidbody_.velocity.y, rigidbody_.velocity.z);

                //指定したスピードから現在の速度を引いて加速力を求める
                //float currentSpeed = moveVec.x - rigidbody_.velocity.x;
                //調整された加速力で力を加える
                //rigidbody_.AddForce(new Vector3(currentSpeed, 0, 0));


                //_gameObject.transform.position += moveVec;
            }
            else
            {
                rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z);
            }
        }
        else
        {
            rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z);
        }
    }
}
