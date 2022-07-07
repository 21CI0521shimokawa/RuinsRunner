using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;

public class TextManeger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI QuizText;
    [SerializeField] float TextSpeed;
    private string nowDispStr = ""; //���ۂɉ�ʂɕ\��������p�̕�����
    private string maxDispStr = ""; //�\�������������e�̕�����
    private float nowDispCount = 0.0f; //���݉������ڂ܂ŕ\�����邩�̃J�E���^�[

    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                maxDispStr = "���{�d�q�̏��ݒn�́H";

                //���t���[���J�E���^�[�����Z����
                nowDispCount += Time.deltaTime /TextSpeed;

                //�J�E���^�[�̕��܂ŕ\���p�̕�����փR�s�[���Atext�ɓn��
                nowDispStr = maxDispStr.Substring(0, Mathf.Min((int)nowDispCount, maxDispStr.Length));
                QuizText.text = nowDispStr;
            });
    }
}
