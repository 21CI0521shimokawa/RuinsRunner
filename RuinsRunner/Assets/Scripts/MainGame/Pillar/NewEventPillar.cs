using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEventPillar : MonoBehaviour
{
    [SerializeField] bool isFallDown = true;

    [SerializeField] float fallStartPosZ = 30.0f;
    [SerializeField] float fallEndPosZ = 20.0f;

    float betweenDistance;

    float startLocalPositionY;

    private void Start()
    {
        betweenDistance = fallStartPosZ - fallEndPosZ; //正の値にする為、開始Z - 終了Z
        startLocalPositionY = transform.localPosition.y;
    }

    private void Update()
    {
        if (isFallDown)
        {
            //指定範囲外では動かさない
            if (this.transform.position.z > fallStartPosZ ||
            this.transform.position.z < fallEndPosZ)
                return;

            //距離の割合と比例させて傾ける
            float distanceRate = (fallStartPosZ - this.transform.position.z) / betweenDistance; //距離の割合

            //回転
            //0.0f~0.6fを0.0f~1.0fに変換
            float rotateRate = Mathf.InverseLerp(0.0f, 0.6f, distanceRate);

            Vector3 localAngle = transform.localEulerAngles;
            localAngle.z = 90f * rotateRate;
            transform.localEulerAngles = localAngle;

            //落下
            //0.6f~1.0fを0.0f~1.0fに変換
            float fallRate = Mathf.InverseLerp(0.6f, 1.0f, distanceRate);

            Vector3 localPotision = transform.localPosition;
            localPotision.y = Mathf.Lerp(startLocalPositionY, -10.0f, fallRate);
            transform.localPosition = localPotision;
        }
    }

    #region 旧実装
    //[SerializeField] bool isFallDown = true;
    //[SerializeField] float fallStartTime = 10f;
    //bool isFalleing_;
    //void Start()
    //{
    //    isFalleing_ = false;
    //    StartCoroutine("ToFallOver");
    //}
    //IEnumerator ToFallOver()
    //{
    //    float passedTime = 0.0f;
    //    while(passedTime < fallStartTime)
    //    {
    //        passedTime += Time.deltaTime;
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    BoxCollider bCollider = gameObject.GetComponent<BoxCollider>();
    //    if (bCollider != null)
    //        bCollider.isTrigger = true;
    //    isFalleing_ = true;
    //    float accel = 0f;
    //    while (transform.localEulerAngles.z < 90)
    //    {
    //        transform.Rotate(0f, 0f, 1f + accel, Space.Self);
    //        accel += 0.1f;
    //        yield return new WaitForSeconds(0.01f);
    //        if (transform.localEulerAngles.z > 90)
    //        {
    //            transform.Rotate(0f, 0f, -(transform.localEulerAngles.z - 90));
    //        }
    //    }

    //    if (isFallDown)
    //    {
    //        while (transform.position.z > -5)
    //        {
    //            transform.position -= new Vector3(0, 0.1f, 0);
    //            yield return new WaitForSeconds(0.01f);
    //        }
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        isFalleing_ = false;
    //        Debug.Log("柱コルーチンを終了しました");
    //    }

    //    yield break;
    //}
    #endregion
}
