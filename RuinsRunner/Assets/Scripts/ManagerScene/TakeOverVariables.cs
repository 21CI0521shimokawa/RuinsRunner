using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOverVariables : MonoBehaviour
{
    private void Start()
    {
        score_ = 0;
    }
    //�V�[�����܂����ŕێ��������ϐ����̐錾��ǉ����Ă���
    //���[�U�[��`�̃N���X�Ȃǂ��ǉ�����ꍇ�́A���̂��̂��܂�getset�v���p�e�B�ł����Ă�����ق������S���Ƃ͎v��
    int score_;
    int coinCount_;

    public int Score
    {
        get { return score_; }
        set { score_ = value; }
    }

    public int CoinCount
    {
        get { return coinCount_; }
        set { coinCount_ = value; }
    }
}
