using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQTEState_Success : StateBase
{
    MiniGameQTEManager manager_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("QTEManager");
        manager_ = managerGameObject.GetComponent<MiniGameQTEManager>();

        Debug.Log("�����I");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //�Ō�̃{�^���܂ŏI����Ă��Ȃ��Ȃ�
        if(manager_.buttonNumber < manager_.buttonQuantity - 1)
        {
            //�{�^����i�߂đ��s
            manager_.NextButton();
            return new MiniGameQTEState_Game();
        }

        //�Q�[���N���A
        return new MiniGameQTEState_GameClear();
    }

    public override void StateFinalize()
    {

    }
}
