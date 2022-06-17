using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : StateBase
{
    PlayerController playerController_;
    [SerializeField] float speed_;   //左右移動速度

    //前後移動関係
    bool isMoveZ;               //前後移動中かどうか
    double moveZStartTime_;     //前後移動を始めた時間
    float moveZStartPositionZ_;  //前後移動を始めたときのZ座標
    float moveZEndPositionZ_;   //前後移動が終わる時のZ座標

    float moveZTime_;            //何秒かけて移動するか

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speed_ = 5;

        moveZStartTime_ = 0.0;
        moveZStartPositionZ_ = playerController_.gameObject.transform.position.z;
        moveZTime_ = 1.0f;

        playerController_.animator.SetTrigger("StateRun");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //前後移動
        //FrontAndBackMove(gameObject);

        //横移動
        SideMove(gameObject);

        //仮
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nextState = new PlayerStateDefeat();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextState = new PlayerStateJump();
        }

        //現在のZ座標よりプレイヤがいる予定の場所の方が敵に遠かったら（仮）
        if (gameObject.transform.position.z < playerController_.GetPositionZ())
        {
            FrontMove(gameObject);
        }

        //現在のZ座標よりプレイヤがいる予定の場所の方が敵に近かったら
        if (gameObject.transform.position.z > playerController_.GetPositionZ())
        {
            nextState = new PlayerStateStumble();
        }


        //敵に近づきすぎたら（仮）
        if (playerController_.IsBeCaught())
        {
            nextState = new PlayerStateBeCaught();
        }

        playerController_.OnGround();


        return nextState;
    }

    public override void StateFinalize()
    {

    }

    //左右移動
    void SideMove(GameObject _gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = speed_ * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -speed_ * Time.deltaTime;
        }
        else
        {
            moveVec.x = 0;
        }

        _gameObject.transform.position += moveVec;
    }

    //前後移動
    void FrontMove(GameObject _gameObject)
    {
        //今前後移動をしていないかどうか
        if (!isMoveZ)
        {
            //現在のZ座標とプレイヤがいる予定の場所が違ったら
            if (_gameObject.transform.position.z != playerController_.GetPositionZ())
            {
                isMoveZ = true; //前後移動開始
                moveZStartTime_ = StateTimeCount;
                moveZStartPositionZ_ = _gameObject.transform.position.z;
                moveZEndPositionZ_ = playerController_.GetPositionZ();
            }
        }
        //前後移動中
        else
        {
            Vector3 newVec = _gameObject.transform.position;

            //移動時間中かどうか
            if (StateTimeCount - moveZStartTime_ < moveZTime_)
            {
                newVec.z = Mathf.Lerp(moveZStartPositionZ_, moveZEndPositionZ_, Mathf.InverseLerp((float)moveZStartTime_, (float)(moveZStartTime_ + moveZTime_), (float)StateTimeCount));
            }
            else
            {
                newVec.z = moveZEndPositionZ_;
                isMoveZ = false;
            }

            _gameObject.transform.position = newVec;
        }
    }
}
