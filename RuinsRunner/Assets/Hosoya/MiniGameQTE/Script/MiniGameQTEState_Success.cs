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

        Debug.Log("成功！");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        //最後のボタンまで終わっていないなら
        if(manager_.buttonNumber < manager_.buttonQuantity - 1)
        {
            //ボタンを進めて続行
            manager_.NextButton();
            return new MiniGameQTEState_Game();
        }

        //ゲームクリア
        return new MiniGameQTEState_GameClear();
    }

    public override void StateFinalize()
    {

    }
}
