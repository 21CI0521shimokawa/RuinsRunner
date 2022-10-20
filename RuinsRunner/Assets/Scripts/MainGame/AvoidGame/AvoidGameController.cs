using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame
{
    [Header("外部関数取得")]
    [SerializeField,Tooltip("Enemy情報取得")] EnemyController EnemyController;
    [Header("飛んでくるオブジェクト取得")]
    [SerializeField] GameObject BirdPrefubs;
    [Header("AvoidGame設定")]
    [SerializeField,Tooltip("生成される左の上限位置を整数値")] int LeftMaxGenerationPosition;
    [SerializeField,Tooltip("生成される右の上限位置を整数値")] int RightMaxGenerationPosition;
    [SerializeField,Tooltip("生成されるオブジェクトのZ位置")] float BirdGenerationPositionZ;
    [SerializeField,Tooltip("生成されるオブジェクトのY位置")] int BirdGenerationPositionY;
    [SerializeField,Tooltip("現在のミニゲーム回数")] int NowDoAvoidGameCount;
    [SerializeField,Tooltip("生成するインターバル時間")] float IntervalTime;
    [SerializeField] int NumberToGenerate;
    [SerializeField] int NumberToAttack;
    [SerializeField] AudioClip AttackSignsSE;

    /// <summary>
    /// AvoidGameスタート(インターフェース)
    /// </summary>
    public void DoAvoidGame()
    {
        StartCoroutine(AvoidGameMove());//AvoidGameMoveコルーチンスタート
    }

    /// <summary>
    /// リソースを解放
    /// </summary>
    /// <param name="_disposing">リソースを解放したかの判定</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// 解放済みなので処理しない
            return;
        }
        this.isDisposed_ = true; // Dispose済みを記録
        base.Dispose(_disposing); // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
    }

    /// <summary>
    /// 鳥を一定感覚で指定範囲内でランダムで一定回数生成させる関数
    /// </summary>
    /// <returns></returns>
    private IEnumerator AvoidGameMove()
    {
        List<int> GenerationPositionX = new List<int>();
        for (int i = 0; i < NowDoAvoidGameCount; ++i) //NowDoAvoidGameCountの数だけループさせる
        {
            yield return new WaitForSeconds(IntervalTime); //生成のインターバル
            GenerationPositionX.Clear(); //生成するX値をリセットする
            PlayAudio.PlaySE(AttackSignsSE); //生成する時にSEを流す
            int NumToAttack = NumberToAttack;

            for (int j = LeftMaxGenerationPosition; j <= RightMaxGenerationPosition; j++)//取りうる値のうちランダムでかぶらないように選び、指定された数まで生成
            {
                GenerationPositionX.Add(j); //GenerationPositionXに要素(生成する位置情報)を追加
            }

            while (GenerationPositionX.Count > RightMaxGenerationPosition - LeftMaxGenerationPosition - NumberToGenerate + 1)
            {
                int Index = Random.Range(0, GenerationPositionX.Count); //生成するX値をランダムで取得

                int InstantiatePositonX = GenerationPositionX[Index]; //Indexで取得した値を代入
                GameObject BirdObject = Instantiate(BirdPrefubs, new Vector3(InstantiatePositonX, BirdGenerationPositionY, BirdGenerationPositionZ), Quaternion.Euler(0, 180, 0));
                if (NumToAttack > 0)
                {
                    BirdObject.GetComponent<BirdController>().isAttack = true; //NumToAttackがNumberToAttackの値より大きかったら攻撃対象にする
                    --NumToAttack;
                }
                GenerationPositionX.RemoveAt(Index); //ランダムで取得した生成する位置情報を消す
            }
        }
        yield break;
    }
}
