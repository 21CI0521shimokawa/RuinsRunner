using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //GameManager�����ɍ쐬���ꂽ���Ƃ�����O��̏������Ȃ̂ŁA
    //�e�X�g�p�ɂ��̃V�[������n�߂�Ƃ���Start�ɂ����ق���������������Ȃ�
    void Start()
    {
        currentSceneName_ = GetComponent<SceneSuperClass>().GetCurrentSceneName();
    }

    //�V�[����ǉ��A�؂�ւ��������Ƃ��ɌĂяo��
    //���݂̃V�[����ێ����Ă��������ꍇ�͑�������false������
    public void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = true)
    {
        //���[�h�������V�[������ۑ�
        string loadSceneName = SceneDictionary.GetSceneNameString(_sName);

        //�Ƃ肠�����V�[���̃��[�h
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);
        scene_ = SceneManager.GetSceneByName(loadSceneName);

        //���܂ł̃��C���V�[���̍폜�v���������������
        SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(currentSceneName_));

            //if (wouldUnloadThisScene)
            //{
            //    SceneTransition.UnloadScene(currentSceneName_);
            //}
    }
}
