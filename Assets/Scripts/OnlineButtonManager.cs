using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using LightReflectiveMirror;

public class OnlineButtonManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text joinText;

    void Update(){
        if(NetworkServer.active) joinText.text = ((LightReflectiveMirrorTransport)(NetworkManager.singleton).transport).serverId;
        else joinText.text = "";
    }

    public void Disconnect(){
        if(NetworkServer.active){
            NetworkManager.singleton.StopHost();
        } else {
            NetworkManager.singleton.StopClient();
        }
    }
}
