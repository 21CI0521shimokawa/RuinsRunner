using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraTitle : ObjectSuperClass
{
    [SerializeField, Tooltip("移動させる位置情報まとめ")] GameObject[] nextPositions;
    [SerializeField, Tooltip("イージングタイプ")] Ease easeType;
    [SerializeField, Tooltip("回転速度")] float rotationSpeed;
    [SerializeField, Tooltip("移動時間")] float moveTime;

    /// <summary>
    /// ゲームが始まるときに一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        Vector3[] path = nextPositions.Select(x => x.transform.position).ToArray(); //移動させる位置をpathに代入
        this.transform.position = path[0]; //とりあえず最初のポジションを目標値の0番目に設定した所に移動
        this.transform.DOPath(path, moveTime) //MoveTimeの速さで移動
            .OnStart(() =>
            {
                this.transform.DORotate(new Vector3(0, -180f, 0), rotationSpeed); //RotationSpeedの速さで回転
            })
            .SetOptions(false)
            .SetEase(easeType);
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="_Disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool _Disposing)
    {
        if (this.IsDisposed_)
        {// 解放済みなので処理しない
            return;
        }
        this.IsDisposed_ = true; // Dispose済みを記録
        base.Dispose(_Disposing); // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
    }
}
