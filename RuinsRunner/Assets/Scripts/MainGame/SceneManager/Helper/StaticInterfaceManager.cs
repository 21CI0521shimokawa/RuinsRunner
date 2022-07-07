using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public static class StaticInterfaceManager
{
    /// <summary>
    /// 柱を倒す要請を受け命令する
    /// プレイヤーが接触したgameObjectを参照渡しして使う
    /// </summary>
    /// <param name="_pillar"></param>
    static public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// 攻撃要請を受け命令する
    /// 攻撃する側が攻撃対象のgameObjectを渡して使う
    /// </summary>
    static public void CauseDamage(GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.CallReceiveDamage();
    }

    /// <summary>
    /// カメラの移動を命令する
    /// 目的地を渡して使う
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
    /// プレイヤーのZ座標テーブルを移動させる
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