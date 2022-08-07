using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLooksLikeRunning : MonoBehaviour
{
    static float moveMagnification_ = 1.0f;    //ˆÚ“®‚·‚é‘¬“x”{—¦
    static public float moveMagnification
    { 
        get
        {
            return moveMagnification_;
        }

        set
        {
            if(value >= 0.0f)
            {
                moveMagnification_ = value;
            }
        }
    }

    static bool isRunning_; //ˆÚ“®‚³‚¹‚é‚©‚Ç‚¤‚©
    public static void Set_isRunning(bool _value)
    {
        isRunning_ = _value;
    }

    public float moveSpeed;

    void Start()
    {
        moveSpeed = MainGameConst.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning_)
        {
            transform.position += Vector3.back * (moveSpeed * moveMagnification_) * Time.deltaTime;
        }
    }
}
