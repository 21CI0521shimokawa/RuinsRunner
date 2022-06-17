using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateStumble : StateBase
{
    PlayerController playerController;

    float moveStartPositionZ_;
    float moveEndPositionZ_;

    //State Hash
    readonly int hashStateStumble = Animator.StringToHash("Stumble");

    public override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.animator.SetTrigger("StateStumble");

        moveStartPositionZ_ = playerController.gameObject.transform.position.z;
        moveEndPositionZ_ = playerController.GetPositionZ();
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
        AnimatorStateInfo stateInfo = playerController.animator.GetCurrentAnimatorStateInfo(0);

        //アニメーションがこけるモーションか
        if (stateInfo.shortNameHash == hashStateStumble)
        {
            Vector3 newVec = _gameObject.transform.position;

            Debug.Log(stateInfo.normalizedTime);

            //移動時間中かどうか
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
            return false;
        }
    }
}
