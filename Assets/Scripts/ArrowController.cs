using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public bool hasBeenShot = false;
    private float timer = 0.0f;

    void Start()
    {
        //FlyWithRotation();
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
    }

    void FlyWithRotation()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb.velocity != Vector3.zero)
        {
            //var zAxis = transform.rotation.z;
            //var rotationToSet = Quaternion.LookRotation(rb.velocity);
            //rotationToSet.
            //Debug.Log($"rotation: {rotationToSet.ToString()}");
            //transform.rotation.Set(rotationToSet.x, rotationToSet.y, zAxis, rotationToSet.w);
            var vectorForRotation = rb.velocity;
            //Debug.Log($"Velocity: {rb.velocity}");
            //vectorForRotation.z = -90F;
            //vectorForRotation.x = 0;
            //vectorForRotation.y = 0;
            //vectorForRotation.z = 0f;
            //vectorForRotation.z = transform.rotation.z;
            transform.rotation = Quaternion.LookRotation(vectorForRotation);
            //transform.ro
        }
    }
}
