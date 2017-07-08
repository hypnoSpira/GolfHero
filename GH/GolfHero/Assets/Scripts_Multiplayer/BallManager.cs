using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallManager : NetworkBehaviour {
    private PlayerManager playerManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void setPlayerManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public PlayerManager getPlayerManager()
    {
        return this.playerManager;
    }
}
