using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public bool hasBeenShot = false;
    private bool hasStick = false;
    private float timer = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        FlyWithRotation();

        if (hasBeenShot){
            timer += Time.deltaTime;
        }

        if (hasBeenShot && transform.position.y <= 0.1 && timer >= 1.0f){
            Destroy(gameObject);
        }

        if (hasStick && timer >= 5.0f)
        {
            Destroy(gameObject);
        }
    }

    void FlyWithRotation()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb.velocity != Vector3.zero)
        {
            var vectorForRotation = rb.velocity;
            transform.rotation = Quaternion.LookRotation(vectorForRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        hasStick = true;
    }
}
