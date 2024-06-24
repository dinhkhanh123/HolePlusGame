using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [Header(" Hole Mesh ")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header(" Hole vertices radius ")]
    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] Transform rotatingCircle;

    [Space]
    [SerializeField] float moveSpeed;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;

    float x, y;
    Vector3 touch, targetPos;


    void Start()
    {
        GameManager.isMoving = false;
        GameManager.isGameover = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = meshFilter.mesh;

        //Find Hole vertices on the mesh
        FindHoleVertices();

        RotateCircleAnim();
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
                //Mouse move
                GameManager.isMoving = Input.GetMouseButton(0);

                if (!GameManager.isGameover && GameManager.isMoving)
                {
                    //Move hole center
                    MoveHole();
                    //Update hole vertices
                    UpdateHoleVerticesPosition();
                }
        #else
             GameManager.isMoving = Input.touchCount>0&&Input.GetTouch(0).phase == TouchPhase.Moved;

                    if (!GameManager.isGameover && GameManager.isMoving)
                    {
                        //Move hole center
                        MoveHole();
                        //Update hole vertices
                        UpdateHoleVerticesPosition();
                    }
        #endif
    }


    void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(
            holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y),
            moveSpeed * Time.deltaTime
            );

        targetPos = new Vector3(
            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y));

        holeCenter.position = targetPos;
    }

    void RotateCircleAnim()
    {
        rotatingCircle.DORotate(new Vector3(90f, 0f, -90f), .2f)
            .SetEase(Ease.Linear)
            .From(new Vector3(90f, 0f, 0f)).SetLoops(-1, LoopType.Incremental);
    }

    void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }


        //update mesh
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
}
