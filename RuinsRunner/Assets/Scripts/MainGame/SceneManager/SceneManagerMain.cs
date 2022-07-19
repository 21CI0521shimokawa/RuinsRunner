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
        base.SSCInitialize();
        sceneAddRequester_ = GetComponent<SceneAddRequester>();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    /// <summary>
    /// �~�j�Q�[���ւ̑J��
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
        //TODO:�J�����̈ړ�
        //TODO:�v���C���[�̏�ԑJ�ځA�A�j���[�V������
        //TODO:����|���ċ���
        //MG1���[�h
        sceneAddRequester_.RequestAddScene(SceneName.MINIGAME1);
        //TODO:�G�l�~�[�Ƃ߂�
    }
}