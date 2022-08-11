using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager
    : MonoBehaviour
    , IUpdateScore
    , IUpdateCoinCount
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinCountText;
    int score_;
    int coinCount_;
    TakeOverVariables gameManager;

    //得点倍率
    float scoreMagnification_;
    public float scoreMagnification
    {
        get
        {
            return scoreMagnification_;
        }

        set
        {
            if (value >= 0.0f)
            {
                scoreMagnification_ = value;
            }
        }
    }

    private void Start()
    {
        Scene scene = SceneManager.GetSceneByName("Manager");

        //GetRootGameObjectsで、そのシーンのルートGameObjects
        //つまり、ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                coinCount_ = gameManager.CoinCount;
                break;
            }
        }
        //scoreText.text = score_.ToString();
        coinCount_ = 0;
        coinCountText.text = coinCount_.ToString();

        scoreMagnification_ = 1.0f;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //得点が減点されたら倍率と速度をリセット
        if (addScore < 0)
        {
            scoreMagnification_ = 1.0f;
            MoveLooksLikeRunning.moveMagnification = 1.0f;
        }


        //ローカルスコアの更新
        score_ += (int)(addScore * scoreMagnification_);
        score_ = Mathf.Clamp(score_, 0, 99999);

        //共有スコアの更新
        Scene scene = SceneManager.GetSceneByName("Manager");

        gameManager.Score = score_;
        //scoreText.text = score_.ToString();
    }

    public void UpdateCoinCount()
    {
        //ローカルコインカウントの更新
        coinCount_++;
        coinCountText.text = coinCount_.ToString();
        //共有コインカウントの更新
        gameManager.CoinCount = coinCount_;
    }
}
