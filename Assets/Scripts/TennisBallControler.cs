using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TennisBallControler : MonoBehaviour
{
    Vector3 startPos;

    Vector3 ballPosStart;
    Vector3 direction;
    Vector3 startMousePress;
    Vector3 endMouseRelease;

    RaycastHit hit;
    bool isHit;

    [SerializeField] float force;
    [SerializeField] float time;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        //ballPosStart = new Vector3(0, 6, 1);
        //startPos = transform.position;
        //direction = new Vector3(0, 1, 1);

        //Debug.Log(Physics.gravity.y);
        //transform.position = ballPosStart;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = move(direction, time, force);

        Vector3 dirDown = new Vector3(0, 5, 1);
        direction = dirDown - startPos;
        if (CheckCollide(time, direction, force))
        {
            if (hit.collider.transform.CompareTag("Ground"))
            {
                direction.y = -direction.y;
                force = 2f;
            }
            startPos = transform.position;
            time = 0;
        }








        //time += Time.deltaTime * speed;
        //direction = CheckDirection();

        //transform.position = move(direction, time);

        //if (CheckCollide() == true)
        //{
        //    if (!hit.collider.transform.CompareTag("Ground"))
        //    {
        //        direction = -direction;
        //        if (hit.collider.transform.CompareTag("Wall"))
        //            force -= 2;
        //        else
        //            force++;
        //    }
        //    else
        //    {
        //        direction = Vector3.Reflect(direction, hit.normal);
        //    }
        //    startPos = transform.position;
        //    time = 0;
        //}    
    }

    //private void jump()
    //{
        
    //}

    //private Vector3 CheckDirection()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //        startMousePress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    return startMousePress - transform.position;
    //}

    private bool CheckCollide(float time, Vector3 direction, float force)
    {
        float timeFut = time + (Time.deltaTime * speed);

        Vector3 posFuture = move(direction, timeFut, force);
        Vector3 directionFuture = posFuture - transform.position;

        float distance = Vector3.Distance(posFuture, transform.position);
        Debug.DrawRay(transform.position, transform.TransformDirection(direction.normalized));

        isHit = Physics.Raycast(transform.position, directionFuture, out hit, distance);

        return isHit;
    }

    private Vector3 move(Vector3 direction, float time, float force)
    {
        return startPos + (direction * force * time) + 0.5f * Physics.gravity * (time * time);
    }
}
