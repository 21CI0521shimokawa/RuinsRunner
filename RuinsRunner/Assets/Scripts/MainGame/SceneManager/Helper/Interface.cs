using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

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
//TODO:�v���C���[�̌�ޏ����̃G���g���[�Ɏ�������
public interface IDamaged
{
    void CallReceiveDamage();
}

/// <summary>
/// �J�������e�X�g
/// </summary>
public interface ICameraMoveTest
{
    void CallCameraMove(Vector3 destination, GameObject newTarget);
}

/// <summary>
/// �v���C���[��Z�e�[�u�����X�V����
/// </summary>
public interface IMovePlayer
{
    void MovePlayer(int moveAmount);
}

/// <summary>
/// �����Q�[������~�j�Q�[���ɐ؂�ւ��v��
/// </summary>
public interface ISwitchRunToMG
{
    void SwitchMiniGame(GameState gameState);
}