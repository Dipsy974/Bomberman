using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Transform movePoint;


    void Start()
    {
        movePoint.parent = null; //Dissocie le Move Point du joueur pour pas qu'il soit tout le temps relatif au joueur 
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime); 

        //if(Vector3.Distance(transform.position, movePoint.position) > 0.05f) { return; } //Si la distance entre les deux est trop grande, ne bouge pas movePoint

        if(Mathf.Abs(Input.GetAxis("Horizontal")) >= 0f)
        {
            movePoint.position += new Vector3(Mathf.Ceil(Input.GetAxis("Horizontal")), 0, 0);
        }
        
        if(Mathf.Abs(Input.GetAxis("Vertical")) >= 0f)
        {
            movePoint.position += new Vector3(0, 0, Mathf.Ceil(Input.GetAxis("Vertical")));
        }
    }
}
