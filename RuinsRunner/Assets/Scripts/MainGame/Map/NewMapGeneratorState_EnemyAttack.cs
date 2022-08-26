using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGeneratorState_EnemyAttack : StateBase
{
    NewMapGenerator mapGenerator_;
    float madeWallLength_;

    public override void StateInitialize()
    {
        mapGenerator_ = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<NewMapGenerator>();
        madeWallLength_ = 0.0f;
    }

    public override StateBase StateUpdate(GameObject gameObject)
    {
        StateBase nextState = this;

        if (mapGenerator_.IsGenerate())
        {
            if (!mapGenerator_.IsGoalGenerate())
            {
                //�ړ��������������炷
                mapGenerator_.movedDistance -= mapGenerator_.latestFloorInfo.sizeZ;

                //�ʏ�ɖ߂�
                if (mapGenerator_.endEnemyAttack)
                {
                    mapGenerator_.endEnemyAttack = false;
                    nextState = new NewMapGeneratorState_Normal();
                }

                //����ݒu
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[0]);

                //���u�������̒������擾
                int floorSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;

                //�u�������Ɠ������������ǂ�ݒu
                for (int i = 0; i < 2; ++i)
                {
                    int generateWallSizeZ = floorSizeZ;
                    bool isRightWall = (i == 0);   //�E�̕ǂ��琶��
                    bool isTorchWithWallInstalled = false;

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
                            //�ǂ����߂� ��
                            int wallNumber = Random.Range(1, wallPrefabs.Count + 5);

                            if (wallNumber >= wallPrefabs.Count)
                            {
                                wallNumber = 0;
                            }

                            //�ǂ�ݒu
                            GameObject generateWall;

                            //�����t���̕ǂ�ݒu
                            if (wallNumber == 0 && !isTorchWithWallInstalled)
                            {
                                generateWall = mapGenerator_.Generate(mapGenerator_.wallTorchPrefab, floorSizeZ - generateWallSizeZ);
                                isTorchWithWallInstalled = true;
                            }
                            else
                            {
                                generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], floorSizeZ - generateWallSizeZ);
                            }

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

                //��������
                //for(int i = 0; i < floorSizeZ; ++i)
                //{
                //    if(madeWallLength_ % 4 == 0)
                //    {
                //       // GameObject generateTorch = mapGenerator_.Generate(mapGenerator_.torchPrefab, i);
                //    }

                //    madeWallLength_ += 1.0f;
                //}
            }
            else
            {
                mapGenerator_.Generate(mapGenerator_.goalMapPrefab);



                //�������Ȃ�
                mapGenerator_.enabled = false;
            }
        }

        return nextState;
    }

    public override void StateFinalize()
    {
        mapGenerator_ = null;
    }
}
