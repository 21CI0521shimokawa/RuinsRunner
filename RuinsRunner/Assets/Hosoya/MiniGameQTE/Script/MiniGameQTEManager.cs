using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameQTEManager : ObjectSuperClass
{
    //�������s��Ԃ�
    public static bool isGameClear;
    public static bool isFailure;

    ~MiniGameQTEManager()
    {
        isGameClear = false;
        isFailure = false;
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
    List<ButtonType> buttons_;  //���̃Q�[�����ɉ����Ȃ���΂����Ȃ��{�^��
    public List<ButtonType> buttons
    {
        get
        {
            return buttons_;
        }
    }

    int buttonNumber_;      //�����Ԗڂ̃{�^����
    public int buttonNumber
    {
        get
        {
            return buttonNumber_;
        }
    }

    public ButtonType nextButton    //���ɉ����Ȃ���΂����Ȃ��{�^��
    {
        get
        {
            return buttons_[buttonNumber_];
        }
    }

    [SerializeField] int buttonQuantity_;    //�����Ȃ���΂����Ȃ��{�^���̌�
    public int buttonQuantity
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

    [SerializeField] float timeLinitMax_;    //��������
    public float timeLinitMax
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

    float unscaledTimeCount_;   //���b�o�߂�����
    public float unscaledTimeCount
    {
        get
        {
            return unscaledTimeCount_;
        }
    }

    bool isInitializeProcessing_;   //�������������I���������ǂ���
    public bool isInitializeProcessing
    {
        get
        {
            return isInitializeProcessing_;
        }
    }

    private void Awake()
    {
        render_ = gameObject.GetComponent<MiniGameQTE_ImageRender>();
        buttons_ = new List<ButtonType>();
        buttonNumber_ = 0;
        isInitializeProcessing_ = false;
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

            buttons_.Add((ButtonType)r);     //�{�^����ǉ�
        }
    }

    public void NextButton()
    {
        ++buttonNumber_;
        render_.ArrowSymbolPotisionChange();
    }
 

    //�p����ł͈ȉ��̂悤��override���邱��
    //�}�l�[�W���\�[�X�A�A���}�l�[�W�h���\�[�X�ɂ��Ắ���URL���Q�l�ɁAnew�������̂��ǂ����Ŕ��f����
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // ����ς݂Ȃ̂ŏ������Ȃ�
        }

        if (_disposing)
        {
            // �}�l�[�W���\�[�X�̉���������L�q
            buttons_.Clear();
            buttons_ = null;

            state_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q
        buttonQuantity_ = 0;
        timeLinitMax_ = 0;

        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
