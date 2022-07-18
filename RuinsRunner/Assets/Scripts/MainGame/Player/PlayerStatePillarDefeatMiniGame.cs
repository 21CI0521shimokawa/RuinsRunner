using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatePillarDefeatMiniGame : StateBase
{
    PlayerController playerController_;

    //�|����
    GameObject pillar_;
    public GameObject pillar
    {
        set
        {
            pillar_ = value;
        }
    }

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Scene�����Z���[�h�B���݂̃V�[���͎c�����܂܂ŁAScene���ǉ������
        SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

        //�ł����static�͎g�������Ȃ�
        MiniGameQTEManager.buttonQuantity = 1;
        MiniGameQTEManager.timeLinitMax = 5.0f;

        Time.timeScale = 0.01f; //�Q�[�����x��1%�ɕύX
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (MiniGameQTEManager.isGameClear)
        {
            PlayerStateDefeat state = new PlayerStateDefeat();
            state.pillar = pillar_;
            return state;
        }
        if (MiniGameQTEManager.isFailure)
        {
            playerController_.Damage();
            return new PlayerStateStumble();
        }

        return this;
    }

    public override void StateFinalize()
    {
        SceneManager.UnloadSceneAsync("Scene_MiniGameQTE");
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1.0f; //�Q�[�����x��100%�ɕύX
    }
}
