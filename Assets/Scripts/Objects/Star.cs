using UnityEngine;
using UnityEngine.UI;

public class Star : CacheTransform {
    public Sprite[] spritesForFilling;
    public bool isFull;
    public int point;
    public int step;
    public int prewValueStar;
    //public SpriteRenderer sRenderer;
    public Image img;

    void Awake() {
        //sRenderer = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
    }

    void Start() {
    }

    public void SetPoint(int point) {
        this.point = point;
        step = (point - prewValueStar)/(spritesForFilling.Length - 1);
    }

    public void IsFull() {
        if (GameData.Score >= point) {
            isFull = true;
            GetComponent<Animator>().SetTrigger("go");
            //sRenderer.sprite = spritesForFilling[spritesForFilling.Length - 1];
            img.sprite = spritesForFilling[spritesForFilling.Length - 1];
            GamePlay.countStarsLevel++;
//			Debug.Log("Stars: "+GamePlay.countStarsLevel);
        } else {
            SetSpriteToStar();
        }
    }

    private void SetSpriteToStar() {
        var currentPoints = prewValueStar;
        for (var i = 0; i < spritesForFilling.Length; i++) {
            if (GameData.Score >= currentPoints) {
                //sRenderer.sprite = spritesForFilling[i];
                img.sprite = spritesForFilling[i];
                currentPoints += step;
            }
        }
    }
}