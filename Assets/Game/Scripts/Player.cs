using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject listBricksReceive;
    [SerializeField] Transform playerVisual;
    [SerializeField] Animator anim;
    [SerializeField] private Transform parentPositon;
    [SerializeField] private StartRayCast startRayPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float distanceRay;
    private GameObject brickGround;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 posStop;
    private Vector3 direction = new Vector3(0,0,0);
    private Vector3 currentPosCast;
    private float brickPlaceUp = 0;
    public float swipeRange = 100f;
    private bool isPosStop = true;
    private int countBrickPlayer;
    private string currentAnim = "";
    private bool isControl = true;
    private Vector3 lastPosition;
    private float maxDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        posStop = transform.position;
        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
        Debug.Log("Curen" + currentPosCast);
    }

    // Update is called once per frame
    void Update()
    {
        MoveController();
    }

    public void SetIdle()
    {
        ChangAnim(KeyConstants.KEY_ANIM_IDLE);
    }

    public void ChangPositionPlayerVisual(float distance)
    {
        playerVisual.localPosition += new Vector3(0, distance, 0);
    }
    [ContextMenu("AddBrick")]
    public void AddBrick()
    {
        ChangAnim(KeyConstants.KEY_ANIM_JUMP);
        AddBrickGround();
        AddBrickPlayer();
        Invoke(nameof(SetIdle), 0.5f);
    }

    public void AddBrickPlayer()
    {
        countBrickPlayer++;
        
    }

    public void AddBrickGround()
    {
        GameObject brickPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_PREFAB}{KeyConstants.KEY_PREFAB_BRICK}");
        brickGround = Instantiate(brickPrefab, parentPositon);
        Vector3 brickSize = brickGround.GetComponent<Renderer>().bounds.size;
        
        brickGround.transform.localPosition = new Vector3(0, brickPlaceUp + brickSize.y, 0);
        brickPlaceUp += brickSize.y;
        ChangPositionPlayerVisual(brickSize.y);
    }

    public void RemoveBrick()
    {
        RemoveBrickGround();
        RemoveBrickPlayer();
    }

    public void RemoveBrickPlayer()
    {
        countBrickPlayer--;
        

    }

    public void ClearBrick()
    {
        while(countBrickPlayer > 0)
        {
            RemoveBrick();
            Debug.Log(countBrickPlayer);
        }
    }

    public void RemoveBrickGround()
    {
        int countBrickReceive = listBricksReceive.transform.childCount;
        GameObject brickPlayer = listBricksReceive.transform.GetChild(countBrickReceive - 1).gameObject;
        Vector3 brickSize = brickPlayer.GetComponent<Renderer>().bounds.size;
        Destroy(brickPlayer);
        brickPlaceUp -= brickSize.y;
        ChangPositionPlayerVisual(-brickSize.y);
    }

    public void MoveControllerTest()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            
            rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            
            rb.velocity = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            rb.velocity = new Vector3(-1 * speed * Time.deltaTime , 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.zero;
        }

    }

    public bool IsFindWall(Vector3 direction)
    {
        Ray ray = new Ray(currentPosCast, direction);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, Color.red, 1000f);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.tag == KeyConstants.KEY_TAG_WALL || hitInfo.collider.gameObject.tag == KeyConstants.KEY_TAG_CHEST)
            {
                return true;
            }
        }
        return false;
    }

    public void SetDirection()
    {
        if (isControl == false)
        {
            
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPosStop = false;
            touchEndPos = Input.mousePosition;
            Vector3 swipeDirection = touchEndPos - touchStartPos;

            if (swipeDirection.magnitude > swipeRange)
            {
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    if (swipeDirection.x > 0)
                    {
                        // Vuốt sang phải
                        direction = Vector3.right;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                        Debug.Log(direction);
                    }
                    else
                    {
                        // Vuốt sang trái
                        direction = Vector3.left;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                        Debug.Log(direction);
                    }
                }
                else
                {
                    if (swipeDirection.y > 0)
                    {
                        // Vuốt lên trên
                        direction = Vector3.forward;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                        Debug.Log(direction);
                    }
                    else
                    {
                        // Vuốt xuống dưới
                        direction= Vector3.back;
                        currentPosCast = transform.position + new Vector3(0, distanceRay, 0);
                        isPosStop = false;
                        Debug.Log(direction);
                    }
                }
            }
        }
    }

    public void MoveController()
    {
        //if(rb.velocity == Vector3.zero)
        //{
        //    ChangAnim("Idle");
        //}
        SetDirection();
        Debug.Log(posStop);
        if(isPosStop == false)
        {
            isPosStop = IsFindWall(new Vector3(0,-1,0));

            
            if(isPosStop == false)
            {
                posStop = currentPosCast - new Vector3(0, distanceRay, 0);
                currentPosCast += direction;
            }

        }
        if(countBrickPlayer > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, posStop, speed * Time.deltaTime);

         
            if(lastPosition != transform.position)
            {
                isControl = false;
                
            }
            else
            {
                isControl = true;
            }
            lastPosition = transform.position;
        }
        else 
        {
            rb.velocity = Vector3.zero;
            isPosStop = true;
            Debug.LogError("Thua");
        }

    }

    public void ChangAnim(string animName)
    {

        if(currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void SetFinal()
    {
        ChangAnim(KeyConstants.KEY_ANIM_WIN);
        listBricksReceive.SetActive(false);
        playerVisual.localPosition = new Vector3(0, 0, 0.28f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == KeyConstants.KEY_TAG_PUSH)
        {
            Debug.Log("da cham push");
            Vector3 directionOption = Vector3.Cross(direction, Vector3.up).normalized;
            if(directionOption == -other.gameObject.transform.right || directionOption == -other.gameObject.transform.forward)
            {
                direction = directionOption;
            }
            else
            {
                direction = -directionOption;

            }
            Debug.Log(direction);
            isPosStop = false;
            currentPosCast = new Vector3(other.gameObject.transform.localPosition.x, transform.position.y , other.gameObject.transform.localPosition.z) + new Vector3(0, distanceRay, 0);
        }
    }

    

}
