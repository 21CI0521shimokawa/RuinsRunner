using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //シーンを追加、切り替えしたいときに呼び出す
    //現在のシーンを保持しておきたい場合は第二引数にfalseを入れる
    public void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = false)
    {
        //ロードしたいシーン名を保存
        string loadSceneName = SceneDictionary.GetSceneNameString(_sName);

        //とりあえずシーンのロード
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);
        scene_ = SceneManager.GetSceneByName(loadSceneName);

        //今までのメインシーンの削除要請があったら
        if (wouldUnloadThisScene)
        {
            //現在のシーン名を取得
            currentSceneName_ = GetComponent<SceneSuperClass>().GetCurrentSceneName();

            //今までのメインシーンを消す
            SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(currentSceneName_));
            //SceneTransition.UnloadScene(currentSceneName_);
        }
    }
}
