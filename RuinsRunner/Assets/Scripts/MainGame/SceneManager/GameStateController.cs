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
            return; // ����ς݂Ȃ̂ŏ������Ȃ�
        }

        if (_disposing)
        {
            // �}�l�[�W���\�[�X�̉���������L�q
            gameState_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q


        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}