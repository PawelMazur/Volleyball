using UnityEngine;
using System.Collections;

public class SkyCollisionLeft : MonoBehaviour {

	// Use this for initialization

    Player player;
    PlayerControl playerControl;
    public bool playerLose = false;
    public static SkyCollisionLeft instance;
    GameManager gameManager;

    Rigidbody2D targetRigidbody;
    
    void Start () {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Invoke("initManager", 4);
        
	}

    void Update()
    {
        if (gameManager != null)
        {
            if (player != null)
            {
                if (gameManager.currentScorePlayer == 0)
                {
                    player.RpcLose();

                }
                else if (gameManager.currentScoreEnemy == 0 && gameManager.currentScorePlayer > 0)
                {
                    player.RpcWin();
                }
            }
            
        }
    }

    private void initManager()
    {
        GameObject gameObjectManager = GameObject.FindWithTag("GameManager");
        if (gameObjectManager != null)
        {
            gameManager = gameObjectManager.GetComponent<GameManager>();
            Debug.Log("Script Manager 'GameManger' is find  ");
        }
        else
        {
            Debug.Log("can t find 'GameManager ' script");
        }

    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Ball")
        {

            targetRigidbody = collider.attachedRigidbody;
            player = targetRigidbody.GetComponent<Player>();
            playerControl = targetRigidbody.GetComponent<PlayerControl>();

        }    
        
    }
}
