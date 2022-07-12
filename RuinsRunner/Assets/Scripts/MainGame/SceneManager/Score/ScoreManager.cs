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
    }

    /// <summary>
    /// �X�R�A���Z
    /// </summary>
    /// <param name="addScore"></param>
    public void UpdateScore(int addScore)
    {
        //���[�J���X�R�A�̍X�V
        score_ += addScore;
        score_ = Mathf.Clamp(score_, 0, 9999);

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
