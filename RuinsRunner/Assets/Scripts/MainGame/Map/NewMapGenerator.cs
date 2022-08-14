using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGenerator : MonoBehaviour
{
    StateMachine state_;

    ItemGenerator itemGenerator_;

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

    //松明
    [SerializeField] GameObject torchPrefab_;
    public GameObject torchPrefab
    {
        get
        {
            return torchPrefab_;
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
    public float remainTime
    {
        get
        {
            return remainTime_;
        }
    }

    //生成したPrefabList
    static List<GameObject> generatedPrefabsList_ = new List<GameObject>();
    public static List<GameObject> generatedPrefabsList
    {
        get
        {
            return generatedPrefabsList_;
        }
    }


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

    //松明壁
    [SerializeField] GameObject wallTorchPrefab_;
    public GameObject wallTorchPrefab
    {
        get
        {
            return wallTorchPrefab_;
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

    //エネミーアタックのマップチップ
    [SerializeField] GameObject enemyAttackMapPrefab_;
    public GameObject enemyAttackMapPrefab
    {
        get
        {
            return enemyAttackMapPrefab_;
        }
    }

    //穴避けのマップチップ
    [SerializeField] GameObject jumpHoleMapPrefab_;
    public GameObject jumpHoleMapPrefab
    {
        get
        {
            return jumpHoleMapPrefab_;
        }
    }


    //他のエントリーと時間がかぶらないように要調整
    [Tooltip("エネミーアタックに入る経過時間")]
    public int EntryTimeEnemyAttack;
    
    //他のエントリーと時間がかぶらないように要調整
    [Tooltip("穴避けに入る経過時間")]
    public int EntryTimeJumpHole;

    //経過時間
    private float passedTime_;
    [HideInInspector]
    public float passedTime
    {
        get
        {
            return passedTime_;
        }
    }

    //エネミーアタックを呼び出したか
    private bool isCalledEnemyAttack_;

    [HideInInspector]
    public bool isCalledEnemyAttack
    {
        get
        {
            return isCalledEnemyAttack_;
        }
        set
        {
            isCalledEnemyAttack_ = value;
        }
    }
    
    //穴避けを呼び出したか
    private bool isCalledJumpHole_;

    [HideInInspector]
    public bool isCalledJumpHole
    {
        get
        {
            return isCalledJumpHole_;
        }
        set
        {
            isCalledJumpHole_ = value;
        }
    }

    private void Awake()
    {
        movedDistance_ = 0;
        passedTime_ = 0;
        isCalledEnemyAttack_ = false;
        isCalledJumpHole_ = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemGenerator_ = GetComponent<ItemGenerator>();

        state_ = new StateMachine(new NewMapGeneratorState_Normal());
        movedDistance_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //残り時間を更新
        //後々、シーンマネージャーからもらってくるようにして、シーン内で一つの数値を共有したい
        passedTime_ += Time.deltaTime;
        remainTime_ -= Time.deltaTime;
        if (remainTime_ <= 0f)
        {
            remainTime_ = 0f;
        }

        movedDistance_ += MainGameConst.moveSpeed * MoveLooksLikeRunning.moveMagnification * Time.deltaTime;

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

        return GameObjectInstantiateOrReuse(_mapChip, createPosition);
    }


    //オブジェクトの再利用
    GameObject GameObjectInstantiateOrReuse(GameObject _mapChip, Vector3 _createPosition)
    {
        //今まで生成したオブジェクトのリストを取得
        foreach(GameObject generatedObject in generatedPrefabsList_)
        {
            //そのオブジェクトの名前と今生成しようとしているPrefabの名前が同じなら
            if(generatedObject.name == _mapChip.name)
            {
                //そのオブジェクトがアクティブでないなら
                if (!generatedObject.activeInHierarchy)
                {
                    //再利用
                    generatedObject.SetActive(true);
                    generatedObject.transform.position = _createPosition;
                    generatedObject.transform.rotation = Quaternion.identity;

                    //子供もアクティブにする
                    var children = new Transform[generatedObject.transform.childCount];

                    for (var i = 0; i < children.Length; ++i)
                    {
                        generatedObject.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    return generatedObject;
                }
            }
        }

        //生成
        GameObject returnGameObject = Instantiate(_mapChip, _createPosition, Quaternion.identity, mapFolder_.transform);
        returnGameObject.name = _mapChip.name;
        generatedPrefabsList_.Add(returnGameObject);    //リストに追加

        return returnGameObject;
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
        return movedDistance_ >= latestFloorInfo_.sizeZ;
    }

    public bool IsGoalGenerate()
    {
        return remainTime_ <= 0;
    }

    public void ItemGenerator(int _floorSizeZ)
    {
        if(itemGenerator_ != null)
        {
            //生成用関数を呼び出す
        }
    }
}
