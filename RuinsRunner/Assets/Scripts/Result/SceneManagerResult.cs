using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneManagerResult : MonoBehaviour
{
    [SerializeField] float easingTime = 1.5f;
    [SerializeField] CanvasGroup scoreBorad;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highscoreTMP;
    //int型で扱う時に使用
    int score_;
    //ハイスコア
    int highscore_;
    private void Start()
    {
        //シーンの初期化
        Scene scene = SceneManager.GetSceneByName("Manager");

        //スコアボードの初期化
        scoreBorad.alpha = 0f;
        scoreBorad.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //ハイスコアの初期化
        highscore_ = PlayerPrefs.GetInt("Highscore", 0);

        //ハイスコアのテキスト反映
        highscoreTMP.text = highscore_.ToString();

        //スコアを共有シーンから持ってきて反映させる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                break;
            }
        }
        scoreTMP.text = score_.ToString();

        //スコアボードを徐々に表示
        StartCoroutine(GraduallyAppear());
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
}