using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQuiz_Maneger : MonoBehaviour
{
    //正解不正解を返す

    public static bool IsQuizOK;
    public static bool IsQuizNG;

    ~MiniGameQuiz_Maneger()//デストラクタ
    {
        IsQuizOK = false;
        IsQuizNG = false;
    }

    [SerializeField] float TimeLinitMax_;    //制限時間
    public float TimeLimitMax
    {
        get
        {
            return TimeLinitMax_;
        }
        set
        {
            if (value > 0.0f) TimeLinitMax_ = value;
        }
    }

    float UnscaledTimeCount_;   //何秒経過したか
    public float UnscaledTimeCount
    {
        get
        {
            return UnscaledTimeCount_;
        }
    }

    bool IsInitializeProcessing_;   //初期化処理が終了したかどうか
    public bool IsInitializeProcessing
    {
        get
        {
            return IsInitializeProcessing_;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
