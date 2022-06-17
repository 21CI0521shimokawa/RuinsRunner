using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBeCaught : StateBase
{
    public override void StateInitialize()
    {

    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        return nextState;
    }

    public override void StateFinalize()
    {

    }
}
