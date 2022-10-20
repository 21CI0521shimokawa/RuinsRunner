using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SceneDefine;
using UnityEngine.InputSystem;

public class EndrollControll : SceneSuperClass
{
    [Header("�ړ�������e�L�X�g")]
    [SerializeField,Tooltip("�e�L�X�g�I�u�W�F�N�g")] TextMeshProUGUI TargetText;
    [Header("�G���h���[���ݒ�")]
    [SerializeField,Tooltip("�G���h���[���̎���")] float MoveTime;
    [SerializeField,Tooltip("�e�L�X�g�̖ڕW�l")] float DestinationY;
    [SerializeField,Tooltip("�C�[�W���O�̎�ގw��")] Ease EaseType;

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        MoveLooksLikeRunning.moveMagnification = 1.0f; //Player�̈ړ����x���f�t�H���g��Ԃɖ߂�
        DoTextMove(TargetText); //�G���h���[���J�n
    }

    /// <summary>
    /// �G���h���[�������֐�
    /// </summary>
    /// <param name="_TargetText">�G���h���[��������e�L�X�g�I�u�W�F�N�g</param>
    void DoTextMove(TextMeshProUGUI _TargetText)
    {
        _TargetText.transform.DOMoveY(DestinationY, MoveTime) //MoveTime�̑����ňړ�
            .OnComplete(() =>
            {
                SceneFadeManager.StartMoveScene("Scene_Title"); //�ړ�����������V�[���ڍs
            })
            .SetEase(EaseType);
    }
}
