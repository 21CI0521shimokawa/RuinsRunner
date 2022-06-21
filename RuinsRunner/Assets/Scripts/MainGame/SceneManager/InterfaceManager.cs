using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    /// <summary>
    /// ’Œ‚ğ“|‚·—v¿‚ğó‚¯–½—ß‚·‚é
    /// ƒvƒŒƒCƒ„[‚ªÚG‚µ‚½gameObject‚ğQÆ“n‚µ‚µ‚Äg‚¤
    /// </summary>
    /// <param name="_pillar"></param>
    public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// UŒ‚—v¿‚ğó‚¯–½—ß‚·‚é
    /// UŒ‚‚·‚é‘¤‚ªUŒ‚‘ÎÛ‚ÌgameObject‚ğQÆ“n‚µ‚µ‚Äg‚¤
    /// </summary>
    public void CauseDamage(ref GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.Damaged();
    }

    public void MoveCamera(Vector3 _destination, GameObject _newTarget = null)
    {
        ICameraMoveTest obj = Camera.main.GetComponent(typeof(ICameraMoveTest)) as ICameraMoveTest;
        if (obj == null) return;
        obj.CallCameraMove(_destination, _newTarget);
    }
}