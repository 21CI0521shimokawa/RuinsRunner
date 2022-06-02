using UnityEngine;
using System;
using UniRx;
using SceneDefine;
using UnityEngine.SceneManagement;
/// <summary>
///�V�[���}�l�[�W���[�p���������[�N�΍���N���X
/// </summary>
public class SceneSuperClass : MonoBehaviour
{
    private void Awake()
    {
        //�V�[���̖��O����A���g�̃V�[���𔻒f������
        currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetActiveScene().name);
    }

    //���������[�N�֘A-----------------------------------------------------------------
    private CompositeDisposable compositeDisposable_ = new CompositeDisposable();

    public void AddToCompositeDisposable(IDisposable _disposable)
    {
        compositeDisposable_.Add(_disposable);
    }

    void OnDestroy()
    {
        compositeDisposable_.Clear();
    }

    //�V�[���؂�ւ��p�w���p�[-------------------------------------------------------------
    protected SceneName currentSceneName;
    public SceneName GetCurrentSceneName()
    {
        return currentSceneName;
    }
}