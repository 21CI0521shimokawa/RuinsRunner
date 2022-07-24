using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPillar : MonoBehaviour
{
    [SerializeField] bool isFallDown = true;
    [SerializeField] float fallStartTime = 10f;
    bool isFalleing_;
    void Start()
    {
        isFalleing_ = false;
        StartCoroutine("ToFallOver");
    }

    IEnumerator ToFallOver()
    {
        float passedTime = 0.0f;
        while(passedTime < fallStartTime)
        {
            passedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }

        BoxCollider bCollider = gameObject.GetComponent<BoxCollider>();
        if (bCollider != null)
            bCollider.isTrigger = true;
        isFalleing_ = true;
        float accel = 0f;
        while (transform.localEulerAngles.z < 90)
        {
            transform.Rotate(0f, 0f, 1f + accel, Space.Self);
            accel += 0.1f;
            yield return new WaitForSeconds(0.01f);
            if (transform.localEulerAngles.z > 90)
            {
                transform.Rotate(0f, 0f, -(transform.localEulerAngles.z - 90));
            }
        }

        if (isFallDown)
        {
            while (transform.position.z > -5)
            {
                transform.position -= new Vector3(0, 0.1f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(gameObject);
        }
        else
        {
            isFalleing_ = false;
            Debug.Log("柱コルーチンを終了しました");
        }

        yield break;
    }
}
