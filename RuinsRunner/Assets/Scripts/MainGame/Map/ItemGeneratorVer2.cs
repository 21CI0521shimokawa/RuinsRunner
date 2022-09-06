using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorVer2 : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //���T�C�Y
    [SerializeField] float prefabNoSetFloorSizeZ_;

    //item�̕ۑ��ꏊ
    [SerializeField] GameObject coinPrefabs_;
    [SerializeField] GameObject meetPrefabs_;

    //�u�����A�C�e���̌�
    int setItemCount_;


    //�u��X���W�e�[�u��
    [SerializeField] float[] setPositionXTable_;

    //�A���Œu�����̃e�[�u���ԍ�
    int setPositionTableNum_;

    //���ڂ܂ŘA���Œu����
    int consecutiveSetQuantity_;

    //�A�C�e���̊Ԋu
    [SerializeField] float itemSetInterval_;

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator_ = GetComponent<NewMapGenerator>();

        setItemCount_ = 0;
        setPositionTableNum_ = -1;
        consecutiveSetQuantity_ = 0;
    }

    public void Generate(float _floorSizeZ)
    {
        if(itemSetInterval_ <= 0)
        {
            Debug.LogWarning("�A�C�e���̐����Ԋu���s���ł�");
            return;
        }


        float oldPrefabNoSetFloorSizeZ = prefabNoSetFloorSizeZ_;

        prefabNoSetFloorSizeZ_ += _floorSizeZ;
        
        //�u�����A�C�e���̒���
        float setPrefabSizeZ = 0;

        while (IsGenerate())
        {
            //prefab�����߂� ��
            GameObject selectPrefab;
            if(setItemCount_ % 15 == 14)
            {
                selectPrefab = meetPrefabs_;
            }
            else
            {
                selectPrefab = coinPrefabs_;
            }

            //prefab��ݒu


            //GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, setPrefabSizeZ);
            //GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, -oldPrefabNoSetFloorSizeZ + _floorSizeZ - itemSetInterval_ + setPrefabSizeZ);
            GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, -oldPrefabNoSetFloorSizeZ + setPrefabSizeZ);

            //X���ړ�������
            Vector3 newPotision = generatePrefab.transform.position;
            newPotision.x = SetPositionX();

            newPotision.y = SetPositionY(newPotision, generatePrefab);

            generatePrefab.transform.position = newPotision;

            //�Ԋu������
            prefabNoSetFloorSizeZ_ -= itemSetInterval_;

            setPrefabSizeZ += itemSetInterval_;

            ++setItemCount_; 
        }
    }


    float SetPositionX()
    {
        //�A���Œu������������
        if(consecutiveSetQuantity_ > setItemCount_)
        {
            return setPositionXTable_[setPositionTableNum_];
        }

        //�e�[�u���̒����������_���Ō��߂�
        int newSetPositionTableNum = Random.Range(0, setPositionXTable_.Length);
        
        //�O��Ɠ����ꏊ�ɂ͒u���Ȃ�
        while(setPositionTableNum_ == newSetPositionTableNum)
        {
            newSetPositionTableNum = Random.Range(0, setPositionXTable_.Length);
        }
        setPositionTableNum_ = newSetPositionTableNum;

        //���ڂ܂ŘA���Œu�������߂�
        consecutiveSetQuantity_ += Random.Range(1, 6);  //1��~5�܂�

        return setPositionXTable_[setPositionTableNum_];
    }

    //�����ɏ����Ȃ������炿�����Y+�Ɉړ�������
    float SetPositionY(Vector3 _position, GameObject _generateItem)
    {
        Vector3 origin = _position; // ���_
        origin.y += 2;
        Vector3 direction = Vector3.down; // �x�N�g��

        Ray ray = new Ray(origin, direction); // Ray�𐶐�;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 0.01f); // �ΐF�ŉ���

        RaycastHit[] hits = Physics.RaycastAll(ray, 10);

        bool isHit = false;
        foreach (RaycastHit hit in hits) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
        {
            //�g���K�[�������珜�O����
            if (hit.collider.isTrigger == true) { continue; }

            //���g�i�����������A�C�e���j�������珜�O����
            if(hit.collider.gameObject == _generateItem) { continue; }

            //�ꕔ��Tag�����Ă�Q�[���I�u�W�F�N�g�͏��O����
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { continue; }
            //�K�v�ɉ����Ēǉ�

            isHit = true;
            break;
        }

        return isHit ? 1.0f : 2.5f;
    }


    public bool IsGenerate()
    {
        return prefabNoSetFloorSizeZ_ > 0.0f;
    }
}
