using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class LobbyMenu : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, 
        GameObject lobbyPlayer, GameObject gamePlayer) {

        LobbyPlayer lPlayer = lobbyPlayer.GetComponent<LobbyPlayer>();
        Player gPlayer = gamePlayer.GetComponent<Player>();

        gPlayer.playerName = lPlayer.playerName;
        gPlayer.playerColor = lPlayer.playerColor;

        Debug.Log("PlayerName : " + gPlayer.playerName);
    }

    
}
