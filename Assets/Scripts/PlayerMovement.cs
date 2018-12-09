using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float HorizontalSpeed;
    public float VerticalSpeed;

    private Vector3 startedPos;

    public float speed;
    public float jumpSpeed;
    public float gravity;

    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        startedPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        HorizontalSpeed = 3.0f;
        VerticalSpeed = 3.0f;
        speed = 6.5f;
        jumpSpeed = 6.5f;
        gravity = 21.0f;
    }

    void Update()
    {
        if (!GeneralGameValues.Paused)
        {
            Vector3 rotCamY = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

            transform.eulerAngles = rotCamY;

            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = moveDirection * speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

            characterController.Move(moveDirection * Time.deltaTime); 
        }
    }

    public void Revive()
    {
        transform.localPosition = startedPos;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Death()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

}
