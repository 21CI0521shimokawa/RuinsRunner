using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UniRx.Toolkit;
using UniRx.Triggers;
using DG.Tweening;
using UnityEngine;

public class EnemyStateAttackFromBack : StateBase
{

    public enum AttackFromBackState { IDOL, PREPARATION, ATTACK, BUCK, END }
    public AttackFromBackState State;
    EnemyController EnemyController;
    GameObject Enemy;
    GameObject Camera;
    Transform FollowTarget;
    private float NowEnemyPositon;
    private const int MaxMiniGameCont = 2;
    private int NowMiniGameCount;

    public bool _IsCameraReturn;
    public override void StateInitialize()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnemyController.EnemyAnimator.SetTrigger("Chase");
        NowMiniGameCount = 0;
        NowEnemyPositon = Enemy.transform.position.z;
        State = AttackFromBackState.PREPARATION;//同フレームだともしかしたらバグ発生するかも
        StaticInterfaceManager.DoEnemyAttackMove(Camera);

        MovePreration(Enemy);
        MoveAttack(Enemy);
        BackMove(Enemy);
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        #region 旧
        /*  Observable.Interval(System.TimeSpan.FromSeconds(WaitTimes))
               .Select(_ => IventStart())
               .DistinctUntilChanged()
               .Where(x => x)
               .Take(DoTimes)
               .Subscribe(x =>
               {
                   Debug.Log("a");
               });*/
        /*  var NowEnemyPositon = gameObject.transform.position.z;
          var EnemyNewPositon = FollowTarget.position.z;
          if (IventStart())
          {
              gameObject.transform.DOMoveZ(EnemyNewPositon, 1)
               .OnComplete(() =>
              {
                  gameObject.transform.DOMoveZ(NowEnemyPositon, 0.2f);
                  gameObject.transform.DOKill();
              });
              Debug.Log(gameObject.transform.position);
          }*/
        #endregion
        StateBase NextState = this;
        if (State == AttackFromBackState.END)
        {
            NextState = new EnemyStateRun();
        }
        return NextState;
    }

    /// <summary>
    /// X軸の往復運動関数
    /// </summary>
    /// <param name="gameObject"></param>
    private void MovePreration(GameObject gameObject)
    {//別の方法で実装したい。。。
        gameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.PREPARATION)
            .Subscribe(_ =>
            {
                var PrerationTime = Random.Range(2, 5);
                gameObject.transform.DOPath
                    (
                    new[]
                    {
              new Vector3(3.0f,gameObject.transform.position.y,gameObject.transform.position.z),
              new Vector3(-3.0f,gameObject.transform.position.y,gameObject.transform.position.z),
                    },
                    3f, PathType.Linear
                    ).SetOptions(true)
                     .SetLoops(PrerationTime)
                     .OnComplete(() =>
                     {
                         State = AttackFromBackState.ATTACK;
                     });
            });
    }
    /// <summary>
    /// Y軸に移動する攻撃関数
    /// </summary>
    /// <param name="gameObject"></param>
    private void MoveAttack(GameObject gameObject)
    {//実装方法変更予定
        gameObject.ObserveEveryValueChanged(x => State)
           .Where(x => State == AttackFromBackState.ATTACK)
            .Subscribe(_ =>
            {
                var EnemyNewPositon = FollowTarget.position.z;
                gameObject.transform.DOMoveZ(EnemyNewPositon, 1)
                .OnUpdate(()=>
                {
                    HitProcessingWithPlayer(gameObject);
                })
                 .OnComplete(() =>
                 {
                     State = AttackFromBackState.BUCK;
                 });
            });
    }
    /// <summary>
    /// 攻撃後通常時の位置に戻る
    /// </summary>
    /// <param name="gameObject"></param>
    private void BackMove(GameObject gameObject)
    {
        gameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.BUCK)
            .Subscribe(_ =>
            {
                gameObject.transform.DOMoveZ(NowEnemyPositon, 0.5f)
                .OnComplete(() =>
                {
                    State = AttackFromBackState.PREPARATION;
                    NowMiniGameCount++;
                    if (NowMiniGameCount >= MaxMiniGameCont)
                    {
                        State = AttackFromBackState.END;
                    }
                });
            });
    }
    /// <summary>
    /// Playerとの当たり判定の処理関数
    /// </summary>
    /// <param name="gameObject"></param>
    private void HitProcessingWithPlayer(GameObject gameObject)
    {
        gameObject.OnTriggerEnterAsObservable()
            .Select(collison => collison.tag)
            .Where(tag => tag == "Player")
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100);
            });
    }
    public override void StateFinalize()
    {//スタブ
        StaticInterfaceManager.DoReturnCameraMove(Camera);
    }
}
