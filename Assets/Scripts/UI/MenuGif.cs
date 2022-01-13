using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGif : MonoBehaviour
{
    
    public Sprite[] animatedImages;
    public Image animateImageObj;

    /// <summary>
    /// Updates sprite
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        animateImageObj.sprite = animatedImages[(int)(Time.time*10)%animatedImages.Length];
        
    }

    /// <summary>
    /// Alternated between pictures randomely
    /// </summary>
    void randomPic()
    {
        animateImageObj.sprite = animatedImages[Random.Range(0, animatedImages.Length)];
    }
}
