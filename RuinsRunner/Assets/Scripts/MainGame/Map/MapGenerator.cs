using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //�}�b�v�`�b�v�̐����t�H���_�i�e�j
    [SerializeField] GameObject mapFolder_;
    //�}�b�v�`�b�v�̐����ʒu
    [SerializeField] Vector3 createPosition_;
    //�}�b�v�`�b�vprefab�̕ۑ��ꏊ
    [SerializeField] List<GameObject> mapPrefabs_;
    //�S�[���̃}�b�v�`�b�v
    [SerializeField] GameObject goalMapPrefab_;
    //��Ԓ��߂ɐ��������}�b�v�`�b�v�̏���ۑ�����ϐ�
    MapInformation latestMapInfo_;

    float movedDistance_;
    //�V�[���}�l�[�W���[���玝���Ă���悤�ɂȂ�����Serialize�����������Ă���
    [SerializeField]float remainTime_ = 10f;

    private void Awake()
    {
        movedDistance_ = 0;
        latestMapInfo_ = new MapInformation();
    }

    private void Update()
    {
        //�c�莞�Ԃ��X�V
        //��X�A�V�[���}�l�[�W���[���������Ă���悤�ɂ��āA�V�[�����ň�̐��l�����L������
        remainTime_ -= Time.deltaTime;

        if (remainTime_ <= 0f)
        {
            remainTime_ = 0f;
        }

        movedDistance_ += MainGameConst.moveSpeed * Time.deltaTime;

        //todo:�A�^�b�`���Ď��^�]���I�������4.0f��gameObject.GetComponetn<RectTransform>().sizeDelta.z�Ŏ擾������
        if (movedDistance_ >= 4.0f)
        {
            //�}�b�v�`�b�v�̈ʒu�ɉ����Đ����ʒu�����������
            //�@2022/07/06 �גJ 
            Vector3 createPosition = createPosition_;
            createPosition.z -= movedDistance_ - 4.0f;

            if (remainTime_ > 0)
            {
                //�����\�ȃ}�b�v�`�b�v��ێ����Ă�����
                List<GameObject> candidateMapPrefab = new List<GameObject>();

                //latest�ƂȂ���悤�ɐ������Ă������}�b�v�𔻒f
                foreach (GameObject mapPrefab in mapPrefabs_)
                {
                    if (latestMapInfo_.IsConnectBack() == mapPrefab.GetComponent<MapChip>().IsConnectFront())
                    {
                        candidateMapPrefab.Add(mapPrefab);
                    }
                }

                //prefab�̐���
                int randomIndex = Random.Range(0, candidateMapPrefab.Count);

                //Instantiate(candidateMapPrefab[randomIndex], createPosition_, Quaternion.identity, mapFolder_.transform);
                Instantiate(candidateMapPrefab[randomIndex], createPosition, Quaternion.identity, mapFolder_.transform);

                //latest�ɏ����X�V
                latestMapInfo_ = candidateMapPrefab[randomIndex].GetComponent<MapChip>().GetMapInformation();

                //movedDistance_ = 0;
                movedDistance_ -= 4.0f;
            }
            else
            {
                //Instantiate(goalMapPrefab_, createPosition_, Quaternion.identity);
                Instantiate(goalMapPrefab_, createPosition, Quaternion.identity);
                enabled = false;
            }
        }
    }
}
