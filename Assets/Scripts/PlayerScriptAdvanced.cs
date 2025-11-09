using UnityEngine;

public class PlayerScriptAdvanced : MonoBehaviour
{
    public float WalkingSpeed = 5.0f;
    public float JumpForce = 5.0f;
    public float rotationSpeed = 120f;
    private Animator animator;

    private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CapsuleCollider capsule = this.GetComponent<CapsuleCollider>();
        Vector3 rayorigin = capsule.center + gameObject.transform.localPosition;
        float maxDistance = capsule.height * 0.5f + 0.2f;
        Vector3 raydirection = Vector3.down * (maxDistance);
        Debug.DrawRay(rayorigin, raydirection, Color.green);

        Ray ray = new Ray(rayorigin, Vector3.down);
        RaycastHit hit;

        bool grounded = false;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            grounded = true;
            Debug.Log("I hit something " + hit.collider.name);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            animator.SetTrigger("jump");
        }
    }

    private void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * WalkingSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        bool walking = movement.magnitude > 0.01f || Mathf.Abs(turn) > 0.1f;
        animator.SetBool("walk", walking);
    }
}
