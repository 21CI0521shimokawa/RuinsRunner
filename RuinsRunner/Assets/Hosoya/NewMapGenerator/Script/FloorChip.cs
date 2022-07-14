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
}
