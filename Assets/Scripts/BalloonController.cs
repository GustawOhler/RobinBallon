using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float howHighShouldBalloonHide = 10.0f;
    public int Points;
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y > howHighShouldBalloonHide)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<ArrowController>() != null)
        {
            MainController.MainControllerInstance.AddPoints(Points);
            Destroy(gameObject);
        }
    }
}
