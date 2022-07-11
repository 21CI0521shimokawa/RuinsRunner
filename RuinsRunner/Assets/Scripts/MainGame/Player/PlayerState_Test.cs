using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Test : StateBase
{
    PlayerController playerController_;
    int randomGameNumber_;  //�����Q�[���̔ԍ�

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        randomGameNumber_ = Random.Range(0, 2);
        switch(randomGameNumber_)
        {
            case 0:
                //Scene�����Z���[�h�B���݂̃V�[���͎c�����܂܂ŁAScene���ǉ������
                SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

                //�ł����static�͎g�������Ȃ�
                MiniGameQTEManager.buttonQuantity = 3;
                MiniGameQTEManager.timeLinitMax = 50.0f;
                break;

            case 1:
                //Scene�����Z���[�h�B���݂̃V�[���͎c�����܂܂ŁAScene���ǉ������
                SceneManager.LoadSceneAsync("Scene_MiniGameStickRoundAndRound", LoadSceneMode.Additive);

                //�ł����static�͎g�������Ȃ�
                MiniGameStickRoundAndRoundManager.timeLinitMax = 5.0f;
                MiniGameStickRoundAndRoundManager.increasePowerPerSecond = 5.0f;
                MiniGameStickRoundAndRoundManager.decreasePowerPerSecond = 0.2f;
                MiniGameStickRoundAndRoundManager.clearPower = 0.6f;
                break;
        }

        //���t���[�������Ƃ܂����[�h����ĂȂ����ƂɂȂ��Ă�H

        #region ��
        ////���[�h�ς݂̃V�[���ł���΁A���O�ŕʃV�[�����擾�ł���
        //Scene scene = SceneManager.GetSceneByName("Scene_MiniGameQTE");

        ////GetRootGameObjects�ŁA���̃V�[���̃��[�gGameObjects
        ////�܂�A�q�G�����L�[�̍ŏ�ʂ̃I�u�W�F�N�g���擾�ł���
        //foreach (var rootGameObject in scene.GetRootGameObjects())
        //{
        //    MiniGameQTEManager manager = rootGameObject.GetComponent<MiniGameQTEManager>();
        //    if (manager != null)
        //    {
        //        manager.buttonQuantity = 5;
        //        manager.timeLinitMax = 10.0f;
        //        break;
        //    }
        //}
        #endregion

        Time.timeScale = 0.01f; //�Q�[�����x��1%�ɕύX
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        switch (randomGameNumber_)
        {
            case 0:
                if (MiniGameQTEManager.isGameClear)
                {
                    return new PlayerStateRun();
                }
                if (MiniGameQTEManager.isFailure)
                {
                    playerController_.Damage();
                    return new PlayerStateStumble();
                }
                break;

            case 1:
                if (MiniGameStickRoundAndRoundManager.isGameClear)
                {
                    return new PlayerStateRun();
                }
                if (MiniGameStickRoundAndRoundManager.isFailure)
                {
                    playerController_.Damage();
                    return new PlayerStateStumble();
                }
                break;
        }
        return this;
    }
    public override void StateFinalize()
    {
        switch (randomGameNumber_)
        {
            case 0:
                SceneManager.UnloadSceneAsync("Scene_MiniGameQTE");
                break;

            case 1:
                SceneManager.UnloadSceneAsync("Scene_MiniGameStickRoundAndRound");
                break;
        }

        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //�Q�[�����x��100%�ɕύX
    }

}
