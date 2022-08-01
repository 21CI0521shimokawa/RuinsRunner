using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraController : ObjectSuperClass
{
    StateMachine MapState;
    [Header("�J�����̐ݒ�֘A")]
    [SerializeField] Ease SetEaseType;
    [Header("Positon�֘A")]
    [SerializeField] Transform DefaultCameraTransform;
    [Header("�J�����̃X�s�[�h")]
    [SerializeField] float ReturnDefaultCameraPositonCameraSpeed;
    [SerializeField] float EnemyFromBackCameraSpeed;
    [SerializeField] float EndGameCameraSpeed;
    [Header("Player�֘A")]
    [SerializeField] Transform PlayerTransform;

    void Start()
    {
        DefaultCameraTransform = this.transform;
        DoDefaultCameraMove(gameObject);
        DoEnemyFromBackMove(gameObject);
    }

    private void DoDefaultCameraMove(GameObject CameraObject)
    {
        CameraObject.ObserveEveryValueChanged(_ => MapState)
            .Where(_ => MapState == new StateMachine(new NewMapGeneratorState_Normal()))
            .Subscribe(_ =>
            {

                var DoRotation = new Vector3(26.6f, 0, 0);
                CameraObject.transform.DOMove(DefaultCameraTransform.position, ReturnDefaultCameraPositonCameraSpeed)
                    .OnUpdate(() =>
                    {
                        CameraObject.transform.DORotate(DoRotation, ReturnDefaultCameraPositonCameraSpeed);
                    })
                    .SetEase(SetEaseType);
            });
    }
    private void DoEnemyFromBackMove(GameObject CameraObject)
    {
        CameraObject.ObserveEveryValueChanged(_ => MapState)
            .Where(_ => MapState == new StateMachine(new NewMapGeneratorState_EnemyAttack()))
            .Subscribe(_ =>
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
            });
    }
    private void GameEndCameraMove(GameObject CameraObject)
    {
        //MapState�������o�����搏���X�V���܂�
        /*CameraObject.ObserveEveryValueChanged(_ =>MapState)
            .Where(_=>MapState==StateMachine(new new))*/
        var MoveTo = new Vector3(0.0f, 1.93f, 22.0f);//���������邩��ǂ��ɂ����č���ς������B�B
        CameraObject.transform.DOMove(MoveTo, EndGameCameraSpeed)
                    .SetEase(SetEaseType);
    }
}
