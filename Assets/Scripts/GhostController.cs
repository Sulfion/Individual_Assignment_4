using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;
using UnityEngine.VFX;

public class GhostController : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public AudioClip ghostScream;
    public GameObject ghostDeath;
    public GameObject fakeGhostBody;


    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = ghostScream;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    //create timer to track how how long until the ghost picks a new position to move to
    //using RandomNavSphere set destination to new position
    public void Timer()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    //choose a random direction, use navmesh move to it
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //play scream when ghost collides with player
    //some of this is really dumb, can't get ghost to stop moving so I hide renderer, spawn fake stationary ghost, wait until VFX are done, then destroy both
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;               //hide the ghost on collision
            GetComponent<AudioSource>().Play();                                   //play ghost scream
            Instantiate(ghostDeath, transform.position, Quaternion.identity);    //play death particle effect at ghost position
            GameObject fakeGhost = Instantiate(fakeGhostBody, transform.position, Quaternion.identity);//spawn fake ghost body
            Destroy(gameObject, 2.4f);               //wait, then destroy ghost
            Destroy(fakeGhost, 2.4f);               //wait, then destroy fake ghost
        }
    }
}
