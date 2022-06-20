using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyController : ObjectSuperClass
{
    StateMachine EnemyState;
    public Animator EnemyAnimator;

#region KnockBackŠÖ˜A
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

    #region ChaseŠÖ˜A
    #endregion

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
        if(other.gameObject.tag=="Player")
        {
            _IsSqueeze = true;
        }
    }
}

