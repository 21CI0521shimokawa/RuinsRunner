using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Toolkit;
using UniRx.Triggers;
using DG.Tweening;
public class EnemyStateAttackFromBack : StateBase
{

    private enum attackFromBackState { IDOL, PREPARATION, ATTACK, BUCK, END }; //ステート設定
    private attackFromBackState state;
    EnemyController enemyController; //Enemyの親クラス取得
    GameObject enemy; //Enemyオブジェクト取得
    GameObject camera; //Cameraオブジェクト取得
    Transform followTargetTransform; //攻撃する対象オブジェクトのトランスフォーム取得
    private float defaultEnemyPositon; //デフォルトのEnemyの位置(実数)
    private int nowMiniGameCount; //現在のミニゲーム回数
    private const int maxMiniGameCont = 2; //ミニゲーム回数設定
    private const float collisionThrottleTime = 5.0f; //当たり判定実施回数
    private const float setDelayTime = 1.5f; //Z軸に突進する時に待つ時間
    private const float movePrerationXSpeed = 3f; //攻撃する前の横軸の移動時間
    private const float moveAttackZSpeed = 1f; //対象オブジェクトに突進する際のスピード
    private const float moveReturnZSpeed = 0.5f; //突進してからデフォルトの位置に戻る際のスピード

    /// <summary>
    /// ステートが変更されて最初に一度だけ呼ばれる関数
    /// </summary>
    public override void StateInitialize()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy"); //Enemyオブジェクト取得
        camera = GameObject.FindGameObjectWithTag("MainCamera"); //メインカメラオブジェクト取得
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemyの親クラス取得
        followTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //ターゲット(Player)のトランスフォーム取得
        enemyController._EnemyAnimator.SetTrigger("Chase");
        nowMiniGameCount = 0;
        defaultEnemyPositon = enemy.transform.position.z; //Enemyのデフォルト位置を取得
        state = attackFromBackState.PREPARATION;
        StaticInterfaceManager.DoEnemyAttackMove(camera); //CameraControllerのDoEnemyFromBackMove呼び出し
        StaticInterfaceManager.StopConcentrationLineEffect(); //Playerが加速している集中線エフェクトを停止
        MovePreration(enemy);//ステートがPREPARATIONだったら呼ばれる関数
        MoveAttack(enemy); //ステートがATTACKだったら呼ばれる関数
        BackMove(enemy); //ステートがBUCKだったら呼ばれる関数
    }

    /// <summary>
    /// ステートが変更されるまで毎フレーム呼ばれる関数
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        //NowMiniGameCountが2になったらこのミニゲーム終了
        if (state == attackFromBackState.END)
        {
            //ステートをRun状態に戻す
            nextState = new EnemyStateRun();
            //マップ生成を通常に戻す
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>().endEnemyAttack = true;
        }
        return nextState;
    }

    /// <summary>
    /// ステートが終わる時に一度だけ呼ばれる関数
    /// </summary>
    public override void StateFinalize()
    {
        //カメラをデフォルトの位置に戻す
        StaticInterfaceManager.DoReturnCameraMove(camera);
        //前方から飛んでくる鳥を避けるゲーム開始
        StaticInterfaceManager.AvoidGameStart();
    }

    /// <summary>
    /// X軸の往復運動関数(フェーズ1)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void MovePreration(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.PREPARATION)
            .Subscribe(_ =>
            {
                //ループする回数設定
                int prerationTime = UnityEngine.Random.Range(2, 3);

                //移動処理
                enemyController._EnemyTweener = enemyGameObject.transform.DOPath
                (
                    new[]
                    {
                        //右に移動
                        new Vector3(3.0f,enemyGameObject.transform.position.y,enemyGameObject.transform.position.z),
                        //左に移動
                        new Vector3(-3.0f,enemyGameObject.transform.position.y,enemyGameObject.transform.position.z),
                    },
                    //３秒間で往復する
                    movePrerationXSpeed,
                    //イージングタイプを指定
                    PathType.Linear
                 //最初の位置に戻りprerationTime分ループさせる
                 ).SetOptions(true).SetLoops(prerationTime);

                //最初の位置(デフォルトのX地点に戻る)
                enemyController._EnemyTweener
                     .OnComplete(() =>
                     {
                         //攻撃予兆生成
                         enemyController.CreateSignPrefub(enemyController._AttackSignsPrefubs, enemyGameObject.transform);
                         //ステートをAttackに変更
                         state = attackFromBackState.ATTACK;
                     });
            });
    }

    /// <summary>
    /// Z軸に移動する攻撃関数(フェーズ2)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void MoveAttack(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.ATTACK)
            .Subscribe(_ =>
            {
                //当たり判定を実施
                HitProcessingWithPlayer(enemyGameObject);
                //Enemyが突進する位置を指定
                Vector3 enemyNewPositon = followTargetTransform.position;
                //Enemyの移動処理をsetDelay分待ってから行う
                enemyController._EnemyTweener = enemyGameObject.transform.DOMoveZ(enemyNewPositon.z, moveAttackZSpeed).SetDelay(setDelayTime);
                enemyController._EnemyTweener
                     .OnStart(() =>
                     {
                         //Attackアニメーション再生
                         enemyController._EnemyAnimator.SetTrigger("Attack");
                         //砂埃のエフェクトを再生s
                         StaticInterfaceManager.PlayEnemyStormEffect();
                         //攻撃SEを再生
                         PlayAudio.PlaySE(enemyController._AttackSE);
                     })
                     .OnComplete(() =>
                     {
                         //エフェクトを停止
                         StaticInterfaceManager.StopEnemyStormEffect();
                         //ステートをBUCKに変更
                         state = attackFromBackState.BUCK;
                     });
            });
    }

    /// <summary>
    /// 攻撃後通常時の位置に戻る(フェーズ3)
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void BackMove(GameObject enemyGameObject)
    {
        enemyGameObject.ObserveEveryValueChanged(x => state)
            .Where(x => state == attackFromBackState.BUCK)
            .Subscribe(_ =>
            {
                 enemyGameObject.transform.DOMoveZ(defaultEnemyPositon, moveReturnZSpeed) //defaultEnemyPositonにMoveReturnZSpeedの速さで戻る
            　.OnComplete(() =>
             {
                 nowMiniGameCount++; //ミニゲームカウントを1増やす
                 state = attackFromBackState.PREPARATION;
                 if (nowMiniGameCount >= maxMiniGameCont) //NowMiniGameCountが2以上だったらステートをENDに変更
                 {
                     state = attackFromBackState.END;
                 }
             });
            });
    }
    /// <summary>
    /// Playerとの当たり判定の処理関数
    /// </summary>
    /// <param name="enemyGameObject"></param>
    private void HitProcessingWithPlayer(GameObject enemyGameObject)
    {
        enemyGameObject.OnTriggerEnterAsObservable()
            //衝突したオブジェクトのタグが
            .Select(collison => collison.tag)
             //"Player"だったら
            .Where(tag => tag == "Player")
            //一度流したらcollisionThrottleTime分スルーする
            .ThrottleFirst(TimeSpan.FromSeconds(collisionThrottleTime))
            .Subscribe(collision =>
            {
                //スコアを－100する
                StaticInterfaceManager.UpdateScore(-100);
                //プレイヤのステートをPlayerStateStumbleに変更
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3);
            });
    }
}
