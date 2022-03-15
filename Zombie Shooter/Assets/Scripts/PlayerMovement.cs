using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Getting user's keyboard input
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= Time.deltaTime * speed;
            transform.Translate(movement, Space.World);
        }


        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);


        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime); //Dampening effect at end so no snapping
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}
