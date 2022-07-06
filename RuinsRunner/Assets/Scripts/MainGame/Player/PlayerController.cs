using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
    : ObjectSuperClass
    , IMovePlayer
{
    StateMachine state_;
    public Animator animator_;

    Rigidbody rigidbody_;

    //Player�֌W
    [SerializeField] float[] positionZTables; //�v���C����Z���W�̃e�[�u��
    public int tablePositionZ;                //�v���C���̃e�[�u��Z���W

    //���݂�Z���W���擾
    public float GetPositionZ()
    {
        return positionZTables[tablePositionZ];
    }

    //�_���[�W
    public void Damage()
    {
        if (positionZTables.Length - 1 > tablePositionZ)
        {
            ++tablePositionZ;
        }
    }

    //��
    public void Recovery()
    {
        if (0 < tablePositionZ)
        {
            --tablePositionZ;
        }
    }


    //Run�֘A


    //Defeat�֘A


    //Fall�֌W


    // Start is called before the first frame update
    void Start()
    {
        MoveLooksLikeRunning.Set_isRunning(true);   //�ړ��J�n

        state_ = new StateMachine(new PlayerStateRun());

        rigidbody_ = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        state_.Update(gameObject);

        //�킴�Əd�����鑕�u ��X�y�b�N�V�~�����[�V�����p
        //{
        //    for (int i = 0; i < 1000000; ++i)
        //    {
        //        string d = new string("test");
        //        d = null;
        //    }
        //}

        Debug.Log(state_.StateName);
    }


    //�߂܂������ǂ���
    public bool IsBeCaught()
    {
        return gameObject.transform.position.z <= positionZTables[positionZTables.Length - 1];
    }

    //�n�ʂɗ����Ă邩�ǂ���
    public bool OnGround()
    {
        //�ׂ���������Â����邽�߂ɏ����� >=0 �����邭���܂����B -�H�� 7/3
        //�������Ɉړ����Ă��Ȃ���Η����Ă邱�Ƃɂ���
        if(rigidbody_.velocity.y >= -0.01f)
        {
            return true;
        }


        Vector3 origin = gameObject.transform.position; // ���_
        origin += new Vector3(0, 0.05f, 0);
        Vector3 direction = new Vector3(0, -1, 0); // Y��������\���x�N�g��
        Ray ray = new Ray(origin, direction); // Ray�𐶐�;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // �ԐF�ŉ���

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
        {
            //�g���K�[�������珜�O����
            if (hit.collider.isTrigger == true) { return false; }

            //�ꕔ��Tag�����Ă�Q�[���I�u�W�F�N�g�͏��O����
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { return false; }
            //�K�v�ɉ����Ēǉ�


            return true;
        }

        return false;
    }

    //Player���ړ��ł��邩�ǂ���
    //�ړ��ł��邩�m�F
    public bool PlayerMoveChack(Vector3 _checkRayVector)
    {
        //�ړ��ʂ�0�Ȃ珈�����Ȃ�(����K�v���Ȃ�)
        if(_checkRayVector.magnitude == 0)
        {
            return false;
        }

        Vector3 origin = this.transform.position; // ���_
        Vector3 direction = _checkRayVector.normalized; // �x�N�g��

        origin.y += 1;

        //�ړ����鎲�ɉ�����Ray�̌��_���ړ�
        if (origin.x != 0)
        {
            origin.x += 0.15f * Mathf.Sign(_checkRayVector.x);
            _checkRayVector.x += 0.1f;
        }
        if (origin.z != 0)
        {
            origin.z += 0.15f * Mathf.Sign(_checkRayVector.z);
            _checkRayVector.z += 0.1f;
        }

        float length = _checkRayVector.magnitude;


        Ray ray = new Ray(origin, direction); // Ray�𐶐�;
        Debug.DrawRay(ray.origin, ray.direction * length, Color.green, 0.01f); // �ΐF�ŉ���

        RaycastHit[] hits = Physics.RaycastAll(ray, length);

        foreach (RaycastHit hit in hits) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
        {
            //�g���K�[�������珜�O����
            if(hit.collider.isTrigger == true) { continue; }

            //�ꕔ��Tag�����Ă�Q�[���I�u�W�F�N�g�͏��O����
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { continue; }
            //�K�v�ɉ����Ēǉ�

            return false;
        }
        return true;
    }


    //�g���b�v�𓥂񂾂��ǂ���
    public bool OnTrap()
    {
        Vector3 origin = gameObject.transform.position; // ���_
        origin += new Vector3(0, 0, 0.25f);

        Vector3 direction = new Vector3(0, 0, 1); // Z��������\���x�N�g��
        Ray ray = new Ray(origin, direction); // Ray�𐶐�;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red, 0.01f); // �ԐF�ŉ���

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.1f)) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
        {
            //string name = hit.collider.gameObject.name; // �Փ˂�������I�u�W�F�N�g�̖��O���擾
            //Debug.Log(name); // �R���\�[���ɕ\��

            //�g���b�v��������
            if (hit.collider.gameObject.tag == "Trap")
            {

                return true;
            }
        }
        return false;
    }

    //���ɗ����������ǂ���
    public bool FallIntoHole()
    {
        return gameObject.transform.position.y <= -5.0f;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = gameObject.transform.position; // ���_

        float radius = 2.0f;
        Gizmos.color = new Color(1f, 0, 0, 0.5f);
        Gizmos.DrawSphere(origin, radius);
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
            state_ = null;
            animator_ = null;
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

    public void MovePlayer(int _moveAmount)
    {
        tablePositionZ += _moveAmount;
        tablePositionZ = Mathf.Clamp(tablePositionZ, 0, positionZTables.Length - 1);
        Debug.Log(_moveAmount + " �ړ����܂���");
    }
}
