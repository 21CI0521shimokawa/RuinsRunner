using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager
    : MonoBehaviour
    , IUpdateScore
    , IUpdateCoinCount
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinCountText;
    int score_;
    int coinCount_;
    TakeOverVariables gameManager;


    float scoreUpTime_;
    public float scoreUpTime
    {
        get
        {
            return scoreUpTime_;
        }

        set
        {
            scoreUpTime_ = value;
        }
    }

    //���_�{��
    float scoreMagnification_;

    public float scoreMagnification
    {
        get
        {
            return scoreMagnification_;
        }
    }


    //���x�����炷���x
    float speedCurrentVelocity_;

    private void Start()
    {
        Scene scene = SceneManager.GetSceneByName("Manager");

        //GetRootGameObjects�ŁA���̃V�[���̃��[�gGameObjects
        //�܂�A�q�G�����L�[�̍ŏ�ʂ̃I�u�W�F�N�g���擾�ł���
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                coinCount_ = gameManager.CoinCount;
                break;
            }
        }
        //scoreText.text = score_.ToString();
        coinCount_ = 0;
        coinCountText.text = coinCount_.ToString();

        scoreMagnification_ = 1.0f;
        scoreUpTime_ = 0.0f;

        speedCurrentVelocity_ = 0.0f;
    }

    void Update()
    {
        if(scoreUpTime_ > 0.0f)
        {
            scoreUpTime_ -= Time.deltaTime;
            scoreMagnification_ = 3.0f;

            speedCurrentVelocity_ = 0.0f;
        }
        else
        {
            scoreUpTime_ = 0.0f;
            scoreMagnification_ = 1.0f;

            if(MoveLooksLikeRunning.moveMagnification > 1.0f)
            {
                MoveLooksLikeRunning.moveMagnification = Mathf.SmoothDamp(MoveLooksLikeRunning.moveMagnification, 1.0f, ref speedCurrentVelocity_, 0.5f, Mathf.Infinity, Time.deltaTime);
                StaticInterfaceManager.StopConcentrationLineEffect();
            }
        }
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //���_�����_���ꂽ��{���Ƒ��x�����Z�b�g
        if (addScore < 0)
        {
            scoreUpTime_ = 0.0f;
        }
        /*
        //���[�J���X�R�A�̍X�V
        score_ += (int)(addScore * scoreMagnification_);
        score_ = Mathf.Clamp(score_, 0, 99999);

        //���L�X�R�A�̍X�V
        Scene scene = SceneManager.GetSceneByName("Manager");

        gameManager.Score = score_;
        //scoreText.text = score_.ToString();
        */
    }

    public void UpdateCoinCount(int _addValue)
    {
        //���������o�t��������{��������
        if(scoreMagnification > 0)
        {
            _addValue *= (int)scoreMagnification;
        }
        //���[�J���R�C���J�E���g�̍X�V
        coinCount_ += _addValue;
        coinCount_ = Mathf.Max(coinCount_, 0);
        coinCountText.text = coinCount_.ToString();
        //���L�R�C���J�E���g�̍X�V
        gameManager.CoinCount = coinCount_;
    }
}
