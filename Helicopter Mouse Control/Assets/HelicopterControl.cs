
using UnityEngine;

public class HelicopterControl : MonoBehaviour
{
    private new Rigidbody rigidbody;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float pitchAngle = 90f;
    [SerializeField] private float maxTurningAngle = 30f;
    [SerializeField] private Transform camera;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 cameraForwardFlat = new Vector3(camera.forward.x, 0, camera.forward.z).normalized;
        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        transform.Rotate(camera.right, input.y * Time.deltaTime * pitchAngle, Space.World);
        transform.Rotate(camera.forward, -input.x * Time.deltaTime * pitchAngle, Space.World);
        rigidbody.AddForce(cameraForwardFlat * input.y * speed);
        rigidbody.AddForce(camera.right * input.x * speed);

        Quaternion trueRotationTotal = Quaternion.LookRotation(cameraForwardFlat, Vector3.up);
        Quaternion trueRotation = Quaternion.RotateTowards(transform.rotation, trueRotationTotal, maxTurningAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, trueRotation, 3f * Time.deltaTime);
    }
}




