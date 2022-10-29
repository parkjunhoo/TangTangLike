using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 _delta = new Vector3(0,0,-10);

    [SerializeField]
    GameObject _player = null;
    


    void LateUpdate()
    {
        Vector3 playerPos = _player.transform.position;
        transform.position = playerPos + _delta;
    }
}
