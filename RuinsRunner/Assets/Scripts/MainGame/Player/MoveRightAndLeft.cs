using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightAndLeft : MonoBehaviour/*ObjectSuperClass*/
{
    Vector3 moveVec;   //�v���C���̈ړ�����
    [SerializeField] float speed;   //�ړ����x

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVec.x = speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVec.x = -speed;
        }
        else
        {
            moveVec.x = 0;
        }

         Move();
    }

    //�ړ�
    void Move()
    {
        this.gameObject.transform.position += moveVec * Time.deltaTime;
    }

}
