using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BallManager : NetworkBehaviour {
    public PlayerManager playerManager;

    public void setPlayerManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public PlayerManager getPlayerManager()
    {
        return this.playerManager;
    }
}
