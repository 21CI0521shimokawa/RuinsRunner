using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
using UnityEngine.UI;
using SceneDefine;
using UnityEngine.SceneManagement;

public class SceneManagerMain 
    : SceneSuperClass
    , IExitGame
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
        //����m�F�p
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StaticInterfaceManager.ExitGame();
        }
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

    //�Q�[��
    public void ExitToResult()
    {
        sceneAddRequester_.RequestAddScene(SceneName.RESULT);
    }
}