using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapGenerator : MonoBehaviour
{
    StateMachine state_;

    ItemGenerator itemGenerator_;

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

    //�G�l�~�[�A�^�b�N�̃}�b�v�`�b�v
    [SerializeField] GameObject enemyAttackMapPrefab_;
    public GameObject enemyAttackMapPrefab
    {
        get
        {
            return enemyAttackMapPrefab_;
        }
    }

    //�������̃}�b�v�`�b�v
    [SerializeField] GameObject jumpHoleMapPrefab_;
    public GameObject jumpHoleMapPrefab
    {
        get
        {
            return jumpHoleMapPrefab_;
        }
    }


    //���̃G���g���[�Ǝ��Ԃ����Ԃ�Ȃ��悤�ɗv����
    [Tooltip("�G�l�~�[�A�^�b�N�ɓ���o�ߎ���")]
    public int EntryTimeEnemyAttack;
    
    //���̃G���g���[�Ǝ��Ԃ����Ԃ�Ȃ��悤�ɗv����
    [Tooltip("�������ɓ���o�ߎ���")]
    public int EntryTimeJumpHole;

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

    //�G�l�~�[�A�^�b�N���Ăяo������
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
    
    //���������Ăяo������
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

        return Instantiate(_mapChip, createPosition, Quaternion.identity, mapFolder_.transform);
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
        //todo:�A�^�b�`���Ď��^�]���I�������4.0f��gameObject.GetComponetn<RectTransform>().sizeDelta.z�Ŏ擾������
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
            //�����p�֐����Ăяo��
        }
    }
}
