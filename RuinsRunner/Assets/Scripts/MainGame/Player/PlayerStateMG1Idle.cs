using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMG1Idle : StateBase
{
    StateBase nextState;
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateMG1Idle");
        nextState = this;
        Debug.Log(this.ToString() + "‚É“ü‚Á‚½");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //ó‘Ô‘JˆÚ
        //—Dæ“x’á-------------------------------------------------------------------

        //‘–‚èó‘Ô
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            nextState = new PlayerStateMG1Run();

        //ƒWƒƒƒ“ƒvó‘Ô
        if (Input.GetKeyDown(KeyCode.W))
            nextState = new PlayerStateMG1Jump();

        //—‰ºó‘Ô
        if (!playerController_.OnGround())
            nextState = new PlayerStateMG1Fall();

        //—Dæ“x‚-------------------------------------------------------------------
        return nextState;
    }
    public override void StateFinalize()
    {

    }

}