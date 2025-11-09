using UnityEngine;

[RequireComponent (typeof(Rigidbody))] 
public class PlatformScript : MonoBehaviour
{
    public bool MoveUpDown = false;
    public float MoveUpDownStrength = 1.0f;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = this.GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveUpDown)
        {
            this.GetComponent<Rigidbody>().MovePosition(startPosition + Vector3.up * Mathf.Sin(Time.time));
        }
    }
}
