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



    public void MoveCamera(Vector3 _destination, GameObject _newTarget = null)
    {
        ICameraMoveTest obj = Camera.main.GetComponent(typeof(ICameraMoveTest)) as ICameraMoveTest;
        if (obj == null) return;
        obj.CallCameraMove(_destination, _newTarget);
    }
}