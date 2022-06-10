using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase : StateMachine
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
    ///【現在使用不可】
    /// </summary>
    public string StateName
    {
        get { return stateName_; }
    }

    string previousStateName_;
    /// <summary>
    /// ☆☆一つ前のstate(class)名を取得☆☆
    ///【現在使用不可】
    /// </summary>
    public string PreviousStateName
    {
        get { return previousStateName_; }
    }

    //コンストラクタ
    public StateBase()
    {
        CountReset();
    }

    /// <summary>
    /// ☆☆StateUpdate以外でStateを変えるときに呼び出す関数☆☆
    /// </summary>
    override public StateMachine ChangeState(StateMachine nextState)
    {
        string stateName = this.GetType().Name;  //State(Class)名の取得
        string nextStateName = nextState.GetType().Name;  //State(Class)名の取得

        //今と違うStateに移行しようとしていたら
        if (stateName != nextStateName)
        {
            previousStateName_ = stateName;
            stateName_ = nextStateName;

            //StateMachineのChangeStateを処理する
            base.ChangeState(nextState);

            CountReset();

            return nextState;
        }

        return this;
    }


    /// <summary>
    /// <para>☆☆更新処理の時に呼び出す関数☆☆</para>
    /// <para>☆引数にGameObjectを持たせるとそれがクラス内で使えるようになる☆</para>
    /// </summary>
    override public StateMachine Update(GameObject gameObject)
    {
        //StateのUpdate処理
        StateMachine nextState = StateUpdate(gameObject);

        //Stateが変わっていないなら
        if (this == ChangeState(nextState))
        {
            CountUpdate();
            return this;
        }

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

    //抽象メソッド=================================================================
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
