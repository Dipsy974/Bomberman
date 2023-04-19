using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int positionX, positionY;
    private bool isAvailable = true;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPositionXY(int x, int y)
    {
        positionX = x;
        positionY = y;
    }

    public Vector2 GetPosition()
    {
        return new Vector3(positionX,positionY); 
    }

    public void SetAvailable(bool set)
    {
        isAvailable = set; 
    }

    public bool GetIsAvailable()
    {
        return isAvailable; 
    }
}
