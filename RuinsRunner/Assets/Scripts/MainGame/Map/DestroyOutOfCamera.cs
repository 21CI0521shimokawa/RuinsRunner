using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfCamera : MonoBehaviour
{
    private void Awake()
    {
        //TODO:本来ならば時間ではなく位置で判断すべき（テスト実装のためこのような形をとっただけ）
        Invoke("DestroyMe", 15 * 60 * Time.deltaTime);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
