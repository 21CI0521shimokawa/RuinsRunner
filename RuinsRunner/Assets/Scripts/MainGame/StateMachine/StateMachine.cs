using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>�������̃N���X�̌p���͔񐄏�����</para>
/// <para>����StateBaseClass���g�p���Ă�����������</para>
/// </summary>
public abstract class StateMachine
{
    //�R���X�g���N�^
    protected StateMachine()
    {
        this.StateInitialize();
    }

    /// <summary>
    /// �����X�V�����̎��ɌĂяo���֐�����
    /// </summary>
    virtual public StateMachine Update(GameObject gameObject)
    {
        StateMachine nextState = StateUpdate(gameObject);

        ChangeState(nextState);

        return nextState;
    }



    /// <summary>
    /// ����StateUpdate�ȊO��State��ς���Ƃ��ɌĂяo���֐�����
    /// </summary>
    virtual public StateMachine ChangeState(StateMachine nextState)
    {
        this.StateFinalize();

        return nextState;
    }


    //���ۃ��\�b�h=================================================================
    abstract protected void StateInitialize();
    abstract protected StateMachine StateUpdate(GameObject gameObject);
    abstract protected void StateFinalize();
}
