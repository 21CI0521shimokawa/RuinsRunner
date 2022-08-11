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
        if(SceneManager.GetActiveScene().name != "Manager")
        {
            currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetActiveScene().name);
            return;
        }
        //�}�l�[�W���[�V�[�������m���Ă��܂���NULL�ɂȂ��Ă��܂����[�h���Ŏx����������̂ŁA�C�Ă�[�Ƃ��ĒT��
        //�i�����V�[���W�J���ɂ�����������܂����Ԃ��l���Ȃ��Ƃ����Ȃ��Ȃ�Ǝv���j
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != "Manager")
            {
                currentSceneName = SceneDictionary.GetSceneNameEnum(SceneManager.GetSceneAt(i).name);
                break;
            }
        }
        Debug.Log("�����ɓ����Ă���Ƃ������Ƃ̓}�l�[�W���[�V�[���ȊO�F���ł��Ă��Ȃ�");
    }

    public SceneName GetCurrentSceneName()
    {
        SetSceneName();
        return currentSceneName;
    }
    #endregion
}