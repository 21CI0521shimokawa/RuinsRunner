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
        StateBase nextState = this;

        if (mapGenerator_.IsGenerate())
        {
            //�ړ��������������炷
            mapGenerator_.movedDistance -= mapGenerator_.latestFloorInfo.sizeZ;

            if (!mapGenerator_.IsGoalGenerate())
            {
                //�~�j�Q�[���̃G���g���[�`�F�b�N
                if (MiniGameEntryCheck(ref nextState))
                {
                    //�~�j�Q�[���ɂȂ�����return
                    return nextState;
                }
                #region ������
                //�G�l�~�[�A�^�b�N�̃G���g���[�`�F�b�N
                //if (!mapGenerator_.isCalledEnemyAttack && mapGenerator_.passedTime > mapGenerator_.EntryTimeEnemyAttack)
                //{
                //    nextState = new NewMapGeneratorState_EnemyAttack();
                //    mapGenerator_.isCalledEnemyAttack = true;
                //    mapGenerator_.Generate(mapGenerator_.enemyAttackMapPrefab);
                //    return nextState;
                //}

                ////�������̃G���g���[�`�F�b�N
                //if(!mapGenerator_.isCalledJumpHole && mapGenerator_.passedTime > mapGenerator_.EntryTimeJumpHole)
                //{
                //    //nextState = new NewMapGeneratorState_JumpHole();
                //    mapGenerator_.isCalledJumpHole = true;
                //    mapGenerator_.Generate(mapGenerator_.jumpHoleMapPrefab);
                //    return nextState;
                //}
                #endregion

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

                            if(wallNumber >= wallPrefabs.Count)
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

                //�A�C�e������
                mapGenerator_.ItemGenerator(floorSizeZ);
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


    bool MiniGameEntryCheck(ref StateBase _state)
    {
        for(int i = 0; i < mapGenerator_.miniGameEntryTimes.Length; ++i)
        {
            //���ɌĂяo���Ă�����Ă΂Ȃ�
            if (!mapGenerator_.isCalledMiniGames[i])
            {
                //�ĂԎ��ԂɂȂ��Ă�����Ă�
                if (mapGenerator_.passedTime > mapGenerator_.miniGameEntryTimes[i])
                {
                    mapGenerator_.isCalledMiniGames[i] = true;

                    //prefab����
                    mapGenerator_.Generate(mapGenerator_.miniGameMapPrefabs[i]);

                    //minigame���ɂ���Đ�p�̏���������ꍇ�͂����ɋL�q

                    //�G�l�~�[�A�^�b�N
                    if (mapGenerator_.miniGameNames[i] == "EnemyAttack")
                    {
                        _state = new NewMapGeneratorState_EnemyAttack();
                    }

                    return true;
                }
            }
        }

        return false;
    }
}
