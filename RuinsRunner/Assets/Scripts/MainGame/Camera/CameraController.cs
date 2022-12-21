using UnityEngine;
using DG.Tweening;

public class CameraController
    : ObjectSuperClass
    , ICameraEnemyFromBackMove
    , IReturnDefaultCameraPositonMove
{
    [Header("カメラの設定関連")]
    [SerializeField, Tooltip("イージングの種類")] Ease setEaseType;
    private Tweener tweener; //DoTweenの実行の戻り値としてTweenerを取得
    [Header("Positon関連")]
    [SerializeField, Tooltip("デフォルトのカメラ位置")] Transform defaultCameraTransform;
    [Header("カメラのスピード")]
    [SerializeField, Tooltip("デフォルトのカメラ位置に戻る時のスピード")] float returnDefaultCameraPositonCameraSpeed;
    [SerializeField, Tooltip("EnemyAttackゲームに入る際のカメラ移動スピード")] float enemyFromBackCameraSpeed;
    [Header("Player関連")]
    [SerializeField, Tooltip("Playerオブジェクト取得")] Transform playerTransform;
    [SerializeField, Tooltip("Player情報取得")] PlayerController playerScripts;

    private void Awake()
    {
        //デフォルト位置代入
        defaultCameraTransform = transform;
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // 解放済みなので処理しない
            return;
        }
        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(disposing);
    }

    #region インターフェース

    /// <summary>
    /// EnemyAttack時のカメラの動き関数
    /// </summary>
    /// <param name="cameraObject">動かす対象のオブジェクト</param>
    public void DoEnemyFromBackMove(GameObject cameraObject)
    {
        //カメラのローテーションの値を指定
        Vector3 doRotation = new Vector3(26.7f, 180f, 0f);
        //移動する位置指定
        tweener = cameraObject.transform.DOPath
         (
         new[]
         {
              //カメラをPlayerの前に移動
              new Vector3(defaultCameraTransform.position.x,playerTransform.position.y+5,playerTransform.position.z+8),
         },
         //EnemyFromBackCameraSpeedの速さで移動完了
         enemyFromBackCameraSpeed, PathType.Linear
         );

        tweener.OnStart(() =>
         {
             //カメラ移動中はコントローラーの入力を停止
             playerScripts.canMove = false;
             //カメラが反転するので入力も反転させる
             playerScripts.isReverseLR = true;
         })
         .OnUpdate(() =>
         {
             //カメラを反転させる
             cameraObject.transform.DORotate(doRotation, enemyFromBackCameraSpeed);
         })
         .OnComplete(() =>
         {
             //カメラ移動が完了したのでコントローラーの入力を再開
             playerScripts.canMove = true;
         })
         .SetEase(setEaseType);
    }

    /// <summary>
    /// EnemyAttackが終わった時にDefault位置に戻る時の関数
    /// </summary>
    /// <param name="cameraObject">動かす対象のオブジェクト</param>
    public void DoDefaultCameraPositonMove(GameObject cameraObject)
    {
        //ローテーションの値を指定
        var DoRotation = new Vector3(26.6f, 0, 0);
        //元の位置に戻る
        tweener = cameraObject.transform.DOMove(
              new Vector3(defaultCameraTransform.position.x,
                          defaultCameraTransform.position.y,
                          playerTransform.position.z - 6),
                          returnDefaultCameraPositonCameraSpeed);
        tweener
         .OnStart(() =>
         {
             //カメラ移動中はコントローラーの入力を停止
             playerScripts.canMove = false;
         })
         .OnUpdate(() =>
         {
             //カメラを反転させる
             cameraObject.transform.DORotate(DoRotation, returnDefaultCameraPositonCameraSpeed);
         })
         .OnComplete(() =>
         {
             //カメラ移動が完了したのでコントローラーの入力を再開
             playerScripts.canMove = true;
             //カメラ移動が完了したのでコントローラーの入力を再開
             playerScripts.isReverseLR = false;
         })
        .SetEase(setEaseType);
    }
    #endregion
}
