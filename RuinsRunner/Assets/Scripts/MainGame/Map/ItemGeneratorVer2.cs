using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorVer2 : MonoBehaviour
{
    NewMapGenerator mapGenerator_;

    //床サイズ
    float prefabNoSetFloorSizeZ_;

    //itemの保存場所
    [SerializeField] GameObject coinPrefabs_;
    [SerializeField] GameObject meetPrefabs_;

    //置いたアイテムの個数
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
            GameObject generatePrefab = mapGenerator_.Generate(selectPrefab, setPrefabSizeZ);

            //X軸ランダムに移動させる
            Vector3 newPotision = generatePrefab.transform.position;
            newPotision.x = Random.Range(-5.0f, 5.0f);

            //そこに床がなかったらちょっとY+に移動させる
            newPotision.y = 2.5f;

            Vector3 origin = newPotision; // 原点
            origin.y -= 1;
            Vector3 direction = Vector3.down; // ベクトル

            Ray ray = new Ray(origin, direction); // Rayを生成;
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.green, 0.01f); // 緑色で可視化

            RaycastHit[] hits = Physics.RaycastAll(ray, 5);

            bool isHit = false;
            foreach (RaycastHit hit in hits) // もしRayを投射して何らかのコライダーに衝突したら
            {
                //トリガーだったら除外する
                if (hit.collider.isTrigger == true) { continue; }

                //一部のTagがついてるゲームオブジェクトは除外する
                string hitGameObjectTagName = hit.collider.gameObject.tag;
                if (hitGameObjectTagName == "") { continue; }
                //必要に応じて追加

                isHit = true;
                break;
            }
            
            if(isHit)
            {
                newPotision.y = 1;
            }

            generatePrefab.transform.position = newPotision;

            //今置いたprefabの長さを引く
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
