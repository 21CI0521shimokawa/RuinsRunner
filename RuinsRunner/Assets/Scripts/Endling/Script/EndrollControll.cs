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
    [Header("移動させるテキスト")]
    [SerializeField] TextMeshProUGUI TargetText;
    [Header("エンドロール設定")]
    [SerializeField] float MoveTime;
    [SerializeField] float DestinationY;
    [SerializeField] Ease EaseType;
    [Header("scene移行設定")]
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
