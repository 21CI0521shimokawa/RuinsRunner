using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //マップチップの生成位置
    [SerializeField] Vector3 createPosition_;
    //マップチップprefabの保存場所
    [SerializeField] List<GameObject> mapPrefabs_;
    //ゴールのマップチップ
    [SerializeField] GameObject goalMapPrefab_;
    //一番直近に生成したマップチップの情報を保存する変数
    MapInformation latestMapInfo_;

    float movedDistance_;
    //シーンマネージャーから持ってくるようになったらSerialize属性を消していい
    [SerializeField]float remainTime_ = 10f;

    private void Awake()
    {
        movedDistance_ = 0;
        latestMapInfo_ = new MapInformation();
    }

    private void Update()
    {
        //残り時間を更新
        //後々、シーンマネージャーからもらってくるようにして、シーン内で一つの数値を共有したい
        remainTime_ -= Time.deltaTime;

        if (remainTime_ <= 0f)
        {
            remainTime_ = 0f;
        }
            movedDistance_ += MainGameConst.moveSpeed * Time.deltaTime;
        //todo:アタッチして試運転が終わったら4.0fをgameObject.GetComponetn<RectTransform>().sizeDelta.zで取得したい
        if (movedDistance_ >= 4.0f)
        {
            if (remainTime_ > 0)
            {
                //生成可能なマップチップを保持しておく器
                List<GameObject> candidateMapPrefab = new List<GameObject>();

                //latestとつながるように生成してもいいマップを判断
                foreach (GameObject mapPrefab in mapPrefabs_)
                {
                    if (latestMapInfo_.IsConnectBack() == mapPrefab.GetComponent<MapChip>().IsConnectFront())
                    {
                        candidateMapPrefab.Add(mapPrefab);
                    }
                }

                //prefabの生成
                int randomIndex = Random.Range(0, candidateMapPrefab.Count);
                Instantiate(candidateMapPrefab[randomIndex], createPosition_, Quaternion.identity);

                //latestに情報を更新
                latestMapInfo_ = candidateMapPrefab[randomIndex].GetComponent<MapChip>().GetMapInformation();

                movedDistance_ = 0;
            }
            else
            {
                Instantiate(goalMapPrefab_, createPosition_, Quaternion.identity);
                enabled = false;
            }
        }
    }
}
