using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isFree = true;
    public bool isSomeoneRunninHere = false;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            isFree = true;
        }
    }

}
