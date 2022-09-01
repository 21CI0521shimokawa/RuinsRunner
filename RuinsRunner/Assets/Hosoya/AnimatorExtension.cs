using UniRx;
using UnityEngine;

//�Q�l
//https://light11.hatenadiary.com/entry/2020/02/23/201528

public static class AnimatorExtension
{

    public static void SetTriggerOneFrame(this Animator self, string name)
    {
        self.SetTrigger(name);
        Observable
            .NextFrame()
            .Subscribe(_ => { }, () => {
                // 1�t���[�����Update��Ƀg���K�[�����Z�b�g����
                if (self != null)
                {
                    self.ResetTrigger(name);
                }
            });
    }

    public static void SetTriggerOneFrame(this Animator self, int id)
    {
        self.SetTrigger(id);
        Observable
            .NextFrame()
            .Subscribe(_ => { }, () => {
                // 1�t���[�����Update��Ƀg���K�[�����Z�b�g����
                if (self != null)
                {
                    self.ResetTrigger(id);
                }
            });
    }
}
