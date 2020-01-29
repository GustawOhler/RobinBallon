using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Kolizja!");
        //if (collision.gameObject.GetComponent<BalloonController>() != null)
        //{
        //    Destroy(collision.gameObject);
        //    Debug.Log("Zdobyty punkt!");
        //}
        GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
