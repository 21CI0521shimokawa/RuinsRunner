using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformViewportToWorld : MonoBehaviour
{
    public bool _CameraInvisible;

    private void Start()
    {
        _CameraInvisible = false;
    }
    private void OnBecameInvisible()
    {
        _CameraInvisible = true;
    }
}