using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGeneratorState_JumpHole : StateBase
{
    GameObject map_;
    NewMapGenerator mapGenerator_;
    public override void StateInitialize()
    {
        mapGenerator_ = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>();
        mapGenerator_.Generate(map_);
    }
    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if (mapGenerator_.IsGenerate())
        {
            nextState = new NewMapGeneratorState_Normal();
        }

        return nextState;
    }

    public override void StateFinalize()
    {
    }

}
