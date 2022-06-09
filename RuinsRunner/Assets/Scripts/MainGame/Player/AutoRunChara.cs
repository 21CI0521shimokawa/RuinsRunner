using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunChara : MonoBehaviour/*ObjectSuperClass*/
{
    [SerializeField] float speed;   //ˆÚ“®‘¬“x

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //ˆÚ“®
    void Move()
    {
        this.gameObject.transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Set_speed(float speed)
    {
        this.speed = speed;
    }

    public float Get_speed()
    {
        return speed;
    }
}
