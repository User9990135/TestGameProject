using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform CamerTransform;

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(CamerTransform.position.x, CamerTransform.position.y, this.transform.position.z);

    }
}
