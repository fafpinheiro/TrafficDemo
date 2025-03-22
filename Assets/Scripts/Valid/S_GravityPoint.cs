using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GravityPoint : MonoBehaviour
{

    [SerializeField]private float _gravityStrength = 10f;
    [SerializeField] private LayerMask _affectedLayer;
    [SerializeField] private float _affectedDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyGravityField();
    }

    private void ApplyGravityField()
    {
        // Find all objects in the affected layer
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, _affectedDistance, _affectedLayer);

        foreach (Collider obj in objectsInRange)
        {
            // Skip the gravitational point itself
            if (obj.CompareTag("GravityPoint")) continue;

            

            // Calculate the distance between the object and the gravitational point
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            // Avoid division by zero if the object is at the same position as the gravity point
            if (distance > 0f)
            {
                // Apply the gravitational force to the object
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Calculate the direction of gravity
                    Vector3 directionToGravity = (transform.position - obj.transform.position).normalized;

                    // Calculate the gravitational force (inverse square law)

                    // For circular motion, we need the force to match the required centripetal force
                    float gravitationalForce = _gravityStrength / (distance * distance);

                    // Calculate the required tangential velocity to keep the object in a circular orbit
                    float requiredSpeed = Mathf.Sqrt(gravitationalForce / distance);

                    // Find the tangential direction (perpendicular to the direction towards the gravity point)
                    Vector3 tangentialVelocity = Vector3.Cross(directionToGravity, Vector3.up) * requiredSpeed;

                    // Apply the tangential velocity to the object
                    rb.velocity = tangentialVelocity;

                    // Apply the gravitational force to keep the object in orbit
                    Vector3 gravitationalForceVector = directionToGravity * gravitationalForce; //m*(tangencialSpeed)^2 / r
                    rb.AddForce(gravitationalForceVector, ForceMode.Acceleration);
                }
            }
        }
    }

    // This method is called when another collider enters the gravitation point's collider
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the object colliding has a specific tag or other properties
        if ((_affectedLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(collision.gameObject);  // Destroy the colliding object
            Debug.Log("Object destroyed: " + collision.gameObject.name);
        }
    }
}
