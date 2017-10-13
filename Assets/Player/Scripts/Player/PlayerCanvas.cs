using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour {


    public static PlayerCanvas canvas;

    [SerializeField] Text scoreEnemy;
    [SerializeField] Text scorePlayer;
    [SerializeField] Text gameStatusText;
    [SerializeField] Text logText;
    [SerializeField] Text playerName;
    [SerializeField] Text enemyName;

	// Use this for initialization
	void Awake () {

        if (canvas == null)
        {
            canvas = this;
        }
        else if (canvas != this)
            Destroy(gameObject);
	}

    void Reset()
    {
        scoreEnemy = GameObject.Find("EnemyScore").GetComponent<Text>();
        scorePlayer = GameObject.Find("PlayerScore").GetComponent<Text>();
        gameStatusText = GameObject.Find("GameStatusText").GetComponent<Text>();
        logText = GameObject.Find("LogText").GetComponent<Text>();
        playerName = GameObject.Find("PlayerName").GetComponent<Text>();
        enemyName = GameObject.Find("EnemyName").GetComponent<Text>();


    }



    public void SetScoreEnemy(string amount)
    {
        scoreEnemy.text = amount.ToString();
    }

    public void SetScorePlayer(string amount)
    {
        scorePlayer.text = amount.ToString();
    }

    public void SetGameStatusText(string amount)
    {
        gameStatusText.text = amount.ToString();
    }

    public void SetLogText(string amount)
    {
        logText.text = amount.ToString();
    }

    public void PlayerName(string amount)
    {
        playerName.text = amount.ToString();
    }

    public void EnemyName(string amount)
    {
        enemyName.text = amount.ToString();
    }

    
}
