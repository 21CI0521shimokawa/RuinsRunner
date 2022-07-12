using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public class GameStateRun : StateBase
{
    StateBase nextState;

    public override void StateInitialize()
    {
        nextState = this;
        //TODO:Cameraの移動を伝えるインターフェースの実装、呼び出し
        //仮実装
        StaticInterfaceManager.MoveCamera(new Vector3(0, 5, 5));
        //TODO:ランゲーム状態に入っていることを親であるGameStateControllerに伝えておきたい
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (this.StateTimeCount >= 10)
        {
            nextState = new GameStateMiniGame1();
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
        //ランゲーム用のオブジェクトを徐々に透過させたりするコルーチンの呼び出しをしてもいいと思う（見栄え）
    }
}
