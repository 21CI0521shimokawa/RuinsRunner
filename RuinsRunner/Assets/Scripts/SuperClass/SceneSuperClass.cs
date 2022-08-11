using UnityEngine;
using System;
using UniRx;
using SceneDefine;
using UnityEngine.SceneManagement;
/// <summary>
///シーンマネージャー用メモリリーク対策基底クラス
/// </summary>
[RequireComponent(typeof(SceneAddRequester))]
public class SceneSuperClass : MonoBehaviour
{
    #region ANTI MEMORY LEAK
    private CompositeDisposable compositeDisposable_ = new CompositeDisposable();

    public void AddToCompositeDisposable(IDisposable _disposable)
    {
        compositeDisposable_.Add(_disposable);
    }

    void OnDestroy()
    {
        compositeDisposable_.Clear();
    }
    #endregion

    #region SCENE CHANGE HELPER
    protected SceneName currentSceneName = SceneName.NULL;

    protected void SetSceneName()
    {
        if (currentSceneName != SceneName.NULL) return;
        //シーンの名前から、自身のシーンを判断させる
        if(SceneManager.GetActiveScene().name != "Manager")
        {
            currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetActiveScene().name);
            return;
        }
        //マネージャーシーンを検知してしまうとNULLになってしまいロード等で支障をきたすので、イてれーとして探す
        //（複数シーン展開時にもしかしたらまた順番を考えないといけなくなると思う）
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != "Manager")
            {
                currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetSceneAt(i).name);
                break;
            }
        }
        Debug.Log("ここに入っているということはマネージャーシーン以外認識できていない");
    }

    public SceneName GetCurrentSceneName()
    {
        SetSceneName();
        return currentSceneName;
    }
    #endregion
}