using GameStateDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �~�j�Q�[��1�i�����j�̃V�[������
/// </summary>
public class GameStateMiniGame1 : StateBase
{
    public override void StateInitialize()
    {
        //TODO:Camera�̈ړ���`����C���^�[�t�F�[�X�̎����A�Ăяo��
        //��
        StaticInterfaceManager.MoveCamera(new Vector3(10, 5, 15));
        //TODO:�~�j�Q�[����Ԃɓ����Ă��邱�Ƃ�e�ł���GameStateController�ɓ`���Ă�������
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        gameObject.GetComponent<SceneManagerMain>().SwitchState(GameState.MiniGame1);

        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //�����Q�[���p�̃I�u�W�F�N�g���ďo�������Ă������̂��ȂƁi���h���I�ȁj
    }
}
