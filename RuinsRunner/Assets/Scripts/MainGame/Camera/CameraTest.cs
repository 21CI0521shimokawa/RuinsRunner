/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest 
    : MonoBehaviour
    , ICameraMoveTest
{
    [SerializeField] GameObject target_;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target_.transform);
    }

    public void CallCameraMove(Vector3 _destination, GameObject _newTarget)
    {
        //引数に次のターゲットが渡されなかった場合、引き続きターゲットを注視する
        if (_newTarget == null)
        {
            _newTarget = target_;
        }
        StartCoroutine(Move(_destination, _newTarget));
    }

    IEnumerator Move(Vector3 _destination, GameObject _target)
    {
        while((transform.position - _destination).sqrMagnitude >= 0.01f)
        {
            //カメラを5%近づける
            transform.position += (_destination - transform.position) * 0.05f;
            transform.LookAt(new Vector3(0, 0, target_.transform.position.z));
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}*/
