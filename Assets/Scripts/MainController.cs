using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject BalloonPrefab;
    public float SecsForNewBalloon = 2.0f;
    public float MinXForNewBalloon = -50.0f;
    public float MaxXForNewBalloon = 50.0f;
    public float MinZForNewBalloon = -50.0f;
    public float MaxZForNewBalloon = 50.0f;
    public float VelocityOfBallonGoingUp = 5.0f;
    public static MainController MainControllerInstance { get { return _instance; } }
    public GameObject PointsText;
    public int Points
    {
        get
        {
            return _points;
        }
        private set
        {
            _points = value;
        }
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
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > SecsForNewBalloon)
        {
            var balObj = Instantiate(BalloonPrefab, RandomPosition(), BalloonPrefab.transform.rotation);
            var balRB = balObj.GetComponent<Rigidbody>();
            balRB.velocity = new Vector3(0, VelocityOfBallonGoingUp, 0);
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
}
