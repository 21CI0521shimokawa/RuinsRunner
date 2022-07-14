using UnityEngine;

public class NewMapChip : MonoBehaviour
{
    [SerializeField, Tooltip("マップチップの大きさ(Z)")] int sizeZ_;
    public int sizeZ
    {
        get
        {
            return sizeZ_;
        }
    }


}
