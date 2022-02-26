using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    Material material;
    //把值曝露出去
    [SerializeField]Vector2 scrollVelocity;
    void Awake()
    {
        material=GetComponent<Renderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset+=scrollVelocity*Time.deltaTime;
    }
}
