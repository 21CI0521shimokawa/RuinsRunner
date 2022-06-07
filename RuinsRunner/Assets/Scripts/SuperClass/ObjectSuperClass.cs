using System;
using UnityEngine;
using SceneDefine;
/// <summary>
/// �Q�[���I�u�W�F�N�g�p���������[�N�΍���N���X
/// �p����ł͕K��REGION NEW IMPLEMETATION -> REGION NEED TO OVERRIDE���Q�Ƃ̂��ƁADispose()���I�[�o�[���C�h���邱��
/// </summary>
public abstract class ObjectSuperClass 
    : MonoBehaviour
    , IDisposable
{
    #region BEFORE IMPLEMENTATION
    /// <summary>
    /// �ȑO�g�p���Ă�������
    /// </summary>

    ////�V�[���I�����ɂ܂Ƃ߂ă��\�[�X���J�����Ă����@�\�ɓo�^����
    ////�����ɂ�this��n��
    //protected abstract void RegistToCompositDisposable(IDisposable _thisGameObject);

    ////�p����ŎQ�Ƃ������\�[�X�����ׂă����[�X���邽�߂ɁA�����I�ɎQ�Ƃ���������
    ////null�����0����A""����Ȃǂł悢
    //public abstract void Dispose();
    #endregion

    #region NEW IMPLEMENTATION
    /// <summary>
    /// ���ݎg�p���Ă������
    /// <summary>

    private bool isDisposed_;
    private SceneSuperClass sceneManager_;

    private void Start()
    {
        sceneManager_ = GameObject.FindWithTag("SceneManager").GetComponent<SceneSuperClass>();
        sceneManager_.AddToCompositeDisposable(this);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~ObjectSuperClass() => this.Dispose(false);

    protected virtual void Dispose(bool _disposing)
    {
        //����ς݂������珈�����Ȃ�
        if (this.isDisposed_) return;

        if (_disposing)
        {
            //�}�l�[�W���\�[�X�̉��������ǉ�
        }
        //�A���}�l�[�W���\�[�X�̉��������ǉ�

        this.isDisposed_ = true;
    }

    #region NEED TO OVERRIDE
    /// <summary>
    /// �p����Dispose() override �e���v���[�g
    /// <summary>
    //�p����ł͈ȉ��̂悤��override���邱��
    //�}�l�[�W���\�[�X�A�A���}�l�[�W�h���\�[�X�ɂ��Ắ���URL���Q�l�ɁAnew�������̂��ǂ����Ŕ��f����
    //https://hilapon.hatenadiary.org/entry/20100904/1283570083
    /*
        protected override void Dispose(bool _disposing)
        {
            if (this.isDisposed)
            {
                return; // ����ς݂Ȃ̂ŏ������Ȃ�
            }

            if (_disposing)
            {
                // �}�l�[�W���\�[�X�̉���������L�q
            }

            // �A���}�l�[�W���\�[�X�̉���������L�q

            // Dispose�ς݂��L�^
            this.isDisposed = true;

            // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
            base.Dispose(_disposing);
        }*/
    #endregion
    #endregion
}