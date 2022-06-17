using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : StateBase
{
    PlayerController playerController_;
    [SerializeField] float speed_;   //���E�ړ����x

    //�O��ړ��֌W
    bool isMoveZ;               //�O��ړ������ǂ���
    double moveZStartTime_;     //�O��ړ����n�߂�����
    float moveZStartPositionZ_;  //�O��ړ����n�߂��Ƃ���Z���W
    float moveZEndPositionZ_;   //�O��ړ����I��鎞��Z���W

    float moveZTime_;            //���b�����Ĉړ����邩

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

        //�O��ړ�
        //FrontAndBackMove(gameObject);

        //���ړ�
        SideMove(gameObject);

        //��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            nextState = new PlayerStateDefeat();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextState = new PlayerStateJump();
        }

        //���݂�Z���W���v���C��������\��̏ꏊ�̕����G�ɉ���������i���j
        if (gameObject.transform.position.z < playerController_.GetPositionZ())
        {
            FrontMove(gameObject);
        }

        //���݂�Z���W���v���C��������\��̏ꏊ�̕����G�ɋ߂�������
        if (gameObject.transform.position.z > playerController_.GetPositionZ())
        {
            nextState = new PlayerStateStumble();
        }


        //�G�ɋ߂Â���������i���j
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

    //���E�ړ�
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

    //�O��ړ�
    void FrontMove(GameObject _gameObject)
    {
        //���O��ړ������Ă��Ȃ����ǂ���
        if (!isMoveZ)
        {
            //���݂�Z���W�ƃv���C��������\��̏ꏊ���������
            if (_gameObject.transform.position.z != playerController_.GetPositionZ())
            {
                isMoveZ = true; //�O��ړ��J�n
                moveZStartTime_ = StateTimeCount;
                moveZStartPositionZ_ = _gameObject.transform.position.z;
                moveZEndPositionZ_ = playerController_.GetPositionZ();
            }
        }
        //�O��ړ���
        else
        {
            Vector3 newVec = _gameObject.transform.position;

            //�ړ����Ԓ����ǂ���
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
