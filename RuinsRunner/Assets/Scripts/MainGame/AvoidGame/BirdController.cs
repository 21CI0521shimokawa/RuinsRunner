using System;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BirdController : ObjectSuperClass
{
    [Header("鳥の設定")]
    [SerializeField, Tooltip("鳥の移動する時間")] float birdSpeed;
    [SerializeField, Tooltip("鳥の移動目的地設定")] float birdDestinationPositonZ;
    [SerializeField, Tooltip("攻撃対象以外の鳥のオブジェクト破棄する時間")] float destroyTime;
    [SerializeField, Tooltip("鳥の待機時間")] float delayTime;
    [SerializeField, Tooltip("イージング設定")] Ease birdEaseType;
    [SerializeField, Tooltip("鳥の当たり判定")] BoxCollider boxCollider;
    private bool isAnimEnd = false; //攻撃対象の鳥以外だったら離れていくアニメーションを再生させるための変数
    private const float collisionThrottleTime = 3.0f; //当たり判定を変数分スルーする
    [Header("アニメーター")]
    [SerializeField, Tooltip("アニメーター取得")] Animator animator;
    [Header("外部連携")]
    public bool IsAttack = false; //AvoidGameControllerで使用中

    /// <summary>I
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        //Playerタグがついているオブジェクトと衝突判定
        HitProcessingWithPlayer();
        //鳥の挙動の呼び出し
        BirdMove();
    }

    /// <summary>
    /// 鳥の動きの関数
    /// </summary>
    private void BirdMove()
    {
        //delayTime分待ってから移動開始しイージングタイプを指定
        var tween = transform.DOMoveZ(birdDestinationPositonZ, birdSpeed).SetDelay(delayTime).SetEase(birdEaseType);

        tween
           .OnStart(() =>
           {
               if (!IsAttack)
               {
                   boxCollider.enabled = false;
                   DestroyAfterSecond(destroyTime).Forget();
               }
           })
           .OnUpdate(() =>
           {
               //攻撃対象以外の鳥だったら離れる
               if (!this.IsAttack)
               {
                   //攻撃対象以外の鳥だったら離れる
                   Leave();
               }
               else
               {
                   //攻撃対象の鳥だったら攻撃開始
                   Attack();
               }
           })
           .OnComplete(() =>
           {
               //移動完了したらオブジェクトを破棄
               Destroy(this.gameObject);
           });
    }

    /// <summary>
    /// Playerとの当たり判定の処理関数
    /// </summary>
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnterAsObservable()
            //衝突したタグが
            .Select(collison => collison.tag)
             //"Player"だったら
            .Where(tag => tag == "Player")
             //一度流したらcollisionThrottleTime分スルーする
            .ThrottleFirst(TimeSpan.FromSeconds(collisionThrottleTime))
            .Subscribe(collision =>
            {
                //スコアを-100する
                StaticInterfaceManager.UpdateScore(-100);
                //プレイヤのステートをPlayerStateStumbleに変更
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3);
                //Playerタグがついているオブジェクトに衝突したら鳥オブジェクトを破棄
                Destroy(gameObject);
            });
    }

    /// <summary>
    /// 鳥の離れる処理
    /// </summary>
    private void Leave()
    {
        if (isAnimEnd) return;
        animator.SetBool("Leave", true);
        isAnimEnd = true;
    }

    /// <summary>
    /// 鳥の攻撃する処理
    /// </summary>
    private void Attack()
    {
        if (this.transform.position.z < 15 && !isAnimEnd)
        {
            animator.SetBool("Attack", true);
            isAnimEnd = true;
        }
    }

    /// <summary>
    /// 鳥をdestroyTimeで破棄
    /// </summary>
    /// <param name="destroyTime">破棄する時間</param>
    /// <returns></returns>
    private async UniTask DestroyAfterSecond(float destroyTime)
    {
        //破棄する時間分待つ
        await UniTask.Delay(TimeSpan.FromSeconds(destroyTime));
        //オブジェクトを破棄
        Destroy(gameObject);
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // 解放済みなので処理しない
            return;
        }
        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(disposing);
    }
}
