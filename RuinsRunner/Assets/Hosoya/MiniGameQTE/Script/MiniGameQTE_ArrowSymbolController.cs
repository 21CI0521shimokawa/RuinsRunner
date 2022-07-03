using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameQTE_ArrowSymbolController : ObjectSuperClass
{
    Image image_;
    Vector2 moveVec_;

    Vector3 startPos_;
    Vector3 turnPos_;

    // Start is called before the first frame update
    void Start()
    {
        image_ = gameObject.GetComponent<Image>();
        moveVec_ = new Vector2(0, 10);
        startPos_ = image_.rectTransform.anchoredPosition;
        turnPos_ = new Vector3(0, -190, 0);
    }

    // Update is called once per frame
    void Update()
    {
        image_.rectTransform.anchoredPosition += moveVec_ * Time.unscaledDeltaTime / 0.5f;

        if (moveVec_.y > 0)
        {
            if(image_.rectTransform.anchoredPosition.y >= turnPos_.y)
            {
                moveVec_.y *= -1;
            }
        }
        else
        {
            if (image_.rectTransform.anchoredPosition.y <= startPos_.y)
            {
                moveVec_.y *= -1;
            }
        }
    }

    //継承先では以下のようにoverrideすること
    //マネージリソース、アンマネージドリソースについては↓のURLを参考に、newしたものかどうかで判断する
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // 解放済みなので処理しない
        }

        if (_disposing)
        {
            // マネージリソースの解放処理を記述
            image_ = null;
        }

        // アンマネージリソースの解放処理を記述


        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
