using UnityEngine;
using DG.Tweening;

public class LodingUIAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーションさせるUI取得")] RectTransform uiRectTransform;
    [SerializeField, Tooltip("アニメーション座標取得")] float doMoveToYValue;
    [SerializeField, Tooltip("移動時間")] float moveTime;
    [SerializeField, Tooltip("イージングタイプ")] Ease easeType;
    [SerializeField, Tooltip("ループする時のイージングタイプ")] LoopType loopType;
    private const int loopTime = -1; //ループさせるための変数

    void Start()
    {
        //doMoveToYValueにmoveTimeの速さで移動させる
        uiRectTransform.DOLocalMoveY(doMoveToYValue, moveTime)
            //アニメーションを相対的にする
            .SetRelative(true)
            //イージングタイプを指定
            .SetEase(easeType)
            //ループする時のイージングタイプを指定し移動終わったら最初の位置に戻るようにする
            .SetLoops(loopTime, loopType);
    }
}
