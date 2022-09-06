using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        // カーソル非表示
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
