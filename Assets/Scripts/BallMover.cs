using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BallMover : NetworkBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float bounds = 12;

    [SerializeField]
    ScoreManager scoreManager;

    Vector3 startPos;
    Vector3 startDirection = Vector3.left;

    [SyncVar]
    Vector3 direction = Vector3.left;

    double hitCooldown = 0;

    void Start(){
        startPos = this.transform.position;
    }

    public void resetBall(){
        startDirection = Vector3.left;
        direction = Vector3.left;
        this.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        //if hosting and no other player, don't move
        if(NetworkServer.active && NetworkServer.connections.Count < 2) return;

        this.transform.position += direction * Time.deltaTime * speed;
        if(Mathf.Abs(this.transform.position.z) > 4.5 && Time.timeAsDouble - hitCooldown > 0.125){
            direction = new Vector3(direction.x, 0, direction.z * -1);
            hitCooldown = Time.timeAsDouble;
        }

        //scores only managed by server
        if(!isServer) return;
        if(Mathf.Abs(this.transform.position.x) > bounds){

            if(this.transform.position.x > 0){
                scoreManager.scoreLeft++;
            } else{
                scoreManager.scoreRight++;
            }

            syncPositionRpc(startPos);

            startDirection = startDirection == Vector3.left ? Vector3.right : startDirection = Vector3.left;
            direction = startDirection;
        }
    }

    [ClientRpc]
    public void syncPositionRpc(Vector3 position){
        this.transform.position = position;
    }

    [Command(requiresAuthority =false)]
    public void registerHit(float offset, bool left){
        direction = new Vector3(direction.x * -1, 0, offset);
        syncPositionRpc(this.transform.position); //sync other client to host
    }
}
