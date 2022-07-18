using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneDefine;

public class SceneManagerTitle : SceneSuperClass
{
    [SerializeField] Fade fade_;
    [Tooltip("�t�F�[�h�C���ɂ����鎞��")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("�t�F�[�h�A�E�g�ɂ����鎞��")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("�f���ڍs�܂ł̎���")]
    [SerializeField] float displayTime = 2.0f;

    bool isFading_;
    float passedTime_;

    private void Start()
    {
        isFading_ = true;
        passedTime_ = 0.0f;
        fade_.FadeOut(fadeoutTime, 
            () => 
            {
                isFading_ = false;
            });
    }

    private void Update()
    {
        //�t�F�[�h���͈ڍs���󂯕t���Ȃ�
        if (isFading_) return;
        //�p�b�h�̓��͂𔽉f
        Gamepad gamepad = Gamepad.current;

        //�p�b�h�̉�������̓��͂����ꂽ��
        if(Gamepad.current.buttonWest.wasPressedThisFrame)
        {           
            //�t�F�[�h�A�E�g
            fade_.FadeIn(fadeinTime,
                () =>
                {
                    isFading_ = true;
                    //�t�F�[�h�A�E�g�I����
                    //�����Q�[���V�[���ֈڍs�i��������Ƃ��̃V�[�������݂����j
                    GetComponent<SceneAddRequester>().RequestAddScene(SceneName.RUNGAME, true);
                });
        }

        passedTime_ += Time.deltaTime;
        if(passedTime_ > displayTime)
        {
            isFading_ = true;
            //�t�F�[�h�A�E�g
            fade_.FadeIn(fadeinTime,
                () =>
                {
                    //�t�F�[�h�A�E�g�I����
                    //�f���V�[���ֈڍs
                    GetComponent<SceneAddRequester>().RequestAddScene(SceneName.DEMO, true);
                });
        }
    }
}
