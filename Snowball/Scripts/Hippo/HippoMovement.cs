using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class HippoMovement : MonoBehaviour
{
    public float moveSpeed = 7f;
    public SkeletonAnimation hippoAnims;
    public GameObject snowball;

    private string _horizontal = "Horizontal";
    private string _vertical = "Vertical";
    private float horizontalInput;
    private float verticalInput;

    private void Update()
    {
        horizontalInput = SimpleInput.GetAxis(_horizontal);
        verticalInput = SimpleInput.GetAxis(_vertical);

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * moveSpeed);  

        if (horizontalInput < 0)
        {
            TurnLeft();
        }
        else if (horizontalInput > 0)
        {
            TurnRight();
        } else
        {
            hippoAnims.AnimationName = "Idle";
        }
    }

    private void TurnLeft()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        hippoAnims.AnimationName = "run";
    }

    private void TurnRight()
    {
        transform.localScale = new Vector3(1, 1, 1);
        hippoAnims.AnimationName = "run";
    }
}
