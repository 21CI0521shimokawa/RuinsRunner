using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField] GameStateController gController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Palyer"))
        {
            //ゲームオーバーを伝える
        }
    }
}
