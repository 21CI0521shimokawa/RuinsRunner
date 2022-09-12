using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class SceneManagerDemo : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;

    bool isFade;

    void Start()
    {
        isFade = false;
        videoPlayer.loopPointReached += LoopPointReached;
        videoPlayer.Play();
    }

    private void Update()
    {
        if (isFade) return;

        //�p�b�h�̓��͂𔽉f
        Gamepad gamepad = Gamepad.current;

        if (gamepad.buttonEast.wasPressedThisFrame && !isFade)
        {
            SceneFadeManager.StartMoveScene("Scene_Title");
            isFade = true;

        }
    }

    public void LoopPointReached(VideoPlayer vp)
    {
        Debug.Log("�Đ��I��");
        SceneFadeManager.StartMoveScene("Scene_Title");
    }
}