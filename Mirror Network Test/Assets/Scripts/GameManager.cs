using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private Text playersTxt;

    [SyncVar(hook = nameof(OnUpdateTextConnected))] public int playersConnected;

    public override void OnStartClient()
    {
        base.OnStartClient();
        playersConnected += 1;
        
    }

    private void OnUpdateTextConnected(int oldValue, int newValue)
    {
        playersTxt.text = "Players connected: " + newValue;
    }
}
