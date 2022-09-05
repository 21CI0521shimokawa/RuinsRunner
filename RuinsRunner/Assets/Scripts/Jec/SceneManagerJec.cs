using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneDefine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneManagerJec : SceneSuperClass
{
    [SerializeField] Fade fade;
    float passedTime = 0;
    private void Start()
    { 
        passedTime = 0;
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
        if(passedTime > 2)
        {
            GetComponent<SceneAddRequester>().RequestAddScene(SceneName.TITLE, true);
        }
    }
}
