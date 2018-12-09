using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public float mouseSpeedH;
    public float mouseSpeedV;

    public float yaw;
    public float pitch;

    private void Start()
    {
        mouseSpeedH = 1f;
        mouseSpeedV = 1f;
        yaw = 1f;
        pitch = 1f;
    }

    void Update()
    {
        if (!GeneralGameValues.Paused)
        {
            Cursor.lockState = CursorLockMode.Locked;

            yaw += mouseSpeedH * Input.GetAxis("Mouse X");
            pitch -= mouseSpeedH * Input.GetAxis("Mouse Y");

            if (pitch > 88f)
                pitch = 88f;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
