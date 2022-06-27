using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLooksLikeRunning : MonoBehaviour
{
    static bool isRunning_; //ˆÚ“®‚³‚¹‚é‚©‚Ç‚¤‚©
    public static void Set_isRunning(bool _value)
    {
        isRunning_ = _value;
    }

    float moveSpeed;

    void Start()
    {
        moveSpeed = MainGameConst.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning_)
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
    }
}
