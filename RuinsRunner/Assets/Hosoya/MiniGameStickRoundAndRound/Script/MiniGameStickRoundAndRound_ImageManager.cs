using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameStickRoundAndRound_ImageManager : ObjectSuperClass
{
    MiniGameStickRoundAndRoundManager manager_;
    [SerializeField] H_GaugeManager powerGauge_;              //パワーゲージ
    [SerializeField] Image clearArea_;

    [SerializeField] Image arrow_;

    // Start is called before the first frame update
    void Start()
    {
        manager_ = gameObject.GetComponent<MiniGameStickRoundAndRoundManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //更新
        powerGauge_.GaugeValueChange(0.0f, 1.0f, manager_.power);

        clearArea_.rectTransform.offsetMin = new Vector2(MiniGameStickRoundAndRoundManager.clearPower * 100, clearArea_.rectTransform.offsetMin.y);

        arrow_.rectTransform.Rotate(new Vector3(0, 0, -180 * Time.unscaledDeltaTime));
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

        }

        // アンマネージリソースの解放処理を記述


        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
