using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfCamera : MonoBehaviour
{
    private void Awake()
    {
        //TODO:本来ならば時間ではなく位置で判断すべき（テスト実装のためこのような形をとっただけ）

        //☆仮で10秒に変更したよ(2022/06/21 細谷)☆
        Invoke("DestroyMe", /*15 * 60 * Time.deltaTime*/ 10);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
