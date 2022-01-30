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

    [Header("Movement settings")]
    [Tooltip("The current timing, used for movement calls")]
    public float timing = 0;
    [Tooltip("The max time a full movement takes")]
    public float totalTime;
    [Tooltip("The farthest this object will move from its starting position for red and blue")]
    public float maxDisplacement = 1;
    [Tooltip("The starting position relative to the object's movement for red and blue")]
    public Vector3 startMovePos;
    [Tooltip("The maximum rotation for this object for green")]
    public float maxRotation = 45;
    [Tooltip("The starting rotation for this object for green")]
    public float startRotation;


    //delegate for frame by frame calls
    private delegate void FrameAction();
    //array for holding frame by frame calls for easy access
    private FrameAction[] frameActions;
    //delegate for color change calls
    private delegate void ChangeAction();
    //array for holding color change calls for easy access
    private ChangeAction[] changeActions;

    //rigidbody
    private Rigidbody rb;


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

        //gets rigidbody
        rb = gameObject.GetComponent<Rigidbody>();

        //sets up starting position for movement
        startMovePos = transform.position;

        //sets up starting rotation for rotation
        startRotation = transform.rotation.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (contactReceivers.Count == 0)
        {
            frameActions[(int)currentColor]();
        }
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
        timing = 0;
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
        Vector3 pos = transform.position;
        pos.x = startMovePos.x + Mathf.Sin( (timing * ( totalTime / (2 * Mathf.PI) ) ) ) * maxDisplacement;
        transform.position = pos;
        timing += Time.fixedDeltaTime;
    }

    //action to perform every frame when the object is Grey
    public void GreenFrameAction()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = startRotation + Mathf.Sin((timing * (totalTime / (2 * Mathf.PI)))) * maxRotation;
        transform.eulerAngles = rot;
        timing += Time.fixedDeltaTime;
    }

    //action to perform every frame when the object is Grey
    public void BlueFrameAction()
    {
        Vector3 pos = transform.position;
        pos.y = startMovePos.y - Mathf.Sin((timing * (totalTime / (2 * Mathf.PI)))) * maxDisplacement;
        transform.position = pos;
        timing += Time.fixedDeltaTime;
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
