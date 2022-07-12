using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOverVariables : MonoBehaviour
{
    private void Start()
    {
        score = 0;
    }
    //シーンをまたいで保持したい変数等の宣言を追加していく
    //ユーザー定義のクラスなども追加する場合は、他のものも含めgetsetプロパティでもつけてやったほうが安全だとは思う
    int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
}
