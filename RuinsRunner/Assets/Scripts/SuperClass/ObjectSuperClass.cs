using System;
using UnityEngine;
using SceneDefine;
/// <summary>
/// ゲームオブジェクト用メモリリーク対策基底クラス
/// アタッチするスクリプトはMonoBehaviorではなくこちらの継承を強く推奨
/// </summary>
public abstract class ObjectSuperClass 
    : MonoBehaviour
    , IDisposable
{
    //シーン終了時にまとめてリソースを開放してくれる機能に登録する
    //引数にはthisを渡す
    protected abstract void RegistToCompositDisposable(IDisposable _thisGameObject);

    //継承先で参照したリソースをすべてリリースするために、自明的に参照を解除する
    //null代入や0代入、""代入などでよい
    public abstract void Dispose();
}