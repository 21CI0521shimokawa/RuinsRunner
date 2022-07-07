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
    //�����鑬�x�̃x�[�X�l
    float speed_ = 20f;
    //�ړ����x�̍ő�A�ŏ�
    float maxSpeed_ = 8f;
    float minSpeed_ = -8f;
    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateMG1Jump");
        nextState = this;
        rigidBody_ = playerController_.GetComponent<Rigidbody>();
        isJumped = false;
        Debug.Log(this.ToString() + "�ɓ�����");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //1�t���[���ڂ�������W�����v
        if (StateFlameCount == 1)
        {
            Jump(gameObject);
            return this;
        }

        //��ԑO��������J�ڂ��Ȃ�
        if (!isJumped)
        {
            //��񂾂�����
            if (!playerController_.OnGround())
                isJumped = true;
            else
                return this;
        }

        //��ԑJ��
        //�D��x��-------------------------------------------------------------------

        //�ҋ@���
        if (playerController_.OnGround() && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Idle();

        //������
        if (playerController_.OnGround() && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
            nextState = new PlayerStateMG1Run();

        //�������
        if (!playerController_.OnGround() && rigidBody_.velocity.y < 0)
            nextState = new PlayerStateMG1Fall();

        //�D��x��-------------------------------------------------------------------
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
        if (moveVec.x < 0)
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
