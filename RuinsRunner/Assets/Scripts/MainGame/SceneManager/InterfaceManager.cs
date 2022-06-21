using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    /// <summary>
    /// ����|���v�����󂯖��߂���
    /// �v���C���[���ڐG����gameObject���Q�Ɠn�����Ďg��
    /// </summary>
    /// <param name="_pillar"></param>
    public void ToFallOverPillar(ref GameObject _pillar)
    {
        IToFallenOver obj = _pillar.GetComponent(typeof(IToFallenOver)) as IToFallenOver;
        if (obj == null) return;
        obj.CallToFallOver();
    }

    /// <summary>
    /// �U���v�����󂯖��߂���
    /// �U�����鑤���U���Ώۂ�gameObject���Q�Ɠn�����Ďg��
    /// </summary>
    public void CauseDamage(ref GameObject _object)
    {
        IDamaged obj = _object.GetComponent(typeof(IDamaged)) as IDamaged;
        if (obj == null) return;
        obj.Damaged();
    }
}