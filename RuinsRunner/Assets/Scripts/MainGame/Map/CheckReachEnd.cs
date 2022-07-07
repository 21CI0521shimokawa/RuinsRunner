using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;
public class CheckReachEnd : MonoBehaviour
{
    SceneManagerMain sceneManager_;
    GameObject pillar_;

    private void Start()
    {
        sceneManager_ = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerMain>();
        pillar_ = GameObject.FindGameObjectWithTag("PillarForEvent");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StaticInterfaceManager.ToFallOverPillar(ref pillar_);
            StaticInterfaceManager.SwitchRunToMG(GameState.MiniGame1, sceneManager_);
        }
    }
}