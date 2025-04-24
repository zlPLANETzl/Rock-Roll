using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWall : MonoBehaviour
{
    private float push = 0.3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = -collision.contacts[0].normal;
            collision.transform.position += normal * push; // ¼ÒÆø ¹Ð¾î³¿
        }        
    }
}
