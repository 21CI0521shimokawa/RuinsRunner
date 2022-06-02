using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneManage))]
public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //GameManagerが既に作成されたことがある前提の書き方なので、
    //テスト用にこのシーンから始めるときはStartにしたほうがいいかもしれない
    void Awake()
    {
        scene_ = SceneManager.GetSceneByName("Manager");
        currentSceneName_ = GetComponent<SceneManage>().GetCurrentSceneName();
    }

    //シーンを追加、切り替えしたいときに呼び出す
    //現在のシーンを保持しておきたい場合は第二引数にfalseを入れる
    void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = true)
    {
        foreach (var rootGameObject in scene_.GetRootGameObjects())
        {
            SceneTransition sceneTransition = rootGameObject.GetComponent<SceneTransition>();
            if (sceneTransition)
            {
                if (wouldUnloadThisScene)
                {
                    sceneTransition.AddSceneUnloadMyself(_sName, currentSceneName_);
                }
                else
                {
                    sceneTransition.AddScene(_sName);
                }
            }
        }
    }
}
