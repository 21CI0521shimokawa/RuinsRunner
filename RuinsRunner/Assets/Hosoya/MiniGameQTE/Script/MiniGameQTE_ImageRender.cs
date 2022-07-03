using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameQTE_ImageRender : ObjectSuperClass
{
    MiniGameQTEManager manager_;
    GameObject canbas_;
    [SerializeField] GameObject[] imagePrefabs_;
    [SerializeField] GameObject arrowSymbolPrefab_;

    List<Image> buttonImages_;      //今ある画像のリスト
    Image arrowSymbol_;             //矢印
    [SerializeField] Slider timeGauge_;              //タイムゲージ

    const int imageSize_ = 60;      //画像サイズ

    bool isInitializeProcessing_;   //初期化処理が終了したかどうか

    // Start is called before the first frame update
    void Start()
    {
        isInitializeProcessing_ = false;

        manager_ = gameObject.GetComponent<MiniGameQTEManager>();
        canbas_ = GameObject.FindGameObjectWithTag("QTECanbas");
        buttonImages_ = new List<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //初期化処理を行っていないかどうか
        if(!this.isInitializeProcessing_)
        {
            //Managerの初期化が終わっているかどうか
            if (manager_.isInitializeProcessing)
            {
                //初期化処理
                ImagesInitialization();
                isInitializeProcessing_ = true;
            }
            else
            {
                return;
            }
        }

        //更新
        timeGauge_.value = Mathf.InverseLerp(manager_.timeLinitMax, 0, manager_.unscaledTimeCount);
    }

    //初期化処理
    void ImagesInitialization()
    {
        Vector3 instanceatePos = new Vector3(0, -80, 0);
        for(int i = 0; i < manager_.buttons.Count; ++i)
        {
            //今ある全ての画像を左にずらす
            foreach(Image image in buttonImages_)
            {
                image.rectTransform.anchoredPosition -= new Vector2(imageSize_, 0);
            }

            //生成
            {
                Image newImage = Instantiate(imagePrefabs_[(int)manager_.buttons[i]], Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Image>();
                newImage.rectTransform.anchoredPosition = instanceatePos;
                buttonImages_.Add(newImage);
            }

            instanceatePos.x += 60;
        }

        //矢印の生成
        {
            Image newImage = Instantiate(arrowSymbolPrefab_, Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Image>();
            arrowSymbol_ = newImage;
            newImage.rectTransform.Rotate(new Vector3(0, 0, 90));
            ArrowSymbolPotisionChange();
        }

        //タイムゲージの生成
        //{
        //    Slider slider = Instantiate(timeGaugePrefab_, Vector3.zero, Quaternion.identity, canbas_.transform).GetComponent<Slider>();
        //    timeGauge_ = slider;
        //    //slider.gameObject.rectTransform.anchoredPosition = new Vector2(0, 100);
        //}
    }

    public void ArrowSymbolPotisionChange()
    {
        arrowSymbol_.rectTransform.anchoredPosition = new Vector2(buttonImages_[manager_.buttonNumber].rectTransform.anchoredPosition.x, -200);
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
            manager_ = null;
            canbas_ = null;
            imagePrefabs_ = null;
            arrowSymbolPrefab_ = null;
            //timeGaugePrefab_ = null;
            buttonImages_ = null;
            arrowSymbol_ = null;
            timeGauge_ = null;
        }

        // アンマネージリソースの解放処理を記述
        isInitializeProcessing_ = false;

        // Dispose済みを記録
        this.isDisposed_ = true;

        // ★★★忘れずに、基底クラスの Dispose を呼び出す【重要】
        base.Dispose(_disposing);
    }
}
