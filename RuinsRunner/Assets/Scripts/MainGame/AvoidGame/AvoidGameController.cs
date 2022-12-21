using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class AvoidGameController
    : ObjectSuperClass
    , IAvoidGame
{
    [Header("���ł���I�u�W�F�N�g�擾")]
    [SerializeField] GameObject birdPrefubs;
    [Header("AvoidGame�ݒ�")]
    [SerializeField, Tooltip("��������鍶�̏���ʒu�𐮐��l")] int leftMaxGenerationPossition;
    [SerializeField, Tooltip("���������E�̏���ʒu�𐮐��l")] int rightMaxGenerationPosition;
    [SerializeField, Tooltip("���������I�u�W�F�N�g��Z�ʒu")] float birdGenerationPositionZ;
    [SerializeField, Tooltip("���������I�u�W�F�N�g��Y�ʒu")] int birdGenerationPositionY;
    [SerializeField, Tooltip("���݂̃~�j�Q�[����")] int doAvoidGameCount;
    [SerializeField, Tooltip("��������C���^�[�o������")] float intervalTime;
    [SerializeField] int numberToGenerate;
    [SerializeField] int numberToAttack;
    [SerializeField] AudioClip attackSignsSE;

    /// <summary>
    /// AvoidGame�X�^�[�g(�C���^�[�t�F�[�X)
    /// </summary>
    public void DoAvoidGame()
    {
        AvoidGameMove().Forget();
    }

    /// <summary>
    /// ���\�[�X�����
    /// </summary>
    /// <param name="disposing">���\�[�X������������̔���</param>
    protected override void Dispose(bool disposing)
    {
        if (this.isDisposed_)
        {
            // ����ς݂Ȃ̂ŏ������Ȃ�
            return;
        }
        // Dispose�ς݂��L�^
        this.isDisposed_ = true;

        // �������Y�ꂸ�ɁA���N���X�� Dispose ���Ăяo���y�d�v�z
        base.Dispose(disposing);
    }

    /// <summary>
    /// ������芴�o�Ŏw��͈͓��Ń����_���ň��񐔐���������֐�
    /// </summary>
    /// <returns></returns>
    private async UniTask AvoidGameMove()
    {
        List<int> GenerationPositionX = new List<int>();
        //doAvoidGameCount�̐��������[�v������
        for (int i = 0; i < doAvoidGameCount; ++i)
        {
            //�����̃C���^�[�o��time
            await UniTask.Delay(TimeSpan.FromSeconds(intervalTime));
            //��������X�l�����Z�b�g����
            GenerationPositionX.Clear();
            //�������鎞��SE�𗬂�
            PlayAudio.PlaySE(attackSignsSE);
            int NumToAttack = numberToAttack;

            //��肤��l�̂��������_���ł��Ԃ�Ȃ��悤�ɑI�сA�w�肳�ꂽ���܂Ő���
            for (int j = leftMaxGenerationPossition; j <= rightMaxGenerationPosition; j++)
            {
                //GenerationPositionX�ɗv�f(��������ʒu���)��ǉ�
                GenerationPositionX.Add(j);
            }

            while (GenerationPositionX.Count > rightMaxGenerationPosition - leftMaxGenerationPossition - numberToGenerate + 1)
            {
                //��������X�l�������_���Ŏ擾
                int Index = UnityEngine.Random.Range(0, GenerationPositionX.Count);
                //Index�Ŏ擾�����l����
                int InstantiatePositonX = GenerationPositionX[Index];
                GameObject birdObject = Instantiate
                    (
                    birdPrefubs,
                    new Vector3(InstantiatePositonX, birdGenerationPositionY, birdGenerationPositionZ), Quaternion.Euler(0, 180, 0)
                    );
                if (NumToAttack > 0)
                {
                    //NumToAttack��NumberToAttack�̒l���傫��������U���Ώۂɂ���
                    birdObject.GetComponent<BirdController>().IsAttack = true;
                    --NumToAttack;
                }
                //�����_���Ŏ擾������������ʒu��������
                GenerationPositionX.RemoveAt(Index);
            }
        }
    }
}
