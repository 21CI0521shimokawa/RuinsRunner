using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGenerator : MonoBehaviour
{
    StateMachine state_;

    //マップチップの生成フォルダ（親）
    [SerializeField] GameObject mapFolder_;
    //マップチップの生成位置
    [SerializeField] Vector3 createPosition_;
    //一番直近に生成したマップチップの情報を保存する変数
    MapInformation latestMapInfo_;

    //移動距離
    float movedDistance_;
    public float movedDistance
    {
        get
        {
            return movedDistance_;
        }
        set
        {
            if (value > 0) movedDistance_ = value;
        }
    }

    //シーンマネージャーから持ってくるようになったらSerialize属性を消していい
    [SerializeField] float remainTime_ = 10f;


    //StateNormal
    //wallPrefabの保存場所
    [SerializeField] List<GameObject> wallPrefabs_;
    public List<GameObject> wallPrefabs
    {
        get
        {
            return wallPrefabs;
        }
    }
    //floorPrefabの保存場所
    [SerializeField] List<GameObject> floorPrefabs_;
    public List<GameObject> floorPrefabs
    {
        get
        {
            return floorPrefabs;
        }
    }
    //ゴールのマップチップ
    [SerializeField] GameObject goalMapPrefab_;
    public GameObject goalMapPrefab
    {
        get
        {
            return goalMapPrefab_;
        }
    }



    private void Awake()
    {
        movedDistance_ = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        state_ = new StateMachine(new NewMapGeneratorState_Normal());
        movedDistance_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //残り時間を更新
        //後々、シーンマネージャーからもらってくるようにして、シーン内で一つの数値を共有したい
        remainTime_ -= Time.deltaTime;
        if (remainTime_ <= 0f)
        {
            remainTime_ = 0f;
        }

        movedDistance_ += MainGameConst.moveSpeed * Time.deltaTime;
        state_.Update(gameObject);
    }

    public GameObject Generate(GameObject _mapChip, float _generateOffsetZ = 0.0f)
    {
        //マップチップの位置に応じて生成位置を微調整する
        Vector3 createPosition = createPosition_;
        createPosition.z -= movedDistance_ - 4.0f;

        //offset
        createPosition.z += _generateOffsetZ;

        return Instantiate(_mapChip, createPosition, Quaternion.identity, mapFolder_.transform);
    }

    public bool IsGenerate()
    {
        //todo:アタッチして試運転が終わったら4.0fをgameObject.GetComponetn<RectTransform>().sizeDelta.zで取得したい
        return movedDistance_ >= 4.0f;
    }

    public bool IsGoalGenerate()
    {
        return remainTime_ <= 0;
    }
}
