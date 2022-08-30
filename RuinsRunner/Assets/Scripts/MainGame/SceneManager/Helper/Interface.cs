using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateDefine;

/// <summary>
/// ものを倒す
/// </summary>
public interface IToFallenOver
{
    void CallToFallOver();
}

/// <summary>
/// ダメージを受ける（ペナルティを受ける）
/// </summary>
//TODO:プレイヤーの後退処理のエントリーに実装する
public interface IDamaged
{
    void CallReceiveDamage();
}

/// <summary>
/// カメラ仮テスト
/// </summary>
public interface ICameraMoveTest
{
    void CallCameraMove(Vector3 destination, GameObject newTarget);
}

/// <summary>
/// プレイヤーのZテーブルを更新する
/// </summary>
public interface IMovePlayer
{
    void MovePlayer(int moveAmount);
}

/// <summary>
/// ランゲームからミニゲームに切り替え要請
/// </summary>
public interface ISwitchRunToMG
{
    void SwitchMiniGame(GameState gameState);
}

/// <summary>
/// スコアの加算・減算
/// </summary>
public interface IUpdateScore
{
    void UpdateScore(int addScore);
}
/// <summary>
/// コインカウンターの増加
/// </summary>
public interface IUpdateCoinCount
{
    void UpdateCoinCount();
}

public interface IExitGame
{
    void ExitToResult();
}
/// <summary>
/// EnemyAttackゲームが始まった時のカメラムーブ
/// </summary>
public interface ICameraEnemyFromBackMove
{
    void DoEnemyFromBackMove(GameObject CameraObject);
}
/// <summary>
/// Defaultの位置に戻るカメラムーブ
/// </summary>
public interface IReturnDefaultCameraPositonMove
{
    void DoDefaultCameraPositonMove(GameObject CameraObject);
}
/// <summary>
/// 前方から飛んでくるオブジェクトを避けるゲーム開始
/// </summary>
public interface IAvoidGame
{
    void DoAvoidGame();
}
/// <summary>
/// カメラの位置修正
/// </summary>
public interface IEditingCameraPositon
{
    void DoEditingCameraPositon(GameObject CameraObject);
}
/// <summary>
/// お肉を取った時の集中線Effect
/// </summary>
public interface IGetMeet
{
    void PlayConcentrationLine();
}
/// <summary>
/// 集中線削除
/// </summary>
public interface IStopConcentrationLineEffect
{ 
    void StopConcentrationLine();
}
/// <summary>
/// 突進する時のEffect
/// </summary>
public interface ICreateStormEffect
{
    void PlayStormEffect();
}
/// <summary>
/// 突進する時のEffect停止
/// </summary>
public interface IStopStormEffect
{
    void StopStormEffect();
}