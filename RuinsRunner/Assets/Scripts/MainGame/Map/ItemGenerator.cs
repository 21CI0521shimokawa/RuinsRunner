using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //���T�C�Y
    float prefabNoSetFloorSizeZ_;

    //itemPrefab�̕ۑ��ꏊ
    [SerializeField] List<GameObject> itemPrefabs_;

    // Start is called before the first frame update
    void Start()
    {
        prefabNoSetFloorSizeZ_ = 0.0f;
        mapGenerator_ = GetComponent<NewMapGenerator>();
    }

    public void Generate(float _floorSizeZ)
    {
        prefabNoSetFloorSizeZ_ += _floorSizeZ;

        float setPrefabSizeZ = 0.0f;
        while (IsGenerate())
        {
            //prefab�����߂� ��
            int prefabNumber = Random.Range(0, itemPrefabs_.Count);

            //prefab��ݒu
            GameObject generatePrefab = mapGenerator_.Generate(itemPrefabs_[prefabNumber], setPrefabSizeZ);

            //���u����prefab�̒���������
            prefabNoSetFloorSizeZ_ -= itemPrefabs_[prefabNumber].GetComponent<NewMapChip>().sizeZ;

            setPrefabSizeZ += itemPrefabs_[prefabNumber].GetComponent<NewMapChip>().sizeZ;
        }


        //�V�������
        //�u�������ɉ����ăR�C���ɂ��邩���ɂ��邩���߂�
        //2m�������炢�ɂȂ�悤��Prefab�𐶐�����
        //X�������_���Ɉړ�������
        //�����ɏ����Ȃ������炿�����Y+�Ɉړ�������
        //�������A�C�e���J�E���^�݂����Ȃ��̂𑝂₷

    }

    public bool IsGenerate()
    {
        return prefabNoSetFloorSizeZ_ >= 0.0f;
    }
}
