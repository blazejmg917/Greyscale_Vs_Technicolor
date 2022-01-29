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

    //all color receivers currently in contact with this object
    private List<ColorReceiver> contactReceivers;

    //delegate for frame by frame calls
    private delegate void FrameAction();
    //array for holding frame by frame calls for easy access
    private FrameAction[] frameActions;
    //delegate for color change calls
    private delegate void ChangeAction();
    //array for holding color change calls for easy access
    private ChangeAction[] changeActions;


    void Start()
    {
        //at start of runtime, set the color to its initial state
        //currentColor = startColor;
        //get the mesh renderer for easy access
        matRenderer = GetComponent<MeshRenderer>();

        //set up the contactReceivers list
        contactReceivers = new List<ColorReceiver>();

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

    

    // sets the current color of this object to the specified color
    public void SetColor(Colors.Color newColor)
    {
        if(newColor == currentColor)
        {
            return;
        }
        currentColor = newColor;
        matRenderer.material = Colors.GetColorMat(newColor);
        changeActions[(int)newColor]();
        foreach(ColorReceiver receiver in contactReceivers)
        {
            receiver.CheckColor(gameObject);
        }
    }

    //returns this object's current color
    public Colors.Color GetColor()
    {
        return currentColor;
    }


    public void Link(ColorReceiver receiver)
    {
        Debug.Log("received link");
        if (!contactReceivers.Contains(receiver))
        {
            Debug.Log("successfully made link");
            contactReceivers.Add(receiver);
        }
    }

    public void Unlink(ColorReceiver receiver)
    {
        Debug.Log("received unlink");
        if (contactReceivers.Contains(receiver))
        {
            Debug.Log("successfully unlinked");
            contactReceivers.Remove(receiver);
        }
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
