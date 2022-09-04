using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame

{
    [Header("�֐��擾")]
    [SerializeField] EnemyController EnemyController;
    [Header("���ł���I�u�W�F�N�g�֘A")]
    [SerializeField] GameObject RockPrefubs;
    [Header("�~�j�Q�[���ݒ�")]
    [SerializeField] int LeftMaxGenerationPosition;
    [SerializeField] int RightMaxGenerationPosition;
    [SerializeField] float RockGenerationPositionZ;
    [SerializeField] int RockGenerationPositionY;
    [SerializeField] int WhatTimeDoAvoidGame;
    [SerializeField] float IntervalTime;
    [SerializeField] int NumberToGenerate;
    [SerializeField] int NumberToAttack;
    [SerializeField] AudioClip AttackSignsSE;
    public void DoAvoidGame()
    {
        StartCoroutine(AvoidGameMove());
    }
    private IEnumerator AvoidGameMove()
    {
        List<int> generationPositionX = new List<int>();
        for (int i = 0; i < WhatTimeDoAvoidGame; ++i)
        {
            yield return new WaitForSeconds(IntervalTime);
            generationPositionX.Clear();
            PlayAudio.PlaySE(AttackSignsSE);
            int numToAtk = NumberToAttack;

            //��肤��l�iint�^�ɕύX���܂����j�̂��������_���ł��Ԃ�Ȃ��悤�ɑI�сA�w�肳�ꂽ���܂Ő���
            for (int j = LeftMaxGenerationPosition; j <= RightMaxGenerationPosition; j++)
            {
                generationPositionX.Add(j);
            }

            while (generationPositionX.Count > RightMaxGenerationPosition - LeftMaxGenerationPosition - NumberToGenerate + 1)
            {

                int index = Random.Range(0, generationPositionX.Count);

                int posX = generationPositionX[index];
                var gameObj = Instantiate(RockPrefubs, new Vector3(posX, RockGenerationPositionY, RockGenerationPositionZ), Quaternion.Euler(0, 180, 0));
                if (numToAtk > 0)
                {
                    gameObj.GetComponent<RockController>().isAttack = true;
                    --numToAtk;
                }
                generationPositionX.RemoveAt(index);
            }

            //var RockGenerationPositionX = Random.Range(LeftMaxGenerationPosition, RightMaxGenerationPosition + 1);
            //GameObject InstanceObject=Instantiate(RockPrefubs, new Vector3(RockGenerationPositionX, RockGenerationPositionY, RockGenerationPositionZ), Quaternion.Euler(-180, 0, 0));
         //   EnemyController.CreateSignPrefub(EnemyController._AttackSignsPrefubs, InstanceObjectTransform);
        }
        yield break;
    }
}
