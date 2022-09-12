using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SceneDefine;
using System;
using UniRx;

public class SceneManagerResult : SceneSuperClass
{
    [SerializeField] float easingTime = 1.5f;
    [SerializeField] CanvasGroup scoreBorad;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highscoreTMP;
    //int�^�ň������Ɏg�p
    int score_;
    //�n�C�X�R�A
    int highscore_;
    //�t�F�[�h�A�E�g�̎��Ɏg���ϐ�
    bool IsFade;
    private void Start()
    {
        //�V�[���̏�����
        Scene scene = SceneManager.GetSceneByName("Manager");

        //�t�F�[�h�A�E�g�̏�����
        IsFade = false;
        //�X�R�A�{�[�h�̏�����
        scoreBorad.alpha = 0f;
        scoreBorad.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //�n�C�X�R�A�̏�����
        highscore_ = PlayerPrefs.GetInt("Highscore", 0);

        //�X�R�A�����L�V�[�����玝���Ă��Ĕ��f������
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.CoinCount;
                break;
            }
        }
        scoreTMP.text = score_.ToString();

        //�n�C�X�R�A�X�V
        if(highscore_ < score_)
        {
            highscore_ = score_;
            PlayerPrefs.SetInt("HighScore", score_);
            PlayerPrefs.Save();
        }

        //�n�C�X�R�A�̃e�L�X�g���f
        highscoreTMP.text = highscore_.ToString();

        //�X�R�A�{�[�h�����X�ɕ\��
        StartCoroutine(GraduallyAppear());

        DoFade();
    }

    private void Update()
    {
        Time.timeScale = 1.0f;

        if (Gamepad.current == null)
        {
            return;
        }
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            SceneAddRequester sceneAddRequester = GetComponent<SceneAddRequester>();
            //��Ƀ����Q�[���V�[��������
            // sceneAddRequester.RequestUnLoadScene(SceneName.RUNGAME);
            //    //�G���f�B���O�̃��[�h
            //    //SceneManager.LoadScene("Scene_Ending");
            //    //�����Ă��̃V�[��������
            //���K�V�[
            //sceneAddRequester.RequestAddScene(SceneName.ENDING, true);
            IsFade = true;
        }
    }

    IEnumerator GraduallyAppear()
    {
        while (scoreBorad.alpha < 1.0f)
        {
            scoreBorad.alpha += 0.1f;
            if (scoreBorad.alpha >= 1.0f)
            {
                scoreBorad.alpha = 1.0f;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield break;
    }
    /// <summary>
    /// ���d�ǂݍ��ݖh�~�t�F�[�h�A�E�g�֐�
    /// </summary>
    void DoFade()
    {
        gameObject.ObserveEveryValueChanged(_ => IsFade)
                  .Where(x => IsFade)
                  .Subscribe(_ =>
                  {
                      SceneFadeManager.StartMoveScene("Scene_Ending");
                  });
    }
}