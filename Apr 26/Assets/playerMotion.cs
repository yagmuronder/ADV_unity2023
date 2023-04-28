using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMotion : MonoBehaviour
{
   // public Transform target;
    public NavMeshAgent agent;
    Animator animatorPersonaje;
    //public Transform MarcaDestino;
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animatorPersonaje = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //MOVE OUR AGENT
                agent.SetDestination(hit.point);
                //agent.destination = MarcaDestino.position = hit.point;
                //MarcaDestino.GetComponent<AudioSource>().Play();
            }
        }
           

            

        //if (agent.isOnOffMeshLink) { animatorPersonaje.SetTrigger("saltar"); }

        //animatorPersonaje.SetFloat("lateral", transform.InverseTransformDirection(agent.velocity).x);
        //animatorPersonaje.SetFloat("avance", transform.InverseTransformDirection(agent.velocity).z);
    }
}

