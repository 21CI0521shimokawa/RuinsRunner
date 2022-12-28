using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LodingUIFade : MonoBehaviour
{
    //処理を待つために生成するUniTask
    AutoResetUniTaskCompletionSource<bool> autoResetUniTaskCompletionSource = AutoResetUniTaskCompletionSource<bool>.Create();

    /// <summary>
    /// 指定のキャンパスグループがフェードインしたらコールする関数
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public async UniTask<bool> FadeInFunction(CanvasGroup CanvasGroup,float FadeTime)
    {
        //フェードイン
        await CanvasGrouopExtensions.FadeIn(CanvasGroup, FadeTime).AsyncWaitForCompletion();
        //タスクを完了させる
        autoResetUniTaskCompletionSource.TrySetResult(true);
        //タスクをbool型に変換
        var result = await autoResetUniTaskCompletionSource.Task;
        //変換した値を返す
        return result;
    }

    /// <summary>
    /// 指定のキャンパスグループがフェードアウトしたらコールする関数
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public async UniTask<bool>FadeOutFunction(CanvasGroup CanvasGroup, float FadeTime)
    {
        //フェードアウト
        await CanvasGrouopExtensions.FadeOut(CanvasGroup, FadeTime).AsyncWaitForCompletion();
        //タスクを完了させる
        autoResetUniTaskCompletionSource.TrySetResult(true);
        //タスクをbool型に変換
        var result = await autoResetUniTaskCompletionSource.Task;
        //変換した値を返す
        return result;
    }
}

/// <summary>
/// フェード処理の心臓部(thisを付ける事で拡張メソッドを使う値自身を指定)
/// </summary>
public static class CanvasGrouopExtensions
{

    public static Tweener FadeIn(this CanvasGroup CanvasGroup, float FadeTime)
    {
        return CanvasGroup.DOFade(1.0f, FadeTime);
    }

    public static Tweener FadeOut(this CanvasGroup CanvasGroup, float FadeTime)
    {
        return CanvasGroup.DOFade(0.0f, FadeTime);
    }
}

