using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LodingUIFade : MonoBehaviour
{
    //������҂��߂ɐ�������UniTask
    AutoResetUniTaskCompletionSource<bool> autoResetUniTaskCompletionSource = AutoResetUniTaskCompletionSource<bool>.Create();

    /// <summary>
    /// �w��̃L�����p�X�O���[�v���t�F�[�h�C��������R�[������֐�
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public async UniTask<bool> FadeInFunction(CanvasGroup CanvasGroup,float FadeTime)
    {
        //�t�F�[�h�C��
        await CanvasGrouopExtensions.FadeIn(CanvasGroup, FadeTime).AsyncWaitForCompletion();
        //�^�X�N������������
        autoResetUniTaskCompletionSource.TrySetResult(true);
        //�^�X�N��bool�^�ɕϊ�
        var result = await autoResetUniTaskCompletionSource.Task;
        //�ϊ������l��Ԃ�
        return result;
    }

    /// <summary>
    /// �w��̃L�����p�X�O���[�v���t�F�[�h�A�E�g������R�[������֐�
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="FadeTime"></param>
    /// <returns></returns>
    public async UniTask<bool>FadeOutFunction(CanvasGroup CanvasGroup, float FadeTime)
    {
        //�t�F�[�h�A�E�g
        await CanvasGrouopExtensions.FadeOut(CanvasGroup, FadeTime).AsyncWaitForCompletion();
        //�^�X�N������������
        autoResetUniTaskCompletionSource.TrySetResult(true);
        //�^�X�N��bool�^�ɕϊ�
        var result = await autoResetUniTaskCompletionSource.Task;
        //�ϊ������l��Ԃ�
        return result;
    }
}

/// <summary>
/// �t�F�[�h�����̐S����(this��t���鎖�Ŋg�����\�b�h���g���l���g���w��)
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

