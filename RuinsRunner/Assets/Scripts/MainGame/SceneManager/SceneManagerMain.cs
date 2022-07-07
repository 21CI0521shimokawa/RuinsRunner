using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using TMPro;
using UnityEngine.UI;
using SceneDefine;

[RequireComponent(typeof(SceneAddRequester))]
public class SceneManagerMain 
    : SceneSuperClass
    , ISwitchRunToMG
{
    [SerializeField] float remainTime_;
    [SerializeField] TextMeshProUGUI stateText_;
    SceneAddRequester sceneAddRequester_;
    GameState gState_;

    private void Awake()
    {
        base.SSCInitialize();
        gState_ = GameState.Run;
        sceneAddRequester_ = GetComponent<SceneAddRequester>();
    }

    private void Update()
    {
    }

    //デバッグ用
    //左上の状態を表示するやつ
    public void SwitchState(GameState _nextGameState)
    {
        //gState_ = _nextGameState;
        //stateText_.text = gState_.ToString();
        //stateText_.color = Color.black;
    }

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