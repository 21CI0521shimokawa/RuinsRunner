using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyController : ObjectSuperClass
{
    StateBase EnemyState;
    public Animator EnemyAnimator;

#region KnockBack�֘A
    public bool _IsSqueeze;
    public bool _Issqueeze
    {
        get { return _IsSqueeze; }
        set { _IsSqueeze = value; }
    }
    #endregion

    #region Chase�֘A
    #endregion

    void Start()
    {
        EnemyState = new EnemyStateRun();
    }
    void Update()
    {
        EnemyState = (StateBase)EnemyState.Update(this.gameObject);
    }
}

