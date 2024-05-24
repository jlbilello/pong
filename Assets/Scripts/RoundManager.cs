using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using LightReflectiveMirror;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject paddle1;
    bool paddle1Init = false;

    [SerializeField]
    GameObject paddle2;
    bool paddle2Init = false;

    [SerializeField]
    BallMover ballMover;

    public static RoundManager instance;

    void Awake(){
        instance = this;
    }

    public void OnServerReady(NetworkConnectionToClient conn)
    {
        if(!paddle1Init){
            paddle1Init = true;
            NetworkServer.AddPlayerForConnection(conn, paddle1);
            return;
        }

        if(!paddle2Init){
            paddle2Init = true;
            NetworkServer.AddPlayerForConnection(conn, paddle2);
            return;
        }
    }

    public void OnServerDisconnect(){
        paddle2Init = false;
    }
}
