using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    
    [Header("Color properties")]
    [Tooltip("The starting color of this object")]
    public Colors.Color startColor = Colors.Color.Grey;
    [Tooltip("The current color of this object.")]
    public Colors.Color currentColor;
    //this object's mesh renderer
    private MeshRenderer matRenderer;
    //delegate for frame by frame calls
    delegate void FrameAction();
    //array for holding frame by frame calls for easy access
    FrameAction[] frameActions;
    //delegate for color change calls
    delegate void ChangeAction();
    //array for holding color change calls for easy access
    ChangeAction[] changeActions;


    void Start()
    {
        //at start of runtime, set the color to its initial state
        //currentColor = startColor;
        //get the mesh renderer for easy access
        matRenderer = GetComponent<MeshRenderer>();

        //set up the frameActions array for easy access
        frameActions = new FrameAction[] { GreyFrameAction, RedFrameAction, GreenFrameAction, BlueFrameAction };

        //set up the changeActions array for easy access
        changeActions = new ChangeAction[] { GreyChangeAction, RedChangeAction, GreenChangeAction, BlueChangeAction };

        //sets the color to the correct starting color
        SetColor(startColor);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    //handles collisions with triggers, namely color bullets
    void OnTriggerEnter(Collider col)
    {
        //attempt to get the ColorBullet script from the trigger
        GameObject bullet = col.gameObject;
        //if the script exists, set this object's color to the color of the bullet
        if ( bullet.GetComponent<ColorBullet>() != null)
        {
            SetColor(bullet.GetComponent<ColorBullet>().GetColor());
            Destroy(bullet);

        }
    }

    // sets the current color of this object to the specified color
    private void SetColor(Colors.Color newColor)
    {
        currentColor = newColor;
        matRenderer.material = Colors.GetColorMat(newColor);
        changeActions[(int)newColor]();
    }

    //returns this object's current color
    public Colors.Color GetColor()
    {
        return currentColor;
    }

    //action to perform every frame when the object is Grey
    public void GreyFrameAction()
    {
        return;
    }

    //action to perform every frame when the object is Grey
    public void RedFrameAction()
    {
        return;
    }

    //action to perform every frame when the object is Grey
    public void GreenFrameAction()
    {
        return;
    }

    //action to perform every frame when the object is Grey
    public void BlueFrameAction()
    {
        return;
    }

    //action to perform when the object is turned Grey
    public void GreyChangeAction()
    {
        Debug.Log("changed to grey");
    }

    //action to perform when the object is turned Grey
    public void RedChangeAction()
    {
        Debug.Log("changed to red");
    }

    //action to perform when the object is turned Grey
    public void GreenChangeAction()
    {
        Debug.Log("changed to green");
    }

    //action to perform when the object is turned Grey
    public void BlueChangeAction()
    {
        Debug.Log("changed to blue");
    }

}
