using SceneDefine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移（ロード、アンロード）
/// </summary>
public class SceneTransition : MonoBehaviour
{
    static public void AddScene(SceneName _sName)
    {
        SceneManager.LoadScene(SceneDictionary.GetSceneNameString(_sName), LoadSceneMode.Additive);
    }

    //新しいシーンを追加した後、呼び出したシーン自身を破棄する
    static public void AddSceneUnloadMyself(SceneName _sName, SceneName _calledScene)
    {
        //シーンを追加
        AddScene(_sName);

        //コールされたシーンの破棄
        UnloadScene(_calledScene);
    }

    static public void UnloadScene(SceneName _sName)
    {
        MonoBehaviour mono = new MonoBehaviour();
        mono.StartCoroutine(CoroutineUnloadScene(_sName));
    }

    static IEnumerator CoroutineUnloadScene(SceneName _sName)
    {
        SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(_sName));
        yield return null;

        //アンロード後の処理を書く
        //TODO:現状ここには書かずに、各シーンでイベントハンドラーSceneUnloaded()を作成し、ゲームオブジェクトにnull代入する予定
        //参考url:https://tech.pjin.jp/blog/2018/10/24/unity_scene-manager_event/
        //null代入以外の解決参考
        //参考url:https://qiita.com/toRisouP/items/4574a30622f43ddbde79

        //不使用アセットをアンロードしてメモリを解放
        //けっこう重い処理なので、別に管理することも考える
        yield return Resources.UnloadUnusedAssets();
    }
}