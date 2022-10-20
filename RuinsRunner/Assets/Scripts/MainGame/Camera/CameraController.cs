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
    [Header("�J�����̐ݒ�֘A")]
    [SerializeField,Tooltip("�C�[�W���O�̎��")] Ease SetEaseType;
    [Header("Positon�֘A")]
    [SerializeField,Tooltip("�f�t�H���g�̃J�����ʒu")] Transform DefaultCameraTransform;
    [Header("�J�����̃X�s�[�h")]
    [SerializeField,Tooltip("�f�t�H���g�̃J�����ʒu�ɖ߂鎞�̃X�s�[�h")] float ReturnDefaultCameraPositonCameraSpeed;
    [SerializeField,Tooltip("EnemyAttack�Q�[���ɓ���ۂ̃J�����ړ��X�s�[�h")] float EnemyFromBackCameraSpeed;
    [Header("Player�֘A")]
    [SerializeField,Tooltip("Player�I�u�W�F�N�g�擾")] Transform PlayerTransform;
    [SerializeField,Tooltip("Player���擾")] PlayerController PlayerScripts;

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    private void Awake()
    {
        DefaultCameraTransform = this.transform;//�f�t�H���g�ʒu���
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="_disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// ����ς݂Ȃ̂ŏ������Ȃ�
            return; 
        }
        this.isDisposed_ = true; // Dispose�ς݂��L�^
        base.Dispose(_disposing); // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
    }

    #region �C���^�[�t�F�[�X

    /// <summary>
    /// EnemyAttack���̃J�����̓����֐�
    /// </summary>
    /// <param name="CameraObject">�������Ώۂ̃I�u�W�F�N�g</param>
    public void DoEnemyFromBackMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.7f, 180f, 0f);
        CameraObject.transform.DOPath //�ړ�����ʒu�w��
              (
              new[]
              {
              new Vector3(DefaultCameraTransform.position.x,PlayerTransform.position.y+5,PlayerTransform.position.z+8), //�J������Player�̑O�Ɉړ�
              },
              EnemyFromBackCameraSpeed, PathType.Linear //EnemyFromBackCameraSpeed�̑����ňړ�����
              )
              .OnStart(() =>
              {
                  PlayerScripts.canMove = false; //�J�����ړ����̓R���g���[���[�̓��͂��~
                  PlayerScripts.isReverseLR = true; //�J���������]����̂œ��͂����]������
              })
              .OnUpdate(() =>
              {
                  CameraObject.transform.DORotate(DoRotation, EnemyFromBackCameraSpeed); //�J�����𔽓]������
              })
              .OnComplete(() =>
              {
                  PlayerScripts.canMove = true; //�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
              })
              .SetEase(SetEaseType);
    }

    /// <summary>
    /// EnemyAttack���I���������Default�ʒu�ɖ߂鎞�̊֐�
    /// </summary>
    /// <param name="CameraObject">�������Ώۂ̃I�u�W�F�N�g</param>
    public void DoDefaultCameraPositonMove(GameObject CameraObject)
    {
        var DoRotation = new Vector3(26.6f, 0, 0);
        CameraObject.transform.DOMove(new Vector3(DefaultCameraTransform.position.x, DefaultCameraTransform.position.y, PlayerTransform.position.z - 6), ReturnDefaultCameraPositonCameraSpeed) //���̈ʒu�ɖ߂�
             .OnStart(() =>
             {
                 PlayerScripts.canMove = false; //�J�����ړ����̓R���g���[���[�̓��͂��~
             })
             .OnUpdate(() =>
             {
                 CameraObject.transform.DORotate(DoRotation, ReturnDefaultCameraPositonCameraSpeed); //�J�����𔽓]������
             })
             .OnComplete(() =>
             {
                 PlayerScripts.canMove = true; //�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
                 PlayerScripts.isReverseLR = false;//�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
             })
            .SetEase(SetEaseType);
    }
    #endregion
}
