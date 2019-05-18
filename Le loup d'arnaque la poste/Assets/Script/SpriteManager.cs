using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class SpriteManager
{
    [SerializeField]
    public Sprite[] goingTop;
    [SerializeField]
    public Sprite[] goingBot;
    [SerializeField]
    public Sprite[] goingLeft;
    [SerializeField]
    public Sprite[] goingRight;
    [SerializeField]
    public float changeTime = 0.2f;
    [SerializeField]
    public DirectionEnum ActualDir {
        get { return actualDir; }
        set {
            if (value != actualDir)
            {
                actualDir = value;
                switch (actualDir)
                {
                    case DirectionEnum.Bottom:
                        rend.sprite = goingBot[1];
                        break;
                    case DirectionEnum.Left:
                        rend.sprite = goingLeft[1];
                        break;
                    case DirectionEnum.Top:
                        rend.sprite = goingTop[1];
                        break;
                    case DirectionEnum.Right:
                        rend.sprite = goingRight[1];
                        break;
                    default: break;
                }
            }
        }
    }
    private DirectionEnum actualDir;
    private float timeSinceLastChange = 0f;
    private SpriteRenderer rend;
    private int index;
    public bool update { get; private set; }

    public void init(SpriteRenderer renderer)
    {
        index = 0;
        rend = renderer;
        update = false;
    }

    public void stop()
    {
        update = false;

        switch (actualDir)
        {
            case DirectionEnum.Bottom:
                rend.sprite = goingBot[1];
                break;
            case DirectionEnum.Left:
                rend.sprite = goingLeft[1];
                break;
            case DirectionEnum.Top:
                rend.sprite = goingTop[1];
                break;
            case DirectionEnum.Right:
                rend.sprite = goingRight[1];
                break;
            default: break;
        }
    }

    public void restart()
    {
        update = true;

        timeSinceLastChange = 0f;
    }

    public void Update()
    {
        if (update)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange > changeTime)
            {
                timeSinceLastChange %= changeTime;
                index = (++index) % goingTop.Length;
                
                switch (actualDir)
                {
                    case DirectionEnum.Bottom:
                        rend.sprite = goingBot[index];
                        break;
                    case DirectionEnum.Left:
                        rend.sprite = goingLeft[index];
                        break;
                    case DirectionEnum.Top:
                        rend.sprite = goingTop[index];
                        break;
                    case DirectionEnum.Right:
                        rend.sprite = goingRight[index];
                        break;
                    default: break;
                }
            }
        }
    }
}
