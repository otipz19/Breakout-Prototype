using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int score;

    private BrickType _type;

    public BrickType type
    {
        get
        {
            return _type;
        }
        set
        {
            Material mat = GetComponentInChildren<SpriteRenderer>().material;

            switch (value)
            {
                case BrickType.yellow:
                    mat.color = Color.yellow;
                    score = 1;
                    break;
                case BrickType.green:
                    mat.color = Color.green;
                    score = 3;
                    break;
                case BrickType.orange:
                    mat.color = new Color(1f, 0.45f, 0);
                    score = 5;
                    break;
                case BrickType.red:
                    mat.color = Color.red;
                    score = 7;
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if(collidedWith.tag == "Ball")
        {
            GameManager.S.IncreaseScore(score);
            Destroy(this.gameObject);
        }
    }
}

public enum BrickType
{
    yellow,
    green,
    orange,
    red,
}
