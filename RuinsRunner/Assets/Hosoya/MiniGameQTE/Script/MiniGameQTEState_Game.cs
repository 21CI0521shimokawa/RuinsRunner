using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        isPressedButtons_[0] = Input.GetKeyDown(KeyCode.Q);
        isPressedButtons_[1] = Input.GetKeyDown(KeyCode.W);
        isPressedButtons_[2] = Input.GetKeyDown(KeyCode.E);
        isPressedButtons_[3] = Input.GetKeyDown(KeyCode.R);
        isPressedButtons_[4] = Input.GetKeyDown(KeyCode.UpArrow);
        isPressedButtons_[5] = Input.GetKeyDown(KeyCode.DownArrow);
        isPressedButtons_[6] = Input.GetKeyDown(KeyCode.RightArrow);
        isPressedButtons_[7] = Input.GetKeyDown(KeyCode.LeftArrow);
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
        return manager_.unscaledTimeCount >= manager_.timeLinitMax;
    }
}
