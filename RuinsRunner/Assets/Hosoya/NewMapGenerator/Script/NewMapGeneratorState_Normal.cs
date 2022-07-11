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
                //床を決める 仮
                int floorNumber = Random.Range(0, mapGenerator_.floorPrefabs.Count);

                //床を設置
                GameObject generateFloor = mapGenerator_.Generate(mapGenerator_.floorPrefabs[floorNumber]);

                //今置いた床の長さを取得
                int wallSizeZ = generateFloor.GetComponent<FloorChip>().sizeZ;
                int generateWallSizeZ = wallSizeZ;

                //置いた床と同じ長さだけ壁を設置
                while(generateWallSizeZ > 0)
                {
                    List<GameObject> wallPrefabs = new List<GameObject>();
                    //床の長さより長い壁を除外
                    foreach(GameObject wallChipPrefab in mapGenerator_.wallPrefabs)
                    {
                        if(wallChipPrefab.GetComponent<WallChip>().sizeZ < generateWallSizeZ)
                        {
                            wallPrefabs.Add(wallChipPrefab);
                        }
                    }

                    //壁を決める
                    int wallNumber = Random.Range(0, wallPrefabs.Count);

                    //壁を設置
                    GameObject generateWall = mapGenerator_.Generate(wallPrefabs[wallNumber], wallSizeZ - generateWallSizeZ);

                    //残りの床の長さを取得
                    generateWallSizeZ -= generateWall.GetComponent<WallChip>().sizeZ;
                }

                mapGenerator_.movedDistance -= wallSizeZ;
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
