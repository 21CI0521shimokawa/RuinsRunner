using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Meet : ItemBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void TakenItem(GameObject _player)
    {
        //_player.GetComponent<PlayerController>().Recovery();
        StaticInterfaceManager.MovePlayerZ(-1, _player.GetComponent<PlayerController>());
    }

    protected override void ReleaseProcess_ManagedResource()
    {

    }

    protected override void ReleaseProcess_UnManagedResource()
    {

    }
}
