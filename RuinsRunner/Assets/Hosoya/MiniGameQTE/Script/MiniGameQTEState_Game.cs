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
            case 1:     //����
                nextState = new MiniGameQTEState_Success();
                break;

            case -1:    //���s
                nextState = new MiniGameQTEState_Failure();
                break;

            case 0:     //�ʏ�
                break;

            default:    //�G���[
                break;
        }


        if (IsTimeUp())
        {
            //���s
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
        //�L�[�{�[�h
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            #region ��
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

        //�Q�[���p�b�h
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

    //�{�^���m�F
    int ButtonsConfirmation(ref bool[] _buttonFlags)
    {
        //�����Ȃ���΂����Ȃ��{�^���ȊO�������ꂽ��
        if (IsButtonsPressed(ref _buttonFlags, manager_.nextButton))
        {
            return -1;  //���s
        }

        //���̃{�^����������Ă��邩�ǂ���
        if (_buttonFlags[(int)manager_.nextButton])
        {
            return 1;   //����
        }

        return 0;   //�ʏ�
    }


    //�����ɓn���ꂽ�{�^���ȊO�̃{�^����������Ă��邩�ǂ���
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

    //�������Ԃ�������
    bool IsTimeUp()
    {
        return manager_.unscaledTimeCount >= MiniGameQTEManager.timeLinitMax;
    }
}
