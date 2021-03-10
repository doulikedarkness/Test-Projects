using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private float radiusOfDetectionEnemy = 0.3f; //радиус в котором происходит столкновение с врагом и после которого идет "рикошет"
    private float radiusOfDestruction = 50f; //радиус взрыва "рикошета"
    private float explosionForce = 300f;     //сила "рикошета"
    private Vector3[] rndPositionsOfExplosion = new Vector3[2]; //массив случайных отклонений для выбора в методе внизу (можно было в принципе не делать)
    private GameObject go;
    private Rigidbody rb;


    private void Awake() //чтобы вечно не писать go.GetComponent<Rigidbody>()... просто rb и go
    {
        go = this.gameObject; 
        rb = go.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector2.up, radiusOfDetectionEnemy, 1 << 9))
        {
            Debug.Log("Шарик попал во врага!");
            Vector3 v3 = CalculateRandomSide();
            rb.AddExplosionForce(explosionForce, v3, radiusOfDestruction); //После попадания делаем эффект отбивания снежка от тела
                
        }
    }

    private Vector3 CalculateRandomSide() //отклонение случайное в сторону снежка после попадания (эффект рикошета)
    {
        rndPositionsOfExplosion[0] = new Vector3(go.transform.localPosition.x + 40f, go.transform.localPosition.y + 20);
        rndPositionsOfExplosion[1] = new Vector3(go.transform.localPosition.x - 20f, go.transform.localPosition.y - 5);

        return rndPositionsOfExplosion[Random.Range(0, rndPositionsOfExplosion.Length)];
    }
}
