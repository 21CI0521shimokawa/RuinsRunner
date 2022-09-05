using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTitleMove : MonoBehaviour
{ 

    void Start()
    {
        MoveLooksLikeRunning.Set_isRunning(true);
        GetComponent<Animator>().SetTriggerOneFrame("StateRun");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
