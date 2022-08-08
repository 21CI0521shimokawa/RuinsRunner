using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfCamera : MonoBehaviour
{
    [SerializeField] int destroyZPos = -15;
    private void Awake()
    {
        //☆仮で10秒に変更したよ(2022/06/21 細谷)☆
        //Invoke(nameof(DestroyMe), /*15 * 60 * Time.deltaTime*/ 10);
    }

    private void Update()
    {
        //空のオブジェクトmapEaterにぶつかったら消す方法もいいのではと感じる
        if (transform.position.z <= -10)
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        //そのオブジェクトがMapGeneratorによって生成されたものかどうか確認
        foreach(GameObject generatedObject in NewMapGenerator.generatedPrefabsList)
        {
            //自分と同じものが見つかったら
            if(generatedObject == gameObject)
            {
                gameObject.SetActive(false);    //非アクティブにする
                return;
            }
        }

        Destroy(gameObject);
    }
}