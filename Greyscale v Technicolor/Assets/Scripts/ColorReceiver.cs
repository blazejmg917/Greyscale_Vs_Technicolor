using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReceiver : MonoBehaviour
{
    [Tooltip("required color to be activated")]
    public Colors.Color activeColor;
    [Tooltip("whether or not the receiver is currently activated")]
    public bool currentlyActivated = false;
    //all of the objects currently in contact with this object
    List<GameObject> contactObjects;
    //all of the objects currently in contact that are the correct color for activation
    List<GameObject> activators;

    //test
    public Material activeMat;
    //test
    public Material inactiveMat;
    //test
    private MeshRenderer matRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
        contactObjects = new List<GameObject>();
        activators = new List<GameObject>();
        //test
        matRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        //get the game object of the collider
        GameObject go = col.collider.gameObject;
        //if the ColorObject script exists in the gameobject
        if (go.GetComponent<ColorObject>() != null)
        {
            //if it doesn't already exist in the list of objects in contact, add it to the list and then check its color to see if its an activator
            if (!contactObjects.Contains(go))
            {
                contactObjects.Add(go);
                Debug.Log("Item added to contact list. Current total: " + contactObjects.Count);
                CheckColor(go);
                go.GetComponent<ColorObject>().Link(this);
            }

        }
    }

    void OnCollisionExit(Collision col)
    {
        //get the gameObject for the collider
        GameObject go = col.collider.gameObject;
        //if the object is in the objects list, remove it from the list
        if (contactObjects.Contains(go))
        {
            
            contactObjects.Remove(go);
            go.GetComponent<ColorObject>().Link(this);
            Debug.Log("Item removed from contact list. Current total: " + contactObjects.Count);
            //if the object is an activator, remove it from the list
            if (activators.Contains(go))
            {
                activators.Remove(go);
                Debug.Log("Item removed from activator list. Current total: " + activators.Count);
                CheckActive();
            }
        }
    }

    //checks if the color of a given gameobject matches the activation color of this activator
    public void CheckColor(GameObject go)
    {
        //get the colorScript, and return if it doesn't exist
        ColorObject colorScript = go.GetComponent<ColorObject>();
        if(colorScript == null)
        {
            return;
        }

        //if the current color of the object is the same as the activator color
        if(colorScript.currentColor == activeColor)
        {
            //if it isn't already in the list of activators, add it to the list
            if(!activators.Contains(go)){
                activators.Add(go);
                Debug.Log("Item added to activator list. Current total: " + activators.Count);
                CheckActive();
            }

        }
        else
        {
            //if the object with an incorrect color is currently in the activator list, remove it
            if (activators.Contains(go))
            {
                activators.Remove(go);
                Debug.Log("Item removed from activator list. Current total: " + activators.Count);
                CheckActive();
            }
        }
    }

    //checks if this activator should be active or not, and changes the currentlyActive bool accordingly
    public void CheckActive()
    {
        //if the amount of activators is greater than 0, set the object to active, otherwise set to inactive.
        currentlyActivated = (activators.Count > 0);
        Debug.Log("Currently active set to " + currentlyActivated);
        //test
        if (currentlyActivated)
        {
            matRenderer.material = activeMat;
        }
        else
        {
            matRenderer.material = inactiveMat;
        }
        
        return;
    }

}
