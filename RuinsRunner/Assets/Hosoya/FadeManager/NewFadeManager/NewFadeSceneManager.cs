using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SceneDefine;

public class NewFadeSceneManager : MonoBehaviour
{
    [SerializeField] GameObject fadePrefabTemp_;
    static GameObject fadePrefab_;
    static SceneName nextScene_;
    static float fadeOutTimeMax_;
    static float standByTimeMax_;
    static float fadeInTimeMax_;

    [SerializeField] Image fadeImage_;
    float stateTime_;
    enum FadeType { FadeOut, standBy, FadeIn };
    FadeType fadeState_;

    private void Awake()
    {
        //fadePrefabÇÃê›íË
        if(fadePrefab_ == null)
        {
            fadePrefab_ = fadePrefabTemp_;
            Destroy(gameObject);
        }
    }

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

                if (fadeOutTimeMax_ <= stateTime_)
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

                    //sceneà⁄çs
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneAddRequester>().RequestAddScene(nextScene_, true);
                }
                break;

            case FadeType.FadeIn:
                nextColor.a = Mathf.InverseLerp(fadeInTimeMax_, 0.0f, stateTime_);
                if (fadeInTimeMax_ <= stateTime_)
                {
                    stateTime_ = 0.0f;

                    //é©êgçÌèú
                    Destroy(gameObject);
                }
                break;
        }
        fadeImage_.color = nextColor;
    }

    public static void StartMoveScene(SceneName _nextScene, float _fadeOutTime = 1.0f, float _standByTime = 1.0f, float _fadeInTime = 1.0f)
    {
        //ê›íË
        nextScene_ = _nextScene;
        fadeOutTimeMax_ = _fadeOutTime;
        standByTimeMax_ = _standByTime;
        fadeInTimeMax_ = _fadeInTime;

        GameObject instanceateObject = Instantiate(fadePrefab_);
    }
}
