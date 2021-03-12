using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public PlayerChangableValues changableValues;

    [SerializeField, SyncVar(hook = nameof(OnChangeColor))] private Color myColor;
    [SerializeField] private Text cdText;
    [SerializeField] private Button buttonSkill;
    [SerializeField] private Canvas canvas; 

    private float cd = 1;
    private bool isBuffTime = false;

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

        transform.position += new Vector3(horizontal, 0f, vertical) * changableValues.moveSpeed * Time.deltaTime;
    }

    private void ActionInputs()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CmdChangeColor();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isBuffTime)
        {
            OnAttack();
        }
    }

    public void OnAttack()
    {
        if (cd <= 1)
        {
            StartCoroutine(hungerBuff());
        }
    }

    private void CooldownTimer()
    {
        if (cd <= changableValues.attackCooldown && cd > 1)
        {
            cd -= Time.deltaTime;
            cdText.text = ((int)cd).ToString();
            buttonSkill.interactable = false;
        }
        else if (cd <= 1 && !isBuffTime) //buff time so buttons isnt interactable while under the buff
        {
            cdText.text = string.Empty;
            buttonSkill.interactable = true;
        }
    }

    public IEnumerator hungerBuff()
    {
        transform.localScale += changableValues.scaleFor;
        Color defColor = buttonSkill.image.color;
        buttonSkill.image.color = Color.cyan;
        isBuffTime = true;

        yield return new WaitForSeconds(changableValues.hungerBuffTime);

        buttonSkill.image.color = defColor;
        transform.localScale -= changableValues.scaleFor;
        isBuffTime = false;

        cd = changableValues.attackCooldown;                                //reset CD to let it be active again
        yield return null;
    }

    [System.Serializable]
    public class PlayerChangableValues
    {
        public float hungerBuffTime = 3f;
        public float moveSpeed = 10f;
        public int attackCooldown = 8;

        [Space] public Vector3 scaleFor;
    }
}
