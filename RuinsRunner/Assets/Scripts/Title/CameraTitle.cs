using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraTitle : MonoBehaviour
{
    [SerializeField] GameObject[] NextPositions;
    [SerializeField] PathType PathType;
    [SerializeField] Ease EaseType;
    [SerializeField] float MoveTime;
    void Start()
    {
        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
        this.transform.position = path[0];
        this.transform.DOPath(path, MoveTime)
            .OnStart(() =>
            {//���s�J�n���̃R�[���o�b�N
                this.transform.DORotate(new Vector3(0, -180f, 0), 1.0f);
            })
            .OnComplete(() =>
            {//���s�������̃R�[���o�b�N
                #region ������
                #endregion
            })
            .SetOptions(false)//true�ɂ����path[0]�ɖ߂�
            .SetEase(EaseType);//�C�[�W���O�^�C�v�w��
    }
}
