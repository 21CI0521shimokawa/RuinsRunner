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
                //移動した分だけ減らす
                mapGenerator_.movedDistance -= mapGenerator_.latestFloorInfo.sizeZ;

                //通常に戻す
                if (mapGenerator_.endEnemyAttack)
                {
                    mapGenerator_.endEnemyAttack = false;
                    nextState = new NewMapGeneratorState_Normal();
                }

                //床を設置
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[0]);

                //今置いた床の長さを取得
                int floorSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;

                //置いた床と同じ長さだけ壁を設置
                for (int i = 0; i < 2; ++i)
                {
                    int generateWallSizeZ = floorSizeZ;
                    bool isRightWall = (i == 0);   //右の壁から生成
                    bool isTorchWithWallInstalled = false;

                    while (generateWallSizeZ > 0)
                    {
                        List<GameObject> wallPrefabs = new List<GameObject>();

                        //一部の壁を除外
                        foreach (GameObject wallChipPrefab in mapGenerator_.wallPrefabs)
                        {
                            //床の長さより長い壁を除外
                            if (wallChipPrefab.GetComponent<WallChip>().sizeZ <= generateWallSizeZ)
                            {
                                //その方向に配置できない壁は除外
                                if (mapGenerator_.IsWallPlacementCheck(wallChipPrefab, isRightWall))
                                {
                                    wallPrefabs.Add(wallChipPrefab);
                                }
                            }
                        }

                        if (wallPrefabs.Count != 0)
                        {
                            //壁を決める 仮
                            int wallNumber = Random.Range(1, wallPrefabs.Count + 5);

                            if (wallNumber >= wallPrefabs.Count)
                            {
                                wallNumber = 0;
                            }

                            //壁を設置
                            GameObject generateWall;

                            //松明付きの壁を設置
                            if (wallNumber == 0 && !isTorchWithWallInstalled)
                            {
                                generateWall = mapGenerator_.Generate(mapGenerator_.wallTorchPrefab, floorSizeZ - generateWallSizeZ);
                                isTorchWithWallInstalled = true;
                            }
                            else
                            {
                                generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], floorSizeZ - generateWallSizeZ);
                            }

                            //場所移動
                            mapGenerator_.WallMove(generateWall, isRightWall);

                            //残りの床の長さを取得
                            generateWallSizeZ -= generateWall.GetComponent<WallChip>().sizeZ;
                        }
                        else
                        {
                            Debug.Log("aaa");
                            generateWallSizeZ = 0;
                        }
                    }
                }

                //松明生成
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



                //生成しない
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
