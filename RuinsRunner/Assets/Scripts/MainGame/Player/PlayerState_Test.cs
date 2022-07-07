using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Test : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Sceneを加算ロード。現在のシーンは残ったままで、Sceneが追加される
        SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

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


        //できればstaticは使いたくない
        MiniGameQTEManager.buttonQuantity = 10;
        MiniGameQTEManager.timeLinitMax = 5.0f;


        Time.timeScale = 0.01f; //ゲーム速度を1%に変更
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if(MiniGameQTEManager.isGameClear)
        {
            return new PlayerStateRun();
        }
        if(MiniGameQTEManager.isFailure)
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
