using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>
{

}

public class Player : NetworkBehaviour {

    
    [SerializeField] ToggleEvent m_SharedEvent;
	[SerializeField] ToggleEvent m_LocalEvent;
    [SerializeField] ToggleEvent m_RemoteEvent;

    [SyncVar (hook="OnNameChanged")] public string playerName;
    [SyncVar (hook="OnColorChanged")] public Color playerColor;

    static public  List<Player> players = new List<Player>();

    [SerializeField] float respawnTime = 5f;

    GameManager gameManager;
    PlayerSetup playerSetup = new PlayerSetup();
    

    private bool initSetup = false;

    public static Player instance;

    public int number;

    const int maxScore = 3;
    
    Player player;

    void Start()
    {
        
        Invoke("InitPlayer", 2);
        
        if (instance == null)
        {
            instance = this;
        }

        EnabledPlayer();
        
    }

    public void InitPlayer()
    {
        GameObject gameObjectManager = GameObject.FindWithTag("GameManager");
        if (gameObjectManager != null)
        {
            gameManager = gameObjectManager.GetComponent<GameManager>();
            //Debug.Log("Script 'GameManger' is find  ");
        }
        else
        {
            Debug.Log("can t find 'GameManager ' script");
        }

        playerSetup.SetClassIdPlayer(this.ToString());
        playerSetup.SetColorPlayer(playerColor);
        playerSetup.SetNamePlayer(playerName);
        playerSetup.SetNumberPlayer(players.Count);
        number = players.Count;
        gameManager.playerSetupInManager.Add(playerSetup);
        Debug.Log("Number : " + number);

        if (number == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        /*
        Debug.Log("Player Class ID : " + this.ToString());
        Debug.Log("Player Color : " + playerColor);
        Debug.Log("Player Name : " + playerName);
        Debug.Log("Player Number : " + players.Count);
        */
        //Debug.Log("Player Number : " + players.Count);
    }   
    

    [ServerCallback]
    void OnEnabled()
    {
        if (!players.Contains(this))
            players.Add(this);
        
        //print("print " + )
    }

    void EnabledPlayer()
    {
        m_SharedEvent.Invoke(true);
        if (isLocalPlayer)
        {
            m_LocalEvent.Invoke(true);
        }
        else
        {
            m_RemoteEvent.Invoke(true);
        }
    }

    void DisablePlayer()
    {
        m_SharedEvent.Invoke(false);
        if (isLocalPlayer)
        {
            m_LocalEvent.Invoke(false);
        }
        else
        {
            m_RemoteEvent.Invoke(false);
        }
    }

    //[Command]
    [ClientRpc]
    public void RpcLose()
    {
        if (isLocalPlayer)
        {
            PlayerCanvas.canvas.SetGameStatusText("You Lose");
        }
        Invoke("BackToLobby", 5f);
    }

    //[Command]
    [ClientRpc]
    public void RpcWin()
    {
        if (isLocalPlayer)
        {
            PlayerCanvas.canvas.SetGameStatusText("You Win");
        }
        Invoke("BackToLobby", 5f);
    }
    /*
    [ClientRpc]
    void RpcGameOver(NetworkInstanceId  networkID, string name)
    {
        DisablePlayer();

        if (isLocalPlayer)
        {
            if (netId == networkID)
            {
                PlayerCanvas.canvas.SetGameStatusText("You Lose !!!");
            }
            else
            {
                PlayerCanvas.canvas.SetGameStatusText("You Win ! ");
            }
        }
    }*/

    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().SendReturnToLobby();
    }
    
    
    void OnNameChanged(string value)
    {
        playerName = value;
        gameObject.name = playerName;
        //PlayerCanvas.canvas.PlayerName(playerName);
    }

    void OnColorChanged(Color value)
    {
        playerColor = value;
        GetComponentInChildren<RendererToggle>().ChangeColor(playerColor);
    }

    
}
