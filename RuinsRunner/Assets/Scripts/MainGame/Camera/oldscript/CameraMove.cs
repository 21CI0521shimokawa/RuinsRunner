using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using System.Linq;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject CameraTarget;
    [SerializeField] GameObject[] NextPositions;
    [SerializeField] PathType PathType;
    [SerializeField] Ease EaseType;
    [SerializeField] float MoveTime;
    void Start()
    {
        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
        this.transform.position = path[0];
        this.transform.DOPath(path, MoveTime).SetDelay(5f)
            .OnStart(() =>
            {//���s�J�n���̃R�[���o�b�N
                this.transform.DORotate(Vector3.up * -90f, 4f);
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
