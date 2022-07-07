using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQuizState_Success : StateBase
{
    MiniGameQuiz_Maneger Maneger;

    public override void StateInitialize()
    {
        GameObject ManegerGameObject = GameObject.FindGameObjectWithTag("QuizManeger");
        Maneger = ManegerGameObject.GetComponent<MiniGameQuiz_Maneger>();

        MiniGameQuiz_Maneger.IsQuizOK = true;

        Debug.Log("ê¨å˜");
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;
        return nextState;
    }

    public override void StateFinalize()
    {

    }
}
