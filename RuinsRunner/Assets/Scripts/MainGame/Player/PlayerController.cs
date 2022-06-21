using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectSuperClass
{
    StateMachine state;
    public Animator animator;
    
    InterfaceManager interfaceManager;
    public InterfaceManager GetInterfaceManager()
    {
        return interfaceManager;
    }

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



    // Start is called before the first frame update
    void Start()
    {
        state = new StateMachine(new PlayerStateRun());

        interfaceManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<InterfaceManager>();

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


    private void OnCollisionEnter(Collision collision)
    {
        //GetSceneManagerMain().ToFallOverPillar(ref collision.gameObject);
    }


    /// <summary>
    /// �p����Dispose() override �e���v���[�g
    /// <summary>
    //�p����ł͈ȉ��̂悤��override���邱��
    //�}�l�[�W���\�[�X�A�A���}�l�[�W�h���\�[�X�ɂ��Ắ���URL���Q�l�ɁAnew�������̂��ǂ����Ŕ��f����
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083

    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {
            return; // ����ς݂Ȃ̂ŏ������Ȃ�
        }

        if (_disposing)
        {
            // �}�l�[�W���\�[�X�̉���������L�q
            state = null;
            animator = null;
            interfaceManager = null;
            positionZTables = null;
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q

        //�Ȃɂ����΂����� �Ƃ肠����0�������Ă݂�
        tablePositionZ = 0;


        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
