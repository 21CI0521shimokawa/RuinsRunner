using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 FrozeYPosition;
    private void Awake()
    {
        FrozeYPosition.y = this.transform.position.y;
    }
    void Update()
    {
        this.transform.position =new Vector3(Player.transform.position.x,FrozeYPosition.y,Player.transform.position.z);
    }
}
