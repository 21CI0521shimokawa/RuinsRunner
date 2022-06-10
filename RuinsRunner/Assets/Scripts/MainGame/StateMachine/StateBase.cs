using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : StateMachine
{
    int stateFlameCount_;
    /// <summary>
    /// ��������state�ɂȂ��Ă��牽�t���[���o�߂���������
    /// </summary>
    public int StateFlameCount
    {
        get { return stateFlameCount_; }
    }

    double stateTimeCount_;
    /// <summary>
    /// ��������state�ɂȂ��Ă��牽�b�o�߂���������
    /// </summary>
    public double StateTimeCount
    {
        get { return stateTimeCount_; }
    }

    string stateName_;
    /// <summary>
    /// �������݂�state(class)�����擾����
    ///�y���ݎg�p�s�z
    /// </summary>
    public string StateName
    {
        get { return stateName_; }
    }

    string previousStateName_;
    /// <summary>
    /// ������O��state(class)�����擾����
    ///�y���ݎg�p�s�z
    /// </summary>
    public string PreviousStateName
    {
        get { return previousStateName_; }
    }

    //�R���X�g���N�^
    public StateBase()
    {
        CountReset();
    }

    /// <summary>
    /// ����StateUpdate�ȊO��State��ς���Ƃ��ɌĂяo���֐�����
    /// </summary>
    override public StateMachine ChangeState(StateMachine nextState)
    {
        string stateName = this.GetType().Name;  //State(Class)���̎擾
        string nextStateName = nextState.GetType().Name;  //State(Class)���̎擾

        //���ƈႤState�Ɉڍs���悤�Ƃ��Ă�����
        if (stateName != nextStateName)
        {
            previousStateName_ = stateName;
            stateName_ = nextStateName;

            //StateMachine��ChangeState����������
            base.ChangeState(nextState);

            CountReset();

            return nextState;
        }

        return this;
    }


    /// <summary>
    /// <para>�����X�V�����̎��ɌĂяo���֐�����</para>
    /// <para>��������GameObject����������Ƃ��ꂪ�N���X���Ŏg����悤�ɂȂ遙</para>
    /// </summary>
    override public StateMachine Update(GameObject gameObject)
    {
        //State��Update����
        StateMachine nextState = StateUpdate(gameObject);

        //State���ς���Ă��Ȃ��Ȃ�
        if (this == ChangeState(nextState))
        {
            CountUpdate();
            return this;
        }

        return nextState;
    }


    //�J�E���^�[�̏�����
    private void CountReset()
    {
        stateFlameCount_ = 0;
        stateTimeCount_ = 0.0;
    }

    //�J�E���^�[�̍X�V����
    private void CountUpdate()
    {
        ++stateFlameCount_;
        stateTimeCount_ += Time.deltaTime;
    }

    //���ۃ��\�b�h=================================================================
    protected override void StateInitialize()
    {
        throw new System.NotImplementedException();
    }

    protected override StateMachine StateUpdate(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    protected override void StateFinalize()
    {
        throw new System.NotImplementedException();
    }
}
