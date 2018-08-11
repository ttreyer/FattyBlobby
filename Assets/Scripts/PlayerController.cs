using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float distanceToMove;
    public float moveSpeed;

    private Vector3 endPosition;
    private float moveHorizontal;
    private float moveVertical;
    private bool isMoving;


    void Start()
    {
        endPosition = transform.position;
        isMoving = false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if(transform.position == endPosition)
            {
                isMoving = false;
            }
        }
    }
    void Update()
    {

        if(!isMoving)
        {
            float moveHorizontal = 0;
            float moveVertical = 0;

            if(Input.GetAxisRaw("Vertical") != 0)
            {
                moveVertical = Input.GetAxisRaw("Vertical");
            }
            else
            {
                moveHorizontal = Input.GetAxisRaw("Horizontal");
            }

            endPosition = new Vector3(endPosition.x + distanceToMove * moveHorizontal, endPosition.y + distanceToMove * moveVertical, endPosition.z);
            isMoving = true;
        }
      
    }
}