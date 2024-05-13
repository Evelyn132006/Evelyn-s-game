using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Bear : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    private bool canFollow=false;
    private Animator anim;
    private float timer;
    public AudioClip bear_attack;
    private AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(canFollow)
        {
            agent.SetDestination(player.transform.position);
            if(agent.remainingDistance<3 && agent.remainingDistance>0 && timer>2)
            {
                anim.SetTrigger("attack");
                audio_source.PlayOneShot(bear_attack);
                player.GetComponent<player>().healthSlider.value -= 15;
                timer = 0;
            }
        }
        else
        {

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("FPSController"))
        {
            anim.SetBool("walk", true);
            anim.SetBool("sleep", false);
            canFollow = true;
        }
        else
        {
            canFollow = false;
            anim.SetBool("walk", false);
            anim.SetBool("sleep", true);
        }
    }
}
