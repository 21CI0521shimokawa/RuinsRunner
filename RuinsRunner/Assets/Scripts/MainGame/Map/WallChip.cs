using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChip : NewMapChip
{
    [SerializeField, Tooltip("�E���ɔz�u�ł��邩�ǂ���")] bool isPlacementRight_;
    public bool isPlacementRight
    {
        get
        {
            return isPlacementRight_;
        }
    }

    [SerializeField, Tooltip("�����ɔz�u�ł��邩�ǂ���")] bool isPlacementLeft_;
    public bool isPlacementLeft
    {
        get
        {
            return isPlacementLeft_;
        }
    }
}
