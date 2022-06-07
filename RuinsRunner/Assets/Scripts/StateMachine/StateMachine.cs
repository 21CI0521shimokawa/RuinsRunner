using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    int stateFlameCount_;
    /// <summary>
    /// ☆☆そのstateになってから何フレーム経過したか☆☆
    /// </summary>
    public int StateFlameCount
    {
        get { return stateFlameCount_; }
    }

    double stateTimeCount_;
    /// <summary>
    /// ☆☆そのstateになってから何秒経過したか☆☆
    /// </summary>
    public double StateTimeCount
    {
        get { return stateTimeCount_; }
    }

    string stateName_;
    /// <summary>
    /// ☆☆現在のstate(class)名を取得☆☆
    /// </summary>
    public string StateName
    {
        get { return stateName_; }
    }

    string previousStateName_;
    /// <summary>
    /// ☆☆一つ前のstate(class)名を取得☆☆
    /// </summary>
    public string PreviousStateName
    {
        get { return previousStateName_; }
    }



    //コンストラクタ
    protected StateMachine()
    {
        this.StateInitialize();
        CountReset();
        this.stateName_ = this.GetType().Name;  //State(Class)名の取得
    }

    //デストラクタ
    ~StateMachine()
    {
    }

    /// <summary>
    /// ☆☆更新処理の時に呼び出す関数☆☆
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

    #region private関数

    //Stateの切り替え時の処理
    private StateMachine ChangeState(StateMachine nowState, StateMachine nextState)
    {
        nextState.previousStateName_ = nowState.stateName_;

        nowState.StateFinalize();

        return nextState;
    }

    //カウンターの初期化
    private void CountReset()
    {
        stateFlameCount_ = 0;
        stateTimeCount_ = 0.0;
    }

    //カウンターの更新処理
    private void CountUpdate()
    {
        ++stateFlameCount_;
        stateTimeCount_ += Time.deltaTime;
    }

    #endregion

    //抽象メソッド=================================================================

    virtual protected void StateInitialize()
    {
        Debug.LogWarning("StateInitializeがオーバーライドされていません");
    }

    virtual protected StateMachine StateUpdate()
    {
        Debug.LogWarning("StateUpdateがオーバーライドされていません");
        return this;
    }

    virtual protected void StateFinalize()
    {
        Debug.LogWarning("StateFinalizeがオーバーライドされていません");
    }
}
