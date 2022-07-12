using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;


public class CameraMG1 : MonoBehaviour
{
    [SerializeField] float MoveTime;
    [SerializeField] float LimitSpeed;
    [SerializeField] float AfterMoving; //Debug用
    [SerializeField] float LimitPositionY;
    [SerializeField] GameObject Player;
    [SerializeField] Rigidbody PlayerRigidboby;
    [SerializeField] Ease EaseType;


    private void Start()
    {
        DoMove();
        this.UpdateAsObservable()
            .Select(_ => Mathf.Abs(PlayerRigidboby.velocity.magnitude) >= LimitSpeed)
            .DistinctUntilChanged()
            .Where(y => y)
            .Take(5)
            .Subscribe(_ =>
            {
                StartCoroutine(AddSpeed());
            });
    }

    void DoMove()
    {
        this.UpdateAsObservable()
            .Select(_=>this.transform.position.y>=LimitPositionY)
            .Where(y=>y)
            .Subscribe(_ =>
            {
                transform.position += new Vector3(0,-2, 0) * Time.deltaTime;
            });
    }
    #region コルーチン
    private IEnumerator AddSpeed()
    {
        var MoveTo = Player.transform.position.y -1;
        this.transform.DOMoveY(MoveTo, 1)
            .OnComplete(() =>
            {
                AfterMoving =this.transform.position.y;
            });
        yield return new WaitForSeconds(3);
        Debug.Log("加速終わり");
        yield break;
    }
    #endregion
}