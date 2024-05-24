using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using LightReflectiveMirror;

public class OfflineButtonManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_InputField inputField;

    public void StartHost(){
        NetworkManager.singleton.StartHost();
    }

    public void JoinClient(){
        NetworkManager.singleton.networkAddress = inputField.text;
        NetworkManager.singleton.StartClient();
    }
}
