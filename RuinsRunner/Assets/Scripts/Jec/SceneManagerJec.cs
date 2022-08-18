using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneDefine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneManagerJec : SceneSuperClass
{
    [SerializeField] Fade fade;

    [Tooltip("フェードインにかける時間")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("フェードアウトにかける時間")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("フェードイン後に表示する時間")]
    [SerializeField] float displayTime = 2.0f;
    float passedTime = 0;
    private void Start()
    {
        ////フェードイン開始
        //fade.FadeOut(fadeoutTime,
        //    () =>
        //    {
        //        //フェードイン終了後
        //        StartCoroutine(DelayFadeout());
        //    });

        //IEnumerator DelayFadeout()
        //{
        //    //3秒待つ
        //    yield return new WaitForSeconds(displayTime);
        //    //フェードアウト開始
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
