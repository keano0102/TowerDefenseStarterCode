using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private static EnemySpawner instance;
    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    private int ufoCounter = 0;

    public static EnemySpawner Get {  get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void SpawnEnemy(int type, Enums.Path path)

    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        if (path == Enums.Path.Path1)
        {
            spawnPosition = Path1[0].transform.position;
            spawnRotation = Path1[0].transform.rotation;
        }
        else if (path == Enums.Path.Path2)
        {
            spawnPosition = Path2[0].transform.position;
            spawnRotation = Path2[0].transform.rotation;
        }
        else
        {
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
            Debug.LogError("Invalid path specified! ");
            return;
        }

        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);

        var script = newEnemy.GetComponentInParent<Enemy>();



        // set hier het path en target voor je enemy in 
        script.path = path;
        script.target = Path1[1];

    }
    public GameObject RequestTarget(Enums.Path path, int index)
    {
        List<GameObject> currentPath = null;

        switch (path)
        {
            case Enums.Path.Path1:
                currentPath = Path1;
                break;
            case Enums.Path.Path2:
                currentPath = Path2;
                break;
            default:
                Debug.LogError("Invalid path specified!");
                break;
        }
        if(currentPath == null)
        {
            Debug.LogError("Path is null!");
            return null;
        }
        index++;
        if(index >= currentPath.Count)
        {
            return null;
        }
        else
        {
            return currentPath[index];
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnTester()
    {
        SpawnEnemy(0, Enums.Path.Path1);
    }
    public void StartWave(int number)
    { 
        // reset counter
        ufoCounter = 0;
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
            default:
                break;
        }
    }
    public void StartWave1()
    {
        ufoCounter++;

        if (ufoCounter % 6 <= 1)
            return;
        if(ufoCounter<30)
        {
            SpawnEnemy(0, Enums.Path.Path1);
        }
        else
        {
            SpawnEnemy(1, Enums.Path.Path1);
        }
        if(ufoCounter >30)
        {
            CancelInvoke("StartWave1");//the reverse of InvokeRepeating
            //depending on your singleton declaretion, Get might be something else
            GameManager.Instance.EndWave();//let the GameManager know
        }
    }
}
