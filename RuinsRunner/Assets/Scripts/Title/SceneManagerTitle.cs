using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneDefine;
using UnityEngine.UI;

public class SceneManagerTitle : SceneSuperClass
{
    [SerializeField] Fade fade_;
    [SerializeField] GameObject enemy;
    [Tooltip("�t�F�[�h�C���ɂ����鎞��")]
    [SerializeField] float fadeinTime = 1.0f;
    [Tooltip("�t�F�[�h�A�E�g�ɂ����鎞��")]
    [SerializeField] float fadeoutTime = 1.0f;
    [Tooltip("�f���ڍs�܂ł̎���")]
    [SerializeField] float displayTime = 2.0f;
    [Tooltip("���S���o���܂ł̎���")]
    [SerializeField] float logoAppTime = 2.0f;
    [SerializeField] GameObject[] slowObjects;
    [SerializeField] AudioClip audio;

    bool isFading_;
    float passedTime_;
    bool isAlreadyStopped;

    private void Start()
    {
        isFading_ = true;
        isAlreadyStopped = false;
        passedTime_ = 0.0f;
        fade_.FadeOut(fadeoutTime, 
            () => 
            {
                isFading_ = false;
            });
    }

    private void Update()
    {
        if(passedTime_ > logoAppTime && !isAlreadyStopped)
        {
            foreach(GameObject obj in slowObjects)
            {
                MoveLooksLikeRunning move = obj.GetComponent<MoveLooksLikeRunning>();
                if(move != null)
                {
                    move.moveSpeed = 0.1f;
                }
                Animator anim = obj.GetComponent<Animator>();
                if(anim != null)
                {
                    anim.speed = 0.01f;
                }
            }
            enemy.GetComponent<Animator>().SetTrigger("Attack");
            isAlreadyStopped = true;
        }
        FadeUpdate();
    }

    void FadeUpdate()
    {
        //�t�F�[�h���͈ڍs���󂯕t���Ȃ�
        if (isFading_) return;
        //�p�b�h�̓��͂𔽉f
        Gamepad gamepad = Gamepad.current;

        //�p�b�h��X�{�^���̓��͂����ꂽ��
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            PlayAudio.PlaySE(audio);
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
        if (passedTime_ > displayTime)
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
