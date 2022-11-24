using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamController : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem part = GetComponent<ParticleSystem>(); //get the particle system on the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        DistanceFromSteamCheckAndRaycast();
    }

    //create raycast, check if gameobject is player using tag, if it is a player, play steam particle effect
    //check how long player was in raycast, if 3seconds or longer play smoke particle
    public void DistanceFromSteamCheckAndRaycast()
    {
        var ray = new Ray(this.transform.position, this.transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance: 3.5f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                timer += Time.deltaTime;
                if (timer >= 3)
                {
                    var part = GetComponent<ParticleSystem>();
                    part.Play();
                    Debug.Log("Hit");
                    timer = 0;
                }
            }
        }
    }
}
