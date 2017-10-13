using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {

    public static GameManager instance;
    public GameObject ball;
    public Transform spawnBall1;
    public Transform spawnBall2;

    private float startWait = 3;
    private float spawnWait = 3;

    const int maxScore = 3;
    const int maxTime = 6;

    private bool destroyBall = false;
    
    [SyncVar (hook="OnScorePlayer") ] public int currentScorePlayer = maxScore;
    [SyncVar (hook="OnScoreEnemy") ] public int currentScoreEnemy = maxScore;
    
     
    [SyncVar (hook="SyncTime")] public int currentTime = maxTime;
    
    public List<PlayerSetup> playerSetupInManager = new List<PlayerSetup>();

    private bool isSet = false;

    public AudioClip audioClipCountDown;
    public AudioClip audioClipRing;
    public AudioClip audioClipGong;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        initSetup();
        PlayerCanvas.canvas.SetLogText("Wait...");
        Invoke("initPlayerSetup", 4);
        Invoke("WaitOnSpawnBall", 4);
    }

    public void initPlayerSetup()
    {
        PlayerCanvas.canvas.SetLogText("Start");
        for (int i = 0; i < playerSetupInManager.Count; i++)
        {

            //Debug.Log("Manager Player Class ID : " + playerSetupInManager[i].GetclassIdPlayer());
            //Debug.Log("List Players : " + Player.players[i]);
            /*
            Debug.Log("Manager Player Color : " + playerSetupInManager[i].GetColorPlayer());
            Debug.Log("Manager Player Name : " + playerSetupInManager[i].GetNamePlayer());
            Debug.Log("Manager Player Number : " + playerSetupInManager[i].GetNumberPlayer());
            */


            if (i == 0)
            {
                PlayerCanvas.canvas.PlayerName(playerSetupInManager[i].namePlayer);



            }
            if (i == 1)
            {
                PlayerCanvas.canvas.EnemyName(playerSetupInManager[i].namePlayer);

            }
        }
    }


    public void WaitOnSpawnBall()
    {
        isSet = true;
        RpcDisplayTextLog(1);
    }

    public void initSetup()
    {
        PlayerCanvas.canvas.SetLogText("");
        PlayerCanvas.canvas.SetScoreEnemy("Score : " + currentScoreEnemy);
        PlayerCanvas.canvas.SetScorePlayer("Score : " + currentScorePlayer);
    }

    public void OnScorePlayer(int value)
    {
        currentScorePlayer = value;
        PlayerCanvas.canvas.SetScorePlayer("Score : " + currentScorePlayer);

    }

    public void OnScoreEnemy(int value)
    {
        currentScoreEnemy = value;
        PlayerCanvas.canvas.SetScoreEnemy("Score : " + currentScoreEnemy);
        
    }

    public void TakeScorePlayer(int amount)
    {
        int temp = currentScorePlayer - amount;
        if (temp > 0)
        {
            currentScorePlayer -= amount;
        }
        else
        {
            currentScorePlayer = 0;
            destroyBall = true;
        }

    }

    public void TakeScoreEnemy(int amount)
    {
        int temp = currentScoreEnemy - amount;
        if (temp > 0)
        {
            currentScoreEnemy -= amount;
        }
        else
        {
            currentScoreEnemy = 0;
            destroyBall = true;
        }

    }
    
    public void SyncTime(int amount)
    {
        currentTime -= amount;
    }

    [ClientRpc]
    public void RpcDisplayTextLog(int number)
    {
        if (destroyBall == false)
        
        {
            //Audio();
            if (number == 1)
            {           
                Invoke("CreateBallForPlayer", 6);
            }
            if (number == 2)
            {           
                Invoke("CreateBallForEnemy", 6);
            }
            /*
            Invoke("displayText5", currentTime - 6);
            Invoke("displayText4", currentTime - 5);
            Invoke("displayText3", currentTime - 4);
            Invoke("displayText2", currentTime - 3);
            Invoke("displayText1", currentTime - 2);
            Invoke("displayText0", currentTime - 1);
            Invoke("displayText", currentTime);
            */
            /*
            Invoke("CmdDisplayText5", 0);
            Invoke("CmdDisplayText4", 1);
            Invoke("CmdDisplayText3", 2);
            Invoke("CmdDisplayText2", 3);
            Invoke("CmdDisplayText1", 4);
            Invoke("CmdDisplayText0", 5);
            Invoke("CmdDisplayText", 6);

             */
 
            Invoke("displayText5", 0);
            Invoke("displayText4", 1);
            Invoke("displayText3", 2);
            Invoke("displayText2", 3);
            Invoke("displayText1", 4);
            Invoke("displayText0", 5);
            Invoke("displayText", 6);
        }
    }

    /*
    [Command]
    private void CmdDisplayText5()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 5);
    }

    [Command]
    private void CmdDisplayText4()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 4);
    }

    [Command]
    private void CmdDisplayText3()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 3);
    }

    [Command]
    private void CmdDisplayText2()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 2);
    }

    [Command]
    private void CmdDisplayText1()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 1);
    }

    [Command]
    private void CmdDisplayText0()
    {
        AudioGong();
        PlayerCanvas.canvas.SetLogText("" + 0);
    }

    [Command]
    private void CmdDisplayText()
    {
        PlayerCanvas.canvas.SetLogText(" ");
    }
    */

    
    private void displayText5()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 5);
    }

    private void displayText4()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 4);
    }

    private void displayText3()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 3);
    }

    private void displayText2()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 2);
    }

    private void displayText1()
    {
        AudioRing();
        PlayerCanvas.canvas.SetLogText("" + 1);
    }

    private void displayText0()
    {
        AudioGong();
        PlayerCanvas.canvas.SetLogText("" + 0);
    }

    private void displayText()
    {
        PlayerCanvas.canvas.SetLogText(" ");
    }


    public void CreateBallForPlayer()
    {
        if (currentScorePlayer > 0 && currentScoreEnemy > 0)
        {
            CmdStartCreatBall(spawnBall1.position, spawnBall1.rotation);
        }
        
    }

    public void CreateBallForEnemy()
    {
        if (currentScorePlayer > 0 && currentScoreEnemy > 0)
        {
            CmdStartCreatBall(spawnBall2.position, spawnBall2.rotation);
        }
        
    }

    [Command]
    void CmdStartCreatBall(Vector3 position, Quaternion rotation)
    {
        //if (SkyCollisionRight.instance.currentScore > 0 && SkyCollisionLeft.instance.currentScore > 0) {
            GameObject ballInstance = Instantiate(ball, position, rotation) as GameObject;
            NetworkServer.Spawn(ballInstance);
        //}
    }

    void WaitOnBackToLobby()
    {
        Invoke("BackToLobby", 5);
    }

    void BackToLobby()
    {
        FindObjectOfType<NetworkLobbyManager>().SendReturnToLobby();
    }

    void Audio()
    {
        audioSource.clip = audioClipCountDown;
        audioSource.Play();
    }

    void AudioRing()
    {
        audioSource.clip = audioClipRing;
        audioSource.Play();
    }

    void AudioGong()
    {
        audioSource.clip = audioClipGong;
        audioSource.Play();
    }

    
}
