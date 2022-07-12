using GameStateDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ミニゲーム1（仮名）のシーン中の
/// </summary>
public class GameStateMiniGame1 : StateBase
{
    StateBase nextState;
    public override void StateInitialize()
    {
        nextState = this;
        //TODO:Cameraの移動を伝えるインターフェースの実装、呼び出し
        //仮
        StaticInterfaceManager.MoveCamera(new Vector3(10, 5, 15));
        //TODO:ミニゲーム状態に入っていることを親であるGameStateControllerに伝えておきたい
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateRun();
        }

        return nextState;
    }
    /// <summary>
    /// 外からステートを変えたいときに使用する（急ぎで作ったため、もっと安全に清書できるハズ）
    /// </summary>
    public void ChangeState(StateBase _NextState)
    {
        nextState = _NextState;
    }
    public override void StateFinalize()
    {
        //ランゲーム用のオブジェクトを再出現させてもいいのかなと（見栄え的な）
    }
}
