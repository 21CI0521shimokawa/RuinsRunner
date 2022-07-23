using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraMoveGameEnd : StateBase
{
    CameraController CameraState;
    Transform PlayerTransform;
    private const float MoveTime = 3.0f;
    public override void StateInitialize()
    {
        CameraState = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase StateBase = this;

        return StateBase;
    }
    public override void StateFinalize()
    {//スタブ
    }
    private void DoMove(GameObject CameraObject)
    {//依頼されてたカメラの動きの関数
        var MoveTo = new Vector3(0.0f, 1.93f, 22.0f);//強引すぎるからどうにかして今後変えたい。。
        CameraObject.transform.DOMove(MoveTo, MoveTime)
                    .SetEase(Ease.Linear);
    }
}
