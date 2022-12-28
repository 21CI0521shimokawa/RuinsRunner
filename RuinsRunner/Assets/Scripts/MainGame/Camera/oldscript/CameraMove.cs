using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject CameraTarget;
    [SerializeField] GameObject[] NextPositions;
    [SerializeField] PathType PathType;
    [SerializeField] Ease EaseType;
    [SerializeField] float MoveTime;
    void Start()
    {
        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
        this.transform.position = path[0];
        this.transform.DOPath(path, MoveTime).SetDelay(5f)
            .OnStart(() =>
            {//実行開始時のコールバック
                this.transform.DORotate(Vector3.up * -90f, 4f);
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
