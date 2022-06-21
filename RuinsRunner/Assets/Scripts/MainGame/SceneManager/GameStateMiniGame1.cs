using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ミニゲーム1（仮名）のシーン中の
/// </summary>
public class GameStateMiniGame1 : StateBase
{
    Camera cam_;
    public override void StateInitialize()
    {
        cam_ = Camera.main;
        //TODO:Cameraの移動を伝えるインターフェースの実装、呼び出し
        //TODO:ミニゲーム状態に入っていることを親であるGameStateControllerに伝えておきたい
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if(this.StateTimeCount >= 10)
        {
            nextState = new GameStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //ランゲーム用のオブジェクトを再出現させてもいいのかなと（見栄え的な）
    }
}
