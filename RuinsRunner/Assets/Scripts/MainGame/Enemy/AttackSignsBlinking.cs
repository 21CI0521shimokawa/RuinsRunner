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
    [SerializeField,Tooltip("�_�ł��鎞��")] float BlinkingTime;
    [SerializeField,Tooltip("�_�ł�����Q�[���I�u�W�F�N�g�̕`����擾")] Renderer Renderer;
    private const int LoopTime = -1; //�������[�v�p

    /// <summary>
    /// ��������Ă����x�����Ă΂��֐�
    /// </summary>
    void Start()
    {
        this.Renderer.material.DOColor(Color.red, BlinkingTime).SetLoops(LoopTime, LoopType.Yoyo); //�I�u�W�F�N�g�̐F��ԐF��BlinkingTime�̑����ŕω������������[�v
    }
}
