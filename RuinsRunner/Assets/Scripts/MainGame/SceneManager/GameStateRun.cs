using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : StateBase
{
    Camera cam_;
    
    public override void StateInitialize() 
    {
        cam_ = Camera.main;
        //TODO:Cameraの移動を伝えるインターフェースの実装、呼び出し
        //TODO:ランゲーム状態に入っていることを親であるGameStateControllerに伝えておきたい
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateMiniGame1();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        //ランゲーム用のオブジェクトを徐々に透過させたりするコルーチンの呼び出しをしてもいいと思う（見栄え）
    }
}
