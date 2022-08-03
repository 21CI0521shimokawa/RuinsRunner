using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] NewMapGenerator mapGenerator_;
    float startTime_;

    Image icon_;

    Vector3 startPos_;
    [SerializeField] Vector3 endPos_;
    // Start is called before the first frame update
    void Start()
    {
        icon_ = gameObject.GetComponent<Image>();
        startPos_ = icon_.rectTransform.anchoredPosition;

        startTime_ = mapGenerator_.remainTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = startPos_;
        newPos += (endPos_ - startPos_) * Mathf.InverseLerp(startTime_, 0.0f, mapGenerator_.remainTime);
        icon_.rectTransform.anchoredPosition = newPos;
    }
}
