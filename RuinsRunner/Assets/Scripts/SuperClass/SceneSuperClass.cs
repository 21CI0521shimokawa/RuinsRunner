using UnityEngine;
using System;
using UniRx;
using SceneDefine;
using UnityEngine.SceneManagement;
/// <summary>
///シーンマネージャー用メモリリーク対策基底クラス
/// </summary>
public class SceneSuperClass : MonoBehaviour
{
    private void Awake()
    {
        //シーンの名前から、自身のシーンを判断させる
        currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetActiveScene().name);
    }

    //メモリリーク関連-----------------------------------------------------------------
    private CompositeDisposable compositeDisposable_ = new CompositeDisposable();

    public void AddToCompositeDisposable(IDisposable _disposable)
    {
        compositeDisposable_.Add(_disposable);
    }

    void OnDestroy()
    {
        compositeDisposable_.Clear();
    }

    //シーン切り替え用ヘルパー-------------------------------------------------------------
    protected SceneName currentSceneName;
    public SceneName GetCurrentSceneName()
    {
        return currentSceneName;
    }
}