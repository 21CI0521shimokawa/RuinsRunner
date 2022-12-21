using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 攻撃予兆作成クラス
/// </summary>
public class AttackSignsBlinking : MonoBehaviour
{
    [Header("攻撃予兆作成の設定")]
    [SerializeField, Tooltip("点滅する時間")] float blinkingTime;
    [SerializeField, Tooltip("点滅させるゲームオブジェクトの描画情報取得")] new Renderer blinkingRenderer;
    private const int loopTime = -1; //ループ用

    /// <summary>
    /// 生成されてから一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        //オブジェクトの色を赤色にBlinkingTimeの速さで変化させループ
        blinkingRenderer.material.DOColor(Color.red, blinkingTime).SetLoops(loopTime, LoopType.Yoyo);
    }
}
