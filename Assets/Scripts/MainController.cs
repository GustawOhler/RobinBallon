using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Models;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject BalloonPrefab;
    public GameObject OculusPlayerHandler;
    public GameObject ComputerPlayerHandler;
    private float MinXForNewBalloon = -50.0f;
    private float MaxXForNewBalloon = 50.0f;
    private float MinZForNewBalloon = -50.0f;
    private float MaxZForNewBalloon = 50.0f;
    public float VelocityOfBallonGoingUp = 5.0f;
    public static MainController MainControllerInstance { get { return _instance; } }
    public List<GameObject> PointsText;
    public List<GameObject> LevelText;
    public List<GameObject> TimeText;
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
    public readonly List<LevelProperties> PossibleLevelProperties = new List<LevelProperties>()
    {
        new LevelProperties(1.5f, 120, "Normal"),
        new LevelProperties(1.5f, 240, "Long"),
        new LevelProperties(0.25f, 60, "Hardcore"),
        new LevelProperties(0.85f, int.MaxValue, "Training")
    };
    private LevelProperties ChosenLevel;
    public enum LevelDifficulties { Normal, Long, Hardcore, Training };
    public int Points
    {
        get; private set;
    }

    private System.Random randGenerator;
    private float BalloonSpawnTimer = 0.0f;
    private float GameTimer = 0.0f;
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
        ChooseLevelDiff(LevelDifficulties.Normal);
    }

    void Start()
    {
        randGenerator = new System.Random();
        Points = 0;
        PointsText.Find(x => x.activeSelf).GetComponent<TextMesh>().text = "Punkty: " + Points;
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
        if (ChosenLevel.Name == "Training")
        {
            TimeText.Find(x => x.activeSelf).SetActive(false);
        }
        //ChooseLevelDiff(LevelDifficulties.Normal);
    }

    void Update()
    {
        BalloonSpawnTimer += Time.deltaTime;
        GameTimer += Time.deltaTime;
        if (TimeText.Find(x => x.activeSelf) != null)
        {
            TimeText.Find(x => x.activeSelf).GetComponent<TextMesh>().text = "Time left: " + (ChosenLevel.SecondsForGame - (int)GameTimer);
        }

        if((float)ChosenLevel.SecondsForGame - GameTimer < 0.0f)
        {
            SceneManager.LoadScene("MenuScene");
        }

        if (BalloonSpawnTimer > ChosenLevel.BalloonTimeInterval)
        {
            var balObj = Instantiate(BalloonPrefab, RandomPosition(), BalloonPrefab.transform.rotation);
            var choosenProperty = PossibleBalloonProperties[randGenerator.Next(0, PossibleBalloonProperties.Count - 1)];
            balObj.GetComponent<MeshRenderer>().material = BalloonColors.Find(m => m.name == choosenProperty.MaterialName);
            var balRB = balObj.GetComponent<Rigidbody>();
            balRB.velocity = new Vector3(0, VelocityOfBallonGoingUp * choosenProperty.SpeedMultiplier, 0);
            balObj.GetComponent<BalloonController>().Points = choosenProperty.Points;
            BalloonSpawnTimer = 0.0f;
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
        PointsText.Find(x => x.activeSelf).GetComponent<TextMesh>().text = "Punkty: " + Points;
    }

    public void AddPoints(int points)
    {
        Points += points;
        PointsText.Find(x => x.activeSelf).GetComponent<TextMesh>().text = "Punkty: " + Points;
    }

    public void ChooseLevelDiff(LevelDifficulties chosenDiff)
    {
        switch (chosenDiff)
        {
            case LevelDifficulties.Normal:
                ChosenLevel = PossibleLevelProperties.Find(p => p.Name == "Normal");
                break;
            case LevelDifficulties.Long:
                ChosenLevel = PossibleLevelProperties.Find(p => p.Name == "Long");
                break;
            case LevelDifficulties.Hardcore:
                ChosenLevel = PossibleLevelProperties.Find(p => p.Name == "Hardcore");
                break;
            case LevelDifficulties.Training:
                ChosenLevel = PossibleLevelProperties.Find(p => p.Name == "Training");
                break;
            default:
                break;
        }
        LevelText.Find(x => x.activeSelf).GetComponent<TextMesh>().text = ChosenLevel.Name;
    }
}
