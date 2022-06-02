using SceneDefine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneManage))]
public class SceneAddRequester : MonoBehaviour
{
    Scene scene_;
    SceneName currentSceneName_;

    //GameManager�����ɍ쐬���ꂽ���Ƃ�����O��̏������Ȃ̂ŁA
    //�e�X�g�p�ɂ��̃V�[������n�߂�Ƃ���Start�ɂ����ق���������������Ȃ�
    void Awake()
    {
        scene_ = SceneManager.GetSceneByName("Manager");
        currentSceneName_ = GetComponent<SceneManage>().GetCurrentSceneName();
    }

    //�V�[����ǉ��A�؂�ւ��������Ƃ��ɌĂяo��
    //���݂̃V�[����ێ����Ă��������ꍇ�͑�������false������
    void RequestAddScene(SceneName _sName, bool wouldUnloadThisScene = true)
    {
        foreach (var rootGameObject in scene_.GetRootGameObjects())
        {
            SceneTransition sceneTransition = rootGameObject.GetComponent<SceneTransition>();
            if (sceneTransition)
            {
                if (wouldUnloadThisScene)
                {
                    sceneTransition.AddSceneUnloadMyself(_sName, currentSceneName_);
                }
                else
                {
                    sceneTransition.AddScene(_sName);
                }
            }
        }
    }
}
