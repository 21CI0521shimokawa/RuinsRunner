using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class EnemyController
    : ObjectSuperClass
    , IDamaged
{
    StateMachine EnemyState;
    public Animator EnemyAnimator;

    [Header("EnemyAttack関連")]
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
    [SerializeField] 
    void Start()
    {
        EnemyState = new StateMachine(new EnemyStateRun());
    }
    void Update()
    {
        EnemyState.Update(this.gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyAttack"))
        {
            EnemyState = new StateMachine(new EnemyStateAttackFromBack());
        }
    }

    public void CallReceiveDamage()
    {
        EnemyState.CallReceiveDamage();
    }
    public void CreateSignPrefub(GameObject SignPrefub, Transform EnemyTransform)
    {
        var InstansPositon = new Vector3(EnemyTransform.position.x, EnemyTransform.position.y+0.1f, EnemyTransform.position.z+6);
        GameObject InstanceObject=Instantiate(SignPrefub, InstansPositon, EnemyTransform.rotation);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            Destroy(InstanceObject);//消し方をフェードアウトに今後したい..
        });
    }
}
