using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SceneDefine;
using UnityEngine.InputSystem;

public class EndrollControll : SceneSuperClass
{
    [Header("�ړ�������e�L�X�g")]
    [SerializeField] TextMeshProUGUI TargetText;
    [Header("�G���h���[���ݒ�")]
    [SerializeField] float MoveTime;
    [SerializeField] float DestinationY;
    [SerializeField] Ease EaseType;
    [Header("scene�ڍs�ݒ�")]
    [SerializeField] float SceneChangeTime;
    private Tweener _tweener;
    void Start()
    {
        DoTextMove(TargetText);

        MoveLooksLikeRunning.moveMagnification = 1.0f;
    }
    void DoTextMove(TextMeshProUGUI _TargetText)
    {
        _tweener = _TargetText.transform.DOMoveY(DestinationY, MoveTime)
            .OnUpdate(() =>
            {
            })
            .OnComplete(() =>
            {
                SceneFadeManager.StartMoveScene("Scene_Title");
            })
            .SetEase(EaseType);
    }
}
