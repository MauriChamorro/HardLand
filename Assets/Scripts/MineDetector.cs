using System.Collections.Generic;
using UnityEngine;

public class MineDetector : MonoBehaviour
{
    public GameObject pj;
    public Color colorTarget1;
    public Color colorTarget2;
    public SphereCollider colliderDetector;
    public ColorLerper colorLerper;

    public List<GameObject> collisions;
    public float maxDistanceToDetectMines = 1000000f;

    private void Awake()
    {
        colliderDetector = GetComponent<SphereCollider>();
        colorLerper = GetComponent<ColorLerper>();
    }

    void Start()
    {
        colorLerper.Initializer(colorTarget1, colorTarget2);
    }

    void Update()
    {
        transform.position = pj.transform.position;

        //alterna entre los dos colores en cada update segun la distancia a las minas cercanas
        colorLerper.UpdateLerp(DistanceToNearestMine());
    }

    public float DistanceToNearestMine()
    {
        var shortestDistance = maxDistanceToDetectMines;
        foreach (GameObject mine in collisions)
        {
            var distance = Vector3.Distance(transform.position, mine.transform.position);
            if (mine)
            {
                if (distance < shortestDistance)
                    shortestDistance = distance;
            }
        }

        return shortestDistance;
    }

    public void Initializer(float pRadius, Color pColoDetector)
    {
        colliderDetector = GetComponent<SphereCollider>();
        colliderDetector.radius = pRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mine")
        {
            if (!collisions.Exists(g => g.name == other.gameObject.name))
                collisions.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Mine")
        {
            if (collisions.Exists(g => g.name == other.gameObject.name))
                collisions.Remove(collisions.Find(g => g.name == other.gameObject.name));
        }
    }

    public void EmptyCollisions()
    {
        collisions.Clear();
    }
}