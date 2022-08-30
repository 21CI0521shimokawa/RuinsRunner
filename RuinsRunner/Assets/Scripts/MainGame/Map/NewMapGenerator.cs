using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGenerator : MonoBehaviour
{
    StateMachine state_;

    ItemGeneratorVer2 itemGenerator_;

    //�}�b�v�`�b�v�̐����t�H���_�i�e�j
    [SerializeField] GameObject mapFolder_;
    //�}�b�v�`�b�v�̐����ʒu
    [SerializeField] Vector3 createPosition_;

    //��Ԓ��߂ɐ����������`�b�v�̏���ۑ�����ϐ�
    [SerializeField, Tooltip("��������}�b�v�`�b�v�Ƃ��������f�[�^�����Ă���")] FloorChip latestFloorInfo_;
    public NewMapChip latestFloorInfo
    {
        get
        {
            return latestFloorInfo_;
        }
    }

    //����
    [SerializeField] GameObject torchPrefab_;
    public GameObject torchPrefab
    {
        get
        {
            return torchPrefab_;
        }
    }

    //�ړ�����
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

    //�V�[���}�l�[�W���[���玝���Ă���悤�ɂȂ�����Serialize�����������Ă���
    [SerializeField] float remainTime_ = 10f;
    public float remainTime
    {
        get
        {
            return remainTime_;
        }
    }

    //��������PrefabList
    static List<GameObject> generatedPrefabsList_ = new List<GameObject>();
    public static List<GameObject> generatedPrefabsList
    {
        get
        {
            return generatedPrefabsList_;
        }
    }


    //StateNormal
    //wallPrefab�̕ۑ��ꏊ
    [SerializeField] List<GameObject> wallPrefabs_;
    public List<GameObject> wallPrefabs
    {
        get
        {
            return wallPrefabs_;
        }
    }
    //floorPrefab�̕ۑ��ꏊ
    [SerializeField] List<GameObject> floorPrefabs_;
    public List<GameObject> floorPrefabs
    {
        get
        {
            return floorPrefabs_;
        }
    }

    //������
    [SerializeField] GameObject wallTorchPrefab_;
    public GameObject wallTorchPrefab
    {
        get
        {
            return wallTorchPrefab_;
        }
    }

    //�S�[���̃}�b�v�`�b�v
    [SerializeField] GameObject goalMapPrefab_;
    public GameObject goalMapPrefab
    {
        get
        {
            return goalMapPrefab_;
        }
    }


    [Header("�~�j�Q�[�����ĂԂ��߂ɐݒ肷�鍀��")]
    [Header("���ڐ��͑S�ē����ɂ��邱��")]
    //�}�b�v�`�b�v���Ăяo���o�ߎ���
    [Tooltip("�~�j�Q�[���ɓ���o�ߎ���")]
    [SerializeField] int[] miniGameEntryTimes_;
    public int[] miniGameEntryTimes
    {
        get
        {
            return miniGameEntryTimes_;
        }
    }

    //�}�b�v�`�b�v
    [Tooltip("�~�j�Q�[����Prefab")]
    [SerializeField] GameObject[] miniGameMapPrefabs_;
    public GameObject[] miniGameMapPrefabs
    {
        get
        {
            return miniGameMapPrefabs_;
        }
    }

    //�~�j�Q�[����
    [Tooltip("�~�j�Q�[���̖��O\n��p��State�ɓ��ꂽ���Ƃ��ȂǂɎg��")]
    [SerializeField] string[] miniGameNames_;
    public string[] miniGameNames
    {
        get
        {
            return miniGameNames_;
        }
    }

    //�Ăяo�������ǂ���
    bool[] isCalledMiniGames_;
    public bool[] isCalledMiniGames
    {
        get
        {
            return isCalledMiniGames_;
        }
    }

    //�o�ߎ���
    private float passedTime_;
    [HideInInspector]
    public float passedTime
    {
        get
        {
            return passedTime_;
        }
    }

    //�G�l�~�[�A�^�b�N���I���������ǂ���
    bool endEnemyAttack_;
    public bool endEnemyAttack
    {
        get
        {
            return endEnemyAttack_;
        }
        set
        {
            endEnemyAttack_ = value;
        }
    }

    private void Awake()
    {
        movedDistance_ = 0;
        passedTime_ = 0;

        isCalledMiniGames_ = new bool[miniGameEntryTimes_.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        itemGenerator_ = GetComponent<ItemGeneratorVer2>();

        state_ = new StateMachine(new NewMapGeneratorState_Normal());
        //state_ = new StateMachine(new NewMapGeneratorState_EnemyAttack());

        generatedPrefabsList_ = new List<GameObject>();

        movedDistance_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�c�莞�Ԃ��X�V
        //��X�A�V�[���}�l�[�W���[���������Ă���悤�ɂ��āA�V�[�����ň�̐��l�����L������
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

    //����
    public GameObject Generate(GameObject _mapChip, float _generateOffsetZ = 0.0f)
    {
        //�}�b�v�`�b�v�̈ʒu�ɉ����Đ����ʒu�����������
        Vector3 createPosition = createPosition_;

        //offset
        createPosition.z += _generateOffsetZ;

        //����������̂����Ȃ�latestFloorInfo���X�V
        FloorChip floorData = _mapChip.GetComponent<FloorChip>();
        if(floorData != null)
        {
            latestFloorInfo_ = floorData;
        }

        return GameObjectInstantiateOrReuse(_mapChip, createPosition);
    }


    //�I�u�W�F�N�g�̍ė��p
    GameObject GameObjectInstantiateOrReuse(GameObject _mapChip, Vector3 _createPosition)
    {
        //���܂Ő��������I�u�W�F�N�g�̃��X�g���擾
        foreach(GameObject generatedObject in generatedPrefabsList_)
        {
            //���̃I�u�W�F�N�g�̖��O�ƍ��������悤�Ƃ��Ă���Prefab�̖��O�������Ȃ�
            if(generatedObject.name == _mapChip.name)
            {
                //���̃I�u�W�F�N�g���A�N�e�B�u�łȂ��Ȃ�
                if (!generatedObject.activeInHierarchy)
                {
                    //�ė��p
                    generatedObject.SetActive(true);
                    generatedObject.transform.position = _createPosition;
                    generatedObject.transform.rotation = Quaternion.identity;

                    //�q�����A�N�e�B�u�ɂ���
                    var children = new Transform[generatedObject.transform.childCount];

                    for (var i = 0; i < children.Length; ++i)
                    {
                        generatedObject.transform.GetChild(i).gameObject.SetActive(true);
                    }

                    return generatedObject;
                }
            }
        }

        //����
        GameObject returnGameObject = Instantiate(_mapChip, _createPosition, Quaternion.identity, mapFolder_.transform);
        returnGameObject.name = _mapChip.name;
        generatedPrefabsList_.Add(returnGameObject);    //���X�g�ɒǉ�

        return returnGameObject;
    }


    //����]
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
                    //��]
                    _mapChip.transform.Rotate(new Vector3(0, 180, 0));

                    //�ʒu����
                    _mapChip.transform.position += new Vector3(0, 0, chipData.sizeZ);
                }
            }
        }
        else
        {
            Debug.LogWarning("FloorChip��������܂���I�I");
        }
    }

    //�ǈړ�
    public void WallMove(GameObject _mapChip, bool _isRightWall)
    {
        //�E�̕ǂȂ��]����K�v���Ȃ��̂ŏI��
        if(!_isRightWall)
        {
            WallChip chipData = _mapChip.GetComponent<WallChip>();

            //��]
            _mapChip.transform.Rotate(new Vector3(0, 180, 0));

            //�ʒu����
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
            itemGenerator_.Generate(_floorSizeZ);
        }
    }
}
