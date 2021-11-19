using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{

    public Enemy stats;
    public NavMeshAgent agent;

    public List<GameObject> waypoints = new List<GameObject>();

    GameObject nextWaypoint;
    


    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.speed;

        waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));

        FindNextWaypoint();

        agent.SetDestination(nextWaypoint.transform.position);

    }

    void Update()
    {
        ChangeDestination();
    }

    void ChangeDestination()
    {
        if(agent.remainingDistance < 0.1f)
        {
            waypoints.Remove(nextWaypoint);
            if (waypoints.Count != 0)
            {
                FindNextWaypoint();
                agent.SetDestination(nextWaypoint.transform.position);
            }
        }
    }

    void FindNextWaypoint()
    {
        nextWaypoint = waypoints[0];
        float distance = Vector3.Distance(transform.position, nextWaypoint.transform.position);


        for(int i = 1; i < waypoints.Count; i++)
        {
            float tmpDistance = Vector3.Distance(transform.position, waypoints[i].transform.position);
            if (tmpDistance < distance)
            {
                distance = tmpDistance;
                nextWaypoint = waypoints[i];
            }


        }

    }



}
