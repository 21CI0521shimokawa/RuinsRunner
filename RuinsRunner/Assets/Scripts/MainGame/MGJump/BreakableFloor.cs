using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour
{
    [SerializeField] AudioClip audioClip_;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForEvent"))
        {
            PlayAudio.PlaySE(audioClip_);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ForEvent"))
        {
            PlayAudio.PlaySE(audioClip_);
            Destroy(gameObject);
        }
    }
}
