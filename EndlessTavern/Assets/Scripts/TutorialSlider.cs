using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlider : MonoBehaviour
{
    [SerializeField]
    Sprite [] tutorialSlides = new Sprite [3];
    [SerializeField]
    Canvas tutorialCanvas;
    private int spriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = tutorialSlides[spriteIndex];
    }
    public void loadNextSlide()
    {
        
        spriteIndex += 1;
        if (spriteIndex < tutorialSlides.Length)
            GetComponent<SpriteRenderer>().sprite = tutorialSlides[spriteIndex];
        if (spriteIndex == tutorialSlides.Length-1)
        {
            tutorialCanvas.transform.GetChild(0).gameObject.SetActive(false);
            tutorialCanvas.transform.GetChild(2).gameObject.SetActive(true);
        }
         

    }
}
