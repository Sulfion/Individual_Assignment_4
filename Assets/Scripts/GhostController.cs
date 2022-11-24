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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            Instantiate(ghostDeath, transform.position, Quaternion.identity);    
            Destroy(gameObject, 2.4f);
        }
    }
}
