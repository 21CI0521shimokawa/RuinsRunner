using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    PlayerController playerController;

    float jumpPower_;

    public override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.animator.SetTrigger("StateJump");

        jumpPower_ = 5.0f;
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

    bool Action(GameObject _gameobject)
    {
        Vector3 playerPosition = _gameobject.transform.position;

        if(StateFlameCount == 1)
        {
            Rigidbody rigidbody = _gameobject.GetComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.up * jumpPower_, ForceMode.Impulse);
        }

        _gameobject.transform.position = playerPosition;

        return StateTimeCount >= 1.0;
    }
}
