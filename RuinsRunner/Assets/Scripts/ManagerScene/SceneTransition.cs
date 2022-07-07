using SceneDefine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���J�ځi���[�h�A�A�����[�h�j
/// </summary>
public class SceneTransition : MonoBehaviour
{
    static public void AddScene(SceneName _sName)
    {
        SceneManager.LoadScene(SceneDictionary.GetSceneNameString(_sName), LoadSceneMode.Additive);
    }

    //�V�����V�[����ǉ�������A�Ăяo�����V�[�����g��j������
    static public void AddSceneUnloadMyself(SceneName _sName, SceneName _calledScene)
    {
        //�V�[����ǉ�
        AddScene(_sName);

        //�R�[�����ꂽ�V�[���̔j��
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

        //�A�����[�h��̏���������
        //TODO:���󂱂��ɂ͏������ɁA�e�V�[���ŃC�x���g�n���h���[SceneUnloaded()���쐬���A�Q�[���I�u�W�F�N�g��null�������\��
        //�Q�lurl:https://tech.pjin.jp/blog/2018/10/24/unity_scene-manager_event/
        //null����ȊO�̉����Q�l
        //�Q�lurl:https://qiita.com/toRisouP/items/4574a30622f43ddbde79

        //�s�g�p�A�Z�b�g���A�����[�h���ă����������
        //���������d�������Ȃ̂ŁA�ʂɊǗ����邱�Ƃ��l����
        yield return Resources.UnloadUnusedAssets();
    }
}