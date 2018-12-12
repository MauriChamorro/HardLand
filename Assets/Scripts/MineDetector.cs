using System.Collections.Generic;
using UnityEngine;

public class MineDetector : MonoBehaviour
{
    public GameObject pj;
    public Color colorTarget1;
    public Color colorTarget2;
    public SphereCollider sCollider;
    public ColorLerper colorLerper;

    public List<GameObject> collisions;

    private void Awake()
    {
        sCollider = GetComponent<SphereCollider>();
        colorLerper = GetComponent<ColorLerper>();
    }

    void Start()
    {
        colorLerper.Initializer(colorTarget1, colorTarget2);
    }

    void Update()
    {
        transform.position = pj.transform.position;
        colorLerper.UpdateLerp(GOMenorDistancia());
    }

    public float GOMenorDistancia()
    {
        float min = 1000000f;
        foreach (GameObject mine in collisions)
        {
            if (mine)
            {
                float auxMin = Vector3.Distance(transform.position, mine.transform.position);

                if (auxMin < min)
                    min = auxMin;
            }
        }

        return min;
    }

    public void Initializer(float pRadius, Color pColoDetector)
    {
        sCollider = GetComponent<SphereCollider>();
        sCollider.radius = pRadius;
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
