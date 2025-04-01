using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float initialSpeed = 10f;     // 시작 속도
    public float maxSpeed = 50f;         // 최대 속도
    public float accelerationPerSec = 20f;

    private float currentSpeed;
    private float elapsedTime;
    private Vector3 direction;
    private Rigidbody rb;
    private System.Action<Vector3> onDashEnd;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Vector3 dir, System.Action<Vector3> callback)
    {
        direction = dir.normalized;
        onDashEnd = callback;
        rb = GetComponent<Rigidbody>();

        currentSpeed = initialSpeed;
        rb.velocity = direction * currentSpeed;
        elapsedTime = 0f;
    }

    void FixedUpdate()
    {
        if (currentSpeed < maxSpeed)
        {
            elapsedTime += Time.fixedDeltaTime;
            float expectedSpeed = initialSpeed + accelerationPerSec * elapsedTime;
            currentSpeed = Mathf.Min(expectedSpeed, maxSpeed);
        }

        rb.velocity = direction * currentSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) return;

        onDashEnd?.Invoke(transform.position);
        Destroy(gameObject);
    }
}
