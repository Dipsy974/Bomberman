using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    public Tile tileOn;
    public ParticleSystem destroyedVFX;
    public List<PUSpawn> possiblePowerUps;
    private PowerUps powerToSpawn; 

    [System.Serializable]
    public class PUSpawn
    {
        public PowerUps powerUpPrefab;
        public float probability; 
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile GetTileOn()
    {
        return tileOn; 
    }
    public void SetTileOn(Tile tile)
    {
        tileOn = tile; 
    }

    public void GetDestroyed()
    {
        Instantiate(destroyedVFX, transform.position, Quaternion.identity);
        SpawnObject(); 
        Destroy(gameObject);
    }

    public void SpawnObject()
    {

        var probability = Random.Range(0f, 1f);
        Debug.Log(probability); 
        foreach (var powerUp in possiblePowerUps)
        {
            if(probability <= powerUp.probability)
            {
                powerToSpawn = powerUp.powerUpPrefab; 
            }
        }

        Debug.Log(powerToSpawn); 
        
        if(powerToSpawn != null)
        {
            Instantiate(powerToSpawn, transform.position, Quaternion.identity); 
        }

    }
}
