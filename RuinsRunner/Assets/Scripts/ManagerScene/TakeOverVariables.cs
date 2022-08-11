using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOverVariables : MonoBehaviour
{
    private void Start()
    {
        score_ = 0;
    }
    //シーンをまたいで保持したい変数等の宣言を追加していく
    //ユーザー定義のクラスなども追加する場合は、他のものも含めgetsetプロパティでもつけてやったほうが安全だとは思う
    int score_;
    int coinCount_;

    public int Score
    {
        get { return score_; }
        set { score_ = value; }
    }

    public int CoinCount
    {
        get { return coinCount_; }
        set { coinCount_ = value; }
    }
}
