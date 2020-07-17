using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBall : MonoBehaviour
{
    public float attackOncePerSec = 5;

    private bool isSpawned = false;
    private float timer;
    private GameObject player;
    private Rigidbody rb;
    private GameObject hitParticles;
    private GameObject randomEnemy;
    private GameObject[] enemies;
    private List<GameObject> enemiesActive = new List<GameObject>();

    private void Start()
    {
        hitParticles = GameObject.FindWithTag("ParticlesEnemy");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timer = Time.time + attackOncePerSec;
        hitParticles.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hitting player");
            GameManager.playerHP -= 1;
            hitParticles.SetActive(true);
            hitParticles.transform.position = player.transform.position;
            Timer();
        }
    }

    private void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (Time.time > attackOncePerSec && isSpawned == false && Misc._isPaused == false)
        {
            CalculateRandomEnemy();
            SnowballThrowing();

            timer = Time.time + attackOncePerSec; //обновляем таймер чтобы снежок бросился только 1 раз

            StartCoroutine(delay());
        }
    }

    private void SnowballThrowing()
    {
        this.gameObject.transform.position = randomEnemy.transform.position; //перемещаем снежок к врагу (было бы проще если бы можно было Instantiate)
        rb.velocity = player.transform.position - transform.position; //высчитываем траекторию к персонажу от врага
        rb.MovePosition(player.transform.position); //двигаем по этой траектории
    }

    private void CalculateRandomEnemy()
    {
        for (int i = 0; i < enemies.Length; i++) //проходим через всех врагов
        {
            if (enemies[i].GetComponent<Enemy>()._onDisabled == false) //смотрим кто из них стоит жив на поле
            {
                enemiesActive.Add(enemies[i]); //того кто жив добавляем в новый лист (НЕ МАССИВ т.к может не совпадать индекс и будет ексепшон null!!)
            }
        }
        randomEnemy = enemiesActive[Random.Range(0, enemiesActive.Count)]; //выбираем случайного врага кто будет кидать снежок
    }

    IEnumerator delay()
    {
        isSpawned = true;
        yield return new WaitForSecondsRealtime(attackOncePerSec);
        enemiesActive.Clear(); //очищаем лист живых вргаов на поле т.к после удара снежком их может быть меньше
        hitParticles.SetActive(false);
        isSpawned = false;
    }
    
}
