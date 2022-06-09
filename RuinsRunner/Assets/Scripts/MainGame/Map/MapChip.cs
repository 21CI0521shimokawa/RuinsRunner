using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChip : MonoBehaviour
{
    [SerializeField] bool isConnectFront;
    [SerializeField] bool isConnectBack;

    MapInformation mapInformation_;
    public bool IsConnectFront() 
    {
        if(mapInformation_ == null) mapInformation_ = new MapInformation(isConnectFront, isConnectBack);
        return mapInformation_.IsConnectFront(); 
    }
    public bool IsConnectBack()
    {
        if (mapInformation_ == null) mapInformation_ = new MapInformation(isConnectFront, isConnectBack);
        return mapInformation_.IsConnectBack(); 
    }

    public MapInformation GetMapInformation() { return mapInformation_; }
}
