using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneDefine;
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
    private void Start()
    {
        //フェードイン開始
        fade.FadeOut(fadeoutTime,
            () =>
            {
                //フェードイン終了後
                StartCoroutine(DelayFadeout());
            });

        IEnumerator DelayFadeout()
        {
            //3秒待つ
            yield return new WaitForSeconds(displayTime);
            //フェードアウト開始
            fade.FadeIn(fadeinTime,
                () =>
                {
                    GetComponent<SceneAddRequester>().RequestAddScene(SceneName.TITLE, true);
                });
            yield break;
        }
    }
}
