using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMG1 : MonoBehaviour
{
    [SerializeField] float CameramoveAdjust = 2f;
    //[SerializeField] GameObject Target;
    [SerializeField] GameObject Camera;
    //[SerializeField] GameObject[] NextPositions;
    //[SerializeField] PathType PathType;
    //[SerializeField] Ease EaseType;
    //[SerializeField] float MoveTime;
    //Tweener tweenerMain;
    //Tweener tweenerSub = null;
    private void Update()
    {
        if(Camera.transform.position.y > 11)
        {
            Camera.transform.position -= new Vector3(0, Time.deltaTime * CameramoveAdjust, 0);
        }
/*        Vector3[] path = NextPositions.Select(x => x.transform.position).ToArray();
        Camera.transform.position = path[0];
        tweenerMain = Camera.transform.DOMoveY(path[1].y, MoveTime)
    .OnStart(() =>
    {//実行開始時のコールバック
        Camera.transform.DORotate(Vector3.right * 15f, 1f);
    });

        if (Camera.transform.position.y > Target.transform.position.y + 5)
        {
            tweenerMain.Pause();

            if (tweenerSub == null)
            {
                tweenerSub = transform.DOMoveY(Target.transform.position.y + 5, 0.1f)
                    .OnComplete(() =>
                    {
                        tweenerMain.Play();
                        tweenerSub.Kill();
                        tweenerSub = null;
                    });
            }
            else
            {
                //tweenerSub.ChangeEndValue(new Vector3(transform.position.x, Target.transform.position.y, transform.position.z));
            }
        }


        if (tweenerSub == null) return;
        if ((Camera.transform.position.y + 5 - Target.transform.position.y) * (Camera.transform.position.y + 5 - Target.transform.position.y) < 0.01f)
        {
            tweenerSub.Complete();
        }*/
    }

}
