using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateBeCaught : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Scene�����Z���[�h�B���݂̃V�[���͎c�����܂܂ŁAScene���ǉ������
        SceneManager.LoadSceneAsync("Scene_MiniGameStickRoundAndRound", LoadSceneMode.Additive);

        //�ł����static�͎g�������Ȃ�
        MiniGameStickRoundAndRoundManager.timeLinitMax = 5.0f;
        MiniGameStickRoundAndRoundManager.increasePowerPerSecond = 5.0f;
        MiniGameStickRoundAndRoundManager.decreasePowerPerSecond = 0.4f;
        MiniGameStickRoundAndRoundManager.clearPower = 0.75f;

        Time.timeScale = 0.01f; //�Q�[�����x��1%�ɕύX
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (MiniGameStickRoundAndRoundManager.isGameClear)
        {
            for(int i = 0; i < 2; ++i)
            {
                playerController_.Recovery();
            }
            return new PlayerStateRun();
        }
        if (MiniGameStickRoundAndRoundManager.isFailure)
        {
            return new PlayerStateDeath(); //�� �Ƃ肠�����E���Ƃ�
        }

        return this;
    }

    public override void StateFinalize()
    {
        SceneManager.UnloadSceneAsync("Scene_MiniGameStickRoundAndRound");
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //�Q�[�����x��100%�ɕύX
    }
}
