using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundState_Game : StateBase
{
    MiniGameStickRoundAndRoundManager manager_;

    //点
    Vector2[] rotationPositions = new Vector2[] { 
        new Vector2(0.0f, 1.0f),//上
        new Vector2(0.7f, 0.7f),//右上
        new Vector2(1.0f, 0.0f),//右
        new Vector2(0.7f, -0.7f),//右下
        new Vector2(0.0f, -1.0f),//下
        new Vector2(-0.7f, -0.7f),//左下
        new Vector2(-1.0f, -0.0f),//左
        new Vector2(-0.7f, 0.7f),//左上
    };

    //1フレーム前の一番近かった点の番号
    int beforePositionNumber;

    public override void StateInitialize()
    {
        GameObject managerGameObject = GameObject.FindGameObjectWithTag("RoundAndRoundManager");
        manager_ = managerGameObject.GetComponent<MiniGameStickRoundAndRoundManager>();
        beforePositionNumber = 0;
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        Action();
        Debug.Log(manager_.power);

        if (IsTimeUp())
        {
            if(manager_.power >= MiniGameStickRoundAndRoundManager.clearPower)
            {
                nextState = new MiniGameStickRoundAndRoundState_GameClear(); 
            }
            else
            {
                nextState = new MiniGameStickRoundAndRoundState_Failure();
            }
        }

        return nextState;
    }
    public override void StateFinalize()
    {

    }

    //制限時間が来たら
    bool IsTimeUp()
    {
        return manager_.unscaledTimeCount >= MiniGameStickRoundAndRoundManager.timeLinitMax;
    }

    void Action()
    {
        //ゲージ減少
        manager_.power -= MiniGameStickRoundAndRoundManager.decreasePowerPerSecond * Time.unscaledDeltaTime;


        //スティックの値を取得
        Vector2 stickLValue = ControllerManager.GetGamepadStickL();

        //ちょっと余裕を持たせる
        stickLValue.x = Mathf.Lerp(-1.0f, 1.0f, Mathf.InverseLerp(-0.8f, 0.8f, stickLValue.x));
        stickLValue.y = Mathf.Lerp(-1.0f, 1.0f, Mathf.InverseLerp(-0.8f, 0.8f, stickLValue.y));

        //一番近い点の番号を取得
        int closestPoint = GetClosestPointNumber(stickLValue);

        //中心からある程度離れているか
        if ((stickLValue - Vector2.zero).magnitude > 0.85f)
        {
            //1フレーム前と違う点だったら
            if(closestPoint != beforePositionNumber)
            {
                //ゲージ上昇
                manager_.power += MiniGameStickRoundAndRoundManager.increasePowerPerSecond * Time.unscaledDeltaTime;
            }
        }

        beforePositionNumber = closestPoint;
    }

    //一番近い点の番号を取得
    int GetClosestPointNumber(Vector2 _stickValue)
    {
        float[] lengths = new float[rotationPositions.Length];  //点までの距離
        int rtv = 0;    //返す値

        //点までの距離を取得
        for(int i = 0; i < rotationPositions.Length; ++i)
        {
            lengths[i] = (rotationPositions[i] - _stickValue).magnitude;
        }

        //一番近い点を取得
        for(int i = 1; i < rotationPositions.Length; ++i)
        {
            if(lengths[i] > lengths[rtv])
            {
                rtv = i;
            }
        }

        return rtv;
    }
}
