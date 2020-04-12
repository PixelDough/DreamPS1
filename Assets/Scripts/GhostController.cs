using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class GhostController : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string moanSound;

    [FMODUnity.EventRef]
    public string floatSound;

    public NavMeshAgent agent;
    public Vector3 vectorToPlayer = Vector3.zero;
    Animator animator;

    PlayerController player;
    public bool playerSeen = false;
    public bool chasingPlayer = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //animator.speed = 0;
        StopParticles();

        player = FindObjectOfType<PlayerController>();

        StartCoroutine(Wander());

        //FMODUnity.RuntimeManager.PlayOneShotAttached(floatSound, gameObject);
        //moanEvent = FMODUnity.RuntimeManager.CreateInstance(moanSound);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) animator.speed = 1;

        if (!chasingPlayer)
        {
            if (!playerSeen)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 5f)
                {
                    agent.isStopped = true;
                    playerSeen = true;
                    animator.SetTrigger("PlaySighted");
                    Invoke("StartChase", 1f);

                    FMODUnity.RuntimeManager.PlayOneShotAttached(moanSound, gameObject);
                    //moanEvent.start();
                }
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
            vectorToPlayer = transform.position - player.transform.position;
        }
    }


    private void StartChase()
    {
        agent.isStopped = false;
        chasingPlayer = true;
    }


    private IEnumerator Wander()
    {

        yield return new WaitForSeconds(Random.Range(2f, 5f));

        Vector3 posChange = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        agent.SetDestination(transform.position + posChange);

        if (!playerSeen && !chasingPlayer)
        {
            StartCoroutine(Wander());
        }

        yield return null;
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
