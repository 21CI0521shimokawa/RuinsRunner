using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
//TODO:Enemyの怯み処理のエントリー、プレイヤーの後退処理のエントリーに実装する
public interface IDamaged
{
    void Damaged();
}