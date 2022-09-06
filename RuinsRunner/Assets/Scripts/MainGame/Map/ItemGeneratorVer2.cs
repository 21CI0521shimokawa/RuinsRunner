using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorVer2 : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //床サイズ
    [SerializeField] float prefabNoSetFloorSizeZ_;

    //itemの保存場所
    [SerializeField] GameObject coinPrefabs_;
    [SerializeField] GameObject meetPrefabs_;

    //置いたアイテムの個数
    int setItemCount_;


    //置くX座標テーブル
    [SerializeField] float[] setPositionXTable_;

    //連続で置く時のテーブル番号
    int setPositionTableNum_;

    //何個目まで連続で置くか
    int consecutiveSetQuantity_;

    //アイテムの間隔
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
            Debug.LogWarning("アイテムの生成間隔が不正です");
            return;
        }


        float oldPrefabNoSetFloorSizeZ = prefabNoSetFloorSizeZ_;

        prefabNoSetFloorSizeZ_ += _floorSizeZ;
        
        //置いたアイテムの長さ
        float setPrefabSizeZ = 0;

        while (IsGenerate())
        {
            //prefabを決める 仮
            GameObject selectPrefab;
            if(setItemCount_ % 15 == 14)
            {
                selectPrefab = meetPrefabs_;
            }
            else
            {
                selectPrefab = coinPrefabs_;
            }

            //prefabを設置


            //GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, setPrefabSizeZ);
            //GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, -oldPrefabNoSetFloorSizeZ + _floorSizeZ - itemSetInterval_ + setPrefabSizeZ);
            GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, -oldPrefabNoSetFloorSizeZ + setPrefabSizeZ);

            //X軸移動させる
            Vector3 newPotision = generatePrefab.transform.position;
            newPotision.x = SetPositionX();

            newPotision.y = SetPositionY(newPotision, generatePrefab);

            generatePrefab.transform.position = newPotision;

            //間隔を引く
            prefabNoSetFloorSizeZ_ -= itemSetInterval_;

            setPrefabSizeZ += itemSetInterval_;

            ++setItemCount_; 
        }
    }


    float SetPositionX()
    {
        //連続で置く個数だったら
        if(consecutiveSetQuantity_ > setItemCount_)
        {
            return setPositionXTable_[setPositionTableNum_];
        }

        //テーブルの中から一つランダムで決める
        int newSetPositionTableNum = Random.Range(0, setPositionXTable_.Length);
        
        //前回と同じ場所には置かない
        while(setPositionTableNum_ == newSetPositionTableNum)
        {
            newSetPositionTableNum = Random.Range(0, setPositionXTable_.Length);
        }
        setPositionTableNum_ = newSetPositionTableNum;

        //何個目まで連続で置くか決める
        consecutiveSetQuantity_ += Random.Range(1, 6);  //1個~5個まで

        return setPositionXTable_[setPositionTableNum_];
    }

    //そこに床がなかったらちょっとY+に移動させる
    float SetPositionY(Vector3 _position, GameObject _generateItem)
    {
        Vector3 origin = _position; // 原点
        origin.y += 2;
        Vector3 direction = Vector3.down; // ベクトル

        Ray ray = new Ray(origin, direction); // Rayを生成;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 0.01f); // 緑色で可視化

        RaycastHit[] hits = Physics.RaycastAll(ray, 10);

        bool isHit = false;
        foreach (RaycastHit hit in hits) // もしRayを投射して何らかのコライダーに衝突したら
        {
            //トリガーだったら除外する
            if (hit.collider.isTrigger == true) { continue; }

            //自身（今生成したアイテム）だったら除外する
            if(hit.collider.gameObject == _generateItem) { continue; }

            //一部のTagがついてるゲームオブジェクトは除外する
            string hitGameObjectTagName = hit.collider.gameObject.tag;
            if (hitGameObjectTagName == "") { continue; }
            //必要に応じて追加

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
