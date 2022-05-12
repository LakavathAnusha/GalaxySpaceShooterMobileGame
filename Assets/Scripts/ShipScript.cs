using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    #region PUBLIC VARIABLES
    public float rotationSpeed=10f;//rotation speed to make ship rotate in degress per second.
    public float movementSpeed=2f;// the movenet of ship by force applied unit per second.
    #endregion
    #region PRIVATE VARIABLES
    private bool isRotating = false;
    PolygonCollider2D collider2D;
    private const string TURNCOUROTINE_FUNCTION = "TurnRotateOnTap";
    #endregion
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEnable()// when game is active then we are subsribing 
    {
        MyMobileGame.UserInputHandler.onTouchAction += TowardsTouch;
    }
    public void OnDisable()// when game is inactive we are dessceibing
    {
        MyMobileGame.UserInputHandler.onTouchAction -= TowardsTouch; 
    }

    #region MY PUBLIC METHODS
    public void TowardsTouch(Touch touch)
    {
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);//It converts screen point to world point.
        StopCoroutine(TURNCOUROTINE_FUNCTION);
        StartCoroutine(TURNCOUROTINE_FUNCTION, worldTouchPosition);
    }
    #endregion
    #region MY PRIVATE METHODS

    #endregion
    #region COROUTINE FUNCTION
   /* IEnumerator TurnAndRotateAndMoveTowardTouch(Vector3 tempPoint)
    {
        isRotating = true;
        //tempPoint = tempPoint - this.transform.position;// To find the difference between touch position and current ship position.
        //tempPoint.z = transform.position.z;//this is assigning ship position to my touch position.
        //transform.position = tempPoint;
        Quaternion startRotation = this.transform.rotation;//the rotation start point.
        Quaternion endRotation = Quaternion.LookRotation(tempPoint,Vector3.up);// this rotation will look at touchpoint in our direction.
        float time = Quaternion.Angle(startRotation, endRotation)/rotationSpeed;//angle between two rotations
       
        for(float i=0;i<time;i=i+Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, i);
        }
        transform.rotation = endRotation;//we need put a shoting functionality here.
        isRotating = false;
        yield return null;
    }*/
   IEnumerator TurnRotateOnTap(Vector3 tempPoint)
    {
        isRotating = true;
        tempPoint = tempPoint - this.transform.position;// To find the difference between touch position and current ship position.
        tempPoint.z = transform.position.z;//assigning touch point of z to ship position of z.
        Quaternion startRotation = this.transform.rotation;//took the value of ship rotation.
        Quaternion endRotation = Quaternion.LookRotation(tempPoint, Vector3.forward);
        for(float i=0;i<1f;i+=Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, 1f);//for smoothness
        }
     
        transform.rotation = endRotation;
        yield return null;
    }
    #endregion
    #region PUBLIC METHODS
    public void OnHit()
    {
        gameManager.LoseLife();
        StartCoroutine(StartInvincibilityTimer(2.5f));
    }
    private IEnumerator StartInvincibilityTimer(float timeLimit)
    {
        collider2D.enabled = false;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float timer = 0;
        float blinkSpeed = 0.25f;

        while (timer < timeLimit)
        {
            yield return new WaitForSeconds(blinkSpeed);

            spriteRenderer.enabled = !spriteRenderer.enabled;
            timer += blinkSpeed;
        }

        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
    #endregion
}
