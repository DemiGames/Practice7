using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public GameManager gm;
    private Image soundImage;
    public Sprite musicFirstSprite;
    public Sprite musicSecondSprite;
    void Awake()
    {
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        soundImage = GetComponent<Image>();
    }

    public void ChangeSprite()
    {
        //gm.playMusic = !gm.playMusic;
        if (gm.playMusic)
            soundImage.sprite = musicFirstSprite;
        else
            soundImage.sprite = musicSecondSprite;
    }
}
