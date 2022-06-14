using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>����Object�ɕϐ��Ƃ��Ď�������Class����</para>
/// </summary>
public class StateMachine
{
    StateBase nowState_;    //���݂�State


    string stateName_;
    /// <summary>
    /// �������݂�state(class)�����擾����
    /// </summary>
    public string StateName
    {
        get { return stateName_; }
    }

    string previousStateName_;
    /// <summary>
    /// ������O��state(class)�����擾����
    /// </summary>
    public string PreviousStateName
    {
        get { return previousStateName_; }
    }

    /// <summary>
    /// <para>�����R���X�g���N�^����</para>
    /// <para>���������ɊJ�n����State��n������</para>
    /// </summary>
    public StateMachine(StateBase startState)
    {
        nowState_ = startState;
        stateName_ = startState.GetType().Name;
    }

    /// <summary>
    /// <para>�����X�V�����̎��ɌĂяo���֐�����</para>
    /// <para>����������gameobject��n����State���Ŏg���遙��</para>
    /// </summary>
    public void Update(GameObject gameObject)
    {
        //�X�V����
        StateBase nextState = nowState_.StateUpdate(gameObject);

        //�J�E���^�̍X�V
        nextState.CountUpdate();

        //State�ύX����
        ChangeState(nextState);
    }



    //State�̕ύX����
    private void ChangeState(StateBase nextState)
    {
        string stateName = nowState_.GetType().Name;  //State(Class)���̎擾
        string nextStateName = nextState.GetType().Name;  //State(Class)���̎擾

        //���ƈႤState�Ɉڍs���悤�Ƃ��Ă�����
        if (stateName != nextStateName)
        {
            nowState_.StateFinalize();

            previousStateName_ = stateName;
            stateName_ = nextStateName;

            nowState_ = nextState;
        }
    }
}
