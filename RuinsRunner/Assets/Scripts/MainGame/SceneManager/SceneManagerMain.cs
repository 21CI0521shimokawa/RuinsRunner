using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using UnityEngine.UI;

public class SceneManagerMain : SceneSuperClass
{
    [SerializeField] float remainTime_;
    [SerializeField ]Text timeText;
    GameState gState_;

    private void Start()
    {
        gState_ = GameState.Run;
        timeText = GetComponent<Text>();
    }

    private void Update()
    {
        switch (gState_)
        {
            case GameState.Run:
                remainTime_ -= Time.deltaTime;
                if(remainTime_ <= 0f)
                {
                    remainTime_ = 0f;
                    gState_ = GameState.GameOver;
                }
                timeText.text = remainTime_.ToString();
                break;

            case GameState.GameOver:
                timeText.text = "GameOver";
                break;
        }
    }
}