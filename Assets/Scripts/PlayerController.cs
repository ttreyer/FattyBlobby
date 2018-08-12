using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float distanceToMove;
    public float moveSpeed;

    private Vector3 endPosition;
    private float moveHorizontal;
    private float moveVertical;
    private bool isMoving;
    private bool wantToGrow;
    private bool wantToShrink;
    private bool isHorizontalGrowth;

    private CollisionChecker collisionChecker;
    private SpriteRenderer spriteRender;

    private GameObject ui;
    private UIController uiController;

    public Sprite squareBlob;
    public Sprite rectangleBlob;

    private bool isAlive = true;
    private Animator animator;

    public void Grow()
    {
        wantToGrow = true;
    }

    public void Shrink()
    {
        wantToShrink = true;
    }

    private void UpdateScale(Vector3 newScale) {
        transform.localScale = newScale;
        animator.SetBool("IsWider", newScale.x > newScale.y);
    }

    private void DoShrink()
    {

        if (transform.localScale.x == 1 && transform.localScale.y == 1)
        {
            wantToShrink = false;
            return;
        }

        
        Vector3 nextSize;

        if (!isHorizontalGrowth)
        {
            Vector3 evolution = new Vector3(1.0f, 0.0f, 0.0f);
            UpdateScale(transform.localScale - evolution);

            nextSize = new Vector3(transform.localScale.x + 1, transform.localScale.y, transform.localScale.z);
            spriteRender.gameObject.transform.localScale = new Vector3(6f, 6f, 1f);
            spriteRender.sprite = rectangleBlob;
            isHorizontalGrowth = !isHorizontalGrowth;
        }
        else
        {
            Vector3 evolution = new Vector3(0.0f, 1.0f, 0.0f);
            UpdateScale(transform.localScale - evolution);

            nextSize = new Vector3(transform.localScale.x, transform.localScale.y + 1, transform.localScale.z);
            spriteRender.gameObject.transform.localScale = new Vector3(3f, 6f, 1f);
            spriteRender.sprite = squareBlob;
            isHorizontalGrowth = !isHorizontalGrowth;
        }
        
        uiController.UpdatePlayerSize(transform.localScale,nextSize);
        wantToShrink = false;
    }

    private void DoGrow()
    {
        
        Vector3 nextSize;

        if(isHorizontalGrowth)
        {
            if(collisionChecker.CanGo("Right"))
            {
                Vector3 evolution = new Vector3(1.0f, 0.0f, 0.0f);
                UpdateScale(transform.localScale + evolution);

                nextSize = new Vector3(transform.localScale.x, transform.localScale.y +1, transform.localScale.z);
                spriteRender.gameObject.transform.localScale = new Vector3(3f, 6f, 1f);
                spriteRender.sprite = rectangleBlob;
                isHorizontalGrowth = false;

            } else
            {
                Die();
                return;
            }

        } else
        {
            if (collisionChecker.CanGo("Up"))
            {
                Vector3 evolution = new Vector3(0.0f, 1.0f, 0.0f);
                UpdateScale(transform.localScale + evolution);

                nextSize = new Vector3(transform.localScale.x + 1, transform.localScale.y, transform.localScale.z);
                spriteRender.gameObject.transform.localScale = new Vector3(6f, 6f, 1f);
                spriteRender.sprite = squareBlob;
                isHorizontalGrowth = true;
            } else
            {
                Die();
                return;
            }
        }

        uiController.UpdatePlayerSize(transform.localScale,nextSize);
        wantToGrow = false;
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
        isHorizontalGrowth = true;
        wantToGrow = false;
        collisionChecker = GetComponentInChildren<CollisionChecker>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();

        ui = GameObject.FindGameObjectWithTag("Ui");
        uiController = ui.GetComponent<UIController>();
        animator = GetComponent<Animator>();
    }

    void Die() {
        isAlive = false;
        animator.SetTrigger("Die");
        uiController.PrintGameOver(0.7f);

        // Reset for the animation to have to correct size
        spriteRender.gameObject.transform.localScale = new Vector3(6f, 6f, 1f);
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
    void Update() {
        if (!isAlive) {
            if (Input.GetKey(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            return;
        }

        animator.SetBool("IsWalking", isMoving);

        if(!isMoving)
        {
            if (wantToShrink)
            {
                DoShrink();
                return;
            }

            if(wantToGrow)
            {
                DoGrow();
                return;
            }
            
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
            if (moveHorizontal != 0 || moveVertical != 0)
                isMoving = true;
        }
      
    }
}