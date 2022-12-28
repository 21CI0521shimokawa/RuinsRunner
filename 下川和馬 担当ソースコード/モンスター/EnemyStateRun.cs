using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateRun : StateBase
{
    StateBase nextState; //このステート(RunState)
    EnemyController enemyController; //Enemyの親クラスを取得
    private Rigidbody enemyRigidBody; //Enemyの剛体取得
    private Transform followTargetTransform; ////攻撃する対象オブジェクトのトランスフォーム取得

    /// <summary>
    /// ステートが変更されて最初に一度だけ呼ばれる関数
    /// </summary>
    public override void StateInitialize()
    {
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemyの親クラス取得
        enemyRigidBody = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>(); //Enemyの剛体取得
        followTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //ターゲット(Player)のトランスフォーム取得
        enemyController._EnemyAnimator.SetTrigger("Run"); //Runアニメーション再生
        nextState = this;
    }

    /// <summary>
    /// ステートが変更されるまで毎フレーム呼ばれる関数
    /// </summary>
    /// <param name="EnemygameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject EnemygameObject)
    {
        return nextState;
    }

    /// <summary>
    /// ステートが終わる時に一度だけ呼ばれる関数
    /// </summary>
    public override void StateFinalize()
    {
        //スタブ
    }
}
