using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        speed_ = 8;

        moveZStartTime_ = 0.0;
        moveZStartPositionZ_ = playerController_.gameObject.transform.position.z;
        moveZTime_ = 1.0f;

        //���郂�[�V��������Ȃ�������SetTrigger����
        //if (!playerController_.animator_.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
        //{
        //    //playerController_.animator_.SetTrigger("StateRun");
        //}

        //1�t���[�������g���K�[���Z�b�g����
        playerController_.animator_.SetTriggerOneFrame("StateRun");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //�O��ړ�
        //FrontAndBackMove(gameObject);

        //���ړ�
        SideMove(gameObject);

        //�i���j
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nextState = new PlayerState_Test(); //��

            playerController_.LostCoin(5);
        }

        //�� ���F�{�^��

        Gamepad gamepad = Gamepad.current;

        if (gamepad != null)
        {
            //���� �{�^��
            //if (Keyboard.current.rKey.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame)
            //{
            //    GameObject pillar = PillarChack();
            //    if (pillar != null)
            //    {
            //        //PlayerStatePillarDefeatMiniGame state = new PlayerStatePillarDefeatMiniGame();
            //        //state.pillar = pillar;
            //        //nextState = state;

            //        //StateBase buf3 = new PlayerStatePillarDefeatMiniGame();
            //        //((PlayerStatePillarDefeatMiniGame)buf3).pillar = pillar;

            //        //StateBase buf3 = new PlayerStatePillarDefeatMiniGame();
            //        //(buf3 as PlayerStatePillarDefeatMiniGame).pillar = pillar;
            //    }
            //}

            //�W�����v �ԃ{�^��
            if (Keyboard.current.qKey.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                nextState = new PlayerStateJump();
            }
        }
        else
        {
            //���� �{�^��
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                GameObject pillar = PillarChack();
                if (pillar != null)
                {
                    PlayerStatePillarDefeatMiniGame state = new PlayerStatePillarDefeatMiniGame();
                    state.pillar = pillar;
                    nextState = state;

                    //StateBase buf3 = new PlayerStatePillarDefeatMiniGame();
                    //((PlayerStatePillarDefeatMiniGame)buf3).pillar = pillar;

                    //StateBase buf3 = new PlayerStatePillarDefeatMiniGame();
                    //(buf3 as PlayerStatePillarDefeatMiniGame).pillar = pillar;
                }
            }

            //�W�����v �ԃ{�^��
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                nextState = new PlayerStateJump();
            }
        }

        //���݂�Z���W���v���C��������\��̏ꏊ�̕����G�ɉ���������i���j
        //if�������� '<' ����@'!=' �ɂ��܂����i�O��̈ړ����m�F�������������߁j �H�� 6/29
        if (gameObject.transform.position.z != playerController_.GetPositionZ())
        {
            FrontMove(gameObject);    //�����̂����Œ����蔲����
        }

        //�g���b�v�𓥂񂾂�
        if (playerController_.OnTrap())
        {
            playerController_.Damage();
            nextState = new PlayerStateStumble();
        }

        //���݂�Z���W���v���C��������\��̏ꏊ�̕����G�ɋ߂�������
        //if (gameObject.transform.position.z > playerController_.GetPositionZ())
        //{
        //    nextState = new PlayerStateStumble();
        //}

        //�G�ɋ߂Â���������i���j
        //if (playerController_.IsBeCaught())
        //{
        //    nextState = new PlayerStateBeCaught();
        //}

        //�������Ă�����
        if (!playerController_.OnGround())
        {
            nextState = new PlayerStateFall();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //rigidbody_.velocity = new Vector3(0, rigidbody_.velocity.y, rigidbody_.velocity.z); //X�������̈ړ�������
    }

    //���E�ړ�
    void SideMove(GameObject _gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        #region ��
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

        #region ��
        //moveVec.x += gamepadStickLX > 0.6f ? speed_ : 0;
        //moveVec.x += gamepadStickLX < -0.6f ? -speed_ : 0;
        #endregion

        //�X�e�B�b�N��|�����������ړ�
        moveVec.x = Mathf.Lerp(0, speed_, Mathf.InverseLerp(0.1f, 0.8f, Mathf.Abs(gamepadStickLX)));
        moveVec.x *= Mathf.Sign(gamepadStickLX);

        //�ړ��{���ɉ����Ĉړ����x���グ��
        //�������Ă���{����50������������
        moveVec.x *= ((MoveLooksLikeRunning.moveMagnification - 1.0f) * 0.5f) + 1.0f;

        if (moveVec.x != 0)
        {
            if (playerController_.PlayerMoveChack(moveVec.normalized * 0.05f))
            {
                playerController_.rigidbody_.velocity = new Vector3(playerController_.isReverseLR ? -moveVec.x : moveVec.x, playerController_.rigidbody_.velocity.y, playerController_.rigidbody_.velocity.z);

                //�w�肵���X�s�[�h���猻�݂̑��x�������ĉ����͂����߂�
                //float currentSpeed = moveVec.x - rigidbody_.velocity.x;
                //�������ꂽ�����͂ŗ͂�������
                //rigidbody_.AddForce(new Vector3(currentSpeed, 0, 0));


                //_gameObject.transform.position += moveVec;
            }
            else
            {
                playerController_.rigidbody_.velocity = new Vector3(0, playerController_.rigidbody_.velocity.y, playerController_.rigidbody_.velocity.z);
            }
        }
        else
        {
            playerController_.rigidbody_.velocity = new Vector3(0, playerController_.rigidbody_.velocity.y, playerController_.rigidbody_.velocity.z);
        }
    }

    //�O��ړ�
    void FrontMove(GameObject _gameObject)
    {
        //���O��ړ������Ă��Ȃ����ǂ���
        if (!isMoveZ)
        {
            ////���݂�Z���W�ƃv���C��������\��̏ꏊ���������
            if (_gameObject.transform.position.z != playerController_.GetPositionZ())
            {
                //�O�ɏ�Q�����Ȃ����v���C����������x���ɉ���������
                if(playerController_.PlayerMoveChack(new Vector3(0, 0, 1)) || _gameObject.transform.position.z < 7.0f)
                {
                    isMoveZ = true; //�O��ړ��J�n
                    moveZStartTime_ = StateTimeCount;
                    moveZStartPositionZ_ = _gameObject.transform.position.z;
                    moveZEndPositionZ_ = playerController_.GetPositionZ();
                }
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

            //_gameObject.transform.position = newVec;
            playerController_.rigidbody_.MovePosition(newVec);
        }
    }


    //�����m
    GameObject PillarChack()
    {
        //Pillar�i���́j���擾
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pillar");

        Vector3 origin = playerController_.gameObject.transform.position; // ���_
        float radius = 2.0f;

        //�͈͓���collider�����m
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);

        foreach (Collider hit in hitColliders)
        {
            for (int g = 0; g < gameObjects.Length; ++g)
            {
                //Ray����������gameobject�Ǝ擾����gameobject����v������
                if (hit.gameObject == gameObjects[g])
                {
                    return gameObjects[g];
                }
            }
        }
        return null;
    }
}
