using UnityEngine;
using System.Collections;

public class SkyCollisionRight : MonoBehaviour {

    Player player;
    public bool playerLose = false;
    public static SkyCollisionRight instance;

    GameManager gameManager;
    Rigidbody2D targetRigidbody;
    bool turn = false;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        Invoke("initManager", 4);

    }

    void Update()
    {
        if (gameManager != null)
        {
            //Debug.Log("SkyCollisionRight Collider 2D : " + player);
            //Debug.Log("SkyCollisionRight CurrentScore : " + gameManager.currentScoreEnemy);
            if (player != null)
            {
                if (gameManager.currentScoreEnemy == 0)
                {
                    player.RpcLose();

                }
                else if (gameManager.currentScorePlayer == 0 && gameManager.currentScoreEnemy > 0)
                {
                    player.RpcWin();
                }
            }
            
        }
        
        if (targetRigidbody != null)
        {
            if (!turn)
            {
                targetRigidbody.transform.rotation = Quaternion.Euler(0, 180, 0);
                turn = true;
            }
        }

    }

    public void turnPlayer(){
        targetRigidbody.transform.rotation = Quaternion.Euler(0, 180, 0);
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

        }

    }

}
