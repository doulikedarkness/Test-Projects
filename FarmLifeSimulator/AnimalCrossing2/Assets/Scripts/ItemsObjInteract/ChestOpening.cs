using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("opened", false);
            anim.SetBool("idle", false);
            anim.SetBool("closed", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("closed", false);
            anim.SetBool("opened", true);
            anim.SetBool("idle", true);
        }
    }

}
