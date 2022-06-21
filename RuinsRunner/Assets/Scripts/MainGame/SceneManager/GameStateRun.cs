using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : StateBase
{
    Camera cam_;
    
    public override void StateInitialize() 
    {
        cam_ = Camera.main;
        //TODO:Camera�̈ړ���`����C���^�[�t�F�[�X�̎����A�Ăяo��
        //TODO:�����Q�[����Ԃɓ����Ă��邱�Ƃ�e�ł���GameStateController�ɓ`���Ă�������
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

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
