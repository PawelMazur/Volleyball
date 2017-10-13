using UnityEngine;
using System.Collections;

public class DestroyBall : MonoBehaviour {

    private GameManager gameManager;
    public float m_TimeToDestroy = 2f;
    private bool collisionActive = true;
    public AudioClip audioClip;
    public AudioClip audioClipGrid;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject gameObjectManager = GameObject.FindWithTag("GameManager");
        if (gameObjectManager != null)
        {
            gameManager = gameObjectManager.GetComponent<GameManager>();
        }
        else
        {
            Debug.Log("Can t find 'GameManager' script ");
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {

        if (collisionActive)
        {

            if (collider.gameObject.tag == "GroundLeft")
            {
                collisionActive = false;
                Invoke("DestroyBallWithDelay", m_TimeToDestroy);
                gameManager.RpcDisplayTextLog(2);
                gameManager.TakeScorePlayer(1);
                //SkyCollisionLeft.instance.TakeScore(1);
                //Debug.Log("GroundLeft : ");
                Audio();
            }

            if (collider.gameObject.tag == "GroundRight")
            {
                collisionActive = false;
                Invoke("DestroyBallWithDelay", m_TimeToDestroy);
                gameManager.RpcDisplayTextLog(1);
                gameManager.TakeScoreEnemy(1);
                //SkyCollisionRight.instance.TakeScore(1);
                //Debug.Log("GroundRight : ");
                Audio();
            }
        }

        if (collider.gameObject.tag == "Grid")
        {
            audioSource.clip = audioClipGrid;
            audioSource.Play();
        }
    }


    void DestroyBallWithDelay()
    {
        Destroy(gameObject);
    }

    public void Audio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
