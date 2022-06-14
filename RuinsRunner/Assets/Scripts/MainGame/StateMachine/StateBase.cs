using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
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



    //コンストラクタ
    public StateBase()
    {
        CountReset();
        StateInitialize();
    }

    //カウンターの初期化
    private void CountReset()
    {
        stateFlameCount_ = 0;
        stateTimeCount_ = 0.0;
    }

    /// <summary>
    /// <para>★★カウンタの更新処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    public void CountUpdate()
    {
        ++stateFlameCount_;
        stateTimeCount_ += Time.deltaTime;
    }

    //抽象メソッド=================================================================
    /// <summary>
    /// <para>★★State内の初期化処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract public void StateInitialize();
    /// <summary>
    /// <para>★★State内の更新処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract public StateBase StateUpdate(GameObject gameObject);
    /// <summary>
    /// <para>★★State内の終了処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract public void StateFinalize();
}
