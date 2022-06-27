using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    PlayerController playerController_;
    Rigidbody rigidbody_;

    [SerializeField] float sideMoveSpeed_;   //���E�ړ����x
    float jumpPower_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateJump");

        rigidbody_ = playerController_.GetComponent<Rigidbody>();

        jumpPower_ = 5.0f;
        sideMoveSpeed_ = 5;
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //���ړ�
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
            rigidbody_.AddForce(Vector3.up * jumpPower_, ForceMode.Impulse);
        }

        _gameobject.transform.position = playerPosition;

        return StateTimeCount >= 1.0;
    }

    //���E�ړ�
    void SideMove(GameObject _gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = sideMoveSpeed_/* * Time.deltaTime*/;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -sideMoveSpeed_/* * Time.deltaTime*/;
        }
        else
        {
            moveVec.x = 0;
        }

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                //�w�肵���X�s�[�h���猻�݂̑��x�������ĉ����͂����߂�
                float currentSpeed = moveVec.x - rigidbody_.velocity.x;
                //�������ꂽ�����͂ŗ͂�������
                rigidbody_.AddForce(new Vector3(currentSpeed, 0, 0));

                //_gameObject.transform.position += moveVec;
            }
            else
            {
                rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z);
            }
        }
    }
}
