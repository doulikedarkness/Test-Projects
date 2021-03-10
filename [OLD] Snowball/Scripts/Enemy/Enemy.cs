using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool _onDisabled = false;
    public int enemyLevel = 1;
    public int howManyPointsForLvl1Enemy = 1;
    public int howManyPointsForLvl2Enemy = 3;
    public int howManyPointsForLvl3Enemy = 8;
    public float timeWaitForShocking = 1.7f;
    public float runninAwaySpeed = 0.3f;
    public Transform pointToRunAway;
    public GameObject snowHitParticles;
    public SkeletonAnimation animationn;


    private static GameObject[] spawnPoints = new GameObject[5];
    private static List<GameObject> spawnPointsFree = new List<GameObject>();
    private GameObject[] allEnemies = new GameObject[3];
    private GameObject randomSpawnPoint;
    private GameObject randomEnemyDed;
    private List<GameObject> enemiesDead = new List<GameObject>();
    private bool _runningAway = false;
    private bool _haveBeenDamaged = false;
    private bool _runningToPoint = false;
    private bool _isPointDeclared = false;


    private void Awake()
    {

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = GameObject.Find("[Spawn] " + i);
        }

        for (int i = 0; i < allEnemies.Length; i++)
        {
            allEnemies[i] = GameObject.Find("[Enemy] " + i);
        }
        animationn = gameObject.GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        snowHitParticles.SetActive(false);
    }

    private void Update()
    {
        CheckForFreeSpawnPoint();
        CheckForRandomEnemyDed();
        EnemyRun();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snowball") && _haveBeenDamaged == false)
        {
            snowHitParticles.transform.position = transform.localPosition;
            snowHitParticles.SetActive(true);
            Debug.Log("[Enemy] Задели врага, добавляем поинты");
            if(enemyLevel == 2)
            {
                GameManager.score += howManyPointsForLvl2Enemy;
                this.gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, pointToRunAway.transform.rotation, 360f);
                StartCoroutine(RunAway());
            }
            else if(enemyLevel >= 3)
            {
                GameManager.score += howManyPointsForLvl3Enemy;
                this.gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, pointToRunAway.transform.rotation, 360f);
                StartCoroutine(RunAway());
            } else
            {
                GameManager.score += howManyPointsForLvl1Enemy;
                this.gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, pointToRunAway.transform.rotation, 360f);
                StartCoroutine(RunAway());
            }
        }

        if(other.CompareTag("RunawayPoint"))
        {
            _runningAway = false;
            enemiesDead.Add(this.gameObject);
            _onDisabled = true;
        }

        if (other.CompareTag("SpawnPoint") && other.GetComponent<SpawnPoint>().isSomeoneRunninHere == false) 
        {
            other.GetComponent<SpawnPoint>().isFree = false;
            other.GetComponent<SpawnPoint>().isSomeoneRunninHere = false;
            _runningToPoint = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RunawayPoint"))
        {
            _onDisabled = false;
            _isPointDeclared = false;
            this.gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, randomSpawnPoint.transform.rotation, 360f);
        }
    }

        private void CheckForRandomEnemyDed()
    {
        if(enemiesDead.Count >= 1)
        {
            randomEnemyDed = enemiesDead[Random.Range(0, enemiesDead.Count)];
        }
        if(randomEnemyDed != null)
        {
            randomSpawnPoint.GetComponent<SpawnPoint>().isSomeoneRunninHere = true;
            randomEnemyDed.GetComponent<Enemy>()._onDisabled = false;
            randomEnemyDed.GetComponent<Enemy>()._haveBeenDamaged = false;
            randomEnemyDed.GetComponent<Enemy>()._runningAway = false;
            randomEnemyDed.GetComponent<Enemy>()._runningToPoint = true;
            enemiesDead.Remove(randomEnemyDed);
        }
    }

    private void CheckForFreeSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(spawnPoints[i].GetComponent<SpawnPoint>().isFree == true && spawnPoints[i].GetComponent<SpawnPoint>().isSomeoneRunninHere == false)
            {
                spawnPointsFree.Add(spawnPoints[i]);
            } else if(spawnPoints[i].GetComponent<SpawnPoint>().isFree == false || spawnPoints[i].GetComponent<SpawnPoint>().isSomeoneRunninHere == false && spawnPointsFree.Contains(spawnPoints[i]) == true)
            {
                spawnPointsFree.Remove(spawnPoints[i]);
            }
        }
        if(spawnPointsFree != null && _isPointDeclared == false)
        {
            randomSpawnPoint = spawnPointsFree[Random.Range(0, spawnPointsFree.Count)];
            _isPointDeclared = true;
        }
    }

    IEnumerator RunAway()
    {
        _haveBeenDamaged = true;
        animationn.AnimationName = "lose_balance";
        yield return new WaitForSecondsRealtime(timeWaitForShocking);
        animationn.AnimationName = "run";
        _runningAway = true;
        snowHitParticles.SetActive(false);
    }

    private void EnemyRun()
    {
        if (_runningToPoint == true && _onDisabled == false && _haveBeenDamaged == false && _runningAway == false)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, randomSpawnPoint.transform.position, runninAwaySpeed * Time.deltaTime);
        }

        if (_runningAway == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointToRunAway.position, runninAwaySpeed * Time.deltaTime); //transform.Translate((pointToRunAway.position) * runninAwaySpeed * Time.deltaTime);
        }

        if (_runningAway == false && _haveBeenDamaged == true && _onDisabled == true && _runningToPoint == false)
        {
            this.gameObject.transform.position = pointToRunAway.transform.position;
            this.gameObject.SetActive(false);
        }
    }
}
