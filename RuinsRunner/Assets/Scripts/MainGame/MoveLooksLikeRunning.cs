using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLooksLikeRunning : MonoBehaviour
{
    float moveSpeed;
    void Start()
    {
        moveSpeed = MainGameConst.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }
}
