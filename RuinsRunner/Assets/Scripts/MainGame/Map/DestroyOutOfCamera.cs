using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfCamera : MonoBehaviour
{
    [SerializeField] int destroyZPos = -15;
    private void Awake()
    {
        //������10�b�ɕύX������(2022/06/21 �גJ)��
        //Invoke(nameof(DestroyMe), /*15 * 60 * Time.deltaTime*/ 10);
    }

    private void Update()
    {
        //��̃I�u�W�F�N�gmapEater�ɂԂ�������������@�������̂ł͂Ɗ�����
        if (transform.position.z <= -10)
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        //���̃I�u�W�F�N�g��MapGenerator�ɂ���Đ������ꂽ���̂��ǂ����m�F
        foreach(GameObject generatedObject in NewMapGenerator.generatedPrefabsList)
        {
            //�����Ɠ������̂�����������
            if(generatedObject == gameObject)
            {
                gameObject.SetActive(false);    //��A�N�e�B�u�ɂ���
                return;
            }
        }

        Destroy(gameObject);
    }
}