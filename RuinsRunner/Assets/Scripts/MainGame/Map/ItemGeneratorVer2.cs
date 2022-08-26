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

    // Start is called before the first frame update
    void Start()
    {
        prefabNoSetFloorSizeZ_ = 0.0f;
        mapGenerator_ = GetComponent<NewMapGenerator>();

        setItemCount_ = 0;
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

            //X�������_���Ɉړ�������
            Vector3 newPotision = generatePrefab.transform.position;
            newPotision.x = Random.Range(-5.0f, 5.0f);

            //�����ɏ����Ȃ������炿�����Y+�Ɉړ�������
            newPotision.y = 2.5f;

            Vector3 origin = newPotision; // ���_
            origin.y -= 1;
            Vector3 direction = Vector3.down; // �x�N�g��

            Ray ray = new Ray(origin, direction); // Ray�𐶐�;
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.green, 0.01f); // �ΐF�ŉ���

            RaycastHit[] hits = Physics.RaycastAll(ray, 5);

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

            //���u����prefab�̒���������
            prefabNoSetFloorSizeZ_ -= 3;

            setPrefabSizeZ += 3;

            ++setItemCount_; 
        }
    }

    public bool IsGenerate()
    {
        return prefabNoSetFloorSizeZ_ >= 0.0f;
    }
}
