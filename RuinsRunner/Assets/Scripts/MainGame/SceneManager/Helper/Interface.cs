using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̂�|��
/// </summary>
public interface IToFallenOver
{
    void CallToFallOver();
}

/// <summary>
/// �_���[�W���󂯂�i�y�i���e�B���󂯂�j
/// </summary>
//TODO:Enemy�̋��ݏ����̃G���g���[�A�v���C���[�̌�ޏ����̃G���g���[�Ɏ�������
public interface IDamaged
{
    void Damaged();
}