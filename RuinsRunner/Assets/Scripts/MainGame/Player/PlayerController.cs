using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;

    //Run関連

    //Defeat関連
    bool defert_Attack;
    public bool Defert_Attack
    {
        get { return defert_Attack; }
        set { defert_Attack = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new StateMachine(new PlayerStateRun());
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(gameObject);
    }


    //isDisposedがエラーになる

    //protected override void Dispose(bool _disposing)
    //{
    //    if (this.isDisposed)
    //    {
    //        return; // 解放済みなので処理しない
    //    }

    //    if (_disposing)
    //    {
    //        // マネージリソースの解放処理を記述
    //    }

    //    // アンマネージリソースの解放処理を記述

    //    // Dispose済みを記録
    //    this.isDisposed = true;

    //    // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
    //    base.Dispose(_disposing);
    //}
}
