using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGeneratorState_Normal : StateBase
{
    NewMapGenerator mapGenerator_;

    public override void StateInitialize()
    {
        mapGenerator_ = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>();
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        if (mapGenerator_.IsGenerate())
        {
            if (!mapGenerator_.IsGoalGenerate())
            {
                //�ړ��������������炷
                mapGenerator_.movedDistance -= mapGenerator_.latestFloorInfo.sizeZ;

                //�������߂� ��
                int floorNumber = Random.Range(0, mapGenerator_.floorPrefabs.Count);

                //����ݒu
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[floorNumber]);

                //��]
                mapGenerator_.FloorRotate(generateFloor);

                //���u�������̒������擾
                int floorSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;

                //�u�������Ɠ������������ǂ�ݒu
                for(int i = 0; i < 2; ++i)
                {
                    int generateWallSizeZ = floorSizeZ;
                    bool isRightWall = (i == 0);   //�E�̕ǂ��琶��

                    while (generateWallSizeZ > 0)
                    {
                        List<GameObject> wallPrefabs = new List<GameObject>();

                        //�ꕔ�̕ǂ����O
                        foreach (GameObject wallChipPrefab in mapGenerator_.wallPrefabs)
                        {
                            //���̒�����蒷���ǂ����O
                            if (wallChipPrefab.GetComponent<WallChip>().sizeZ <= generateWallSizeZ)
                            {
                                //���̕����ɔz�u�ł��Ȃ��ǂ͏��O
                                if (mapGenerator_.IsWallPlacementCheck(wallChipPrefab, isRightWall))
                                {
                                    wallPrefabs.Add(wallChipPrefab);
                                }
                            }
                        }

                        if (wallPrefabs.Count != 0)
                        {
                            //�ǂ����߂�
                            int wallNumber = Random.Range(0, wallPrefabs.Count);

                            //�ǂ�ݒu
                            GameObject generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], floorSizeZ - generateWallSizeZ);

                            //�ꏊ�ړ�
                            mapGenerator_.WallMove(generateWall, isRightWall);

                            //�c��̏��̒������擾
                            generateWallSizeZ -= generateWall.GetComponent<WallChip>().sizeZ;
                        }
                        else
                        {
                            Debug.Log("aaa");
                            generateWallSizeZ = 0;
                        }
                    }
                }
            }
            else
            {
                mapGenerator_.Generate(mapGenerator_.goalMapPrefab);



                //�������Ȃ�
                mapGenerator_.enabled = false;
            }
        }

        return this;
    }

    public override void StateFinalize()
    {
        mapGenerator_ = null;
    }
}
