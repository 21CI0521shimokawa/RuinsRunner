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
                //移動した分だけ減らす
                mapGenerator_.movedDistance -= mapGenerator_.latestFloorInfo.sizeZ;

                //床を決める 仮
                int floorNumber = Random.Range(0, mapGenerator_.floorPrefabs.Count);

                //床を設置
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[floorNumber]);

                //回転
                mapGenerator_.FloorRotate(generateFloor);

                //今置いた床の長さを取得
                int floorSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;

                //置いた床と同じ長さだけ壁を設置
                for(int i = 0; i < 2; ++i)
                {
                    int generateWallSizeZ = floorSizeZ;
                    bool isRightWall = (i == 0);   //右の壁から生成

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
                            //壁を決める
                            int wallNumber = Random.Range(0, wallPrefabs.Count);

                            //壁を設置
                            GameObject generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], floorSizeZ - generateWallSizeZ);

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
            }
            else
            {
                mapGenerator_.Generate(mapGenerator_.goalMapPrefab);



                //生成しない
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
