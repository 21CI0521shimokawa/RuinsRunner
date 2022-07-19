using UnityEngine;
using System;
using UniRx;
using SceneDefine;
using UnityEngine.SceneManagement;
/// <summary>
///�V�[���}�l�[�W���[�p���������[�N�΍���N���X
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
        //�V�[���̖��O����A���g�̃V�[���𔻒f������
        currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetActiveScene().name);
    }

    public SceneName GetCurrentSceneName()
    {
        SetSceneName();
        return currentSceneName;
    }
    #endregion
}