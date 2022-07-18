using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatePillarDefeatMiniGame : StateBase
{
    PlayerController playerController_;

    //倒す柱
    GameObject pillar_;
    public GameObject pillar
    {
        set
        {
            pillar_ = value;
        }
    }

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Sceneを加算ロード。現在のシーンは残ったままで、Sceneが追加される
        SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

        //できればstaticは使いたくない
        MiniGameQTEManager.buttonQuantity = 1;
        MiniGameQTEManager.timeLinitMax = 5.0f;

        Time.timeScale = 0.01f; //ゲーム速度を1%に変更
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (MiniGameQTEManager.isGameClear)
        {
            PlayerStateDefeat state = new PlayerStateDefeat();
            state.pillar = pillar_;
            return state;
        }
        if (MiniGameQTEManager.isFailure)
        {
            playerController_.Damage();
            return new PlayerStateStumble();
        }

        return this;
    }

    public override void StateFinalize()
    {
        SceneManager.UnloadSceneAsync("Scene_MiniGameQTE");
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //ゲーム速度を100%に変更
    }
}
