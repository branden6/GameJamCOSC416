using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3(6, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "barrel")
        {
            Destroy(other.gameObject);
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "borderR")
        {
            GetComponent<Rigidbody>().linearVelocity = new Vector3(-6, 0, 0);
        }
        if (collision.gameObject.name == "borderL")
        {
            GetComponent<Rigidbody>().linearVelocity = new Vector3(6, 0, 0);
        }
    }
}
