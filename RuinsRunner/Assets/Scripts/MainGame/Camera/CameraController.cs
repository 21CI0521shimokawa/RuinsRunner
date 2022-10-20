using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraController
    : ObjectSuperClass
    , ICameraEnemyFromBackMove
    , IReturnDefaultCameraPositonMove
{
    [Header("カメラの設定関連")]
    [SerializeField,Tooltip("イージングの種類")] Ease SetEaseType;
    [Header("Positon関連")]
    [SerializeField,Tooltip("デフォルトのカメラ位置")] Transform DefaultCameraTransform;
    [Header("カメラのスピード")]
    [SerializeField,Tooltip("デフォルトのカメラ位置に戻る時のスピード")] float ReturnDefaultCameraPositonCameraSpeed;
    [SerializeField,Tooltip("EnemyAttackゲームに入る際のカメラ移動スピード")] float EnemyFromBackCameraSpeed;
    [Header("Player関連")]
    [SerializeField,Tooltip("Playerオブジェクト取得")] Transform PlayerTransform;
    [SerializeField,Tooltip("Player情報取得")] PlayerController PlayerScripts;

    /// <summary>
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    private void Awake()
    {
        DefaultCameraTransform = this.transform;//デフォルト位置代入
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="_disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// 解放済みなので処理しない
            return; 
        }
        this.isDisposed_ = true; // Dispose済みを記録
        base.Dispose(_disposing); // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
    }

    #region インターフェース

    /// <summary>
    /// EnemyAttack時のカメラの動き関数
    /// </summary>
    /// <param name="CameraObject">動かす対象のオブジェクト</param>
    public void DoEnemyFromBackMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.7f, 180f, 0f);
        CameraObject.transform.DOPath //移動する位置指定
              (
              new[]
              {
              new Vector3(DefaultCameraTransform.position.x,PlayerTransform.position.y+5,PlayerTransform.position.z+8), //カメラをPlayerの前に移動
              },
              EnemyFromBackCameraSpeed, PathType.Linear //EnemyFromBackCameraSpeedの速さで移動完了
              )
              .OnStart(() =>
              {
                  PlayerScripts.canMove = false; //カメラ移動中はコントローラーの入力を停止
                  PlayerScripts.isReverseLR = true; //カメラが反転するので入力も反転させる
              })
              .OnUpdate(() =>
              {
                  CameraObject.transform.DORotate(DoRotation, EnemyFromBackCameraSpeed); //カメラを反転させる
              })
              .OnComplete(() =>
              {
                  PlayerScripts.canMove = true; //カメラ移動が完了したのでコントローラーの入力を再開
              })
              .SetEase(SetEaseType);
    }

    /// <summary>
    /// EnemyAttackが終わった時にDefault位置に戻る時の関数
    /// </summary>
    /// <param name="CameraObject">動かす対象のオブジェクト</param>
    public void DoDefaultCameraPositonMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.6f, 0, 0);
        CameraObject.transform.DOMove(new Vector3(DefaultCameraTransform.position.x, DefaultCameraTransform.position.y, PlayerTransform.position.z - 6), ReturnDefaultCameraPositonCameraSpeed) //元の位置に戻る
             .OnStart(() =>
             {
                 PlayerScripts.canMove = false; //カメラ移動中はコントローラーの入力を停止
             })
             .OnUpdate(() =>
             {
                 CameraObject.transform.DORotate(DoRotation, ReturnDefaultCameraPositonCameraSpeed); //カメラを反転させる
             })
             .OnComplete(() =>
             {
                 PlayerScripts.canMove = true; //カメラ移動が完了したのでコントローラーの入力を再開
                 PlayerScripts.isReverseLR = false;//カメラ移動が完了したのでコントローラーの入力を再開
             })
            .SetEase(SetEaseType);
    }
    #endregion
}
