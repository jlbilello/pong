using System.Collections;
using System.Collections.Generic;
using LightReflectiveMirror;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongNetworkManager : NetworkManager
{
    //spawn players
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);
        RoundManager.instance.OnServerReady(conn);
    }

    //enable the reconnection player 2
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        RoundManager.instance.OnServerDisconnect();
    }
}
