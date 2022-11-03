using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    //[SerializeField] GameObject BallPrefab;

    Vector3 ballPos;
    
    Vector3 endPointPos;
    Vector3 direction;
    Vector3 startPos;
    bool isTouch;
    RaycastHit hit;
    Vector3 pressMousePos;
    Vector3 releaseMousePos;

    [SerializeField] float posMinZ = -30f;
    [SerializeField] float posMaxZ = 0f;
    [SerializeField] float posMaxX = 20f;
    [SerializeField] float posMinX = -20f;

    [SerializeField] float force = 10f;
    [SerializeField] float time;
    [SerializeField] float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = new Vector3 (0, 4, 1);
        startPos = transform.position;
        direction = new Vector3(0, 1, 1);
;       //direction = endPointPos - ballPos;

        //direction = ballPos(0, transform.position.y++, transform.position.z++);
    }

    void FixedUpdate()
    {
        //if (isTouch)
        //{

        //    isTouch = false;
        //}
        time += Time.fixedDeltaTime * speed;
        
        //transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 1) * time);
        transform.position = move(direction, time);

        float timeFut = time + (Time.fixedDeltaTime * speed);

        Vector3 posFuture = move(direction, timeFut);
        //if (transform.position.x < posMinX || transform.position.x > posMaxX || transform.position.z < posMinZ || transform.position.z > posMaxZ)
        //    transform.position = ballPos;
        Vector3 directionFuture = posFuture - transform.position;

        //bool touch = Physics.Raycast(transform.position, directionFuture, Vector3.Distance(transform.position, directionFuture));
        //if (touch.collider.transform.CompareTag("Wall"))
        //{
        //    Debug.Log(touch.collider.name);
        //}




        if (Input.GetMouseButtonDown(0))
        {
            //pressMousePos = Camera.main.WorldToViewportPoint(Input.mousePosition
            pressMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //releaseMousePos = Camera.main.WorldToViewportPoint(Input.mousePosition);
            releaseMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //Debug.Log( pressMousePos);
            //Debug.Log(releaseMousePos);
        }

        float distance = Vector3.Distance(posFuture, transform.position) + 0.5f;
        //Debug.DrawRay(transform.position, transform.TransformDirection(direction.normalized));
        
        isTouch = Physics.Raycast(transform.position, directionFuture, out hit, distance);

        if (!isTouch) return;

        if (hit.collider.transform.CompareTag("Wall"))
        {
            direction = -direction;
            force *= 0.9f;
            if (force <= 0) force = 0;
        } else if (hit.collider.transform.CompareTag("Player"))
        {
            direction = -direction;
            direction.x = ((releaseMousePos -pressMousePos)/ (releaseMousePos - pressMousePos).magnitude).x;
            direction.z = ((releaseMousePos - pressMousePos) / (releaseMousePos - pressMousePos).magnitude).y;
            force = 12f;
        }
        else
        {
            //direction = new Vector3(transform.position.x, transform.position.y, -(transform.position.z));
            direction = Vector3.Reflect(direction, hit.normal);
            force*=0.9f;
            if (force <= 0) force = 0;
        }
        startPos = transform.position;
        time = 0;
        Debug.Log(transform.position);
        ReloadGame();
    }

    private void ReloadGame()
    { 
        if (transform.position.x > posMaxX || transform.position.x < posMinX || transform.position.z < posMinZ || transform.position.z > posMaxZ)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void jumpOnTheSpot()
    {
        transform.position = ballPos;
        transform.position = new Vector3(0, (transform.position.y + 3), 0);
    }

    private Vector3 move(Vector3 direction, float time)
    {
        return startPos + (direction * force * time) + 0.5f * Physics.gravity * (time * time);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Va cham");
    //    speed = 0f;
    //}
}
