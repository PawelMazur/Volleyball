using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSpawnBall : NetworkBehaviour {

    private static GameManager instance;
    public GameObject ball;
    public Transform spawnBall1;
    public Transform spawnBall2;


    float timeReset = 4f;
    private float timeWait;

    void Start()
    {

        

        if (isServer && isClient)
        {
            Invoke("WaitWithStartCreatBall", 5);
            //print("Number Player : " + Player.players[0].netId);
        }

        /*
        if (isLocalPlayer)
        {
            if (NetworkManager.singleton.IsClientConnected())
            {
                if (isServer && isClient)
                {
                    Invoke("WaitWithStartCreatBall", 5);
                    print("Number Player : " + Player.players[0].netId);
                }
            }
        }*/
        

        
    }
    
    public void WaitWithStartCreatBall()
    {

        StartCreatBall(spawnBall1.position, spawnBall1.rotation);
    }
    
    
    void StartCreatBall(Vector3 position, Quaternion rotation)
    {
        GameObject ballInstance = Instantiate(ball, position, rotation) as GameObject;
        NetworkServer.Spawn(ballInstance);
    }


}
