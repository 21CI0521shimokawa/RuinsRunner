using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Test : StateBase
{
    PlayerController playerController_;
    int randomGameNumber_;  //今やるゲームの番号

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        randomGameNumber_ = Random.Range(0, 2);
        switch(randomGameNumber_)
        {
            case 0:
                //Sceneを加算ロード。現在のシーンは残ったままで、Sceneが追加される
                SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

                //できればstaticは使いたくない
                MiniGameQTEManager.buttonQuantity = 3;
                MiniGameQTEManager.timeLinitMax = 50.0f;
                break;

            case 1:
                //Sceneを加算ロード。現在のシーンは残ったままで、Sceneが追加される
                SceneManager.LoadSceneAsync("Scene_MiniGameStickRoundAndRound", LoadSceneMode.Additive);

                //できればstaticは使いたくない
                MiniGameStickRoundAndRoundManager.timeLinitMax = 5.0f;
                MiniGameStickRoundAndRoundManager.increasePowerPerSecond = 5.0f;
                MiniGameStickRoundAndRoundManager.decreasePowerPerSecond = 0.2f;
                MiniGameStickRoundAndRoundManager.clearPower = 0.6f;
                break;
        }

        //同フレーム内だとまだロードされてないことになってる？

        #region 旧
        ////ロード済みのシーンであれば、名前で別シーンを取得できる
        //Scene scene = SceneManager.GetSceneByName("Scene_MiniGameQTE");

        ////GetRootGameObjectsで、そのシーンのルートGameObjects
        ////つまり、ヒエラルキーの最上位のオブジェクトが取得できる
        //foreach (var rootGameObject in scene.GetRootGameObjects())
        //{
        //    MiniGameQTEManager manager = rootGameObject.GetComponent<MiniGameQTEManager>();
        //    if (manager != null)
        //    {
        //        manager.buttonQuantity = 5;
        //        manager.timeLinitMax = 10.0f;
        //        break;
        //    }
        //}
        #endregion

        Time.timeScale = 0.01f; //ゲーム速度を1%に変更
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        switch (randomGameNumber_)
        {
            case 0:
                if (MiniGameQTEManager.isGameClear)
                {
                    return new PlayerStateRun();
                }
                if (MiniGameQTEManager.isFailure)
                {
                    playerController_.Damage();
                    return new PlayerStateStumble();
                }
                break;

            case 1:
                if (MiniGameStickRoundAndRoundManager.isGameClear)
                {
                    return new PlayerStateRun();
                }
                if (MiniGameStickRoundAndRoundManager.isFailure)
                {
                    playerController_.Damage();
                    return new PlayerStateStumble();
                }
                break;
        }
        return this;
    }
    public override void StateFinalize()
    {
        switch (randomGameNumber_)
        {
            case 0:
                SceneManager.UnloadSceneAsync("Scene_MiniGameQTE");
                break;

            case 1:
                SceneManager.UnloadSceneAsync("Scene_MiniGameStickRoundAndRound");
                break;
        }

        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //ゲーム速度を100%に変更
    }

}
