using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 


public class Bomb : MonoBehaviour
{
    public float timeBeforeExplosion;
    public int explosionRadius;
    public LineRenderer explosionLinePrefab;
    public ParticleSystem explosionVFX; 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.DOColor(Color.red, timeBeforeExplosion); 
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeExplosion -= Time.deltaTime;
        if(timeBeforeExplosion <= 0)
        {
            Explode(); 
            Destroy(this.gameObject); 
        }
    }

    public void Explode()
    {
        Instantiate(explosionVFX, transform.position + Vector3.up, Quaternion.identity); 
        GenerateExplosionLines();
        Camera.main.GetComponent<CamShake>().StartShake(); 
    }

    public void GenerateExplosionLines()
    {
        //RAYCAST 
        List<Ray> list_rays = new List<Ray>();
        List<LineRenderer> list_lines = new List<LineRenderer>();
        List<Vector3> list_radius = new List<Vector3>();


        Vector3 radiusLeft = -Vector3.right * explosionRadius;
        Vector3 radiusRight = Vector3.right * explosionRadius;
        Vector3 radiusUp = Vector3.forward * explosionRadius;
        Vector3 radiusDown = -Vector3.forward * explosionRadius;

        list_radius.Add(radiusLeft);
        list_radius.Add(radiusRight);
        list_radius.Add(radiusUp);
        list_radius.Add(radiusDown);


        Ray rayLeft = new Ray(transform.position, radiusLeft);
        Ray rayRight = new Ray(transform.position, radiusRight);
        Ray rayUp = new Ray(transform.position, radiusUp);
        Ray rayDown = new Ray(transform.position, radiusDown);

        list_rays.Add(rayLeft);
        list_rays.Add(rayRight);
        list_rays.Add(rayUp);
        list_rays.Add(rayDown);


        //LINE RENDERER VISUEL
        LineRenderer lineLeft = Instantiate(explosionLinePrefab, Vector3.zero, Quaternion.identity);
        lineLeft.SetPosition(0, transform.position);

        LineRenderer lineRight = Instantiate(explosionLinePrefab, Vector3.zero, Quaternion.identity);
        lineRight.SetPosition(0, transform.position);


        LineRenderer lineUp = Instantiate(explosionLinePrefab, Vector3.zero, Quaternion.identity);
        lineUp.SetPosition(0, transform.position);


        LineRenderer lineDown = Instantiate(explosionLinePrefab, Vector3.zero, Quaternion.identity);
        lineDown.SetPosition(0, transform.position);


        list_lines.Add(lineLeft);
        list_lines.Add(lineRight);
        list_lines.Add(lineUp);
        list_lines.Add(lineDown);


        for (int i = 0; i < list_rays.Count; i++)
        {
            if (Physics.Raycast(list_rays[i], out RaycastHit hit, explosionRadius))
            {
                list_lines[i].SetPosition(1, hit.transform.position); //Set la fin de la ligne d'explosion si on touche un objet
                if (hit.collider.GetComponent<Destructible>())
                { 
                    hit.collider.GetComponent<Destructible>().GetTileOn().SetAvailable(true);  //Rend la tile disponible
                    hit.collider.GetComponent<Destructible>().GetDestroyed();
                }
                if (hit.collider.GetComponent<PlayerController>())
                {
                    hit.collider.GetComponent<PlayerController>().ResetPosition(); 
                }

            }
            else
            {
                list_lines[i].SetPosition(1, transform.position + list_radius[i]); //Sinon set à son radius de base 
            }
        }

    }
}
