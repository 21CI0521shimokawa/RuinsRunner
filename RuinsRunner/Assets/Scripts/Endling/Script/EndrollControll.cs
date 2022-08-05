using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EndrollControll : MonoBehaviour
{
    [Header("�ړ�������e�L�X�g")]
    [SerializeField] TextMeshProUGUI TargetText;
    [Header("�G���h���[���ݒ�")]
    [SerializeField] float MoveTime;
    [SerializeField] float DestinationY;
    [SerializeField] Ease EaseType;
    [Header("scene�ڍs�ݒ�")]
    [SerializeField] float SceneChangeTime;
    void Start()
    {
        DoTextMove(TargetText);
    }
    void DoTextMove(TextMeshProUGUI _TargetText)
    {
        _TargetText.transform.DOMoveY(DestinationY, MoveTime)
            .OnComplete(() =>
            {
                //scene�ڍs�̏���
            })
            .SetEase(EaseType);
    }
}
