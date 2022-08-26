using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMapChip : NewMapChip
{
    [SerializeField, Tooltip("アイテムの個数")] int itemQuantity_;
    public int itemQuantity
    {
        get
        {
            return itemQuantity_;
        }
    }
}
