using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class BirdController : ObjectSuperClass
{
    [Header("鳥の設定")]
    [SerializeField,Tooltip("鳥の移動する時間")] float BirdSpeed;
    [SerializeField,Tooltip("鳥の移動目的地設定")] float BirdDestinationPositonZ;
    [SerializeField,Tooltip("攻撃対象以外の鳥のオブジェクト破棄する時間")] float DestroyTime;
    [SerializeField,Tooltip("鳥の待機時間")] float DelayTime;
    [SerializeField,Tooltip("イージング設定")] Ease BirdEaseType;
    [SerializeField,Tooltip("鳥の当たり判定")] BoxCollider boxCollider;
    bool isAnimEnd = false;
    [Header("アニメーター")] 
    [SerializeField,Tooltip("アニメーター取得")] Animator animator;
    [Header("外部連携")]
    public bool isAttack = false; //AvoidGameControllerで使用中

    /// <summary>
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        HitProcessingWithPlayer(); //Playerタグがついているオブジェクトと衝突判定
        BirdMove(); //鳥の挙動の呼び出し
    }

    /// <summary>
    /// 鳥の動きの関数
    /// </summary>
    private void BirdMove()
    {
        this.transform.DOMoveZ(BirdDestinationPositonZ, BirdSpeed) //BirdSpeedの速さで移動完了
            .SetDelay(DelayTime)
            .OnStart(() =>
            {
                if (!isAttack)
                {
                    boxCollider.enabled = false;
                    StartCoroutine(DestroyAfterSecond(DestroyTime));
                }
            })
            .OnUpdate(() =>
            {
                if (!this.isAttack) //攻撃対象以外の鳥だったら離れる
                {
                    Leave(); //攻撃対象以外の鳥だったら離れる
                }
                else
                {
                    Attack(); //攻撃対象の鳥だったら攻撃開始
                }
            })
            .OnComplete(() =>
            {
                Destroy(this.gameObject); //移動完了したらオブジェクトを破棄
            })
            .SetEase(BirdEaseType); //イージングタイプ設定
    }

    /// <summary>
    /// Playerとの当たり判定の処理関数
    /// </summary>
    private void HitProcessingWithPlayer()
    {
        this.OnTriggerEnterAsObservable()
            .Select(collison => collison.tag) //衝突したオブジェクトのタグが
            .Where(tag => tag == "Player") //"Player"だったら
            .Subscribe(collision =>
            {
                StaticInterfaceManager.UpdateScore(-100); //スコアを-100する

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Stumble(3); //プレイヤのステートをPlayerStateStumbleに変更

                Destroy(gameObject); //Playerタグがついているオブジェクトに衝突したら鳥オブジェクトを破棄
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
        if(this.transform.position.z < 15 && !isAnimEnd)
        {
            animator.SetBool("Attack", true);
            isAnimEnd=true;
        }
    }

    /// <summary>
    /// 鳥をdestroyTimeで破棄
    /// </summary>
    /// <param name="destroyTime">破棄する時間</param>
    /// <returns></returns>
    IEnumerator DestroyAfterSecond(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject); //オブジェクトを破棄
        yield return null;
    }
}
