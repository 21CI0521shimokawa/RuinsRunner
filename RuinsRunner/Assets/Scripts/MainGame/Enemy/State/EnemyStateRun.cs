using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    StateBase NextState; //このステート(RunState)
    EnemyController EnemyController; //Enemyの親クラスを取得
    private Rigidbody EnemyRigidBody; //Enemyの剛体取得
    private Transform FollowTargetTransform; ////攻撃する対象オブジェクトのトランスフォーム取得

    /// <summary>
    /// ステートが変更されて最初に一度だけ呼ばれる関数
    /// </summary>
    public override void StateInitialize()
    {
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemyの親クラス取得
        EnemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>(); //Enemyの剛体取得
        FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //ターゲット(Player)のトランスフォーム取得
        EnemyController._EnemyAnimator.SetTrigger("Run"); //Runアニメーション再生
        NextState = this;
    }

    /// <summary>
    /// ステートが変更されるまで毎フレーム呼ばれる関数
    /// </summary>
    /// <param name="EnemygameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject EnemygameObject)
    {
        return NextState;
    }

    /// <summary>
    /// ステートが終わる時に一度だけ呼ばれる関数
    /// </summary>
    public override void StateFinalize()
    {
        //スタブ
    }
}
