using System;
using UnityEngine;
using SceneDefine;
/// <summary>
/// �Q�[���I�u�W�F�N�g�p���������[�N�΍���N���X
/// �A�^�b�`����X�N���v�g��MonoBehavior�ł͂Ȃ�������̌p������������
/// </summary>
public abstract class ObjectSuperClass 
    : MonoBehaviour
    , IDisposable
{
    //�V�[���I�����ɂ܂Ƃ߂ă��\�[�X���J�����Ă����@�\�ɓo�^����
    //�����ɂ�this��n��
    protected abstract void RegistToCompositDisposable(IDisposable _thisGameObject);

    //�p����ŎQ�Ƃ������\�[�X�����ׂă����[�X���邽�߂ɁA�����I�ɎQ�Ƃ���������
    //null�����0����A""����Ȃǂł悢
    public abstract void Dispose();
}