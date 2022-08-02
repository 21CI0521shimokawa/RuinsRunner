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
    [SerializeField] Ease SetEaseType;
    [Header("Positon関連")]
    [SerializeField] Transform DefaultCameraTransform;
    [Header("カメラのスピード")]
    [SerializeField] float ReturnDefaultCameraPositonCameraSpeed;
    [SerializeField] float EnemyFromBackCameraSpeed;
    [SerializeField] float EndGameCameraSpeed;
    [Header("Player関連")]
    [SerializeField] Transform PlayerTransform;

    private void Awake()
    {
        DefaultCameraTransform = this.transform;
    }
    void Start()
    {
    }
    private void GameEndCameraMove(GameObject CameraObject)
    {
        //MapStateが実装出来次第随時更新します
        /*CameraObject.ObserveEveryValueChanged(_ =>MapState)
            .Where(_=>MapState==StateMachine(new new))*/
        var MoveTo = new Vector3(0.0f, 1.93f, 22.0f);//強引すぎるからどうにかして今後変えたい。。
        CameraObject.transform.DOMove(MoveTo, EndGameCameraSpeed)
                    .SetEase(SetEaseType);
    }
    #region インターフェース
    public void DoEnemyFromBackMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.7f, 180f, 0f);
        CameraObject.transform.DOPath
              (
              new[]
              {
              new Vector3(PlayerTransform.position.x,PlayerTransform.position.y+5,PlayerTransform.position.z+8),
              },
              3f, PathType.Linear
              ).OnUpdate(() =>
              {
                  CameraObject.transform.DORotate(DoRotation, EnemyFromBackCameraSpeed);
              })
              .OnComplete(() =>
              {
              })
              .SetEase(SetEaseType);
    }

    public void DoDefaultCameraPositonMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.6f, 0, 0);
        CameraObject.transform.DOMove(new Vector3(DefaultCameraTransform.position.x, DefaultCameraTransform.position.y,DefaultCameraTransform.position.z-14), ReturnDefaultCameraPositonCameraSpeed)
            .OnUpdate(() =>
            {
                CameraObject.transform.DORotate(DoRotation, ReturnDefaultCameraPositonCameraSpeed);
            })
            .SetEase(SetEaseType);
    }
    #endregion
}
