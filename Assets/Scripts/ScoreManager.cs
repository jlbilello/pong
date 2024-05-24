using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    [SyncVar(hook = "onScoreLeftChange")]
    public int scoreLeft = 0;

    [SyncVar(hook = "onScoreRightChange")]
    public int scoreRight = 0;

    [SerializeField]
    TMPro.TMP_Text scoreLeftText;

    [SerializeField]
    TMPro.TMP_Text scoreRightText;

    public void onScoreLeftChange(int oldValue, int newValue){
        scoreLeftText.text = newValue.ToString();
    }

    public void onScoreRightChange(int oldValue, int newValue){
        scoreRightText.text = newValue.ToString();
    }
}
