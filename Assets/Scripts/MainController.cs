using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Models;

public class MainController : MonoBehaviour
{
    public GameObject BalloonPrefab;
    public GameObject OculusPlayerHandler;
    public GameObject ComputerPlayerHandler;
    public float SecsForNewBalloon = 2.0f;
    private float MinXForNewBalloon = -50.0f;
    private float MaxXForNewBalloon = 50.0f;
    private float MinZForNewBalloon = -50.0f;
    private float MaxZForNewBalloon = 50.0f;
    public float VelocityOfBallonGoingUp = 5.0f;
    public static MainController MainControllerInstance { get { return _instance; } }
    public GameObject PointsText;
    public Transform BackWall;
    public Transform FrontWall;
    public Transform RightWall;
    public Transform LeftWall;
    public List<Material> BalloonColors;
    public readonly List<BalloonProperties> PossibleBalloonProperties = new List<BalloonProperties>()
    {
        new BalloonProperties("Green", 1F, 100),
        new BalloonProperties("Light-brown", 1.5F, 200),
        new BalloonProperties("Orange", 2.75F, 400),
        new BalloonProperties("Red", 3.75F, 500),
        new BalloonProperties("Yellow", 2F, 300)
    };
    public int Points
    {
        get; private set;
    }

    private System.Random randGenerator;
    private float timer = 0.0f;
    private static MainController _instance;
    private int _points;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        randGenerator = new System.Random();
        Points = 0;
        PointsText.GetComponent<TextMesh>().text = "Punkty: " + Points;
        float cameraZPosition = BackWall.position.z + 5.0f;
        if (UnityEngine.XR.XRDevice.isPresent)
        {
            OculusPlayerHandler.SetActive(true);
            OculusPlayerHandler.transform.position = new Vector3(OculusPlayerHandler.transform.position.x, OculusPlayerHandler.transform.position.y, cameraZPosition);
        }
        else
        {
            ComputerPlayerHandler.SetActive(true);
            ComputerPlayerHandler.transform.position = new Vector3(ComputerPlayerHandler.transform.position.x, ComputerPlayerHandler.transform.position.y, cameraZPosition);
        }
        MinXForNewBalloon = LeftWall.position.x + 2.0f;
        MaxXForNewBalloon = RightWall.position.x - 2.0f;
        MinZForNewBalloon = cameraZPosition + 2.0f;
        MaxZForNewBalloon = FrontWall.position.z - 2.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > SecsForNewBalloon)
        {
            var balObj = Instantiate(BalloonPrefab, RandomPosition(), BalloonPrefab.transform.rotation);
            var choosenProperty = PossibleBalloonProperties[randGenerator.Next(0, PossibleBalloonProperties.Count - 1)];
            balObj.GetComponent<MeshRenderer>().material = BalloonColors.Find(m => m.name == choosenProperty.MaterialName);
            var balRB = balObj.GetComponent<Rigidbody>();
            balRB.velocity = new Vector3(0, VelocityOfBallonGoingUp * choosenProperty.SpeedMultiplier, 0);
            balObj.GetComponent<BalloonController>().Points = choosenProperty.Points;
            timer = 0.0f;
        }
    }

    Vector3 RandomPosition()
    {
        float x = (float)(randGenerator.NextDouble() * (MaxXForNewBalloon - MinXForNewBalloon) + MinXForNewBalloon);
        float z = (float)(randGenerator.NextDouble() * (MaxZForNewBalloon - MinZForNewBalloon) + MinZForNewBalloon);
        return new Vector3(x, 0.55f, z);
    }

    public void AddOnePoint()
    {
        Points++;
        PointsText.GetComponent<TextMesh>().text = "Punkty: " + Points;
    }

    public void AddPoints(int points)
    {
        Points += points;
        PointsText.GetComponent<TextMesh>().text = "Punkty: " + Points;
    }
}
