using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateRun : StateBase
{
    PlayerController playerController_;

    [SerializeField] float speed_;   //���E�ړ����x

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speed_ = 8;

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
        if(playerController_.canMove)
        {
            SideMove(gameObject);
        }

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

        //���݂�Z���W�ƃv���C��������͂��̍��W���قȂ��Ă�����
        //if (gameObject.transform.position.z != playerController_.GetPositionZ())
        {
            playerController_.FrontMove(gameObject);
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
