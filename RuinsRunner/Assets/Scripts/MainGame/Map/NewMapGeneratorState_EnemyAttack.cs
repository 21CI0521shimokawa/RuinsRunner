using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGeneratorState_EnemyAttack : StateBase
{
    GameObject map_;
    NewMapGenerator mapGenerator_;
    public override void StateInitialize()
    {
        mapGenerator_ = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>();
        map_ = mapGenerator_.enemyAttackMapPrefab;
        mapGenerator_.Generate(map_);
        mapGenerator_.isCalledEnemyAttack = true;
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
