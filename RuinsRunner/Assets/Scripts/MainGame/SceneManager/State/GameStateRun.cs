using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public class GameStateRun : StateBase
{
    StateBase nextState;

    public override void StateInitialize()
    {
        nextState = this;
        //TODO:Camera�̈ړ���`����C���^�[�t�F�[�X�̎����A�Ăяo��
        //������
        StaticInterfaceManager.MoveCamera(new Vector3(0, 5, 5));
        //TODO:�����Q�[����Ԃɓ����Ă��邱�Ƃ�e�ł���GameStateController�ɓ`���Ă�������
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateMiniGame1();
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
        //�����Q�[���p�̃I�u�W�F�N�g�����X�ɓ��߂������肷��R���[�`���̌Ăяo�������Ă������Ǝv���i���h���j
    }
}
