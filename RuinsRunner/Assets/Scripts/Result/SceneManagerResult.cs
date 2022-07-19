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
    private void Start()
    {
        //�X�R�A�{�[�h�̏�����
        scoreBorad.alpha = 0f;
        scoreBorad.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Scene scene = SceneManager.GetSceneByName("Manager");

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

        //    //�X�R�A�{�[�h�����X�ɕ\��
        //    DOTween.ToAlpha(
        //        () => new Color(0, 0, 0, scoreBorad.alpha),
        //        color => scoreBorad.alpha = color.a,
        //        1.0f,           //�I�����̃��l��1.0f
        //        easingTime      //�b���ݒ�
        //        )
        //        .SetEase(ease);
        //}

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