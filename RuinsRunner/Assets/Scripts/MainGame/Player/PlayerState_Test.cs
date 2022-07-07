using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState_Test : StateBase
{
    PlayerController playerController_;

    public override void StateInitialize()
    {
        playerController_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Scene�����Z���[�h�B���݂̃V�[���͎c�����܂܂ŁAScene���ǉ������
        SceneManager.LoadSceneAsync("Scene_MiniGameQTE", LoadSceneMode.Additive);

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


        //�ł����static�͎g�������Ȃ�
        MiniGameQTEManager.buttonQuantity = 10;
        MiniGameQTEManager.timeLinitMax = 5.0f;


        Time.timeScale = 0.01f; //�Q�[�����x��1%�ɕύX
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if(MiniGameQTEManager.isGameClear)
        {
            return new PlayerStateRun();
        }
        if(MiniGameQTEManager.isFailure)
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
