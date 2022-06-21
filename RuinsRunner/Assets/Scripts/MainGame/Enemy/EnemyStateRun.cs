using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    EnemyController enemyController;
    public Rigidbody EnemyRigidBody;
    Transform FollowTarget;
    [SerializeField] float MoveSpeed;

    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        EnemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyController.EnemyAnimator.SetTrigger("Run");
        MoveSpeed = 1.5f;
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;
        #region 追跡処理
        Chase(gameObject);
        #endregion
        #region 怯むステート移行(仮)
       if(Input.GetKeyDown(KeyCode.A))
        {
            NextState = new EnemyStateKnockBack();
        }
        #endregion

        return NextState;
    }

    public void Chase(GameObject gameObject)
    {
        Quaternion move_rotation = Quaternion.LookRotation(FollowTarget.transform.position - gameObject.transform.position, Vector3.up);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, move_rotation, 0.1f);
        EnemyRigidBody.velocity = gameObject.transform.forward * MoveSpeed;
    }

    //スタブ
    public override void StateFinalize()
    {
    }
}
