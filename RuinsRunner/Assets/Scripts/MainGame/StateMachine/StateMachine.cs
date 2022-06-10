using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>★★このクラスの継承は非推奨★★</para>
/// <para>★★StateBaseClassを使用してください★★</para>
/// </summary>
public abstract class StateMachine
{
    //コンストラクタ
    protected StateMachine()
    {
        this.StateInitialize();
    }

    /// <summary>
    /// ☆☆更新処理の時に呼び出す関数☆☆
    /// </summary>
    virtual public StateMachine Update(GameObject gameObject)
    {
        StateMachine nextState = StateUpdate(gameObject);

        ChangeState(nextState);

        return nextState;
    }



    /// <summary>
    /// ☆☆StateUpdate以外でStateを変えるときに呼び出す関数☆☆
    /// </summary>
    virtual public StateMachine ChangeState(StateMachine nextState)
    {
        this.StateFinalize();

        return nextState;
    }


    //抽象メソッド=================================================================
    abstract protected void StateInitialize();
    abstract protected StateMachine StateUpdate(GameObject gameObject);
    abstract protected void StateFinalize();
}
