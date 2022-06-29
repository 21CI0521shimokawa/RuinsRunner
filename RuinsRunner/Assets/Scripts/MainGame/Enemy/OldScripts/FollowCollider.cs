using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCollider : MonoBehaviour
{
    protected ChaseMoveControll DefenceTarget;

    void Start()
    {
        DefenceTarget = transform.GetComponentInParent<ChaseMoveControll>();
    }

    protected void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            DefenceTarget.OnEnterFollowTarget();
        }
    }

    protected void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            DefenceTarget.OnExitFollowTarget(c.transform);
        }
    }
}