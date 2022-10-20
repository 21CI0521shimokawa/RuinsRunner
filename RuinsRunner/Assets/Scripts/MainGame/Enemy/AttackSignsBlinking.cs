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
    [SerializeField,Tooltip("点滅する時間")] float BlinkingTime;
    [SerializeField,Tooltip("点滅させるゲームオブジェクトの描画情報取得")] Renderer Renderer;
    private const int LoopTime = -1; //無限ループ用

    /// <summary>
    /// 生成されてから一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        this.Renderer.material.DOColor(Color.red, BlinkingTime).SetLoops(LoopTime, LoopType.Yoyo); //オブジェクトの色を赤色にBlinkingTimeの速さで変化させ無限ループ
    }
}
