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

    private enum AttackFromBackState { IDOL, PREPARATION, ATTACK, BUCK, END }; //ステート設定
    private AttackFromBackState State; 
    EnemyController EnemyController; //Enemyの親クラス取得
    GameObject Enemy; //Enemyオブジェクト取得
    GameObject Camera; //Cameraオブジェクト取得
    Transform FollowTargetTransform; //攻撃する対象オブジェクトのトランスフォーム取得
    private float DefaultEnemyPositon; //デフォルトのEnemyの位置(実数)
    private int NowMiniGameCount; //現在のミニゲーム回数
    private const int MaxMiniGameCont = 2; //ミニゲーム回数設定
    private const int CollisionDetectionImplement = 1; //当たり判定実施回数
    private const float SetDelayTime = 1.5f; //Z軸に突進する時に待つ時間
    private const float MovePrerationXSpeed = 3f; //攻撃する前の横軸の移動時間
    private const float MoveAttackZSpeed = 1f; //対象オブジェクトに突進する際のスピード
    private const float MoveReturnZSpeed = 0.5f; //突進してからデフォルトの位置に戻る際のスピード

    /// <summary>
    /// ステートが変更されて最初に一度だけ呼ばれる関数
    /// </summary>
    public override void StateInitialize()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy"); //Enemyオブジェクト取得
        Camera = GameObject.FindGameObjectWithTag("MainCamera"); //メインカメラオブジェクト取得
        EnemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>(); //Enemyの親クラス取得
        FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //ターゲット(Player)のトランスフォーム取得
        EnemyController._EnemyAnimator.SetTrigger("Chase");
        NowMiniGameCount = 0;
        DefaultEnemyPositon = Enemy.transform.position.z; //Enemyのデフォルト位置を取得
        State = AttackFromBackState.PREPARATION;
        StaticInterfaceManager.DoEnemyAttackMove(Camera); //CameraControllerのDoEnemyFromBackMove呼び出し
        StaticInterfaceManager.StopConcentrationLineEffect(); //Playerが加速している集中線エフェクトを停止
        MovePreration(Enemy);//ステートがPREPARATIONだったら呼ばれる関数
        MoveAttack(Enemy); //ステートがATTACKだったら呼ばれる関数
        BackMove(Enemy); //ステートがBUCKだったら呼ばれる関数
    }

    /// <summary>
    /// ステートが変更されるまで毎フレーム呼ばれる関数
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase NextState = this;
        if (State == AttackFromBackState.END) //NowMiniGameCountが2になったらこのミニゲーム終了
        {
            NextState = new EnemyStateRun(); //ステートをRun状態に戻す
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>().endEnemyAttack = true; //マップ生成を通常に戻す
        }
        return NextState;
    }

    /// <summary>
    /// ステートが終わる時に一度だけ呼ばれる関数
    /// </summary>
    public override void StateFinalize()
    {
        StaticInterfaceManager.DoReturnCameraMove(Camera); //カメラをデフォルトの位置に戻す
        StaticInterfaceManager.AvoidGameStart(); //前方から飛んでくる鳥を避けるゲーム開始
    }
    
    /// <summary>
    /// X軸の往復運動関数(フェーズ1)
    /// </summary>
    /// <param name="EnemygameObject"></param>
    private void MovePreration(GameObject EnemygameObject)
    {
        EnemygameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.PREPARATION)
            .Subscribe(_ =>
            {
                var PrerationTime = Random.Range(2, 3); //ループする回数設定
                EnemygameObject.transform.DOPath //移動する位置設定
                    (
                    new[]
                    {
              new Vector3(3.0f,EnemygameObject.transform.position.y,EnemygameObject.transform.position.z), //右に移動
              new Vector3(-3.0f,EnemygameObject.transform.position.y,EnemygameObject.transform.position.z), //左に移動
                    },
                    MovePrerationXSpeed, PathType.Linear //３秒間で往復する
                    ).SetOptions(true) //最初の位置(デフォルトのX地点に戻る)
                     .SetLoops(PrerationTime) //三回ループ
                     .OnComplete(() =>
                     {
                         EnemyController.CreateSignPrefub(EnemyController._AttackSignsPrefubs, EnemygameObject.transform); //攻撃予兆生成
                         State = AttackFromBackState.ATTACK; //ステートをAttackに変更
                     });
            });
    }

    /// <summary>
    /// Z軸に移動する攻撃関数(フェーズ2)
    /// </summary>
    /// <param name="EnemygameObject"></param>
    private void MoveAttack(GameObject EnemygameObject)
    {//実装方法変更予定
        EnemygameObject.ObserveEveryValueChanged(x => State)
           .Where(x => State == AttackFromBackState.ATTACK)
            .Subscribe(_ =>
            {
                HitProcessingWithPlayer(EnemygameObject); //当たり判定をステートがAttackの時のみ許可
                var EnemyNewPositon = FollowTargetTransform.position; //Enemyが突進する位置を指定
                EnemygameObject.transform.DOMoveZ(EnemyNewPositon.z, MoveAttackZSpeed)
            .SetDelay(SetDelayTime)
            .OnStart(() =>
            {
                EnemyController._EnemyAnimator.SetTrigger("Attack"); //Attackアニメーション再生
                StaticInterfaceManager.PlayEnemyStormEffect(); //砂埃のエフェクトを再生
                PlayAudio.PlaySE(EnemyController._AttackSE); //攻撃SEを再生
            })
             .OnComplete(() =>
             {
                 StaticInterfaceManager.StopEnemyStormEffect(); //エフェクトを停止
                 State = AttackFromBackState.BUCK; //ステートをBUCKに変更
             });
            });
    }

    /// <summary>
    /// 攻撃後通常時の位置に戻る(フェーズ3)
    /// </summary>
    /// <param name="gameObject"></param>
    private void BackMove(GameObject gameObject)
    {
        gameObject.ObserveEveryValueChanged(x => State)
            .Where(x => State == AttackFromBackState.BUCK)
            .Subscribe(_ =>
            {
                gameObject.transform.DOMoveZ(DefaultEnemyPositon, MoveReturnZSpeed) //DefaultEnemyPositonにMoveReturnZSpeedの速さで戻る
                .OnComplete(() =>
                {
                    NowMiniGameCount++; //ミニゲームカウントを1増やす
                    State = AttackFromBackState.PREPARATION;
                    if (NowMiniGameCount >= MaxMiniGameCont) //NowMiniGameCountが2以上だったらステートをENDに変更
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
            .Select(collison => collison.tag) //衝突したオブジェクトのタグが
            .Where(tag => tag == "Player") //"Player"だったら
            .Take(CollisionDetectionImplement) //一回のみ実行
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100); //スコアを－100する
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3); //プレイヤのステートをPlayerStateStumbleに変更
            });
    }
}
