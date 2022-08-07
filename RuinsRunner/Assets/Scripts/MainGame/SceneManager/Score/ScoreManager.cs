using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager 
    : MonoBehaviour
    , IUpdateScore
{
    [SerializeField]TextMeshProUGUI text;
    int score_;

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
            if(value >= 0.0f)
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
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                break;
            }
        }
        text.text = score_.ToString();


        scoreMagnification_ = 1.0f;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //得点が減点されたら倍率と速度をリセット
        if(addScore < 0)
        {
            scoreMagnification_ = 1.0f;
            MoveLooksLikeRunning.moveMagnification = 1.0f;
        }


        //ローカルスコアの更新
        score_ += (int)(addScore * scoreMagnification_);
        score_ = Mathf.Clamp(score_, 0, 99999);

        //共有スコアの更新
        Scene scene = SceneManager.GetSceneByName("Manager");

        //GetRootGameObjectsで、そのシーンのルートGameObjects
        //つまり、ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                //GameManagerが見つかったので
                //gameManagerのスコアを取得
                gameManager.Score = score_;
                break;
            }
        }
        text.text = score_.ToString();
    }
}
