using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silhouette : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Monster;

    private void Update()
    {
        ChangeColorOfGameObject(Player, Color.black);
        ChangeColorOfGameObject(Monster, Color.black);
    }
    /// <summary>
    /// 入力されたオブジェクト及びその子、全ての色を変える
    /// </summary>
    /// <param name="targetObject">色を変更したいオブジェクト</param>
    /// <param name="color">設定したい色</param>
    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {

        //入力されたオブジェクトのRendererを全て取得し、さらにそのRendererに設定されている全Materialの色を変える
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            foreach (Material material in targetRenderer.materials)
            {
                material.color = color;
            }
        }

        //入力されたオブジェクトの子にも同様の処理を行う
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }

    }
}
