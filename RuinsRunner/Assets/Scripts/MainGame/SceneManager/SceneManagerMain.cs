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

    [Tooltip("�Q�[���I�����Ƀ��C���Q�[���V�[���̎��s���x�������Ă�������")]
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

    //�����Q�[�����I�����ă��U���g���V�[����ǉ��ŌĂяo��
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
        Debug.Log("���C���V�[����~���܂���");
        yield break;
    }
}