using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;
using TMPro;

public class TextManeger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI QuizText;
    [SerializeField] float TextSpeed;
    private string nowDispStr = ""; //実際に画面に表示させる用の文字列
    private string maxDispStr = ""; //表示させたい内容の文字列
    private float nowDispCount = 0.0f; //現在何文字目まで表示するかのカウンター

    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                maxDispStr = "日本電子の所在地は？";

                //毎フレームカウンターを加算する
                nowDispCount += Time.deltaTime /TextSpeed;

                //カウンターの分まで表示用の文字列へコピーし、textに渡す
                nowDispStr = maxDispStr.Substring(0, Mathf.Min((int)nowDispCount, maxDispStr.Length));
                QuizText.text = nowDispStr;
            });
    }
}
