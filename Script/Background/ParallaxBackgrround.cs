using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgrround : MonoBehaviour
{

    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - parallaxEffect);

        float distanceToMoVe = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distanceToMoVe, transform.position.y) ;

        if (distanceMove > xPosition + length) 
            xPosition = xPosition + length;
        else if(distanceMove < xPosition - length)
            xPosition = xPosition - length;
    }
}
