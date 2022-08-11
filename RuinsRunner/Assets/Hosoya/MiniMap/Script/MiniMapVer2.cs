using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapVer2 : MonoBehaviour
{
    [SerializeField] NewMapGenerator mapGenerator_;
    float startTime_;

    Image fill_;

    // Start is called before the first frame update
    void Start()
    {
        fill_ = gameObject.GetComponent<Image>();

        startTime_ = mapGenerator_.remainTime;
    }

    // Update is called once per frame
    void Update()
    {
        fill_.fillAmount = Mathf.InverseLerp(startTime_, 0.0f, mapGenerator_.remainTime);
    }
}