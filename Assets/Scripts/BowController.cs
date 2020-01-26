using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;

public class BowController : MonoBehaviour
{
    public float force = 50.0F;
    public GameObject arrowPrefab;
    public float secondsToMaxPower = 1.5f;
    public float endYPos = 0.0F;

    private float currentDistanceFromHands;
    private float distanceToMaxPower;
    private float startYPos;
    private bool isShotLoading;
    private GameObject arrowObject;
    private DateTime startOfMousePress;
    private float shotTimer = 0.0f;
    private bool readyForNextShot = true;
    void Start()
    {
        isShotLoading = false;
    }

    void Update()
    {

        if (UnityEngine.XR.XRDevice.isPresent)
        {
            /*if (isShotLoading)
            {
                currentDistanceFromHands = Vector3.Distance(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote), OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTrackedRemote));
                if (currentDistanceFromHands < distanceToMaxPower)
                {
                    //shotTimer += Time.deltaTime;
                    var locPos = arrowObject.transform.localPosition;
                    locPos += new Vector3(0, currentDistanceFromHands, 0);
                    arrowObject.transform.localPosition = locPos;
                }
                else
                {
                    currentDistanceFromHands = distanceToMaxPower;
                    //shotTimer = secondsToMaxPower;
                }
            }

            if (!isShotLoading && OVRInput.Get(OVRInput.Button.One))
            {
                arrowObject = Instantiate(arrowPrefab, transform);
                startYPos = arrowObject.transform.localPosition.y;
                distanceToMaxPower = endYPos - startYPos;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                //startOfMousePress = DateTime.Now;
                isShotLoading = true;
            }

            if (isShotLoading && !OVRInput.Get(OVRInput.Button.One))
            {
                var fractionOfMax = currentDistanceFromHands / distanceToMaxPower;
                float forceForShot = force * fractionOfMax;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                var forceVector = transform.parent.transform.TransformVector(Vector3.forward);
                rb.AddForce(forceVector * forceForShot, ForceMode.Impulse);
                arrowObject.transform.parent = null;

                isShotLoading = false;
                arrowObject = null;
                shotTimer = 0.0f;
                distanceToMaxPower = 0.0f;
            }*/
            if (isShotLoading)
            {
                if (shotTimer < secondsToMaxPower)
                {
                    shotTimer += Time.deltaTime;
                    var locPos = arrowObject.transform.localPosition;
                    locPos += new Vector3(0, Time.deltaTime * (endYPos - startYPos) / secondsToMaxPower, 0);
                    arrowObject.transform.localPosition = locPos;
                }
                else
                {
                    shotTimer = secondsToMaxPower;
                }
            }

            if (!isShotLoading && OVRInput.Get(OVRInput.Button.Back))
            {
                arrowObject = Instantiate(arrowPrefab, transform);
                startYPos = arrowObject.transform.localPosition.y;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                startOfMousePress = DateTime.Now;
                isShotLoading = true;
            }

            if (isShotLoading && !OVRInput.Get(OVRInput.Button.Back))
            {
                var fractionOfMax = shotTimer / secondsToMaxPower;
                float forceForShot = force * fractionOfMax;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                var forceVector = transform.parent.transform.TransformVector(Vector3.forward);
                rb.AddForce(forceVector * forceForShot, ForceMode.Impulse);
                arrowObject.transform.parent = null;
                arrowObject.GetComponent<ArrowController>().hasBeenShot = true;

                isShotLoading = false;
                arrowObject = null;
                shotTimer = 0.0f;
            }
        }
        else
        {
            if (isShotLoading)
            {
                if (shotTimer < secondsToMaxPower)
                {
                    shotTimer += Time.deltaTime;
                    var locPos = arrowObject.transform.localPosition;
                    locPos += new Vector3(0, Time.deltaTime * (endYPos - startYPos) / secondsToMaxPower, 0);
                    arrowObject.transform.localPosition = locPos;
                }
                else
                {
                    shotTimer = secondsToMaxPower;
                }
            }

            if (!isShotLoading && Input.GetMouseButtonDown(0))
            {
                arrowObject = Instantiate(arrowPrefab, transform);
                startYPos = arrowObject.transform.localPosition.y;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                startOfMousePress = DateTime.Now;
                isShotLoading = true;
            }

            if (isShotLoading && Input.GetMouseButtonUp(0))
            {
                var fractionOfMax = shotTimer / secondsToMaxPower;
                float forceForShot = force * fractionOfMax;
                var rb = arrowObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                var forceVector = transform.parent.transform.TransformVector(Vector3.forward);
                rb.AddForce(forceVector * forceForShot, ForceMode.Impulse);
                arrowObject.transform.parent = null;
                arrowObject.GetComponent<ArrowController>().hasBeenShot = true;

                isShotLoading = false;
                arrowObject = null;
                shotTimer = 0.0f;
            }
        }

    }
}
