using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    EnemyController enemyController;
    public Rigidbody EnemyRigidBody;
    Transform FollowTarget;
    StateBase NextState;

    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        EnemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyController.EnemyAnimator.SetTrigger("Chase");
        NextState = this;
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        #region 追跡処理
        Chase(gameObject);
        #endregion
        #region 怯むステート移行(仮)
        //if(Input.GetKeyDown(KeyCode.A))
        // {
        //     NextState = new EnemyStateKnockBack();
        // }
        #endregion

        return NextState;
    }

    public void Chase(GameObject gameObject)
    {
        //Quaternion move_rotation = Quaternion.LookRotation(FollowTarget.transform.position - gameObject.transform.position, Vector3.up);
        //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, move_rotation, 0.1f);
        //EnemyRigidBody.velocity = gameObject.transform.forward * MoveSpeed;
        float destinationX = FollowTarget.position.x;
        enemyController.GetComponent<Transform>().position = new Vector3(Mathf.Lerp(enemyController.GetComponent<Transform>().position.x, destinationX, 0.1f), 0, Mathf.Lerp(enemyController.GetComponent<Transform>().position.z, 10, 0.01f));
    }

    //スタブ
    public override void StateFinalize()
    {
    }

    public override void ReceiveDamage()
    {
        NextState = new EnemyStateKnockBack();
        Debug.Log("柱にあたりました");
    }
}
