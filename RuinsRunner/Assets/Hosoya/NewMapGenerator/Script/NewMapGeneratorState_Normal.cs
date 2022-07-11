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
            if (mapGenerator_.IsGoalGenerate())
            {
                //�������߂� ��
                int floorNumber = Random.Range(0, mapGenerator_.floorPrefabs.Count);

                //����ݒu
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[floorNumber]);

                //���u�������̒������擾
                int wallSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;
                int generateWallSizeZ = wallSizeZ;

                //�u�������Ɠ������������ǂ�ݒu
                while(generateWallSizeZ > 0)
                {
                    List<GameObject> wallPrefabs = new List<GameObject>();
                    //���̒�����蒷���ǂ����O
                    foreach(GameObject wallChipPrefab in mapGenerator_.wallPrefabs)
                    {
                        if(wallChipPrefab.GetComponent<WallChip>().sizeZ < generateWallSizeZ)
                        {
                            wallPrefabs.Add(wallChipPrefab);
                        }
                    }

                    //�ǂ����߂�
                    int wallNumber = Random.Range(0, wallPrefabs.Count);

                    //�ǂ�ݒu
                    GameObject generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], wallSizeZ - generateWallSizeZ);

                    //�c��̏��̒������擾
                    generateWallSizeZ -= generateWall.GetComponent<WallChip>().sizeZ;
                }

                mapGenerator_.movedDistance -= wallSizeZ;
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
