using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQuizState_Game : StateBase
{
    MiniGameQuiz_Maneger Maneger;

    private bool[] IsPressedKeys_;
    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("QuizManeger");
        Maneger = managerGameObject.GetComponent<MiniGameQuiz_Maneger>();
        IsPressedKeys_ = new bool[2];
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        FlagReset();
        KeyPress();
        switch (QuizGame())
        {
            case -1:
                nextState = new MiniGameQuizState_Success();
                break;
            case 1:
                nextState= new MiniGameQuizState_Failure();
                break;
            case 0:     //í èÌ
                break;

            default:    //ÉGÉâÅ[
                break;
        }
        if(IsTimeUp())
        {
            nextState = new MiniGameQTEState_Failure();
        }
        return nextState;
    }

    void FlagReset()
    {
        for (int i = 0; i < IsPressedKeys_.Length; ++i)
        {
            IsPressedKeys_[i] = false;
        }
    }
    void KeyPress()
    {Å@
        if(Input.GetKeyDown(KeyCode.Q))
        IsPressedKeys_[0] = Input.GetKeyDown(KeyCode.Q);
        if(Input.GetKeyDown(KeyCode.W))
        IsPressedKeys_[1] = Input.GetKeyDown(KeyCode.W);
    }

    int QuizGame()
    {
        if (IsPressedKeys_[0])
        {
            return -1;//é∏îs
        }
        if (IsPressedKeys_[1])
        {
            return 1;//ê¨å˜
        }
        return 0;
    }
    public override void StateFinalize()
    {
        throw new System.NotImplementedException();
    }
    //êßå¿éûä‘Ç™óàÇΩÇÁ
    bool IsTimeUp()
    {
        return Maneger.UnscaledTimeCount >= Maneger.TimeLimitMax;
    }
}
