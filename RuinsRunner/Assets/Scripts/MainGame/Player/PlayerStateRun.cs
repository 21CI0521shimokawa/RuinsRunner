using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : StateBase
{
    PlayerController playerController;
    [SerializeField] float speed;   //���E�ړ����x

    protected override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speed = 5;

        playerController.animator.SetTrigger("StateRun");
    }

    protected override StateMachine StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        //���ړ�
        SideMove(gameObject);

        //��
        if(Input.GetKeyDown(KeyCode.Space))
        {
            nextState = new PlayerStateDefeat();
        }

        return nextState;
    }

    protected override void StateFinalize()
    {

    }

    //�ړ�
    void SideMove(GameObject gameObject)
    {
        Vector3 moveVec = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -speed;
        }
        else
        {
            moveVec.x = 0;
        }

        gameObject.transform.position += moveVec * Time.deltaTime;
    }
}
