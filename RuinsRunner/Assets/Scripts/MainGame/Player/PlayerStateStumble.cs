using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateStumble : StateBase
{
    PlayerController playerController_;

    float moveStartPositionZ_;
    float moveEndPositionZ_;

    //State Hash
    readonly int hashStateStumble = Animator.StringToHash("Stumble");

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //�����郂�[�V��������Ȃ�������SetTrigger����
        //if (!playerController_.animator_.GetCurrentAnimatorStateInfo(0).IsName("Stumble"))
        //{
        //    playerController_.animator_.SetTrigger("StateStumble");
        //}
        //1�t���[�������g���K�[���Z�b�g����
        playerController_.animator_.SetTriggerOneFrame("StateStumble");

        moveStartPositionZ_ = playerController_.gameObject.transform.position.z;
        moveEndPositionZ_ = playerController_.GetPositionZ();

        PlayAudio.PlaySE(playerController_.stumbleSE);
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if (Action(gameObject))
        {
            nextState = new PlayerStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {

    }

    bool Action(GameObject _gameObject)
    {
        AnimatorStateInfo stateInfo = playerController_.animator_.GetCurrentAnimatorStateInfo(0);

        //�A�j���[�V�����������郂�[�V������
        if (stateInfo.shortNameHash == hashStateStumble)
        {
            Vector3 newVec = _gameObject.transform.position;

            Debug.Log(stateInfo.normalizedTime);

            //�ړ����Ԓ����ǂ���
            if (stateInfo.normalizedTime <= 1.0f)
            {
                newVec.z = Mathf.Lerp(moveStartPositionZ_, moveEndPositionZ_, stateInfo.normalizedTime);
            }
            else
            {
                newVec.z = moveEndPositionZ_;
            }

            _gameObject.transform.position = newVec;

            return stateInfo.normalizedTime >= 1.0f;
        }
        else
        {
            Debug.Log("������");
            return false;
        }
    }
}
