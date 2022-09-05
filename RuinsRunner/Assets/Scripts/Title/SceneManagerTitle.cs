using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneDefine;
using UniRx;
using UnityEngine.UI;

public class SceneManagerTitle : SceneSuperClass
{
    [Tooltip("デモ移行までの時間")]
    [SerializeField] float displayTime = 2.0f;
    [SerializeField] GameObject[] slowObjects;
    [SerializeField] AudioClip audio;
    float passedTime_;
    //フェードアウトの時に使う変数
   public bool IsFadeMain;
   public bool IsFadeDemo;

    private void Awake()
    {
        IsFadeMain = false;
        IsFadeDemo = false;
    }
    private void Start()
    {
        passedTime_ = 0.0f;
    }

    private void Update()
    {
        #region 旧スローモーション実装
        //if(passedTime_ > logoAppTime && !isAlreadyStopped)
        //{
        //    foreach(GameObject obj in slowObjects)
        //    {
        //        MoveLooksLikeRunning move = obj.GetComponent<MoveLooksLikeRunning>();
        //        if(move != null)
        //        {
        //            move.moveSpeed = 0.1f;
        //        }
        //        Animator anim = obj.GetComponent<Animator>();
        //        if(anim != null)
        //        {
        //            anim.speed = 0.01f;
        //        }
        //    }
        //    enemy.GetComponent<Animator>().SetTrigger("Attack");
        //    isAlreadyStopped = true;
        //}
        #endregion
        FadeUpdate();
    }

    void FadeUpdate()
    {
        //パッドの入力を反映
        Gamepad gamepad = Gamepad.current;

        if(gamepad == null)
        {
            Debug.Log("a");
            return;
        }

        //パッドのXボタンの入力がされたら
        if (gamepad.buttonEast.wasPressedThisFrame&&!IsFadeMain)
        {
            SceneFadeManager.StartMoveScene("Scene_MainGame");
            IsFadeMain = true;
            PlayAudio.PlaySE(audio);
        }

        passedTime_ += Time.deltaTime;
        if (passedTime_ > displayTime)
        {
            //フェードアウト
            IsFadeDemo = true;
        }
    }
    /*
    /// <summary>
    /// 多重読み込み防止フェードアウト関数
    /// </summary>
    void DoFade(bool _Isfade,string SceneName)
    {
        gameObject.ObserveEveryValueChanged(_ => _Isfade)
                  .Where(x => _Isfade)
                  .Subscribe(_ =>
                  {
                      SceneFadeManager.StartMoveScene(SceneName);
                  });
    }
    */
}
