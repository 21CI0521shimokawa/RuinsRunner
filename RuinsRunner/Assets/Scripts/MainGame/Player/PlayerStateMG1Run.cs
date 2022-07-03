using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMG1Run : StateBase
{
    StateBase nextState;
    PlayerController playerController_;
    Rigidbody rigidBody_;
    //�����鑬�x�̃x�[�X�l
    float speed_ = 20f;
    //�ړ����x�̍ő�A�ŏ�
    float maxSpeed_ = 8f;
    float minSpeed_ = -8f;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateMG1Run");
        nextState = this;
        rigidBody_ = playerController_.GetComponent<Rigidbody>();
        Debug.Log(this.ToString() + "�ɓ�����");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //���E�ړ�
        SideMove();

        //��ԑJ��
        //�D��x��-------------------------------------------------------------------

        //�ҋ@���
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Idle();

        //�W�����v���
        if (Input.GetKeyDown(KeyCode.W))
            nextState =  new PlayerStateMG1Jump();

        //�������
        if (!playerController_.OnGround())
            nextState = new PlayerStateMG1Fall();

        //�D��x��-------------------------------------------------------------------

        return nextState;
    }
    public override void StateFinalize()
    {

    }
    //���E�ړ�
    void SideMove()
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        //����
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

        //��]
        if (moveVec.x > 0)
            playerController_.transform.localEulerAngles = new Vector3(0, 90, 0);
        if(moveVec.x < 0)
            playerController_.transform.localEulerAngles = new Vector3(0, -90, 0);

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                //�������ꂽ�����͂ŗ͂�������
                rigidBody_.AddForce(new Vector3(moveVec.x, 0, 0));
                //���x����
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