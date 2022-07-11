using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameQTEState_Game : StateBase
{
    MiniGameQTEManager manager_;

    bool[] isPressedButtons_;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("QTEManager");
        manager_ = managerGameObject.GetComponent<MiniGameQTEManager>();
        isPressedButtons_ = new bool[8];
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        FlagReset();

        ButtonPress();

        switch(ButtonsConfirmation(ref isPressedButtons_))
        {
            case 1:     //成功
                nextState = new MiniGameQTEState_Success();
                break;

            case -1:    //失敗
                nextState = new MiniGameQTEState_Failure();
                break;

            case 0:     //通常
                break;

            default:    //エラー
                break;
        }


        if (IsTimeUp())
        {
            //失敗
            nextState = new MiniGameQTEState_Failure();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        manager_ = null;
        isPressedButtons_ = null;
    }


    void FlagReset()
    {
        for(int i = 0; i < isPressedButtons_.Length; ++i)
        {
            isPressedButtons_[i] = false;
        }
    }

    void ButtonPress()
    {
        //キーボード
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            #region 旧
            isPressedButtons_[0] = keyboard.qKey.wasPressedThisFrame;
            isPressedButtons_[1] = keyboard.wKey.wasPressedThisFrame;
            isPressedButtons_[2] = keyboard.eKey.wasPressedThisFrame;
            isPressedButtons_[3] = keyboard.rKey.wasPressedThisFrame;
            isPressedButtons_[4] = keyboard.upArrowKey.wasPressedThisFrame;
            isPressedButtons_[5] = keyboard.downArrowKey.wasPressedThisFrame;
            isPressedButtons_[6] = keyboard.rightArrowKey.wasPressedThisFrame;
            isPressedButtons_[7] = keyboard.leftArrowKey.wasPressedThisFrame;
            #endregion
        }

        //ゲームパッド
        Gamepad gamepad = Gamepad.current;
        if(gamepad != null)
        {
            isPressedButtons_[0] = gamepad.buttonEast.wasPressedThisFrame;
            isPressedButtons_[1] = gamepad.buttonSouth.wasPressedThisFrame;
            isPressedButtons_[2] = gamepad.buttonNorth.wasPressedThisFrame;
            isPressedButtons_[3] = gamepad.buttonWest.wasPressedThisFrame;
            isPressedButtons_[4] = gamepad.dpad.up.wasPressedThisFrame;
            isPressedButtons_[5] = gamepad.dpad.down.wasPressedThisFrame;
            isPressedButtons_[6] = gamepad.dpad.right.wasPressedThisFrame;
            isPressedButtons_[7] = gamepad.dpad.left.wasPressedThisFrame;
        }
    }

    //ボタン確認
    int ButtonsConfirmation(ref bool[] _buttonFlags)
    {
        //押さなければいけないボタン以外が押されたら
        if (IsButtonsPressed(ref _buttonFlags, manager_.nextButton))
        {
            return -1;  //失敗
        }

        //そのボタンが押されているかどうか
        if (_buttonFlags[(int)manager_.nextButton])
        {
            return 1;   //成功
        }

        return 0;   //通常
    }


    //引数に渡されたボタン以外のボタンが押されているかどうか
    bool IsButtonsPressed(ref bool[] _buttonFlags, MiniGameQTEManager.ButtonType _exclusionButton)
    {
        for (int i = 0; i < MiniGameQTEManager.buttonTypeLength_; ++i)
        {
            if (_buttonFlags[i] && _exclusionButton != (MiniGameQTEManager.ButtonType)i)
            {
                return true;
            }
        }

        return false;
    }

    //制限時間が来たら
    bool IsTimeUp()
    {
        return manager_.unscaledTimeCount >= MiniGameQTEManager.timeLinitMax;
    }
}
