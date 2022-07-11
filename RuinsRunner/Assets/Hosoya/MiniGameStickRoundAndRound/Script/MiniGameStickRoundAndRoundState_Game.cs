using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameStickRoundAndRoundState_Game : StateBase
{
    MiniGameStickRoundAndRoundManager manager_;

    //�_
    Vector2[] rotationPositions = new Vector2[] { 
        new Vector2(0.0f, 1.0f),//��
        new Vector2(0.7f, 0.7f),//�E��
        new Vector2(1.0f, 0.0f),//�E
        new Vector2(0.7f, -0.7f),//�E��
        new Vector2(0.0f, -1.0f),//��
        new Vector2(-0.7f, -0.7f),//����
        new Vector2(-1.0f, -0.0f),//��
        new Vector2(-0.7f, 0.7f),//����
    };

    //1�t���[���O�̈�ԋ߂������_�̔ԍ�
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

    //�������Ԃ�������
    bool IsTimeUp()
    {
        return manager_.unscaledTimeCount >= MiniGameStickRoundAndRoundManager.timeLinitMax;
    }

    void Action()
    {
        //�Q�[�W����
        manager_.power -= MiniGameStickRoundAndRoundManager.decreasePowerPerSecond * Time.unscaledDeltaTime;


        //�X�e�B�b�N�̒l���擾
        Vector2 stickLValue = ControllerManager.GetGamepadStickL();

        //������Ɨ]�T����������
        stickLValue.x = Mathf.Lerp(-1.0f, 1.0f, Mathf.InverseLerp(-0.8f, 0.8f, stickLValue.x));
        stickLValue.y = Mathf.Lerp(-1.0f, 1.0f, Mathf.InverseLerp(-0.8f, 0.8f, stickLValue.y));

        //��ԋ߂��_�̔ԍ����擾
        int closestPoint = GetClosestPointNumber(stickLValue);

        //���S���炠����x����Ă��邩
        if ((stickLValue - Vector2.zero).magnitude > 0.85f)
        {
            //1�t���[���O�ƈႤ�_��������
            if(closestPoint != beforePositionNumber)
            {
                //�Q�[�W�㏸
                manager_.power += MiniGameStickRoundAndRoundManager.increasePowerPerSecond * Time.unscaledDeltaTime;
            }
        }

        beforePositionNumber = closestPoint;
    }

    //��ԋ߂��_�̔ԍ����擾
    int GetClosestPointNumber(Vector2 _stickValue)
    {
        float[] lengths = new float[rotationPositions.Length];  //�_�܂ł̋���
        int rtv = 0;    //�Ԃ��l

        //�_�܂ł̋������擾
        for(int i = 0; i < rotationPositions.Length; ++i)
        {
            lengths[i] = (rotationPositions[i] - _stickValue).magnitude;
        }

        //��ԋ߂��_���擾
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
