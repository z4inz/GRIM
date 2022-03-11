using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 5f;
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
        movement *= Time.deltaTime * speed;

        transform.Translate(movement, Space.World);

        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime); //Dampening effect at end so no snapping
        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
    }
}
