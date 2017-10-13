using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerScore : NetworkBehaviour {

    const int maxScore = 3;
    [SyncVar(hook = "OnScoreChanged")] int currentScore = maxScore;
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void TakePlayerScore(int amount)
    {
        int temp = currentScore - amount;
        if (temp > 0)
        {
            currentScore -= amount;
        }
        else
        {
            currentScore = 0;
        }

    }

    public void OnScoreChanged(int value)
    {
        currentScore = value;
        PlayerCanvas.canvas.SetScorePlayer("Score : " + currentScore);

        if (currentScore == 0)
        {
            //player.Lose();
            
        }

    }
}
