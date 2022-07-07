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