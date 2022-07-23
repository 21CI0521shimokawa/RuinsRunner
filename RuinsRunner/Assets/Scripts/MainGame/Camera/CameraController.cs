using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class CameraController : ObjectSuperClass
{
    StateMachine CameraState;
    void Start()
    {
        CameraState = new StateMachine(new CameraStateDefault());
    }
    void Update()
    {
        CameraState.Update(this.gameObject);
    }
}
