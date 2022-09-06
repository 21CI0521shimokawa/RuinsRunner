using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClock : MonoBehaviour
{
    [SerializeField] float timer_;

    // Start is called before the first frame update
    void Start()
    {
        timer_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer_ += Time.deltaTime;
    }
}
