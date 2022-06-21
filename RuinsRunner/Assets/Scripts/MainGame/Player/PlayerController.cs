using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;
    
    InterfaceManager interfaceManager;
    public InterfaceManager GetInterfaceManager()
    {
        return interfaceManager;
    }

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



    // Start is called before the first frame update
    void Start()
    {
        state = new StateMachine(new PlayerStateRun());

        interfaceManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<InterfaceManager>();

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


    private void OnCollisionEnter(Collision collision)
    {
        //GetSceneManagerMain().ToFallOverPillar(ref collision.gameObject);
    }


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
            state = null;
            animator = null;
            interfaceManager = null;
            positionZTables = null;
        }

        // アンマネージリソースの解放処理を記述

        //なにかけばいいんだ とりあえず0を代入してみる
        tablePositionZ = 0;


        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
