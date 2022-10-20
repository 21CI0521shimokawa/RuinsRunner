using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraTitle : ObjectSuperClass
{
    [SerializeField,Tooltip("�ړ�������ʒu���")] GameObject[] NextPositions;
    [SerializeField,Tooltip("�C�[�W���O�^�C�v")] Ease EaseType;
    [SerializeField, Tooltip("��]���x")] float RotationSpeed;
    [SerializeField,Tooltip("�ړ�����")] float MoveTime;

    /// <summary>
    /// �Q�[�����n�܂�Ƃ��Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray(); //�ړ�������ʒu��path�ɑ��
        this.transform.position = path[0]; //�Ƃ肠�����ŏ��̃|�W�V������ڕW�l��0�Ԗڂɐݒ肵�����Ɉړ�
        this.transform.DOPath(path, MoveTime) //MoveTime�̑����ňړ�
            .OnStart(() =>
            {
                this.transform.DORotate(new Vector3(0, -180f, 0), RotationSpeed); //RotationSpeed�̑����ŉ�]
            })
            .SetOptions(false)
            .SetEase(EaseType);
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
}
