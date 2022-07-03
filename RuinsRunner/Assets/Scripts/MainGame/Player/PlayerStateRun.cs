using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : StateBase
{
    PlayerController playerController_;
    Rigidbody rigidbody_;

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
        rigidbody_ = playerController_.gameObject.GetComponent<Rigidbody>();
        speed_ = 30;

        moveZStartTime_ = 0.0;
        moveZStartPositionZ_ = playerController_.gameObject.transform.position.z;
        moveZTime_ = 1.0f;

        //走るモーションじゃなかったらSetTriggerする
        if (!playerController_.animator_.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
        {
            playerController_.animator_.SetTrigger("StateRun");
        }
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //前後移動
        //FrontAndBackMove(gameObject);

        //横移動
        SideMove(gameObject);

        //ミニゲーム呼び出し（仮）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new PlayerState_Test(); //仮
        }

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
        //if分条件を '<' から　'!=' にしました（前後の移動を確認したかったため） 工藤 6/29
        if (gameObject.transform.position.z != playerController_.GetPositionZ())
        {
            FrontMove(gameObject);    //こいつのせいで柱すり抜ける
        }

        //トラップを踏んだら
        if(playerController_.OnTrap())
        {
            playerController_.Damage();
            nextState = new PlayerStateStumble();
        }

        //現在のZ座標よりプレイヤがいる予定の場所の方が敵に近かったら
        //if (gameObject.transform.position.z > playerController_.GetPositionZ())
        //{
        //    nextState = new PlayerStateStumble();
        //}

        //敵に近づきすぎたら（仮）
        if (playerController_.IsBeCaught())
        {
            //nextState = new PlayerStateBeCaught();

            nextState = new PlayerStateDeath(); //仮 とりあえず殺しとく
        }

        //落下していたら
        if (!playerController_.OnGround())
        {
            nextState = new PlayerStateFall();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z); //X軸方向の移動を消す
    }

    //左右移動
    void SideMove(GameObject _gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = speed_/* * Time.deltaTime*/;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -speed_/* * Time.deltaTime*/;
        }
        else
        {
            moveVec.x = 0;
        }

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                //指定したスピードから現在の速度を引いて加速力を求める
                float currentSpeed = moveVec.x - rigidbody_.velocity.x;
                //調整された加速力で力を加える
                rigidbody_.AddForce(new Vector3(currentSpeed, 0, 0));

                //_gameObject.transform.position += moveVec;
            }
            else
            {
                rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z);
            }
        }
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

            //_gameObject.transform.position = newVec;
            rigidbody_.MovePosition(newVec);
        }
    }
}
