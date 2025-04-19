using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float followSpeed = 10f;

    private void LateUpdate()
    {
        Vector3 targetPos = transform.position;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
