using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame

{
    [Header("関数取得")]
    [SerializeField] EnemyController EnemyController;
    [Header("飛んでくるオブジェクト関連")]
    [SerializeField] GameObject RockPrefubs;
    [SerializeField] Transform InstanceObjectTransform;
    [Header("ミニゲーム設定")]
    [SerializeField] Transform LeftMaxGenerationPosition;
    [SerializeField] Transform RightMaxGenerationPosition;
    [SerializeField] float RockGenerationPositionZ;
    [SerializeField] int RockGenerationPositionY;
    [SerializeField] int WhatTimeDoAvoidGame;
    [SerializeField] float IntervalTime;
    [SerializeField] AudioClip AttackSignsSE;
    public void DoAvoidGame()
    {
        StartCoroutine(AvoidGameMove());
    }
    private IEnumerator AvoidGameMove()
    {
        for (int i = 0; i < WhatTimeDoAvoidGame; ++i)
        {
            yield return new WaitForSeconds(IntervalTime);
            PlayAudio.PlaySE(AttackSignsSE);
            var RockGenerationPositionX = Random.Range(LeftMaxGenerationPosition.position.x, RightMaxGenerationPosition.position.x);
            GameObject InstanceObject=Instantiate(RockPrefubs, new Vector3(RockGenerationPositionX, RockGenerationPositionY, RockGenerationPositionZ), Quaternion.Euler(-180, 0, 0));
            EnemyController.CreateSignPrefub(EnemyController._AttackSignsPrefubs, InstanceObjectTransform);
        }
        yield break;
    }
}
