using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Coin : ItemBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 120 * Time.deltaTime, 0));
    }

    protected override void TakenItem(GameObject _player)
    {
        Debug.Log("ƒRƒCƒ“‚ªŽæ‚ç‚ê‚½");
    }

    protected override void ReleaseProcess_ManagedResource()
    {

    }

    protected override void ReleaseProcess_UnManagedResource()
    {

    }
}
