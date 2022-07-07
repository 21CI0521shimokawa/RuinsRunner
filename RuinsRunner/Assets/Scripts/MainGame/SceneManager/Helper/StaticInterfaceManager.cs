using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public static class StaticInterfaceManager
{
    /// <summary>
    /// ����|���v�����󂯖��߂���
    /// �v���C���[���ڐG����gameObject���Q�Ɠn�����Ďg��
    /// </summary>
    /// <param name="_pillar"></param>
    static public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// �U���v�����󂯖��߂���
    /// �U�����鑤���U���Ώۂ�gameObject��n���Ďg��
    /// </summary>
    static public void CauseDamage(GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.CallReceiveDamage();
    }

    /// <summary>
    /// �J�����̈ړ��𖽗߂���
    /// �ړI�n��n���Ďg��
    /// </summary>
    /// <param name="_destination"></param>
    /// <param name="_newTarget"></param>
    static public void MoveCamera(Vector3 _destination, GameObject _newTarget = null)
    {
        ICameraMoveTest obj = Camera.main.GetComponent(typeof(ICameraMoveTest)) as ICameraMoveTest;
        if (obj == null) return;
        obj.CallCameraMove(_destination, _newTarget);
    }

    /// <summary>
    /// �v���C���[��Z���W�e�[�u�����ړ�������
    /// </summary>
    /// <param name="_moveAmount"></param>
    /// <param name="_player"></param>
    static public void MovePlayerZ(int _moveAmount, PlayerController _player)
    {
        IMovePlayer obj = _player.GetComponent(typeof(IMovePlayer)) as IMovePlayer;
        if (obj == null) return;
        obj.MovePlayer(_moveAmount);
    }

    static public void SwitchRunToMG(GameState _gameState, SceneManagerMain _sceneManagerMain)
    {
        ISwitchRunToMG obj = _sceneManagerMain.GetComponent(typeof(ISwitchRunToMG)) as ISwitchRunToMG;
        if (obj == null) return;
        obj.SwitchMiniGame(_gameState);
    }
}