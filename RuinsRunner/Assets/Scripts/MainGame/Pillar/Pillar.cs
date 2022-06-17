using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���p
/// ���������^�O�t�����Ă����
/// �w��֐����Ăяo���Γ|���i����X-�����ɂ̂ݓ|���j
/// </summary>
public class Pillar 
    : MonoBehaviour
    , IToFallenOver
{
    private void Awake()
    {
        gameObject.tag = "Pillar";
    }

    //���̒���|�������Ƃ��́A���̊֐���SceneManage�o�R�ŌĂяo��
    public void CallToFallOver()
    {
        StartCoroutine("ToFallOver");
    }

    IEnumerator ToFallOver()
    {
        float accel = 0f;
        while(transform.localEulerAngles.z < 90)
        {
            transform.Rotate(0f, 0f, 1f + accel, Space.Self);
            accel += 0.1f;
            yield return new WaitForSeconds(0.01f);
            if(transform.localEulerAngles.z > 90)
            {
                transform.Rotate(0f, 0f, -(transform.localEulerAngles.z - 90));
            }
        }
        Debug.Log("���R���[�`�����I�����܂���");
        yield break;
    }
}