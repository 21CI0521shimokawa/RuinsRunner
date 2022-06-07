using System;
using UnityEngine;
using SceneDefine;
/// <summary>
/// ゲームオブジェクト用メモリリーク対策基底クラス
/// 継承先では必ずREGION NEW IMPLEMETATION -> REGION NEED TO OVERRIDEを参照のもと、Dispose()をオーバーライドすること
/// </summary>
public abstract class ObjectSuperClass 
    : MonoBehaviour
    , IDisposable
{
    #region BEFORE IMPLEMENTATION
    /// <summary>
    /// 以前使用していた実装
    /// </summary>

    ////シーン終了時にまとめてリソースを開放してくれる機能に登録する
    ////引数にはthisを渡す
    //protected abstract void RegistToCompositDisposable(IDisposable _thisGameObject);

    ////継承先で参照したリソースをすべてリリースするために、自明的に参照を解除する
    ////null代入や0代入、""代入などでよい
    //public abstract void Dispose();
    #endregion

    #region NEW IMPLEMENTATION
    /// <summary>
    /// 現在使用している実装
    /// <summary>

    private bool isDisposed_;
    private SceneSuperClass sceneManager_;

    private void Start()
    {
        sceneManager_ = GameObject.FindWithTag("SceneManager").GetComponent<SceneSuperClass>();
        sceneManager_.AddToCompositeDisposable(this);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ObjectSuperClass() => this.Dispose(false);

    protected virtual void Dispose(bool _disposing)
    {
        //解放済みだったら処理しない
        if (this.isDisposed_) return;

        if (_disposing)
        {
            //マネージリソースの解放処理を追加
        }
        //アンマネージリソースの解放処理を追加

        this.isDisposed_ = true;
    }

    #region NEED TO OVERRIDE
    /// <summary>
    /// 継承先Dispose() override テンプレート
    /// <summary>
    //継承先では以下のようにoverrideすること
    //マネージリソース、アンマネージドリソースについては↓のURLを参考に、newしたものかどうかで判断する
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083
    /*
        protected override void Dispose(bool _disposing)
        {
            if (this.isDisposed)
            {
                return; // 解放済みなので処理しない
            }

            if (_disposing)
            {
                // マネージリソースの解放処理を記述
            }

            // アンマネージリソースの解放処理を記述

            // Dispose済みを記録
            this.isDisposed = true;

            // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
            base.Dispose(_disposing);
        }*/
    #endregion
    #endregion
}