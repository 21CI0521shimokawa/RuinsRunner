using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //床サイズ
    float prefabNoSetFloorSizeZ_;

    //itemPrefabの保存場所
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
            //prefabを決める 仮
            int prefabNumber = Random.Range(0, itemPrefabs_.Count);

            //prefabを設置
            GameObject generatePrefab = mapGenerator_.Generate(itemPrefabs_[prefabNumber], setPrefabSizeZ);

            //今置いたprefabの長さを引く
            prefabNoSetFloorSizeZ_ -= itemPrefabs_[prefabNumber].GetComponent<NewMapChip>().sizeZ;

            setPrefabSizeZ += itemPrefabs_[prefabNumber].GetComponent<NewMapChip>().sizeZ;
        }


        //新しい候補
        //置いた個数に応じてコインにするか肉にするか決める
        //2mおきくらいになるようにPrefabを生成する
        //X軸ランダムに移動させる
        //そこに床がなかったらちょっとY+に移動させる
        //おいたアイテムカウンタみたいなものを増やす

    }

    public bool IsGenerate()
    {
        return prefabNoSetFloorSizeZ_ >= 0.0f;
    }
}
