using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyMobileGame
{
    
    public class UserInputHandler : MonoBehaviour
    {
        #region EVENTS
        public delegate void TapAction(Touch touch);
        public static event TapAction onTouchAction;
        #endregion
        #region PUBLIC VARIABLES
        public float tapMaxMovement=50f;//tap maximum pixes a tap can be move
        #endregion
        #region PRIVATE VARIABLES
        private Vector2 movement;// movent vector track will track how far will move
        private bool tapGestureFailed = false;//will become true if he moves too far.
        #endregion
        #region MONOBEHAVIOUR METHODS
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.touchCount>0) //to finding out no. of touches greater than 0 or not.If no Touches no movemnet.
            {
               Touch touch= Input.touches[0];//Need to find out no of touches on the screen.if there are more no of touches need to calculate.
             if(touch.phase==TouchPhase.Began)//we have several touchphase began.
                {
                    movement = Vector2.zero;
                }
            else if(touch.phase==TouchPhase.Moved||touch.phase==TouchPhase.Stationary)
                {
                    movement += touch.deltaPosition;
                    if (movement.magnitude > tapMaxMovement)
                    {
                        tapGestureFailed = true;
                    }
                }
             
             else//if tapGesture is begger or the person removed the finger then we are calling tapgesture is failed
                {
                    if (!tapGestureFailed)
                    {
                        if (onTouchAction != null)
                        {
                            onTouchAction(touch);
                        }
                        tapGestureFailed = false;//making ready for the next tap;
                    }
                }
            }
        }
        #region MY PUBLIC METHODS
        #endregion
        #region MY PRIVATE METHODS
        #endregion
    }
}
