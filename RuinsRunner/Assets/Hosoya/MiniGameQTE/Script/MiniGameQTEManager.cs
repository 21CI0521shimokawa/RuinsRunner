using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQTEManager : ObjectSuperClass
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


    MiniGameQTE_ImageRender render_;
    public MiniGameQTE_ImageRender render
    {
        get
        {
            return render_;
        }
    }

    public enum ButtonType
    {
        RED, GREEN, YELLOW, BLUE, UP, DOWN, RIGHT, LEFT
    }
    public const int buttonTypeLength_ = 8;
    List<ButtonType> buttons_;  //このゲーム中に押さなければいけないボタン
    public List<ButtonType> buttons
    {
        get
        {
            return buttons_;
        }
    }

    int buttonNumber_;      //今何番目のボタンか
    public int buttonNumber
    {
        get
        {
            return buttonNumber_;
        }
    }

    public ButtonType nextButton    //次に押さなければいけないボタン
    {
        get
        {
            return buttons_[buttonNumber_];
        }
    }

    //できればstaticにしたくない
    [SerializeField] static int buttonQuantity_;    //押さなければいけないボタンの個数
    public static int buttonQuantity
    {
        get
        {
            return buttonQuantity_;
        }
        set
        {
            if (value > 0) buttonQuantity_ = value;
        }
    }

    //できればstaticにしたくない
    [SerializeField] static float timeLinitMax_;    //制限時間
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

    StateMachine state_;

    float unscaledTimeCount_;   //何秒経過したか
    public float unscaledTimeCount
    {
        get
        {
            return unscaledTimeCount_;
        }
    }

    bool isInitializeProcessing_;   //初期化処理が終了したかどうか
    public bool isInitializeProcessing
    {
        get
        {
            return isInitializeProcessing_;
        }
    }


    [SerializeField] AudioClip successSE_;
    public AudioClip successSE
    {
        get
        {
            return successSE_;
        }
    }
    [SerializeField] AudioClip failureSE_;
    public AudioClip failureSE
    {
        get
        {
            return failureSE_;
        }
    }

    private void Awake()
    {
        render_ = gameObject.GetComponent<MiniGameQTE_ImageRender>();
        buttons_ = new List<ButtonType>();
        buttonNumber_ = 0;
        isInitializeProcessing_ = false;

        isGameClear = false;
        isFailure = false;
        unscaledTimeCount_ = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Set_ButtonsRandom();

        state_ = new StateMachine(new MiniGameQTEState_Game());

        isInitializeProcessing_ = true;
    }

    // Update is called once per frame
    void Update()
    {
        state_.Update(gameObject);

        unscaledTimeCount_ += Time.unscaledDeltaTime;
    }

    void Set_ButtonsRandom()
    {
        for (int i = 0; i < buttonQuantity_; ++i)
        {
            int r = Random.Range(0, buttonTypeLength_);

            buttons_.Add((ButtonType)r);     //ボタンを追加
        }
    }

    public void NextButton()
    {
        ++buttonNumber_;
        render_.ArrowSymbolPotisionChange();
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
            buttons_.Clear();
            buttons_ = null;

            state_ = null;
        }

        // アンマネージリソースの解放処理を記述
        //buttonQuantity_ = 0;
        //timeLinitMax_ = 0;
        buttonNumber_ = 0;
        isInitializeProcessing_ = false;
        unscaledTimeCount_ = 0.0f;

        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
