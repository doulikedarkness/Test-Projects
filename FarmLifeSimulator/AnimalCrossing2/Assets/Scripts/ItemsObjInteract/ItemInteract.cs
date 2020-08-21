using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("FirstInteraction", true);
            anim.SetBool("idle", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("FirstInteraction", false);
            anim.SetBool("idle", true);
        }
    }
}
