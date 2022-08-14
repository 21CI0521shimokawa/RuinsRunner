using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ObjectSuperClass
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TakenItem(other.gameObject);
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    //抽象メソッド=================================================================
    /// <summary>
    /// <para>★★このアイテムが取られた時の処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract protected void TakenItem(GameObject _player);

    /// <summary>
    /// <para>★★マネージリソースの解放処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract protected void ReleaseProcess_ManagedResource();

    /// <summary>
    /// <para>★★アンマネージドリソースの解放処理★★</para>
    /// <para>★★呼び出しちゃだめだよ★★</para>
    /// </summary>
    abstract protected void ReleaseProcess_UnManagedResource();

    /// <summary>
    /// 継承先Dispose() override テンプレート
    /// <summary>
    //継承先では以下のようにoverrideすること
    //マネージリソース、アンマネージドリソースについては↓のURLを参考に、newしたものかどうかで判断する
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // 解放済みなので処理しない
        }

        if (_disposing)
        {
            // マネージリソースの解放処理を記述

            ReleaseProcess_ManagedResource();   //子クラスの解放処理
        }

        // アンマネージリソースの解放処理を記述

        ReleaseProcess_UnManagedResource();     //子クラスの解放処理

        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
