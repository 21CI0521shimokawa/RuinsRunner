using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>☆☆Objectに変数として持たせるClass☆☆</para>
/// </summary>
public class StateMachine
{
    StateBase nowState_;    //現在のState


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

    /// <summary>
    /// <para>☆☆コンストラクタ☆☆</para>
    /// <para>☆☆引数に開始時のStateを渡す☆☆</para>
    /// </summary>
    public StateMachine(StateBase startState)
    {
        nowState_ = startState;
        stateName_ = startState.GetType().Name;
    }

    /// <summary>
    /// <para>☆☆更新処理の時に呼び出す関数☆☆</para>
    /// <para>☆☆引数にgameobjectを渡すとState内で使える☆☆</para>
    /// </summary>
    public void Update(GameObject gameObject)
    {
        //更新処理
        StateBase nextState = nowState_.StateUpdate(gameObject);

        //カウンタの更新
        nextState.CountUpdate();

        //State変更処理
        ChangeState(nextState);
    }



    //Stateの変更処理
    private void ChangeState(StateBase nextState)
    {
        string stateName = nowState_.GetType().Name;  //State(Class)名の取得
        string nextStateName = nextState.GetType().Name;  //State(Class)名の取得

        //今と違うStateに移行しようとしていたら
        if (stateName != nextStateName)
        {
            nowState_.StateFinalize();

            previousStateName_ = stateName;
            stateName_ = nextStateName;

            nowState_ = nextState;
        }
    }
}
