using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQuiz_Maneger : MonoBehaviour
{
    //����s������Ԃ�

    public static bool IsQuizOK;
    public static bool IsQuizNG;

    ~MiniGameQuiz_Maneger()//�f�X�g���N�^
    {
        IsQuizOK = false;
        IsQuizNG = false;
    }

    [SerializeField] float TimeLinitMax_;    //��������
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

    float UnscaledTimeCount_;   //���b�o�߂�����
    public float UnscaledTimeCount
    {
        get
        {
            return UnscaledTimeCount_;
        }
    }

    bool IsInitializeProcessing_;   //�������������I���������ǂ���
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
