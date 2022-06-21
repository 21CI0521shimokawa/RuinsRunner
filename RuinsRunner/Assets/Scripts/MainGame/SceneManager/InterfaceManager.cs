using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    /// <summary>
    /// 柱を倒す要請を受け命令する
    /// プレイヤーが接触したgameObjectを参照渡しして使う
    /// </summary>
    /// <param name="_pillar"></param>
    public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// 攻撃要請を受け命令する
    /// 攻撃する側が攻撃対象のgameObjectを参照渡しして使う
    /// </summary>
    public void CauseDamage(ref GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.Damaged();
    }
}