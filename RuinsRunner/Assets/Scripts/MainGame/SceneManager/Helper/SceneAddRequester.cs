using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //�V�[����ǉ��A�؂�ւ��������Ƃ��ɌĂяo��
    //���݂̃V�[����ێ����Ă��������ꍇ�͑�������false������
    public void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = false)
    {
        //���[�h�������V�[������ۑ�
        string loadSceneName = SceneDictionary.GetSceneNameString(_sName);

        //�Ƃ肠�����V�[���̃��[�h
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);
        scene_ = SceneManager.GetSceneByName(loadSceneName);

        //���܂ł̃��C���V�[���̍폜�v������������
        if (wouldUnloadThisScene)
        {
            //���݂̃V�[�������擾
            currentSceneName_ = GetComponent<SceneSuperClass>().GetCurrentSceneName();

            //���܂ł̃��C���V�[��������
            SceneManager.UnloadSceneAsync(SceneDictionary.GetSceneNameString(currentSceneName_));
            //SceneTransition.UnloadScene(currentSceneName_);
        }
    }
}
