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

    //一番直近に生成した床チップの情報を保存する変数
    [SerializeField, Tooltip("生成するマップチップとくっつく床データを入れておく")] FloorChip latestFloorInfo_;
    public NewMapChip latestFloorInfo
    {
        get
        {
            return latestFloorInfo_;
        }
    }

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
            return wallPrefabs_;
        }
    }
    //floorPrefabの保存場所
    [SerializeField] List<GameObject> floorPrefabs_;
    public List<GameObject> floorPrefabs
    {
        get
        {
            return floorPrefabs_;
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

        if(state_ != null)
        {
            state_.Update(gameObject);
        }
    }

    //生成
    public GameObject Generate(GameObject _mapChip, float _generateOffsetZ = 0.0f)
    {
        //マップチップの位置に応じて生成位置を微調整する
        Vector3 createPosition = createPosition_;

        //offset
        createPosition.z += _generateOffsetZ;

        //生成するものが床ならlatestFloorInfoを更新
        FloorChip floorData = _mapChip.GetComponent<FloorChip>();
        if(floorData != null)
        {
            latestFloorInfo_ = floorData;
        }

        return Instantiate(_mapChip, createPosition, Quaternion.identity, mapFolder_.transform);
    }

    //床回転
    public void FloorRotate(GameObject _mapChip)
    {
        FloorChip chipData = _mapChip.GetComponent<FloorChip>();

        if(chipData != null)
        {
            if(chipData.canRotate)
            {
                int randomValue = Random.Range(0, 100);

                if(randomValue > 50)
                {
                    //回転
                    _mapChip.transform.Rotate(new Vector3(0, 180, 0));

                    //位置調整
                    _mapChip.transform.position += new Vector3(0, 0, chipData.sizeZ);
                }
            }
        }
        else
        {
            Debug.LogWarning("FloorChipが見つかりません！！");
        }
    }

    //壁移動
    public void WallMove(GameObject _mapChip, bool _isRightWall)
    {
        //右の壁なら回転する必要がないので終了
        if(!_isRightWall)
        {
            WallChip chipData = _mapChip.GetComponent<WallChip>();

            //回転
            _mapChip.transform.Rotate(new Vector3(0, 180, 0));

            //位置調整
            _mapChip.transform.position += new Vector3(0, 0, chipData.sizeZ);
        }
    }

    public bool IsWallPlacementCheck(GameObject _mapChip, bool _isRightWall)
    {
        if (_isRightWall)
        {
            return _mapChip.GetComponent<WallChip>().isPlacementRight;
        }
        else
        {
            return _mapChip.GetComponent<WallChip>().isPlacementLeft;
        }
    }

    public bool IsGenerate()
    {
        //todo:アタッチして試運転が終わったら4.0fをgameObject.GetComponetn<RectTransform>().sizeDelta.zで取得したい
        return movedDistance_ >= latestFloorInfo_.sizeZ;
    }

    public bool IsGoalGenerate()
    {
        return remainTime_ <= 0;
    }
}
