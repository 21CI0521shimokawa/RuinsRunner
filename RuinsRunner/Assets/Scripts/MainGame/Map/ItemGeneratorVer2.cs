using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorVer2 : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //���T�C�Y
    float prefabNoSetFloorSizeZ_;

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

    // Start is called before the first frame update
    void Start()
    {
        prefabNoSetFloorSizeZ_ = 0.0f;
        mapGenerator_ = GetComponent<NewMapGenerator>();

        setItemCount_ = 0;
        setPositionTableNum_ = -1;
        consecutiveSetQuantity_ = 0;
    }

    public void Generate(float _floorSizeZ)
    {
        prefabNoSetFloorSizeZ_ += _floorSizeZ;

        float setPrefabSizeZ = 0.0f;
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
            GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, setPrefabSizeZ);

            //X���ړ�������
            Vector3 newPotision = generatePrefab.transform.position;
            newPotision.x = SetPositionX();

            //�����ɏ����Ȃ������炿�����Y+�Ɉړ�������
            newPotision.y = 2.5f;

            Vector3 origin = newPotision; // ���_
            origin.y -= 1;
            Vector3 direction = Vector3.down; // �x�N�g��

            Ray ray = new Ray(origin, direction); // Ray�𐶐�;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 0.01f); // �ΐF�ŉ���

            RaycastHit[] hits = Physics.RaycastAll(ray, 10);

            bool isHit = false;
            foreach (RaycastHit hit in hits) // ����Ray�𓊎˂��ĉ��炩�̃R���C�_�[�ɏՓ˂�����
            {
                //�g���K�[�������珜�O����
                if (hit.collider.isTrigger == true) { continue; }

                //�ꕔ��Tag�����Ă�Q�[���I�u�W�F�N�g�͏��O����
                string hitGameObjectTagName = hit.collider.gameObject.tag;
                if (hitGameObjectTagName == "") { continue; }
                //�K�v�ɉ����Ēǉ�

                isHit = true;
                break;
            }
            
            if(isHit)
            {
                newPotision.y = 1;
            }

            generatePrefab.transform.position = newPotision;

            //�Ԋu������
            prefabNoSetFloorSizeZ_ -= 4;

            setPrefabSizeZ += 4;

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


    public bool IsGenerate()
    {
        return prefabNoSetFloorSizeZ_ > 0.0f;
    }
}
