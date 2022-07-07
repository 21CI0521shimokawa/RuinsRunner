using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameQuiz_ImageRender : ObjectSuperClass
{
    GameObject Canvas;
    [SerializeField] GameObject[] ImagePrefubs;
    [SerializeField] TextMeshProUGUI QuizText;
    MiniGameQuiz_Maneger Maneger;


    private bool IsInitializeProcessing;


    void Start()
    {
        IsInitializeProcessing = false;
        Maneger = GetComponent<MiniGameQuiz_Maneger>();
        Canvas = GameObject.FindGameObjectWithTag("QuizCanvas");

    }

    void Update()
    {

    }
}
