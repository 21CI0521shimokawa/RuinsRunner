using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //GameManagerが既に作成されたことがある前提の書き方なので、
    //テスト用にこのシーンから始めるときはStartにしたほうがいいかもしれない
    void Start()
    {
        currentSceneName_ = GetComponent<SceneSuperClass>().GetCurrentSceneName();
    }

    //シーンを追加、切り替えしたいときに呼び出す
    //現在のシーンを保持しておきたい場合は第二引数にfalseを入れる
    public void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = true)
    {
        //ロードしたいシーン名を保存
        string loadSceneName = SceneDictionary.GetSceneNameString(_sName);

        //とりあえずシーンのロード
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);
        scene_ = SceneManager.GetSceneByName(loadSceneName);

        //今までのメインシーンの削除要請があったら消す
        SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(currentSceneName_));

            //if (wouldUnloadThisScene)
            //{
            //    SceneTransition.UnloadScene(currentSceneName_);
            //}
    }
}
