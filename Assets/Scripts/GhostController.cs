using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class GhostController : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;


    bool playerSeen = false;
    bool chasingPlayer = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) animator.speed = 1;

        if (!chasingPlayer)
        {
            if (!playerSeen)
            {
                if (Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) < 5f)
                {
                    playerSeen = true;
                    animator.SetTrigger("PlaySighted");
                    Invoke("StartChase", 1f);
                }
            }
        }
        else
        {
            agent.SetDestination(FindObjectOfType<PlayerController>().transform.position);
        }
    }


    private void StartChase()
    {
        chasingPlayer = true;
    }


    public void StopParticles()
    {
        List<ParticleSystem> parts = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        foreach (ParticleSystem p in parts)
        {
            p.Pause();
        }
    }


    public void StartParticles()
    {
        List<ParticleSystem> parts = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        foreach(ParticleSystem p in parts)
        {
                p.Play();
        }
    }

}
