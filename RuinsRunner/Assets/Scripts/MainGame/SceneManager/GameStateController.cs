using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : ObjectSuperClass
{
    StateMachine gameState_;

    private void Start()
    {
        gameState_ = new StateMachine(new GameStateRun());
    }

    private void Update()
    {
        gameState_.Update(gameObject);
    }

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // 解放済みなので処理しない
        }

        if (_disposing)
        {
            // マネージリソースの解放処理を記述
            gameState_ = null;
        }

        // アンマネージリソースの解放処理を記述


        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}