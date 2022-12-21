using UnityEngine;
using DG.Tweening;

public class CameraController
    : ObjectSuperClass
    , ICameraEnemyFromBackMove
    , IReturnDefaultCameraPositonMove
{
    [Header("�J�����̐ݒ�֘A")]
    [SerializeField, Tooltip("�C�[�W���O�̎��")] Ease setEaseType;
    private Tweener tweener; //DoTween�̎��s�̖߂�l�Ƃ���Tweener���擾
    [Header("Positon�֘A")]
    [SerializeField, Tooltip("�f�t�H���g�̃J�����ʒu")] Transform defaultCameraTransform;
    [Header("�J�����̃X�s�[�h")]
    [SerializeField, Tooltip("�f�t�H���g�̃J�����ʒu�ɖ߂鎞�̃X�s�[�h")] float returnDefaultCameraPositonCameraSpeed;
    [SerializeField, Tooltip("EnemyAttack�Q�[���ɓ���ۂ̃J�����ړ��X�s�[�h")] float enemyFromBackCameraSpeed;
    [Header("Player�֘A")]
    [SerializeField, Tooltip("Player�I�u�W�F�N�g�擾")] Transform playerTransform;
    [SerializeField, Tooltip("Player���擾")] PlayerController playerScripts;

    private void Awake()
    {
        //�f�t�H���g�ʒu���
        defaultCameraTransform = transform;
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(disposing);
    }

    #region �C���^�[�t�F�[�X

    /// <summary>
    /// EnemyAttack���̃J�����̓����֐�
    /// </summary>
    /// <param name="cameraObject">�������Ώۂ̃I�u�W�F�N�g</param>
    public void DoEnemyFromBackMove(GameObject cameraObject)
    {
        //�J�����̃��[�e�[�V�����̒l���w��
        Vector3 doRotation = new Vector3(26.7f, 180f, 0f);
        //�ړ�����ʒu�w��
        tweener = cameraObject.transform.DOPath
         (
         new[]
         {
              //�J������Player�̑O�Ɉړ�
              new Vector3(defaultCameraTransform.position.x,playerTransform.position.y+5,playerTransform.position.z+8),
         },
         //EnemyFromBackCameraSpeed�̑����ňړ�����
         enemyFromBackCameraSpeed, PathType.Linear
         );

        tweener.OnStart(() =>
         {
             //�J�����ړ����̓R���g���[���[�̓��͂��~
             playerScripts.canMove = false;
             //�J���������]����̂œ��͂����]������
             playerScripts.isReverseLR = true;
         })
         .OnUpdate(() =>
         {
             //�J�����𔽓]������
             cameraObject.transform.DORotate(doRotation, enemyFromBackCameraSpeed);
         })
         .OnComplete(() =>
         {
             //�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
             playerScripts.canMove = true;
         })
         .SetEase(setEaseType);
    }

    /// <summary>
    /// EnemyAttack���I���������Default�ʒu�ɖ߂鎞�̊֐�
    /// </summary>
    /// <param name="cameraObject">�������Ώۂ̃I�u�W�F�N�g</param>
    public void DoDefaultCameraPositonMove(GameObject cameraObject)
    {
        //���[�e�[�V�����̒l���w��
        var DoRotation = new Vector3(26.6f, 0, 0);
        //���̈ʒu�ɖ߂�
        tweener = cameraObject.transform.DOMove(
              new Vector3(defaultCameraTransform.position.x,
                          defaultCameraTransform.position.y,
                          playerTransform.position.z - 6),
                          returnDefaultCameraPositonCameraSpeed);
        tweener
         .OnStart(() =>
         {
             //�J�����ړ����̓R���g���[���[�̓��͂��~
             playerScripts.canMove = false;
         })
         .OnUpdate(() =>
         {
             //�J�����𔽓]������
             cameraObject.transform.DORotate(DoRotation, returnDefaultCameraPositonCameraSpeed);
         })
         .OnComplete(() =>
         {
             //�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
             playerScripts.canMove = true;
             //�J�����ړ������������̂ŃR���g���[���[�̓��͂��ĊJ
             playerScripts.isReverseLR = false;
         })
        .SetEase(setEaseType);
    }
    #endregion
}
