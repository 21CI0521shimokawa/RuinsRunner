using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfCamera : MonoBehaviour
{
    private void Awake()
    {
        //TODO:�{���Ȃ�Ύ��Ԃł͂Ȃ��ʒu�Ŕ��f���ׂ��i�e�X�g�����̂��߂��̂悤�Ȍ`���Ƃ��������j

        //������10�b�ɕύX������(2022/06/21 �גJ)��
        Invoke("DestroyMe", /*15 * 60 * Time.deltaTime*/ 10);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
