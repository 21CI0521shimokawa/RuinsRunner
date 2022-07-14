using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChip : NewMapChip
{
    [SerializeField, Tooltip("‰ñ“]‚µ‚Ä¶¬‚Å‚«‚é‚©‚Ç‚¤‚©")] bool canRotate_;
    public bool canRotate
    {
        get
        {
            return canRotate_;
        }
    }
}
