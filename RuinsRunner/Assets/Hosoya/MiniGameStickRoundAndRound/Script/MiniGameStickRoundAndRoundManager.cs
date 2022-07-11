using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundManager : ObjectSuperClass
{
    //�������s��Ԃ�
    static bool isGameClear_;
    public static bool isGameClear
    {
        //��x�ł��Q�Ƃ��ꂽ��false�ɂ���
        get
        {
            bool rtv = isGameClear_;

            isGameClear_ = false;

            return rtv;
        }

        //�N���A�Ǝ��s���������邱�Ƃ͂Ȃ�
        set
        {
            isFailure_ = false;
            isGameClear_ = value;
        }
    }

    static bool isFailure_;
    public static bool isFailure
    {
        //��x�ł��Q�Ƃ��ꂽ��false�ɂ���
        get
        {
            bool rtv = isFailure_;

            isFailure_ = false;

            return rtv;
        }

        //�N���A�Ǝ��s���������邱�Ƃ͂Ȃ�
        set
        {
            isFailure_ = value;
            isGameClear_ = false;
        }
    }

    //�ł����static�ɂ������Ȃ�
    static float timeLinitMax_;    //��������
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

    //�ł����static�ɂ������Ȃ�
    static float increasePowerPerSecond_;    //��b�Ԃɂǂꂾ���p���[�������邩
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

    //�ł����static�ɂ������Ȃ�
    static float decreasePowerPerSecond_;    //��b�Ԃɂǂꂾ���p���[�����邩
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

    //�ł����static�ɂ������Ȃ�
    static float clearPower_; //�N���A�ɕK�v�ȃp���[
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

    float unscaledTimeCount_;   //���b�o�߂�����
    public float unscaledTimeCount
    {
        get
        {
            return unscaledTimeCount_;
        }
    }

    float power_;   //�p���[
    public float power
    {
        get
        {
            return power_;
        }

        //0�ȏ�1�ȉ�
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
            state_ = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q


        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
