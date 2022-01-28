using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBullet : MonoBehaviour
{
    [Header("Color properties")]
    [Tooltip("The starting color of this object")]
    public Colors.Color startColor = Colors.Color.Grey;
    [Tooltip("The current color of this object.")]
    public Colors.Color currentColor;
    //this object's mesh renderer
    private MeshRenderer matRenderer;

    void Start()
    {
        Debug.Log(startColor);
        //at start of runtime, set the color to its initial state
        //currentColor = startColor;
        //get the mesh renderer for easy access
        matRenderer = gameObject.GetComponent<MeshRenderer>();
        if(matRenderer == null)
        {
            Debug.LogWarning("MatRenderer null");
        }
        //sets the color to the correct starting color
        SetColor(startColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // sets the bullet's current color to the given color
    public void SetColor(Colors.Color newColor)
    {
        Debug.Log("new color: " + newColor);
        currentColor = newColor;
        if(matRenderer.material == null)
        {
            Debug.LogWarning("MatRenderer material null");
        }
        if(Colors.GetColorMat(currentColor) == null)
        {
            Debug.LogWarning("returning null");
        }
        matRenderer.material = Colors.GetColorMat(currentColor);
    }

    // returns the bullet's current color
    public Colors.Color GetColor()
    {
        return currentColor;
    }

    //what to do when the bullet is destroyed
    void OnDestroy()
    {

    }
}
