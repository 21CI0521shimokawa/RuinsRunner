using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChip : NewMapChip
{
    [SerializeField, Tooltip("��]���Đ����ł��邩�ǂ���")] bool canRotate_;
    public bool canRotate
    {
        get
        {
            return canRotate_;
        }
    }
}
