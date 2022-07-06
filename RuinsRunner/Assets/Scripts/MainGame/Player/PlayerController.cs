using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
    : ObjectSuperClass
    , IMovePlayer
{
    StateMachine state_;
    public Animator animator_;

    Rigidbody rigidbody_;

    //Player関係
    [SerializeField] float[] positionZTables; //プレイヤのZ座標のテーブル
    public int tablePositionZ;                //プレイヤのテーブルZ座標

    //現在のZ座標を取得
    public float GetPositionZ()
    {
        return positionZTables[tablePositionZ];
    }

    //ダメージ
    public void Damage()
    {
        if (positionZTables.Length - 1 > tablePositionZ)
        {
            ++tablePositionZ;
        }
    }

    //回復
    public void Recovery()
    {
        if (0 < tablePositionZ)
        {
            --tablePositionZ;
        }
    }


    //Run関連


    //Defeat関連


    //Fall関係


    // Start is called before the first frame update
    void Start()
    {
        MoveLooksLikeRunning.Set_isRunning(true);   //移動開始

        state_ = new StateMachine(new PlayerStateRun());

        rigidbody_ = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        state_.Update(gameObject);

        //わざと重くする装置 低スペックシミュレーション用
        //{
        //    for (int i = 0; i < 1000000; ++i)
        //    {
        //        string d = new string("test");
        //        d = null;
        //    }
        //}

        Debug.Log(state_.StateName);
    }


    //捕まったかどうか
    public bool IsBeCaught()
    {
        return gameObject.transform.position.z <= positionZTables[positionZTables.Length - 1];
    }

    //地面に立ってるかどうか
    public bool OnGround()
    {
        //細かい判定を甘くするために条件を >=0 からゆるくしました。 -工藤 7/3
        //下方向に移動していなければ立ってることにする
        if(rigidbody_.velocity.y >= -0.01f)
        {
            return true;
        }


        Vector3 origin = gameObject.transform.position; // 原点
        origin += new Vector3(0, 0.05f, 0);
        Vector3 direction = new Vector3(0, -1, 0); // Y軸方向を表すベクトル
        Ray ray = new Ray(origin, direction); // Rayを生成;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // 赤色で可視化

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            //トリガーだったら除外する
            if (hit.collider.isTrigger == true) { return false; }

            //一部のTagがついてるゲームオブジェクトは除外する
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { return false; }
            //必要に応じて追加


            return true;
        }

        return false;
    }

    //Playerが移動できるかどうか
    //移動できるか確認
    public bool PlayerMoveChack(Vector3 _checkRayVector)
    {
        //移動量が0なら処理しない(する必要がない)
        if(_checkRayVector.magnitude == 0)
        {
            return false;
        }

        Vector3 origin = this.transform.position; // 原点
        Vector3 direction = _checkRayVector.normalized; // ベクトル

        origin.y += 1;

        //移動する軸に応じてRayの原点を移動
        if (origin.x != 0)
        {
            origin.x += 0.15f * Mathf.Sign(_checkRayVector.x);
            _checkRayVector.x += 0.1f;
        }
        if (origin.z != 0)
        {
            origin.z += 0.15f * Mathf.Sign(_checkRayVector.z);
            _checkRayVector.z += 0.1f;
        }

        float length = _checkRayVector.magnitude;


        Ray ray = new Ray(origin, direction); // Rayを生成;
        Debug.DrawRay(ray.origin, ray.direction * length, Color.green, 0.01f); // 緑色で可視化

        RaycastHit[] hits = Physics.RaycastAll(ray, length);

        foreach (RaycastHit hit in hits) // もしRayを投射して何らかのコライダーに衝突したら
        {
            //トリガーだったら除外する
            if(hit.collider.isTrigger == true) { continue; }

            //一部のTagがついてるゲームオブジェクトは除外する
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { continue; }
            //必要に応じて追加

            return false;
        }
        return true;
    }


    //トラップを踏んだかどうか
    public bool OnTrap()
    {
        Vector3 origin = gameObject.transform.position; // 原点
        origin += new Vector3(0, 0, 0.25f);

        Vector3 direction = new Vector3(0, 0, 1); // Z軸方向を表すベクトル
        Ray ray = new Ray(origin, direction); // Rayを生成;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // 赤色で可視化

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // もしRayを投射して何らかのコライダーに衝突したら
        {
            //string name = hit.collider.gameObject.name; // 衝突した相手オブジェクトの名前を取得
            //Debug.Log(name); // コンソールに表示

            //トラップだったら
            if (hit.collider.gameObject.tag == "Trap")
            {

                return true;
            }
        }
        return false;
    }

    //穴に落下したかどうか
    public bool FallIntoHole()
    {
        return gameObject.transform.position.y <= -5.0f;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = gameObject.transform.position; // 原点

        float radius = 2.0f;
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawSphere(origin, radius);
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
            state_ = null;
            animator_ = null;
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

    public void MovePlayer(int _moveAmount)
    {
        tablePositionZ += _moveAmount;
        tablePositionZ = Mathf.Clamp(tablePositionZ, 0, positionZTables.Length - 1);
        Debug.Log(_moveAmount + " 移動しました");
    }
}
