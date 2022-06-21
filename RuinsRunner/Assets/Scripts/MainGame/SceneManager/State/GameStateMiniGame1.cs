using GameStateDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ミニゲーム1（仮名）のシーン中の
/// </summary>
public class GameStateMiniGame1 : StateBase
{
    public override void StateInitialize()
    {
        //TODO:Cameraの移動を伝えるインターフェースの実装、呼び出し
        //仮
        StaticInterfaceManager.MoveCamera(new Vector3(10, 5, 15));
        //TODO:ミニゲーム状態に入っていることを親であるGameStateControllerに伝えておきたい
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        gameObject.GetComponent<SceneManagerMain>().SwitchState(GameState.MiniGame1);

        if (this.StateTimeCount >= 10)
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
