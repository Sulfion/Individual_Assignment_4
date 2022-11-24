using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamController : MonoBehaviour
{
    public Vector3 collision = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem part = GetComponent<ParticleSystem>(); //get the particle system on the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance:3.5f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                var part = GetComponent<ParticleSystem>();
                part.Play();
                Debug.Log("Hit");
            }
        }
    }
}
