using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// 攻撃する側が攻撃対象のgameObjectを参照渡しして使う
    /// </summary>
    static public void CauseDamage(ref GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.Damaged();
    }

    static public void MoveCamera(Vector3 _destination, GameObject _newTarget = null)
    {
        ICameraMoveTest obj = Camera.main.GetComponent(typeof(ICameraMoveTest)) as ICameraMoveTest;
        if (obj == null) return;
        obj.CallCameraMove(_destination, _newTarget);
    }
}