using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField, SyncVar(hook = nameof(OnChangeColor))] private Color myColor;
    [SerializeField] private float moveSpeed = 10f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myColor = meshRenderer.material.color;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdChangeColor();
        }
    }

    [Command]
    private void CmdChangeColor()
    {
        myColor = UnityEngine.Random.ColorHSV();
    }


    private void OnChangeColor(Color oldColor, Color newColor)
    {
        meshRenderer.material.color = newColor;
    }
}
