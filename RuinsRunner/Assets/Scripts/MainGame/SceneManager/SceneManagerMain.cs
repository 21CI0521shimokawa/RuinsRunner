using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using UnityEngine.UI;
using SceneDefine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneAddRequester))]
public class SceneManagerMain 
    : SceneSuperClass
    , ISwitchRunToMG
{
    SceneAddRequester sceneAddRequester_;

    private void Awake()
    {
        sceneAddRequester_ = GetComponent<SceneAddRequester>();
    }

    private void Start()
    {
    }

    private void Update()
    {
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
}