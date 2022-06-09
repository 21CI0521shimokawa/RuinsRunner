using UnityEngine;
public class MapInformation
{
    bool isConnectFront;
    bool isConnectBack;

    public MapInformation(bool _isConnectFront = false, bool _isConnectBack = true)
    {
        isConnectFront = _isConnectFront;
        isConnectBack = _isConnectBack;
    }

    public bool IsConnectFront() { return isConnectFront; }
    public bool IsConnectBack() { return isConnectBack; }
}