using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Bomb bombPrefab;
    public int bombLevel = 1;
    private bool canRewind = false;
    public float cooldown;
    private float cooldownProgress;
    public bool bombAvailable = true;
    public Vector3 originalPosition; 

    public ProgressBar cdBar;  
    public Image rewindUI;  

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        cooldownProgress = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTileOn();

        if (!bombAvailable)
        {
            CooldownBomb(); 
        }

        UpdateCDBar();
        UpdateRewindUI(); 
    }

    public Tile CheckTileOn()
    {
        Tile tileOn = new Tile();
        Ray rayDown = new Ray(transform.position, transform.TransformDirection(-Vector3.up * 2f));
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up * 2f));

        if (Physics.Raycast(rayDown, out RaycastHit hit, 2f))
        {
            if (hit.collider.GetComponent<Tile>() != null)
            {
                return hit.collider.GetComponent<Tile>();
            }
        }

        return null; 
    }

    public void LaunchBomb()
    {
        bombAvailable = false;
        cooldownProgress = 0; 

        var tileOn = CheckTileOn(); 
        if(tileOn != null && tileOn.GetIsAvailable())
        {
            var positionToSpawn = new Vector3(tileOn.GetPosition().x, .5f, tileOn.GetPosition().y);
            var bomb = Instantiate(bombPrefab, positionToSpawn, Quaternion.identity);
            bomb.explosionRadius = bombLevel; 
        }  
    }

    public void IncreaseBombLevel()
    {
        bombLevel += 1; 
    }

    public bool GetCanRewind()
    {
        return canRewind;
    }

    public void SetCanRewind(bool set)
    {
        canRewind = set; 
    }

    public void CooldownBomb()
    {
        cooldownProgress += Time.deltaTime;

        if(cooldownProgress >= cooldown)
        {
            bombAvailable = true; 
        }
    }

    public void UpdateCDBar()
    {
        cdBar.UpdateBar(cooldownProgress, cooldown);
    }
    
    public void UpdateRewindUI()
    {
        rewindUI.enabled = canRewind; 
    }

    public void ResetPosition()
    {
        transform.position = originalPosition; 
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Bomb>() != null)
        {
            var bombJustExited = other.GetComponent<Bomb>();
            bombJustExited.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerUps>())
        {
            other.GetComponent<PowerUps>().GetCollected(this);
            Destroy(other.GetComponent<PowerUps>().gameObject);
        }
    }

}
