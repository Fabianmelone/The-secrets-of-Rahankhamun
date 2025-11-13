using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float WalkingSpeed = 10;
    public float JumpForce = 10;

    private Animator animator;
    // Update is called once per frame


 
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 oldVelocity = this.GetComponent<Rigidbody>().linearVelocity;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newVelocity = direction * WalkingSpeed;
        newVelocity.y = oldVelocity.y;
        this.GetComponent<Rigidbody>().linearVelocity = newVelocity;

        bool walking = direction.magnitude > 0.1f;
        animator.SetBool("walk", walking);
        if (walking)
        {
            this.GetComponent<Rigidbody>().transform.forward = direction.normalized;
        }

        CapsuleCollider capsule = this.GetComponent<CapsuleCollider>();
        Vector3 rayorigin = capsule.center + gameObject.transform.localPosition;
        float maxDistance = capsule.height * 0.5f + 0.02f;
        Vector3 raydirection = Vector3.down * (maxDistance);
        Debug.DrawRay(rayorigin, raydirection, Color.green);

        Ray ray = new Ray(rayorigin, Vector3.down);
        RaycastHit hit;

        bool grounded = false;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            grounded = true;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpForce);
            animator.SetTrigger("jump");
        }
    }

   
}
