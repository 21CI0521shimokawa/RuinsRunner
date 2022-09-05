using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{
    static string nextSceneName_;
    static float fadeOutTimeMax_;
    static float standByTimeMax_;
    static float fadeInTimeMax_;

    [SerializeField] Image fadeImage_;
    float stateTime_;
    enum FadeType { FadeOut, standBy, FadeIn };
    FadeType fadeState_;

    // Start is called before the first frame update
    void Start()
    {
        stateTime_ = 0.0f;
        fadeState_ = FadeType.FadeOut;
    }

    // Update is called once per frame
    void Update()
    {
        stateTime_ += Time.unscaledDeltaTime;

        Color nextColor = fadeImage_.color;
        switch (fadeState_)
        {
            case FadeType.FadeOut:
                nextColor.a = Mathf.InverseLerp(0.0f, fadeOutTimeMax_, stateTime_);

                if(fadeOutTimeMax_ <= stateTime_)
                {
                    fadeState_ = FadeType.standBy;
                    stateTime_ = 0.0f;
                }
                break;

            case FadeType.standBy:
                if (standByTimeMax_ <= stateTime_)
                {
                    fadeState_ = FadeType.FadeIn;
                    stateTime_ = 0.0f;

                    //scene����
                    SceneManager.LoadScene(nextSceneName_, LoadSceneMode.Additive);
                }
                break;

            case FadeType.FadeIn:

                //Activescene�����̃V�[���ł͂Ȃ��Ȃ�
                if (SceneManager.GetActiveScene().name != nextSceneName_)
                {
                    Scene oldActiveScene = SceneManager.GetActiveScene();
                    //���ݓǂݍ��܂�Ă���V�[�����������[�v
                    //for (int i = 0; i < SceneManager.sceneCount; i++)
                    //{
                    //    //�ǂݍ��܂�Ă���V�[�����擾
                    //    Scene scene = SceneManager.GetSceneAt(i);

                    //    //����Scene��T��
                    //    if (scene.name == nextSceneName_)
                    //    {
                    //        if (scene.isLoaded)
                    //        {
                    //            //Scene��ActiveScene�ɐݒ�
                    //            SceneManager.SetActiveScene(scene);
                    //        }
                    //        break;
                    //    }
                    //}

                    //����Scene���擾
                    if (SceneManager.GetSceneByName(nextSceneName_).isLoaded)
                    {
                        //Scene��ActiveScene�ɐݒ�
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextSceneName_));

                        //scene�폜
                        SceneManager.UnloadSceneAsync(oldActiveScene);
                    }
                }


                nextColor.a = Mathf.InverseLerp(fadeInTimeMax_, 0.0f, stateTime_);
                if (fadeInTimeMax_ <= stateTime_)
                {
                    stateTime_ = 0.0f;

                    //���ݓǂݍ��܂�Ă���V�[�����������[�v
                    //for (int i = 0; i < SceneManager.sceneCount; i++)
                    //{
                    //    //�ǂݍ��܂�Ă���V�[�����擾
                    //    Scene scene = SceneManager.GetSceneAt(i);

                    //    //FadeScene��T��
                    //    if(scene.name == "FadeScene")
                    //    {
                    //        //scene�폜
                    //        SceneManager.UnloadSceneAsync(scene);
                    //        break;
                    //    }
                    //}

                    //scene�폜
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("FadeScene"));
                }
                break;
        }
        fadeImage_.color = nextColor;
    }


    public static void StartMoveScene(string _nextSceneName, float _fadeOutTime = 1.0f, float _standByTime = 1.0f, float _fadeInTime = 1.0f)
    {
        //�ݒ�
        nextSceneName_ = _nextSceneName;
        fadeOutTimeMax_ = _fadeOutTime;
        standByTimeMax_ = _standByTime;
        fadeInTimeMax_ = _fadeInTime;

        SceneManager.LoadSceneAsync("FadeScene", LoadSceneMode.Additive);
    }
}
