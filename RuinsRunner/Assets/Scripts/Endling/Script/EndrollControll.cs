using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SceneDefine;

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
    void Start()
    {
        DoTextMove(TargetText);
    }
    void DoTextMove(TextMeshProUGUI _TargetText)
    {
        _TargetText.transform.DOMoveY(DestinationY, MoveTime)
            .OnComplete(() =>
            {
                SceneFadeManager.StartMoveScene("Scene_Title");
            })
            .SetEase(EaseType);
    }
}
