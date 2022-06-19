using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDefeat : StateBase
{
    PlayerController playerController;

    public override void StateInitialize()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.animator.SetTrigger("StateDefeat");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if(Action())
        {
            nextState = new PlayerStateRun();
        }

        return nextState;
    }

    public override void StateFinalize()
    {

    }

    bool Action()
    {
        if(StateTimeCount >= 0.2f)
        {
            FallOverPillar();
        }

        return StateTimeCount >= 1.0f;
    }

    void FallOverPillar()
    {
        //Pillar（実体）を取得
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pillar");

        Vector3[] rayVecs = new Vector3[] { new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector3(-1, 0, 0) };

        for (int i = 0; i < rayVecs.Length; ++i)
        {
            Vector3 origin = playerController.gameObject.transform.position; // 原点
            Vector3 direction = rayVecs[i]; // ベクトル
            Ray ray = new Ray(origin, direction); // Rayを生成;
            Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 1.0f); // 赤色で可視化

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0.1f)) // もしRayを投射して何らかのコライダーに衝突したら
            {
                for (int g = 0; g < gameObjects.Length; ++g)
                {
                    //Rayが当たったgameobjectと取得したgameobjectが一致したら
                    if (hit.collider.gameObject == gameObjects[g])
                    {
                        //実体を引数に渡す
                        playerController.GetSceneManagerMain().ToFallOverPillar(ref gameObjects[g]);
                    }
                }
            }
        }

    }
}
