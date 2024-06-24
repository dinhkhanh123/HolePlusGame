using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton class : Magnet

    public static Magnet instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    [SerializeField] float magnetForce;
    List<Rigidbody> affectedRigibodies = new List<Rigidbody>();
    Transform magnet;

    void Start()
    {
        magnet = transform;
        affectedRigibodies.Clear();
    }

    void FixedUpdate()
    {
        if (!GameManager.isGameover && GameManager.isMoving)
        {
            foreach (Rigidbody rb in affectedRigibodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (!GameManager.isGameover && (tag.Equals("Object") || tag.Equals("Obstacle")))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (!GameManager.isGameover && (tag.Equals("Object") || tag.Equals("Obstacle")))
        {
            RemoveToMagnetField(other.attachedRigidbody);
        }
    }

    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigibodies.Add(rb);
    }
    public void RemoveToMagnetField(Rigidbody rb)
    {
        affectedRigibodies.Remove(rb);
    }
}
