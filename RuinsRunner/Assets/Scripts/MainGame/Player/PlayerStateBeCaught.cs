using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateBeCaught : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Sceneを加算ロード。現在のシーンは残ったままで、Sceneが追加される
        SceneManager.LoadSceneAsync("Scene_MiniGameStickRoundAndRound", LoadSceneMode.Additive);

        //できればstaticは使いたくない
        MiniGameStickRoundAndRoundManager.timeLinitMax = 5.0f;
        MiniGameStickRoundAndRoundManager.increasePowerPerSecond = 5.0f;
        MiniGameStickRoundAndRoundManager.decreasePowerPerSecond = 0.4f;
        MiniGameStickRoundAndRoundManager.clearPower = 0.75f;

        Time.timeScale = 0.01f; //ゲーム速度を1%に変更
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (MiniGameStickRoundAndRoundManager.isGameClear)
        {
            for(int i = 0; i < 2; ++i)
            {
                playerController_.Recovery();
            }
            return new PlayerStateRun();
        }
        if (MiniGameStickRoundAndRoundManager.isFailure)
        {
            return new PlayerStateDeath(); //仮 とりあえず殺しとく
        }

        return this;
    }

    public override void StateFinalize()
    {
        SceneManager.UnloadSceneAsync("Scene_MiniGameStickRoundAndRound");
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //ゲーム速度を100%に変更
    }
}
