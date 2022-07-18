using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneDefine;

public class SceneManagerTitle : SceneSuperClass
{
    [SerializeField] Fade fade_;
    [Tooltip("フェードインにかける時間")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("フェードアウトにかける時間")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("デモ移行までの時間")]
    [SerializeField] float displayTime = 2.0f;

    bool isFading_;
    float passedTime_;

    private void Start()
    {
        isFading_ = true;
        passedTime_ = 0.0f;
        fade_.FadeOut(fadeoutTime, 
            () => 
            {
                isFading_ = false;
            });
    }

    private void Update()
    {
        //フェード中は移行を受け付けない
        if (isFading_) return;
        //パッドの入力を反映
        Gamepad gamepad = Gamepad.current;

        //パッドの何かしらの入力がされたら
        if(Gamepad.current.buttonWest.wasPressedThisFrame)
        {           
            //フェードアウト
            fade_.FadeIn(fadeinTime,
                () =>
                {
                    isFading_ = true;
                    //フェードアウト終了後
                    //ランゲームシーンへ移行（操作説明とかのシーンを挟みたい）
                    GetComponent<SceneAddRequester>().RequestAddScene(SceneName.RUNGAME, true);
                });
        }

        passedTime_ += Time.deltaTime;
        if(passedTime_ > displayTime)
        {
            isFading_ = true;
            //フェードアウト
            fade_.FadeIn(fadeinTime,
                () =>
                {
                    //フェードアウト終了後
                    //デモシーンへ移行
                    GetComponent<SceneAddRequester>().RequestAddScene(SceneName.DEMO, true);
                });
        }
    }
}
