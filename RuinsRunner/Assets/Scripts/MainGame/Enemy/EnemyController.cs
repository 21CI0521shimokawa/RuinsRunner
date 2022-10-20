using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController
    : ObjectSuperClass
{
    StateMachine EnemyState;

    [Header("Enemyアニメーター取得")]
    [SerializeField] Animator EnemyAnimator;
    public Animator _EnemyAnimator
    {
        get { return EnemyAnimator; }
    }
    [Header("EnemyAttackステート関連")]
    [SerializeField] GameObject AttackSignsPrefubs;
    public GameObject _AttackSignsPrefubs
    {
        get { return AttackSignsPrefubs; } 
    }
    [SerializeField] AudioClip AttackSE;
    public AudioClip _AttackSE
    {
        get { return AttackSE; }
    }

    /// <summary>
    /// ゲームが始まる時に一度だけ呼ばれる関数
    /// </summary>
    void Start()
    {
        EnemyState = new StateMachine(new EnemyStateRun()); //AttackFromBackが始まるまではステートをRunにする
    }

    /// <summary>
    /// 存在する限り毎フレーム呼び出される関数
    /// </summary>
    void Update()
    {
        EnemyState.Update(this.gameObject); //現在のステートを毎フレーム呼び出す
    }

    /// <summary>
    /// 当たり判定の処理関数
    /// </summary>
    /// <param name="other"> 衝突判定</param>
    protected void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("EnemyAttack")) //EnemyAttackがついているオブジェクトに衝突したらステートをAttackFromBackに変更
        { 
            EnemyState = new StateMachine(new EnemyStateAttackFromBack());
        }
    }
    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="_disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// 解放済みなので処理しない
            return;
        }
        this.isDisposed_ = true; // Dispose済みを記録
        base.Dispose(_disposing); // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
    }

    /// <summary>
    /// 攻撃予兆の生成
    /// </summary>
    /// <param name="SignPrefub">攻撃予兆オブジェクト</param>
    /// <param name="EnemyTransform">Enemyの位置取得</param>
    public void CreateSignPrefub(GameObject SignPrefub, Transform EnemyTransform)
    {
        var InstansPositon = new Vector3(EnemyTransform.position.x, EnemyTransform.position.y + 0.1f, EnemyTransform.position.z + 6); //攻撃予兆オブジェクトを生成する位置設定
        GameObject InstanceObject = Instantiate(SignPrefub, InstansPositon, EnemyTransform.rotation); //攻撃予兆オブジェクト生成
        DOVirtual.DelayedCall(3.0f, () =>
        {
            Destroy(InstanceObject); //オブジェクト破棄
        });
    }
}
