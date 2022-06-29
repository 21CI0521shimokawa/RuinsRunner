using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Follow,
    Wait
}
public class ChaseMoveControll : MonoBehaviour
{
    public float MoveSpeed;
    protected Rigidbody MonstarRigidBody;
    protected Transform FollowTarget;
    protected MonsterState CurrentMoveMode;

    void Start()
    {
        CurrentMoveMode = MonsterState.Idle;
        MonstarRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DoAutoMovement();
        Debug.Log(CurrentMoveMode);
    }

    protected void DoAutoMovement()
    {
        switch (CurrentMoveMode)
        {
            case MonsterState.Wait:
                break;
            case MonsterState.Follow:
                if (FollowTarget != null)
                {
                    Quaternion move_rotation = Quaternion.LookRotation(FollowTarget.transform.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, move_rotation, 0.1f);
                    MonstarRigidBody.velocity =transform.forward*MoveSpeed;
                }
                break;
        }
    }

    public void OnEnterFollowTarget()
    {
        FollowTarget = null;

        if (CurrentMoveMode == MonsterState.Follow)
        {
            CurrentMoveMode = MonsterState.Idle;
        }
    }

    public void OnExitFollowTarget(Transform Target)
    {
        FollowTarget = Target;

        if (CurrentMoveMode == MonsterState.Idle)
        {
            CurrentMoveMode = MonsterState.Follow;
        }
    }
}