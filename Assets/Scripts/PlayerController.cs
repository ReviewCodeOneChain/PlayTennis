using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject pointDirection;

    Vector3 posPlayerStart;
    //Vector3 posPlayer;
    //Vector3 posPlayerMoveTo;

    [SerializeField] float speed;
    //[SerializeField] static Vector3 PLAYER_BODY = new Vector3(0.0f, -9f, -18.0f);

    // Start is called before the first frame update
    void Start()
    {
        posPlayerStart = new Vector3(0, 2f, -4);
        //posPlayer = Camera.main.transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    } 

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * 0);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed * -1);
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);

    }    
}
