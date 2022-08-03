using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGameController 
    : ObjectSuperClass
    , IAvoidGame

{
    [Header("飛んでくるオブジェクト関連")]
    [SerializeField] GameObject RockPrefubs;
    [Header("ミニゲーム設定")]
    [SerializeField] Transform LeftMaxGenerationPosition;
    [SerializeField] Transform RightMaxGenerationPosition;
    [SerializeField] float RockGenerationPositionZ;
    [SerializeField] int RockGenerationPositionY;
    [SerializeField] int WhatTimeDoAvoidGame;
    [SerializeField] float IntervalTime;
    public void DoAvoidGame()
    {
        StartCoroutine(AvoidGameMove());
    }
    private IEnumerator AvoidGameMove()
    {
        for (int i = 0; i < WhatTimeDoAvoidGame; ++i)
        {
            yield return new WaitForSeconds(IntervalTime);
            var RockGenerationPositionX = Random.Range(LeftMaxGenerationPosition.position.x, RightMaxGenerationPosition.position.x);
            Instantiate(RockPrefubs, new Vector3(RockGenerationPositionX, RockGenerationPositionY, RockGenerationPositionZ), Quaternion.identity);
        }
        yield break;
    }
}
