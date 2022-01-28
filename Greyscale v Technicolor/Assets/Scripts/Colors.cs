using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    //the different possible colors that an object can have
    public enum Color { Grey, Red, Green, Blue };

    [Header("Color Materials")]
    [Tooltip("Grey Material")]
    public Material greyMat;
    [Tooltip("Red Material")]
    public Material redMat;
    [Tooltip("Green Material")]
    public Material greenMat;
    [Tooltip("Blue Material")]
    public Material blueMat;
    //array of all materials for easy access
    private static Material[] matArray;

    void Awake()
    {
        matArray = new Material[] { greyMat, redMat, greenMat, blueMat };
    }

    public static Material GetColorMat(Color colorMat)
    {
        return matArray[(int)colorMat];
    }
}
