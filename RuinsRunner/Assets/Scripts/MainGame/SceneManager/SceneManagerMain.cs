using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using UnityEngine.UI;
using SceneDefine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerMain
    : SceneSuperClass
    , IExitGame
    , ISwitchRunToMG
{
    SceneAddRequester sceneAddRequester_;

    [Tooltip("ゲーム終了時にメインゲームシーンの実行速度を下げていく早さ")]
    [Range(0.01f, 0.1f)]
    [SerializeField] float stopSceneSpeed_ = 0.05f;
    [SerializeField] TextMeshProUGUI scoreTMP;

    private void Awake()
    {
        sceneAddRequester_ = GetComponent<SceneAddRequester>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        //動作確認用
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StaticInterfaceManager.ExitGame();
        }
    }

    /// <summary>
    /// ミニゲームへの遷移
    /// </summary>
    /// <param name="_nextMiniGame"></param>
    public void SwitchMiniGame(GameState _nextMiniGame)
    {
        switch (_nextMiniGame)
        {
            case GameState.MiniGame1:
                SwitchMiniGame1();
                break;
            default:
                break;
        }
    }

    private void SwitchMiniGame1()
    {
        //TODO:カメラの移動
        //TODO:プレイヤーの状態遷移、アニメーションも
        //TODO:柱を倒して橋壊す
        //MG1ロード
        sceneAddRequester_.RequestAddScene(SceneName.MINIGAME1);
        //TODO:エネミーとめる
    }

    //ランゲームを終了してリザルトをシーンを追加で呼び出す
    public void ExitToResult()
    {
        scoreTMP.alpha = 0f;
        StartCoroutine(GraduallyStopScene());
        sceneAddRequester_.RequestAddScene(SceneName.RESULT, true);
    }

    IEnumerator GraduallyStopScene()
    {
        while(Time.timeScale > 0.05f)
        {
            Time.timeScale -= stopSceneSpeed_;
            yield return new WaitForSeconds(0.01f);
        }
        Time.timeScale = 0f;
        Debug.Log("メインシーン停止しました");
        yield break;
    }
}