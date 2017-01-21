using UnityEngine;
using System.Collections;

[System.Serializable, ExecuteInEditMode]
public class ColorSpaceManager : MonoBehaviour {

    public ColorSpace colorSpace;

	// Use this for initialization
    void Awake()
    {
        UnityEditor.PlayerSettings.colorSpace = colorSpace;
	}
}
