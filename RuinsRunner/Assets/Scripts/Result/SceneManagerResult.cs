using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SceneDefine;
using System;
using UniRx;

public class SceneManagerResult : SceneSuperClass
{
    [SerializeField] float easingTime = 1.5f;
    [SerializeField] CanvasGroup scoreBorad;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highscoreTMP;
    //int型で扱う時に使用
    int score_;
    //ハイスコア
    int highscore_;
    //フェードアウトの時に使う変数
    bool IsFade;
    private void Start()
    {
        //シーンの初期化
        Scene scene = SceneManager.GetSceneByName("Manager");

        //フェードアウトの初期化
        IsFade = false;
        //スコアボードの初期化
        scoreBorad.alpha = 0f;
        scoreBorad.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //ハイスコアの初期化
        highscore_ = PlayerPrefs.GetInt("Highscore", 0);

        //スコアを共有シーンから持ってきて反映させる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.CoinCount;
                break;
            }
        }
        scoreTMP.text = score_.ToString();

        //ハイスコア更新
        if(highscore_ < score_)
        {
            highscore_ = score_;
            PlayerPrefs.SetInt("HighScore", score_);
            PlayerPrefs.Save();
        }

        //ハイスコアのテキスト反映
        highscoreTMP.text = highscore_.ToString();

        //スコアボードを徐々に表示
        StartCoroutine(GraduallyAppear());

        DoFade();
    }

    private void Update()
    {
        Time.timeScale = 1.0f;

        if (Gamepad.current == null)
        {
            return;
        }
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            SceneAddRequester sceneAddRequester = GetComponent<SceneAddRequester>();
            //先にランゲームシーンを消去
            // sceneAddRequester.RequestUnLoadScene(SceneName.RUNGAME);
            //    //エンディングのロード
            //    //SceneManager.LoadScene("Scene_Ending");
            //    //続いてこのシーンを消去
            //レガシー
            //sceneAddRequester.RequestAddScene(SceneName.ENDING, true);
            IsFade = true;
        }
    }

    IEnumerator GraduallyAppear()
    {
        while (scoreBorad.alpha < 1.0f)
        {
            scoreBorad.alpha += 0.1f;
            if (scoreBorad.alpha >= 1.0f)
            {
                scoreBorad.alpha = 1.0f;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield break;
    }
    /// <summary>
    /// 多重読み込み防止フェードアウト関数
    /// </summary>
    void DoFade()
    {
        gameObject.ObserveEveryValueChanged(_ => IsFade)
                  .Where(x => IsFade)
                  .Subscribe(_ =>
                  {
                      SceneFadeManager.StartMoveScene("Scene_Ending");
                  });
    }
}