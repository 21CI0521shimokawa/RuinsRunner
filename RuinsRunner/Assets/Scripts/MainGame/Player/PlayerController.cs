using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;

    //Player関係
    [SerializeField] float[] positionZTables; //プレイヤのZ座標のテーブル
    public int tablePositionZ;                //プレイヤのテーブルZ座標

    //現在のZ座標を取得
    public float GetPositionZ()
    {
        return positionZTables[tablePositionZ];
    }

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

        tablePositionZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(gameObject);
    }


    //捕まったかどうか
    public bool IsBeCaught()
    {
        return gameObject.transform.position.z <= tablePositionZ;
    }

    //地面に立ってるかどうか
    public bool OnGround()
    {
        Vector3 origin = gameObject.transform.position; // 原点
        origin += new Vector3(0, 0.05f, 0);
        Vector3 direction = new Vector3(0, -1, 0); // Y軸方向を表すベクトル
        Ray ray = new Ray(origin, direction); // Rayを生成;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // 赤色で可視化

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            string name = hit.collider.gameObject.name; // 衝突した相手オブジェクトの名前を取得
            Debug.Log(name); // コンソールに表示
        }
        return false;
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
