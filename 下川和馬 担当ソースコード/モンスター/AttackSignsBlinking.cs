using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �U���\���쐬�N���X
/// </summary>
public class AttackSignsBlinking : MonoBehaviour
{
    [Header("�U���\���쐬�̐ݒ�")]
    [SerializeField, Tooltip("�_�ł��鎞��")] float blinkingTime;
    [SerializeField, Tooltip("�_�ł�����Q�[���I�u�W�F�N�g�̕`����擾")] new Renderer blinkingRenderer;
    private const int loopTime = -1; //���[�v�p

    /// <summary>
    /// ��������Ă����x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        //�I�u�W�F�N�g�̐F��ԐF��BlinkingTime�̑����ŕω��������[�v
        blinkingRenderer.material.DOColor(Color.red, blinkingTime).SetLoops(loopTime, LoopType.Yoyo);
    }
}
