using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCoinController : MonoBehaviour
{
    float timer_;
    [SerializeField] float deleteTime_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_ += Time.deltaTime;

        if(timer_ >= deleteTime_)
        {
            Destroy(this.gameObject);
        }
    }
}
