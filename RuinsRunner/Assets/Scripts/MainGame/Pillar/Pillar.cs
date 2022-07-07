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
    bool isFalleing_;
    private void Start()
    {
        if(gameObject.CompareTag("Untagged"))
            gameObject.tag = "Pillar";
        isFalleing_ = false;
    }

    //���̒���|�������Ƃ��́A���̊֐���interfaceManager�o�R�ŌĂяo��
    public void CallToFallOver()
    {
        StartCoroutine("ToFallOver");
    }

    IEnumerator ToFallOver()
    {
        BoxCollider bCollider = gameObject.GetComponent<BoxCollider>();
        if(bCollider != null)
            bCollider.isTrigger = true;
        isFalleing_ = true;
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
        isFalleing_ = false;
        Debug.Log("���R���[�`�����I�����܂���");

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFalleing_) return;
        if (other.gameObject.CompareTag("Enemy"))
        {
            StaticInterfaceManager.CauseDamage(other.gameObject);
            StaticInterfaceManager.MovePlayerZ(-1, FindObjectOfType<PlayerController>());
        }
    }
}