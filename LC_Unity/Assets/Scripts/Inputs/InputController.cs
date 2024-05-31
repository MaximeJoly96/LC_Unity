using System.Collections;
using UnityEngine;

namespace Inputs
{
    public class InputController : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            HandleAxes();
            HandleButtons();
            HandleClicks();
        }

        private void HandleAxes()
        {
            if (Input.GetAxis("Horizontal") > 0.0f)
            {
                Debug.Log("going right");
            }
            else if (Input.GetAxis("Horizontal") < 0.0f)
            {
                Debug.Log("going left");
            }

            if (Input.GetAxis("Vertical") > 0.0f)
            {
                Debug.Log("going up");
            }
            else if (Input.GetAxis("Vertical") < 0.0f)
            {
                Debug.Log("going down");
            }
        }

        private void HandleButtons()
        {
            if(Input.GetButtonDown("Select"))
            {
                Debug.Log("Pressed select");
            }

            if(Input.GetButtonDown("Cancel"))
            {
                Debug.Log("Pressed cancel");
            }
        }

        private void HandleClicks()
        {
            if(Input.GetButton("Fire1"))
            {
                Debug.Log("Clicked left");
            }

            if(Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Clicked right");
            }
        }
    }
}