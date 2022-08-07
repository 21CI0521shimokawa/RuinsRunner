using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager 
    : MonoBehaviour
    , IUpdateScore
{
    [SerializeField]TextMeshProUGUI text;
    int score_;

    //���_�{��
    float scoreMagnification_;
    public float scoreMagnification
    {
        get
        {
            return scoreMagnification_;
        }

        set
        {
            if(value >= 0.0f)
            {
                scoreMagnification_ = value;
            }
        }
    }

    private void Start()
    {
        Scene scene = SceneManager.GetSceneByName("Manager");

        //GetRootGameObjects�ŁA���̃V�[���̃��[�gGameObjects
        //�܂�A�q�G�����L�[�̍ŏ�ʂ̃I�u�W�F�N�g���擾�ł���
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                score_ = gameManager.Score;
                break;
            }
        }
        text.text = score_.ToString();


        scoreMagnification_ = 1.0f;
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //���_�����_���ꂽ��{���Ƒ��x�����Z�b�g
        if(addScore < 0)
        {
            scoreMagnification_ = 1.0f;
            MoveLooksLikeRunning.moveMagnification = 1.0f;
        }


        //���[�J���X�R�A�̍X�V
        score_ += (int)(addScore * scoreMagnification_);
        score_ = Mathf.Clamp(score_, 0, 99999);

        //���L�X�R�A�̍X�V
        Scene scene = SceneManager.GetSceneByName("Manager");

        //GetRootGameObjects�ŁA���̃V�[���̃��[�gGameObjects
        //�܂�A�q�G�����L�[�̍ŏ�ʂ̃I�u�W�F�N�g���擾�ł���
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            TakeOverVariables gameManager = rootGameObject.GetComponent<TakeOverVariables>();
            if (gameManager != null)
            {
                //GameManager�����������̂�
                //gameManager�̃X�R�A���擾
                gameManager.Score = score_;
                break;
            }
        }
        text.text = score_.ToString();
    }
}
