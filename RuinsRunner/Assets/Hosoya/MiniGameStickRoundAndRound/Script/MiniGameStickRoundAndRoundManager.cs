using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundManager : ObjectSuperClass
{
    //成功失敗を返す
    static bool isGameClear_;
    public static bool isGameClear
    {
        //一度でも参照されたらfalseにする
        get
        {
            bool rtv = isGameClear_;

            isGameClear_ = false;

            return rtv;
        }

        //クリアと失敗が共存することはない
        set
        {
            isFailure_ = false;
            isGameClear_ = value;
        }
    }

    static bool isFailure_;
    public static bool isFailure
    {
        //一度でも参照されたらfalseにする
        get
        {
            bool rtv = isFailure_;

            isFailure_ = false;

            return rtv;
        }

        //クリアと失敗が共存することはない
        set
        {
            isFailure_ = value;
            isGameClear_ = false;
        }
    }

    //できればstaticにしたくない
    static float timeLinitMax_;    //制限時間
    public static float timeLinitMax
    {
        get
        {
            return timeLinitMax_;
        }
        set
        {
            if (value > 0.0f) timeLinitMax_ = value;
        }
    }

    //できればstaticにしたくない
    static float increasePowerPerSecond_;    //一秒間にどれだけパワーが増えるか
    public static float increasePowerPerSecond
    {
        get
        {
            return increasePowerPerSecond_;
        }
        set
        {
            if (value > 0.0f) increasePowerPerSecond_ = value;
        }
    }

    //できればstaticにしたくない
    static float decreasePowerPerSecond_;    //一秒間にどれだけパワーが減るか
    public static float decreasePowerPerSecond
    {
        get
        {
            return decreasePowerPerSecond_;
        }
        set
        {
            if (value > 0.0f) decreasePowerPerSecond_ = value;
        }
    }

    //できればstaticにしたくない
    static float clearPower_; //クリアに必要なパワー
    public static float clearPower
    {
        get
        {
            return clearPower_;
        }
        set
        {
            if (value > 0.0f) clearPower_ = value;
        }
    }


    StateMachine state_;

    float unscaledTimeCount_;   //何秒経過したか
    public float unscaledTimeCount
    {
        get
        {
            return unscaledTimeCount_;
        }
    }

    float power_;   //パワー
    public float power
    {
        get
        {
            return power_;
        }

        //0以上1以下
        set
        {
            power_ = value;
            power_ = Mathf.Clamp01(power_);
        }
    }

    private void Awake()
    {
    }

    private void Start()
    {
        power_ = 0.0f;
        state_ = new StateMachine(new MiniGameStickRoundAndRoundState_Game());
    }

    private void Update()
    {
        state_.Update(gameObject);

        unscaledTimeCount_ += Time.unscaledDeltaTime;
    }



    //継承先では以下のようにoverrideすること
    //マネージリソース、アンマネージドリソースについては↓のURLを参考に、newしたものかどうかで判断する
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // 解放済みなので処理しない
        }

        if (_disposing)
        {
            // マネージリソースの解放処理を記述
            state_ = null;
        }

        // アンマネージリソースの解放処理を記述


        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
