using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using TMPro;
using UnityEngine.UI;

public class SceneManagerMain : SceneSuperClass
{
    [SerializeField] float remainTime_;
    [SerializeField] TextMeshProUGUI stateText;
    GameState gState_;

    private void Start()
    {
        gState_ = GameState.Run;
    }

    private void Update()
    {
    }

    public void SwitchState(GameState _nextGameState)
    {
        gState_ = _nextGameState;
        stateText.text = gState_.ToString();
        stateText.color = Color.black;
    }
}