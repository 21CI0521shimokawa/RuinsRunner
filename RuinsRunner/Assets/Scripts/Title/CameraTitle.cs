using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraTitle : MonoBehaviour
{
    [SerializeField] GameObject[] NextPositions;
    [SerializeField] PathType PathType;
    [SerializeField] Ease EaseType;
    [SerializeField] float MoveTime;
    void Start()
    {
        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
        this.transform.position = path[0];
        this.transform.DOPath(path, MoveTime)
            .OnStart(() =>
            {//実行開始時のコールバック
                this.transform.DORotate(new Vector3(0, -180f, 0), 1.0f);
            })
            .OnComplete(() =>
            {//実行完了時のコールバック
                #region 未処理
                #endregion
            })
            .SetOptions(false)//trueにするとpath[0]に戻る
            .SetEase(EaseType);//イージングタイプ指定
    }
}
