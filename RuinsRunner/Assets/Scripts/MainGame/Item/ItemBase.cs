using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ObjectSuperClass
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TakenItem(other.gameObject);
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    //���ۃ��\�b�h=================================================================
    /// <summary>
    /// <para>�������̃A�C�e�������ꂽ���̏�������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract protected void TakenItem(GameObject _player);

    /// <summary>
    /// <para>�����}�l�[�W���\�[�X�̉����������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract protected void ReleaseProcess_ManagedResource();

    /// <summary>
    /// <para>�����A���}�l�[�W�h���\�[�X�̉����������</para>
    /// <para>�����Ăяo�����Ⴞ�߂��恚��</para>
    /// </summary>
    abstract protected void ReleaseProcess_UnManagedResource();

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

            ReleaseProcess_ManagedResource();   //�q�N���X�̉������
        }

        // �A���}�l�[�W���\�[�X�̉���������L�q

        ReleaseProcess_UnManagedResource();     //�q�N���X�̉������

        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(_disposing);
    }
}
