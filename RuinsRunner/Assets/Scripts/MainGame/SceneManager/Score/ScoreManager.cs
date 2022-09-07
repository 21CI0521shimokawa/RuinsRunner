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


    float scoreUpTime_;
    public float scoreUpTime
    {
        get
        {
            return scoreUpTime_;
        }

        set
        {
            scoreUpTime_ = value;
        }
    }

    //得点倍率
    float scoreMagnification_;

    public float scoreMagnification
    {
        get
        {
            return scoreMagnification_;
        }
    }


    //速度を減らす速度
    float speedCurrentVelocity_;

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
        scoreUpTime_ = 0.0f;

        speedCurrentVelocity_ = 0.0f;
    }

    void Update()
    {
        if(scoreUpTime_ > 0.0f)
        {
            scoreUpTime_ -= Time.deltaTime;
            scoreMagnification_ = 3.0f;

            speedCurrentVelocity_ = 0.0f;
        }
        else
        {
            scoreUpTime_ = 0.0f;
            scoreMagnification_ = 1.0f;

            if(MoveLooksLikeRunning.moveMagnification > 1.0f)
            {
                MoveLooksLikeRunning.moveMagnification = Mathf.SmoothDamp(MoveLooksLikeRunning.moveMagnification, 1.0f, ref speedCurrentVelocity_, 0.5f, Mathf.Infinity, Time.deltaTime);
                StaticInterfaceManager.StopConcentrationLineEffect();
            }
        }
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
            scoreUpTime_ = 0.0f;
        }
        /*
        //ローカルスコアの更新
        score_ += (int)(addScore * scoreMagnification_);
        score_ = Mathf.Clamp(score_, 0, 99999);

        //共有スコアの更新
        Scene scene = SceneManager.GetSceneByName("Manager");

        gameManager.Score = score_;
        //scoreText.text = score_.ToString();
        */
    }

    public void UpdateCoinCount(int _addValue)
    {
        //もしお肉バフだったら倍率かける
        if(scoreMagnification > 0)
        {
            _addValue *= (int)scoreMagnification;
        }
        //ローカルコインカウントの更新
        coinCount_ += _addValue;
        coinCount_ = Mathf.Max(coinCount_, 0);
        coinCountText.text = coinCount_.ToString();
        //共有コインカウントの更新
        gameManager.CoinCount = coinCount_;
    }
}
