using UnityEngine;

public class Bola : MonoBehaviour
{
    private Rigidbody rb;
    public float direction;
    public float speedTras;
    public float speedRot;
    private Vector3 startedPos;
    public Vector3 move;
    public Vector3 rot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startedPos = transform.localPosition;
        move = startedPos;
        speedTras = 0.2f;
        speedRot = 2;
        direction = 1;
    }

    public void Resume(bool pBool)
    {
        rb.isKinematic = !pBool;
    }

    public void Reset()
    {
        transform.position = startedPos;
    }

    void Update()
    {
        if (GeneralGameValues.Playing)
        {
            move += Vector3.left * speedTras * direction;
            rot += Vector3.forward * speedRot * direction;
            transform.SetPositionAndRotation(move, Quaternion.Euler(rot));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WallBall")
        {
            direction *= -1;
        }
    }

    
}
