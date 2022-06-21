using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public class GameStateRun : StateBase
{

    public override void StateInitialize() 
    {
        //TODO:Camera�̈ړ���`����C���^�[�t�F�[�X�̎����A�Ăяo��
        //������
        StaticInterfaceManager.MoveCamera(new Vector3(0, 5, 5));
        //TODO:�����Q�[����Ԃɓ����Ă��邱�Ƃ�e�ł���GameStateController�ɓ`���Ă�������
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        gameObject.GetComponent<SceneManagerMain>().SwitchState(GameState.Run);

        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateMiniGame1();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //�����Q�[���p�̃I�u�W�F�N�g�����X�ɓ��߂������肷��R���[�`���̌Ăяo�������Ă������Ǝv���i���h���j
    }
}
