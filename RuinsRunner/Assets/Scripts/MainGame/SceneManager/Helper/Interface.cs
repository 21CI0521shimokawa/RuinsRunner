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

/// <summary>
/// �X�R�A�̉��Z�E���Z
/// </summary>
public interface IUpdateScore
{
    void UpdateScore(int addScore);
}
/// <summary>
/// �R�C���J�E���^�[�̑���
/// </summary>
public interface IUpdateCoinCount
{
    void UpdateCoinCount();
}

public interface IExitGame
{
    void ExitToResult();
}
/// <summary>
/// EnemyAttack�Q�[�����n�܂������̃J�������[�u
/// </summary>
public interface ICameraEnemyFromBackMove
{
    void DoEnemyFromBackMove(GameObject CameraObject);
}
/// <summary>
/// Default�̈ʒu�ɖ߂�J�������[�u
/// </summary>
public interface IReturnDefaultCameraPositonMove
{
    void DoDefaultCameraPositonMove(GameObject CameraObject);
}
/// <summary>
/// �O��������ł���I�u�W�F�N�g�������Q�[���J�n
/// </summary>
public interface IAvoidGame
{
    void DoAvoidGame();
}
/// <summary>
/// �J�����̈ʒu�C��
/// </summary>
public interface IEditingCameraPositon
{
    void DoEditingCameraPositon(GameObject CameraObject);
}