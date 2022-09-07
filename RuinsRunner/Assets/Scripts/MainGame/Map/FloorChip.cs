using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChip : NewMapChip
{
    [SerializeField, Tooltip("回転して生成できるかどうか")] bool canRotate_;
    public bool canRotate
    {
        get
        {
            return canRotate_;
        }
    }

    [SerializeField, Tooltip("橋Prefabかどうか")] bool isBridge_;
    public bool isBridge
    {
        get
        {
            return isBridge_;
        }
    }
}
