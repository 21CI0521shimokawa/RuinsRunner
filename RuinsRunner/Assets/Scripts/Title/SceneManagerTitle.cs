using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneDefine;
using UnityEngine.UI;

public class SceneManagerTitle : SceneSuperClass
{
    [SerializeField] Fade fade_;
    [SerializeField] GameObject enemy;
    [Tooltip("フェードインにかける時間")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("フェードアウトにかける時間")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("デモ移行までの時間")]
    [SerializeField] float displayTime = 2.0f;
    [Tooltip("ロゴを出すまでの時間")]
    [SerializeField] float logoAppTime = 2.0f;
    [SerializeField] GameObject[] slowObjects;
    [SerializeField] AudioClip audio;

    bool isFading_;
    float passedTime_;
    bool isAlreadyStopped;

    private void Start()
    {
        isFading_ = true;
        isAlreadyStopped = false;
        passedTime_ = 0.0f;
        fade_.FadeOut(fadeoutTime, 
            () => 
            {
                isFading_ = false;
            });
    }

    private void Update()
    {
        if(passedTime_ > logoAppTime && !isAlreadyStopped)
        {
            foreach(GameObject obj in slowObjects)
            {
                MoveLooksLikeRunning move = obj.GetComponent<MoveLooksLikeRunning>();
                if(move != null)
                {
                    move.moveSpeed = 0.1f;
                }
                Animator anim = obj.GetComponent<Animator>();
                if(anim != null)
                {
                    anim.speed = 0.01f;
                }
            }
            enemy.GetComponent<Animator>().SetTrigger("Attack");
            isAlreadyStopped = true;
        }
        FadeUpdate();
    }

    void FadeUpdate()
    {
        //フェード中は移行を受け付けない
        if (isFading_) return;
        //パッドの入力を反映
        Gamepad gamepad = Gamepad.current;

        //パッドのXボタンの入力がされたら
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            PlayAudio.PlaySE(audio);
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
        if (passedTime_ > displayTime)
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
