using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silhouette : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Monster;

    private void Update()
    {
        ChangeColorOfGameObject(Player, Color.black);
        ChangeColorOfGameObject(Monster, Color.black);
    }
    /// <summary>
    /// ���͂��ꂽ�I�u�W�F�N�g�y�т��̎q�A�S�Ă̐F��ς���
    /// </summary>
    /// <param name="targetObject">�F��ύX�������I�u�W�F�N�g</param>
    /// <param name="color">�ݒ肵�����F</param>
    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            foreach (Material material in targetRenderer.materials)
            {
                material.color = color;
            }
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }

    }
}
