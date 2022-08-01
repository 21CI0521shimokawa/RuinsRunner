using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraStateDefault : StateBase
{
    CameraController StateController;
    GameObject MainCamera;
    private const float MoveTime = 3.0f;
    public override void StateInitialize()
    {
        #region �T�C�h�r���[�ɕύX(���ݕs�g�p)
        /* Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
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
        */
        #endregion
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        StateController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NowState = this;
        #region ���ڍs
        if (Input.GetKeyDown(KeyCode.W))
        {
            NowState = new CameraStateAttackFromBack();
        }
        #endregion
        return NowState;
    }

    public override void StateFinalize()
    {//�X�^�u
    }
    #region �C���^�[�t�F�[�X
    public void MoveEndCollBack(bool IsStart)
    {

    }
    #endregion
}
