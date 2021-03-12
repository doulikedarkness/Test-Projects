using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField, SyncVar(hook = nameof(OnChangeColor))] private Color myColor;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private int attackCooldown = 8;
    [SerializeField] private Text cdText;
    [SerializeField] private Button buttonSkill;
    [SerializeField] private Canvas canvas; 

    private float cd = 1;

    private MeshRenderer meshRenderer;

    public override void OnStartLocalPlayer()
    {
        canvas.enabled = true;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myColor = meshRenderer.material.color;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        CooldownTimer();
        Movements();
        ActionInputs();
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

    private void Movements()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
    }

    private void ActionInputs()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CmdChangeColor();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttack();
        }
    }

    private void OnAttack()
    {
        if (cd <= 1)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            cd = attackCooldown;
            buttonSkill.interactable = false;
        }
    }

    private void CooldownTimer()
    {
        if (cd <= attackCooldown && cd > 1)
        {
            cd -= Time.deltaTime;
            cdText.text = ((int)cd).ToString();
        }
        else if (cd <= 1)
        {
            cdText.text = string.Empty;
            buttonSkill.interactable = true;
        }
    }
}
