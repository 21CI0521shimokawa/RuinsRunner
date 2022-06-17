using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;

    //Player�֌W
    [SerializeField] float[] positionZTables; //�v���C����Z���W�̃e�[�u��
    public int tablePositionZ;                //�v���C���̃e�[�u��Z���W

    //���݂�Z���W���擾
    public float GetPositionZ()
    {
        return positionZTables[tablePositionZ];
    }

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

        tablePositionZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(gameObject);
    }


    //�߂܂������ǂ���
    public bool IsBeCaught()
    {
        return gameObject.transform.position.z <= tablePositionZ;
    }

    //�n�ʂɗ����Ă邩�ǂ���
    public bool OnGround()
    {
        Vector3 origin = gameObject.transform.position; // ���_
        origin += new Vector3(0, 0.05f, 0);
        Vector3 direction = new Vector3(0, -1, 0); // Y��������\���x�N�g��
        Ray ray = new Ray(origin, direction); // Ray�𐶐�;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // �ԐF�ŉ���

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
        {
            string name = hit.collider.gameObject.name; // �Փ˂�������I�u�W�F�N�g�̖��O���擾
            Debug.Log(name); // �R���\�[���ɕ\��
        }
        return false;
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
