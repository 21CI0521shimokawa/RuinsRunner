using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame
{
    [Header("�O���֐��擾")]
    [SerializeField,Tooltip("Enemy���擾")] EnemyController EnemyController;
    [Header("���ł���I�u�W�F�N�g�擾")]
    [SerializeField] GameObject BirdPrefubs;
    [Header("AvoidGame�ݒ�")]
    [SerializeField,Tooltip("��������鍶�̏���ʒu�𐮐��l")] int LeftMaxGenerationPosition;
    [SerializeField,Tooltip("���������E�̏���ʒu�𐮐��l")] int RightMaxGenerationPosition;
    [SerializeField,Tooltip("���������I�u�W�F�N�g��Z�ʒu")] float BirdGenerationPositionZ;
    [SerializeField,Tooltip("���������I�u�W�F�N�g��Y�ʒu")] int BirdGenerationPositionY;
    [SerializeField,Tooltip("���݂̃~�j�Q�[����")] int NowDoAvoidGameCount;
    [SerializeField,Tooltip("��������C���^�[�o������")] float IntervalTime;
    [SerializeField] int NumberToGenerate;
    [SerializeField] int NumberToAttack;
    [SerializeField] AudioClip AttackSignsSE;

    /// <summary>
    /// AvoidGame�X�^�[�g(�C���^�[�t�F�[�X)
    /// </summary>
    public void DoAvoidGame()
    {
        StartCoroutine(AvoidGameMove());//AvoidGameMove�R���[�`���X�^�[�g
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="_disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool _disposing)
    {
        if (this.isDisposed_)
        {// ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        this.isDisposed_ = true; // Dispose�ς݂��L�^
        base.Dispose(_disposing); // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
    }

    /// <summary>
    /// ������芴�o�Ŏw��͈͓��Ń����_���ň��񐔐���������֐�
    /// </summary>
    /// <returns></returns>
    private IEnumerator AvoidGameMove()
    {
        List<int> GenerationPositionX = new List<int>();
        for (int i = 0; i < NowDoAvoidGameCount; ++i) //NowDoAvoidGameCount�̐��������[�v������
        {
            yield return new WaitForSeconds(IntervalTime); //�����̃C���^�[�o��
            GenerationPositionX.Clear(); //��������X�l�����Z�b�g����
            PlayAudio.PlaySE(AttackSignsSE); //�������鎞��SE�𗬂�
            int NumToAttack = NumberToAttack;

            for (int j = LeftMaxGenerationPosition; j <= RightMaxGenerationPosition; j++)//��肤��l�̂��������_���ł��Ԃ�Ȃ��悤�ɑI�сA�w�肳�ꂽ���܂Ő���
            {
                GenerationPositionX.Add(j); //GenerationPositionX�ɗv�f(��������ʒu���)��ǉ�
            }

            while (GenerationPositionX.Count > RightMaxGenerationPosition - LeftMaxGenerationPosition - NumberToGenerate + 1)
            {
                int Index = Random.Range(0, GenerationPositionX.Count); //��������X�l�������_���Ŏ擾

                int InstantiatePositonX = GenerationPositionX[Index]; //Index�Ŏ擾�����l����
                GameObject BirdObject = Instantiate(BirdPrefubs, new Vector3(InstantiatePositonX, BirdGenerationPositionY, BirdGenerationPositionZ), Quaternion.Euler(0, 180, 0));
                if (NumToAttack > 0)
                {
                    BirdObject.GetComponent<BirdController>().isAttack = true; //NumToAttack��NumberToAttack�̒l���傫��������U���Ώۂɂ���
                    --NumToAttack;
                }
                GenerationPositionX.RemoveAt(Index); //�����_���Ŏ擾������������ʒu��������
            }
        }
        yield break;
    }
}
