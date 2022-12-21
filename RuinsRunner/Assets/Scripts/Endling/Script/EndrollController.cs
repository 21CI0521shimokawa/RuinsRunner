using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SceneDefine;
using UnityEngine.InputSystem;
using Loading.Utility;

public class EndrollController : SceneSuperClass
{
    [Header("�ړ�������e�L�X�g")]
    [SerializeField, Tooltip("�e�L�X�g�I�u�W�F�N�g")] TextMeshProUGUI targetText;
    [Header("�G���h���[���ݒ�")]
    [SerializeField, Tooltip("�G���h���[���̎���")] float moveTime;
    [SerializeField, Tooltip("�e�L�X�g�̖ڕW�l")] float destinationY;
    private const float defaultGameTimeScele = 1.0f;
    [SerializeField] Ease easeType;
    [Header("�O���X�N���v�g")]
    [SerializeField, Tooltip("���[�h�֐��Ăяo���p�Ɏ擾")] LodingManeger lodingManeger;

    /// <summary>
    /// �Q�[�����n�܂鎞�Ɉ�x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        //Player�̈ړ����x���f�t�H���g��Ԃɖ߂�
        MoveLooksLikeRunning.moveMagnification = defaultGameTimeScele;
        //�G���h���[���J�n
        DoTextMove();
    }

    /// <summary>
    /// �G���h���[�������֐�
    /// </summary>
    void DoTextMove()
    {
        //MoveTime�̑����ňړ�
        targetText.transform.DOMoveY(destinationY, moveTime)
            .OnComplete(() =>
            {
                //�ړ�����������V�[���ڍs
                lodingManeger.LoadToNextScene("Scene_Title");
            })
            //�C�[�W���O�^�C�v���w��
            .SetEase(easeType);
    }
}
