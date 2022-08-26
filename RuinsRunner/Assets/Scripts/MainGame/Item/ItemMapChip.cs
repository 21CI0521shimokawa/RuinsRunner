using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMapChip : NewMapChip
{
    [SerializeField, Tooltip("ƒAƒCƒeƒ€‚ÌŒÂ”")] int itemQuantity_;
    public int itemQuantity
    {
        get
        {
            return itemQuantity_;
        }
    }
}
