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
    private void Awake()
    {
        gameObject.tag = "Pillar";
    }

    //この柱を倒したいときは、この関数をSceneManage経由で呼び出す
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
        Debug.Log("柱コルーチンを終了しました");
        yield break;
    }
}