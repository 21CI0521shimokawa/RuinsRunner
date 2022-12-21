using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SceneDefine;
using UnityEngine.InputSystem;
using Loading.Utility;

public class EndrollController : SceneSuperClass
{
    [Header("移動させるテキスト")]
    [SerializeField, Tooltip("テキストオブジェクト")] TextMeshProUGUI targetText;
    [Header("エンドロール設定")]
    [SerializeField, Tooltip("エンドロールの時間")] float moveTime;
    [SerializeField, Tooltip("テキストの目標値")] float destinationY;
    private const float defaultGameTimeScele = 1.0f;
    [SerializeField] Ease easeType;
    [Header("外部スクリプト")]
    [SerializeField, Tooltip("ロード関数呼び出し用に取得")] LodingManeger lodingManeger;

    /// <summary>
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        //Playerの移動速度をデフォルト状態に戻す
        MoveLooksLikeRunning.moveMagnification = defaultGameTimeScele;
        //エンドロール開始
        DoTextMove();
    }

    /// <summary>
    /// エンドロール処理関数
    /// </summary>
    void DoTextMove()
    {
        //MoveTimeの速さで移動
        targetText.transform.DOMoveY(destinationY, moveTime)
            .OnComplete(() =>
            {
                //移動完了したらシーン移行
                lodingManeger.LoadToNextScene("Scene_Title");
            })
            //イージングタイプを指定
            .SetEase(easeType);
    }
}
