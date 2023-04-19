using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLine : MonoBehaviour
{
    public float lifeTime;
    private LineRenderer lineRend; 
    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            lineRend.startWidth -= 0.05f;
            lineRend.endWidth -= 0.05f;
        }

        if(lineRend.startWidth <= 0 && lineRend.endWidth <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
