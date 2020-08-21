using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customGrid : MonoBehaviour
{
    public bool isGridEnabled = true;
    //public bool isFalling = false;
    public GameObject target;
    public GameObject building;
    Vector3 pos;
    public float gridSize; //distance beetween 2 points snaps

    void LateUpdate() //LateUpdate runs after Update func// smother movement
    {
        if (isGridEnabled == true)
        {

            pos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
            pos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
            pos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

            building.transform.position = pos;
        }
        //else if (isGridEnabled == false && isFalling == true)
        //{
        //    StartCoroutine(WaitForFall());
        //    isFalling = false;
        //    isGridEnabled = true;
        //}
    }

    IEnumerator WaitForFall()
    {
        yield return new WaitForSecondsRealtime(3);
    }
}
