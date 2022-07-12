using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Coin : ItemBase
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 120 * Time.deltaTime, 0));
    }

    protected override void TakenItem(GameObject _player)
    {
        StaticInterfaceManager.UpdateScore(100);
    }

    protected override void ReleaseProcess_ManagedResource()
    {

    }

    protected override void ReleaseProcess_UnManagedResource()
    {

    }
}
