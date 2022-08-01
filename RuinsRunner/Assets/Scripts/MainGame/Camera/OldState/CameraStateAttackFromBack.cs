using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraStateAttackFromBack : StateBase
{
    CameraController StateController;
    Transform PlayerTransform;
    GameObject MainCamera;
    private const float MoveTime = 3.0f;
    private Vector3 DoRotation;
    public override void StateInitialize()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StateController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DoRotation = new Vector3(26.7f, 180f, 0f);
        DoMove(MainCamera);
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NowState = this;

        return NowState;
    }
    public override void StateFinalize()
    {

    }
    private void DoMove(GameObject CameraObject)
    {
        CameraObject.transform.DOPath
              (
              new[]
              {
              new Vector3(PlayerTransform.position.x,PlayerTransform.position.y+5,PlayerTransform.position.z+8),
              },
              3f, PathType.Linear
              ).OnUpdate(() =>
              {
                  CameraObject.transform.DORotate(DoRotation, MoveTime);
              })
              .OnComplete(() =>
               {
               });
    }
    #region ÉRÉãÅ[É`Éì
    #endregion
}
