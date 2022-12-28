using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraTitle : ObjectSuperClass
{
    [SerializeField, Tooltip("�ړ�������ʒu���܂Ƃ�")] GameObject[] nextPositions;
    [SerializeField, Tooltip("�C�[�W���O�^�C�v")] Ease easeType;
    [SerializeField, Tooltip("��]���x")] float rotationSpeed;
    [SerializeField, Tooltip("�ړ�����")] float moveTime;

    /// <summary>
    /// �Q�[�����n�܂�Ƃ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        Vector3[] path = nextPositions.Select(x => x.transform.position).ToArray(); //�ړ�������ʒu��path�ɑ��
        this.transform.position = path[0]; //�Ƃ肠�����ŏ��̃|�W�V������ڕW�l��0�Ԗڂɐݒ肵�����Ɉړ�
        this.transform.DOPath(path, moveTime) //MoveTime�̑����ňړ�
            .OnStart(() =>
            {
                this.transform.DORotate(new Vector3(0, -180f, 0), rotationSpeed); //RotationSpeed�̑����ŉ�]
            })
            .SetOptions(false)
            .SetEase(easeType);
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="_Disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool _Disposing)
    {
        if (this.IsDisposed_)
        {// ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        this.IsDisposed_ = true; // Dispose�ς݂��L�^
        base.Dispose(_Disposing); // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
    }
}
