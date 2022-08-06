using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneManagerResult : MonoBehaviour
{
    [SerializeField] float easingTime = 1.5f;
    [SerializeField] CanvasGroup scoreBorad;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [SerializeField] TextMeshProUGUI highscoreTMP;
    //int�^�ň������Ɏg�p
    int score_;
    //�n�C�X�R�A
    int highscore_;
    private void Start()
    {
        //�V�[���̏�����
        Scene scene = SceneManager.GetSceneByName("Manager");

        //�X�R�A�{�[�h�̏�����
        scoreBorad.alpha = 0f;
        scoreBorad.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //�n�C�X�R�A�̏�����
        highscore_ = PlayerPrefs.GetInt("Highscore", 0);

        //�n�C�X�R�A�̃e�L�X�g���f
        highscoreTMP.text = highscore_.ToString();

        //�X�R�A�����L�V�[�����玝���Ă��Ĕ��f������
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                break;
            }
        }
        scoreTMP.text = score_.ToString();

        //�X�R�A�{�[�h�����X�ɕ\��
        StartCoroutine(GraduallyAppear());
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
}