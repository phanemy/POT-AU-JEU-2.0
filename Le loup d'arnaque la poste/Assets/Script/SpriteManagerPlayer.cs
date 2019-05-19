using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class SpriteManagerPlayer : SpriteManager
{
    [SerializeField]
    public Sprite[] spritesHumain;
    [SerializeField]
    public Sprite[] sprites33_66;
    [SerializeField]
    public Sprite[] sprites66_99;

    public void changeLycanthropieLevel(int i)
    {
        if(i >=0 && i < 3)
        {
            if(i == 1)
            {
                updateSprite(sprites33_66);
            }
            else if(i==2)
            {
                updateSprite(sprites66_99);
            }
            else
            {
                updateSprite(spritesHumain);
            }
        }
    }

    private void updateSprite(Sprite[] sprites)
    {
        if (sprites.Length >= 12)
        {
            for (int i = 0; i < 3; ++i)
            {
                goingBot[i] = sprites[i ];
                goingLeft[i] = sprites[i + 3];
                goingRight[i] = sprites[i + 6];
                goingTop[i] = sprites[i+9];

            }
            goingBot[3] = sprites[1];
            goingLeft[3] = sprites[4];
            goingRight[3] = sprites[7];
            goingTop[3] = sprites[10];

            restart();
        }

    }
}
