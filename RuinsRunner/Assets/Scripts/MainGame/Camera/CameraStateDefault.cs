using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraStateDefault : StateBase
{
    CameraController StateController;
    GameObject MainCamera;
    private const float MoveTime = 3.0f;
    public override void StateInitialize()
    {
        #region サイドビューに変更(現在不使用)
        /* Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
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
        */
        #endregion
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StateController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NowState = this;
        #region 仮移行
        if (Input.GetKeyDown(KeyCode.W))
        {
            NowState = new CameraStateAttackFromBack();
        }
        #endregion
        return NowState;
    }

    public override void StateFinalize()
    {//スタブ
    }
    #region インターフェース
    public void MoveEndCollBack(bool IsStart)
    {

    }
    #endregion
}
