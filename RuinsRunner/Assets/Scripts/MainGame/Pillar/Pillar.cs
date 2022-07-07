using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 柱用
/// 初期化時タグ付けしてくれる
/// 指定関数を呼び出せば倒れる（現状X-方向にのみ倒れる）
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

    //この柱を倒したいときは、この関数をinterfaceManager経由で呼び出す
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
        Debug.Log("柱コルーチンを終了しました");

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