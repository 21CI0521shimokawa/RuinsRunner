using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChara : MonoBehaviour
{
    StateBase state;

    // Start is called before the first frame update
    void Start()
    {
        //������
        state = new TestNormal();
    }

    // Update is called once per frame
    void Update()
    {
        //�X�V����
        state = (StateBase)state.Update(this.gameObject);
    }
}