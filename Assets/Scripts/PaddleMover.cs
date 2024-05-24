using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PaddleMover : NetworkBehaviour
{
    [SerializeField]
    float bounds = 10;

    [SerializeField]
    float speed = 10;

    [SerializeField]
    BallMover ball;

    [SerializeField]
    bool isLeft;

    double hitCooldown = 0;

    void Update()
    {
        //hack for reconnects
        if(isLeft){
            this.transform.localPosition = new Vector3(-0.4875f, 1, this.transform.localPosition.z);
        } else {
            this.transform.localPosition = new Vector3(0.4875f, 1, this.transform.localPosition.z);
        }

        if(!isOwned){
            return;
        }

        float moveDirection = 0;
        if(Input.GetKey("w") || Input.GetKeyDown(KeyCode.UpArrow)){
            moveDirection = 1;
        } else if(Input.GetKey("s") || Input.GetKeyDown(KeyCode.DownArrow)){
            moveDirection = -1;
        }

        this.transform.position = this.transform.position + new Vector3(0, 0, moveDirection * Time.deltaTime * speed);

        if(this.transform.position.z > bounds){
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, bounds);
        }
        if(this.transform.position.z < -bounds){
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -bounds);
        }

        bool hittable = isLeft && ball.transform.position.x < -9 || !isLeft && ball.transform.position.x > 9;

        if(Time.timeAsDouble - hitCooldown > 1 && hittable && Mathf.Abs(this.transform.position.z - ball.transform.position.z) < 1.5){
            ball.registerHit(ball.transform.position.z - this.transform.position.z + moveDirection, isLeft);
            hitCooldown = Time.timeAsDouble;
        }
    }
}
