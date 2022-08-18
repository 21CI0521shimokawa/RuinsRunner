using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneDefine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneManagerJec : SceneSuperClass
{
    [SerializeField] Fade fade;

    [Tooltip("�t�F�[�h�C���ɂ����鎞��")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("�t�F�[�h�A�E�g�ɂ����鎞��")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("�t�F�[�h�C����ɕ\�����鎞��")]
    [SerializeField] float displayTime = 2.0f;
    float passedTime = 0;
    private void Start()
    {
        ////�t�F�[�h�C���J�n
        //fade.FadeOut(fadeoutTime,
        //    () =>
        //    {
        //        //�t�F�[�h�C���I����
        //        StartCoroutine(DelayFadeout());
        //    });

        //IEnumerator DelayFadeout()
        //{
        //    //3�b�҂�
        //    yield return new WaitForSeconds(displayTime);
        //    //�t�F�[�h�A�E�g�J�n
        //    fade.FadeIn(fadeinTime,
        //        () =>
        //        {
        //            GetComponent<SceneAddRequester>().RequestAddScene(SceneName.TITLE, true);
        //            //SceneManager.LoadScene("Scene_Title");
        //        });
        //    yield break;
        //}
        passedTime = 0;
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
        if(passedTime > 2)
        {
            GetComponent<SceneAddRequester>().RequestAddScene(SceneName.TITLE, true);
        }
    }
}
