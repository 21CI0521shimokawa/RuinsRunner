using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
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



    //�R���X�g���N�^
    protected StateMachine()
    {
        this.StateInitialize();
        CountReset();
        this.stateName_ = this.GetType().Name;  //State(Class)���̎擾
    }

    //�f�X�g���N�^
    ~StateMachine()
    {
    }

    /// <summary>
    /// �����X�V�����̎��ɌĂяo���֐�����
    /// </summary>
    public StateMachine Update()
    {
        StateMachine nextState = StateUpdate();

        CountUpdate();

        if (this != nextState)
        {
            ChangeState(this, nextState);
        }

        return nextState;
    }

    #region private�֐�

    //State�̐؂�ւ����̏���
    private StateMachine ChangeState(StateMachine nowState, StateMachine nextState)
    {
        nextState.previousStateName_ = nowState.stateName_;

        nowState.StateFinalize();

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

    #endregion

    //���ۃ��\�b�h=================================================================

    virtual protected void StateInitialize()
    {
        Debug.LogWarning("StateInitialize���I�[�o�[���C�h����Ă��܂���");
    }

    virtual protected StateMachine StateUpdate()
    {
        Debug.LogWarning("StateUpdate���I�[�o�[���C�h����Ă��܂���");
        return this;
    }

    virtual protected void StateFinalize()
    {
        Debug.LogWarning("StateFinalize���I�[�o�[���C�h����Ă��܂���");
    }
}
