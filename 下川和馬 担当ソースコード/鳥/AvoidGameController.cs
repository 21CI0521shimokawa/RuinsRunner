using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame
{
    [Header("飛んでくるオブジェクト取得")]
    [SerializeField] GameObject birdPrefubs;
    [Header("AvoidGame設定")]
    [SerializeField, Tooltip("生成される左の上限位置を整数値")] int leftMaxGenerationPossition;
    [SerializeField, Tooltip("生成される右の上限位置を整数値")] int rightMaxGenerationPosition;
    [SerializeField, Tooltip("生成されるオブジェクトのZ位置")] float birdGenerationPositionZ;
    [SerializeField, Tooltip("生成されるオブジェクトのY位置")] int birdGenerationPositionY;
    [SerializeField, Tooltip("現在のミニゲーム回数")] int doAvoidGameCount;
    [SerializeField, Tooltip("生成するインターバル時間")] float intervalTime;
    [SerializeField] int numberToGenerate;
    [SerializeField] int numberToAttack;
    [SerializeField] AudioClip attackSignsSE;

    /// <summary>
    /// AvoidGameスタート(インターフェース)
    /// </summary>
    public void DoAvoidGame()
    {
        AvoidGameMove().Forget();
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // 解放済みなので処理しない
            return;
        }
        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(disposing);
    }

    /// <summary>
    /// 鳥を一定感覚で指定範囲内でランダムで一定回数生成させる関数
    /// </summary>
    /// <returns></returns>
    private async UniTask AvoidGameMove()
    {
        List<int> GenerationPositionX = new List<int>();
        //doAvoidGameCountの数だけループさせる
        for (int i = 0; i < doAvoidGameCount; ++i)
        {
            //生成のインターバルtime
            await UniTask.Delay(TimeSpan.FromSeconds(intervalTime));
            //生成するX値をリセットする
            GenerationPositionX.Clear();
            //生成する時にSEを流す
            PlayAudio.PlaySE(attackSignsSE);
            int NumToAttack = numberToAttack;

            //取りうる値のうちランダムでかぶらないように選び、指定された数まで生成
            for (int j = leftMaxGenerationPossition; j <= rightMaxGenerationPosition; j++)
            {
                //GenerationPositionXに要素(生成する位置情報)を追加
                GenerationPositionX.Add(j);
            }

            while (GenerationPositionX.Count > rightMaxGenerationPosition - leftMaxGenerationPossition - numberToGenerate + 1)
            {
                //生成するX値をランダムで取得
                int Index = UnityEngine.Random.Range(0, GenerationPositionX.Count);
                //Indexで取得した値を代入
                int InstantiatePositonX = GenerationPositionX[Index];
                GameObject birdObject = Instantiate
                    (
                    birdPrefubs,
                    new Vector3(InstantiatePositonX, birdGenerationPositionY, birdGenerationPositionZ), Quaternion.Euler(0, 180, 0)
                    );
                if (NumToAttack > 0)
                {
                    //NumToAttackがNumberToAttackの値より大きかったら攻撃対象にする
                    birdObject.GetComponent<BirdController>().IsAttack = true;
                    --NumToAttack;
                }
                //ランダムで取得した生成する位置情報を消す
                GenerationPositionX.RemoveAt(Index);
            }
        }
    }
}
