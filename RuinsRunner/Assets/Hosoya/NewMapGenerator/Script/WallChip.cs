using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChip : NewMapChip
{
    [SerializeField, Tooltip("右側に配置できるかどうか")] bool isPlacementRight_;
    public bool isPlacementRight
    {
        get
        {
            return isPlacementRight_;
        }
    }

    [SerializeField, Tooltip("左側に配置できるかどうか")] bool isPlacementLeft_;
    public bool isPlacementLeft
    {
        get
        {
            return isPlacementLeft_;
        }
    }
}
