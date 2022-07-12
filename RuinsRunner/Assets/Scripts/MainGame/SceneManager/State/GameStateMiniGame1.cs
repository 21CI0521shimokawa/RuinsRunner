using GameStateDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �~�j�Q�[��1�i�����j�̃V�[������
/// </summary>
public class GameStateMiniGame1 : StateBase
{
    StateBase nextState;
    public override void StateInitialize()
    {
        nextState = this;
        //TODO:Camera�̈ړ���`����C���^�[�t�F�[�X�̎����A�Ăяo��
        //��
        StaticInterfaceManager.MoveCamera(new Vector3(10, 5, 15));
        //TODO:�~�j�Q�[����Ԃɓ����Ă��邱�Ƃ�e�ł���GameStateController�ɓ`���Ă�������
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateRun();
        }

        return nextState;
    }
    /// <summary>
    /// �O����X�e�[�g��ς������Ƃ��Ɏg�p����i�}���ō�������߁A�����ƈ��S�ɐ����ł���n�Y�j
    /// </summary>
    public void ChangeState(StateBase _NextState)
    {
        nextState = _NextState;
    }
    public override void StateFinalize()
    {
        //�����Q�[���p�̃I�u�W�F�N�g���ďo�������Ă������̂��ȂƁi���h���I�ȁj
    }
}
