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
    private CollisionChecker collisionChecker;

    private GameObject ui;
    private UIController uiController;

    public void Grow()
    {

        Vector3 nextSize;

        if(transform.localScale.x == transform.localScale.y)
        {
            transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y, transform.localScale.z);
            nextSize = new Vector3(transform.localScale.x, transform.localScale.y +1, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 1, transform.localScale.z);
            nextSize = new Vector3(transform.localScale.x +1, transform.localScale.y, transform.localScale.z);
        }

        uiController.UpdatePlayerSize(transform.localScale,nextSize);
    }

    public void Init(Vector3 initScale)
    {
        transform.localScale = initScale;
        Vector3 nextPlayerScale = new Vector3(initScale.x + 1, initScale.y, initScale.z);

        uiController.UpdatePlayerSize(initScale, nextPlayerScale);
    }

    void Start()
    {
        endPosition = transform.position;
        isMoving = false;
        collisionChecker = GetComponentInChildren<CollisionChecker>();

        ui = GameObject.FindGameObjectWithTag("Ui");
        uiController = ui.GetComponent<UIController>();
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

            if(moveHorizontal > 0 &&  !collisionChecker.CanGo("Right"))
            {
                return;
            }
            else if (moveHorizontal < 0 && !collisionChecker.CanGo("Left"))
            {
                return;
            }
            else if (moveVertical > 0 && !collisionChecker.CanGo("Up"))
            {
                return;
            }
            else if (moveVertical < 0 && !collisionChecker.CanGo("Down"))
            {
                return;
            }

            endPosition = new Vector3(endPosition.x + distanceToMove * moveHorizontal, endPosition.y + distanceToMove * moveVertical, endPosition.z);
            isMoving = true;
        }
      
    }
}