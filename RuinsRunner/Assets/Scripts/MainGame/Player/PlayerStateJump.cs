using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    PlayerController playerController;

    public override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.animator.SetTrigger("StateJump");
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

        //ˆÚ“®i–¢ŽÀ‘•j

        _gameobject.transform.position = playerPosition;

        return StateTimeCount >= 1.0;
    }

    float easeOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    float easeInQuad(float x)
    {
        return x * x;
    }
}
