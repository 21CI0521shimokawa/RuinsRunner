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
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //ローカルスコアの更新
        score_ += addScore;
        score_ = Mathf.Clamp(score_, 0, 9999);

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
