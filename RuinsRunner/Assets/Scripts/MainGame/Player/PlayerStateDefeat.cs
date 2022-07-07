using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDefeat : StateBase
{
    PlayerController playerController_;

    bool falloutPillarSucsess_;

    //倒す柱
    GameObject pillar_;
    public GameObject pillar
    {
        set
        {
            pillar_ = value;
        }
    }

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController_.animator_.SetTrigger("StateDefeat");

        falloutPillarSucsess_ = false;
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if(Action())
        {
            nextState = new PlayerStateRun();
        }

        //トラップを踏んだら
        //if (playerController_.OnTrap())
        //{
        //    playerController_.Damage();
        //    nextState = new PlayerStateStumble();
        //}

        return nextState;
    }

    public override void StateFinalize()
    {

    }

    bool Action()
    {
        if(StateTimeCount >= 0.2f && !falloutPillarSucsess_)
        {
            PillarDefeat();
            falloutPillarSucsess_ = true;
        }

        return StateTimeCount >= 1.0f;
    }

    void PillarDefeat()
    {
        StaticInterfaceManager.ToFallOverPillar(ref pillar_);
    }

    #region 使用しなくなった関数
    bool FallOverPillar()
    {
        #region 旧
        ////Pillar（実体）を取得
        //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pillar");

        //Vector3[] rayVecs = new Vector3[] { new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector3(-1, 0, 0) };

        //for (int i = 0; i < rayVecs.Length; ++i)
        //{
        //    Vector3 origin = playerController_.gameObject.transform.position; // 原点
        //    Vector3 direction = rayVecs[i]; // ベクトル
        //    Ray ray = new Ray(origin, direction); // Rayを生成;
        //    Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 1.0f); // 赤色で可視化

        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 0.1f)) // もしRayを投射して何らかのコライダーに衝突したら
        //    {
        //        for (int g = 0; g < gameObjects.Length; ++g)
        //        {
        //            //Rayが当たったgameobjectと取得したgameobjectが一致したら
        //            if (hit.collider.gameObject == gameObjects[g])
        //            {
        //                //実体を引数に渡す
        //                playerController_.GetInterfaceManager().ToFallOverPillar(ref gameObjects[g]);
        //                return true;
        //            }
        //        }
        //    }
        //}

        //return false;
        #endregion

        //Pillar（実体）を取得
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pillar");

        Vector3 origin = playerController_.gameObject.transform.position; // 原点
        float radius = 2.0f;

        //範囲内のcolliderを検知
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);

        foreach (Collider hit in hitColliders)
        {
            for (int g = 0; g < gameObjects.Length; ++g)
            {
                //Rayが当たったgameobjectと取得したgameobjectが一致したら
                if (hit.gameObject == gameObjects[g])
                {
                    //実体を引数に渡す
                    StaticInterfaceManager.ToFallOverPillar(gameObjects[g]);
                    return true;
                }
            }
        }
        return false;
    }
    #endregion
}
