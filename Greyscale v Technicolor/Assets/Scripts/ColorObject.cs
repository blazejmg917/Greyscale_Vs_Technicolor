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
    
    void Start()
    {
        //at start of runtime, set the color to its initial state
        currentColor = startColor;
        //get the mesh renderer for easy access
        matRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
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
    }

    //returns this object's current color
    public Colors.Color GetColor()
    {
        return currentColor;
    }
}
