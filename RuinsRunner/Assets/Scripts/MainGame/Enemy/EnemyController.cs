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

    #region KnockBack関連
    public bool _IsSqueeze;
    public bool _Issqueeze
    {
        get { return _IsSqueeze; }
        set { _IsSqueeze = value; }
    }
    #endregion
    public bool _IsCatch;
    public bool _Iscatch
    {
        get { return _IsSqueeze; }
        set { _Issqueeze = value; }
    }

    #region Chase関連
    #endregion
    #region EnemyAttack関連
    [SerializeField] GameObject AttackSignsPrefubs;
    public GameObject _AttackSignsPrefubs
    {
        get { return AttackSignsPrefubs; }
    }
    #endregion
    void Start()
    {
        EnemyState = new StateMachine(new EnemyStateRun());
    }
    void Update()
    {
        EnemyState.Update(this.gameObject);
        Debug.Log(EnemyState.StateName);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _IsSqueeze = true;
        }
    }

    public void CallReceiveDamage()
    {
        EnemyState.CallReceiveDamage();
    }
    public void CreateSignPrefub(GameObject SignPrefub, Transform EnemyTransform)
    {
        var InstansPositon = new Vector3(EnemyTransform.position.x, EnemyTransform.position.y+0.1f, EnemyTransform.position.z + 8);
        GameObject InstanceObject=Instantiate(SignPrefub, InstansPositon, EnemyTransform.rotation);
        DOVirtual.DelayedCall(1.0f, () =>
        {
            Destroy(InstanceObject);//消し方をフェードアウトに今後したい..
        });
    }
}
