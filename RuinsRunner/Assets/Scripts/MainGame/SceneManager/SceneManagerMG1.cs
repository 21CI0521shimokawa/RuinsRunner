using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneAddRequester))]
public class SceneManagerMG1 : SceneSuperClass
{
    [SerializeField] PlayerController playerController;
    private void Start()
    {
        playerController.animator_.SetTrigger("SceneMG1");
        playerController.state_.ChangeState(new PlayerStateMG1Idle());
    }
}
