using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //マップチップの生成位置
    [SerializeField] Vector3 createPosition;
    //マップチップprefabの保存場所
    [SerializeField] List<GameObject> mapPrefabs;
    //一番直近に生成したマップチップの情報を保存する変数
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
        //todo:アタッチして試運転が終わったら4.0fをgameObject.GetComponetn<RectTransform>().sizeDelta.zで取得したい
        if(movedDistance >= 4.0f) 
        {
            //生成可能なマップチップを保持しておく器
            List<GameObject> candidateMapPrefab = new List<GameObject>();

            //latestとつながるように生成してもいいマップを判断
            foreach(GameObject mapPrefab in mapPrefabs)
            {
                if (latestMapInfo.IsConnectBack() == mapPrefab.GetComponent<MapChip>().IsConnectFront())
                {
                    candidateMapPrefab.Add(mapPrefab);
                }
            }

            //prefabの生成
            int randomIndex = Random.Range(0, candidateMapPrefab.Count);
            Instantiate(candidateMapPrefab[randomIndex], createPosition, Quaternion.identity);

            //latestに情報を更新
            latestMapInfo = candidateMapPrefab[randomIndex].GetComponent<MapChip>().GetMapInformation();

            movedDistance = 0;
        }
    }
}
