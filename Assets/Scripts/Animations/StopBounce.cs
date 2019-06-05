using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBounce : MonoBehaviour
{
    public Rigidbody2D piece;

    float timmer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timmer += Time.deltaTime;
        if(piece.velocity.y < 0.005 && timmer > 9.0f)
        {
            piece.isKinematic = true;
            piece.velocity = Vector2.zero;
        }
        else
        {
            piece.isKinematic = false;
        }
    }
}
