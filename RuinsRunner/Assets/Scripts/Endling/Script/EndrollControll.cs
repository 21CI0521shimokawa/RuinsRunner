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
    [SerializeField,Tooltip("テキストオブジェクト")] TextMeshProUGUI TargetText;
    [Header("エンドロール設定")]
    [SerializeField,Tooltip("エンドロールの時間")] float MoveTime;
    [SerializeField,Tooltip("テキストの目標値")] float DestinationY;
    [SerializeField,Tooltip("イージングの種類指定")] Ease EaseType;

    /// <summary>
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        MoveLooksLikeRunning.moveMagnification = 1.0f; //Playerの移動速度をデフォルト状態に戻す
        DoTextMove(TargetText); //エンドロール開始
    }

    /// <summary>
    /// エンドロール処理関数
    /// </summary>
    /// <param name="_TargetText">エンドロールさせるテキストオブジェクト</param>
    void DoTextMove(TextMeshProUGUI _TargetText)
    {
        _TargetText.transform.DOMoveY(DestinationY, MoveTime) //MoveTimeの速さで移動
            .OnComplete(() =>
            {
                SceneFadeManager.StartMoveScene("Scene_Title"); //移動完了したらシーン移行
            })
            .SetEase(EaseType);
    }
}
