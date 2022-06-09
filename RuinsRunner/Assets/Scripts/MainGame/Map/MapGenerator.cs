using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //�}�b�v�`�b�v�̐����ʒu
    [SerializeField] Vector3 createPosition;
    //�}�b�v�`�b�vprefab�̕ۑ��ꏊ
    [SerializeField] List<GameObject> mapPrefabs;
    //��Ԓ��߂ɐ��������}�b�v�`�b�v�̏���ۑ�����ϐ�
    MapInformation latestMapInfo;

    float movedDistance;


    private void Awake()
    {
        movedDistance = 0;
        latestMapInfo = new MapInformation();
    }

    private void Update()
    {
        movedDistance += MainGameConst.moveSpeed * Time.deltaTime;
        //todo:�A�^�b�`���Ď��^�]���I�������4.0f��gameObject.GetComponetn<RectTransform>().sizeDelta.z�Ŏ擾������
        if(movedDistance >= 4.0f) 
        {
            //�����\�ȃ}�b�v�`�b�v��ێ����Ă�����
            List<GameObject> candidateMapPrefab = new List<GameObject>();

            //latest�ƂȂ���悤�ɐ������Ă������}�b�v�𔻒f
            foreach(GameObject mapPrefab in mapPrefabs)
            {
                if (latestMapInfo.IsConnectBack() == mapPrefab.GetComponent<MapChip>().IsConnectFront())
                {
                    candidateMapPrefab.Add(mapPrefab);
                }
            }

            //prefab�̐���
            int randomIndex = Random.Range(0, candidateMapPrefab.Count);
            Instantiate(candidateMapPrefab[randomIndex], createPosition, Quaternion.identity);

            //latest�ɏ����X�V
            latestMapInfo = candidateMapPrefab[randomIndex].GetComponent<MapChip>().GetMapInformation();

            movedDistance = 0;
        }
    }
}
