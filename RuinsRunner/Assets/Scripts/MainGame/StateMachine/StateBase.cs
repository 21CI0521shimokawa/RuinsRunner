using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
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



    //�R���X�g���N�^
    public StateBase()
    {
        CountReset();
        StateInitialize();
    }

    //�J�E���^�[�̏�����
    private void CountReset()
    {
        stateFlameCount_ = 0;
        stateTimeCount_ = 0.0;
    }

    /// <summary>
    /// <para>�����J�E���^�̍X�V��������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    public void CountUpdate()
    {
        ++stateFlameCount_;
        stateTimeCount_ += Time.deltaTime;
    }

    //���ۃ��\�b�h=================================================================
    /// <summary>
    /// <para>����State���̏�������������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract public void StateInitialize();
    /// <summary>
    /// <para>����State���̍X�V��������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract public StateBase StateUpdate(GameObject gameObject);
    /// <summary>
    /// <para>����State���̏I����������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract public void StateFinalize();
}
