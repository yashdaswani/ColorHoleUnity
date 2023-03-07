using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{
    [Header ("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 moveLimits;
    //Hole vertices radius from the hole's center
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] Transform rotatingCircle;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;

    [Space]
    [SerializeField] float speed;

    float x, y;
    Vector3 touch, targetPos;

    void Start()
    {
        RotateCircleAnim();
        GameManager.isMoving = false;
        GameManager.isGameOver = false;
        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        mesh = meshFilter.mesh;
        FindHoleVertices();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse
#if UNITY_EDITOR
        //isMoving=true whenever mouse is clicked 
        //isMoving=falseever mouse is released
        GameManager.isMoving = Input.GetMouseButton(0);

        if (!GameManager.isGameOver && GameManager.isMoving)
        {
            //Move hole center
            MoveHole();
            //Update hole vertices
            UpdateHoleVertices();
        }


        //Touch
#else
		//TouchPhase.Moved to prevent hole from jumping at first touch
		GameManager.isMoving = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved;

		if (!GameManager.isGameOver && GameManager.isMoving) {
		//Move hole center
		MoveHole ();
		//Update hole vertices
		UpdateHoleVertices ();
		}
#endif
        GameManager.isMoving = Input.GetMouseButton(0);
        if(!GameManager.isGameOver && GameManager.isMoving)
        {
            MoveHole();
            UpdateHoleVertices();
        }

        
    }

    void RotateCircleAnim()
    {
        //rotate circle arround Y axis by -90°
        //duration: 0.2 seconds
        //start: Vector3 (90f, 0f, 0f)
        //loop: -1 (infinite)
        rotatingCircle
            .DORotate(new Vector3(90f, 0f, -90f), .2f)
            .SetEase(Ease.Linear)
            .From(new Vector3(90f, 0f, 0f))
            .SetLoops(-1, LoopType.Incremental);
    }

    void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        //lerp (smooth) movement
        touch = Vector3.Lerp(
            holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y), //move hole on x & z 
            speed * Time.deltaTime
        );
        Debug.Log(touch);

        targetPos = new Vector3(
            //Clamp: to prevent hole from going outside of the table
            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),//limit X
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)//limit Z
        );
        holeCenter.position = targetPos;
    }

    void UpdateHoleVertices()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }
        //update mesh vertices
        mesh.vertices = vertices;
        //update meshFilter's mesh
        meshFilter.mesh = mesh;
        //update collider
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if(distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i]-holeCenter.position);
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

//GameManager.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

//if (!GameManager.isGameOver && GameManager.isMoving)
//{
//    //Move hole center
//    MoveHole();
//    //Update hole vertices
//    UpdateHoleVertices();
//}