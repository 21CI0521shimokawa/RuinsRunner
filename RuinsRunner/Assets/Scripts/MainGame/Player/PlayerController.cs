using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;

    //Run�֘A

    //Defeat�֘A
    bool defert_Attack;
    public bool Defert_Attack
    {
        get { return defert_Attack; }
        set { defert_Attack = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new StateMachine(new PlayerStateRun());
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(gameObject);
    }


    //isDisposed���G���[�ɂȂ�

    //protected override void Dispose(bool _disposing)
    //{
    //    if (this.isDisposed)
    //    {
    //        return; // ����ς݂Ȃ̂ŏ������Ȃ�
    //    }

    //    if (_disposing)
    //    {
    //        // �}�l�[�W���\�[�X�̉���������L�q
    //    }

    //    // �A���}�l�[�W���\�[�X�̉���������L�q

    //    // Dispose�ς݂��L�^
    //    this.isDisposed = true;

    //    // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
    //    base.Dispose(_disposing);
    //}
}
