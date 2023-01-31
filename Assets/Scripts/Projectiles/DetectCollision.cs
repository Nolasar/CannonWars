using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public enum Boxes { Box1, Box2, Box3 };
    public Boxes box;
    private string correctBox;
    
    void Start()
    {
        ChooseBox();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == correctBox)
        {
            EventBus.onCorrectBoxTouch?.Invoke();
            
        }
        else if (collision.gameObject.tag == "Blade")
        {
            
        }
        else
        {
            EventBus.onWrongBoxTouch?.Invoke();
            
        }
        gameObject.SetActive(false);
    }

    private void ChooseBox()
    {
        // In editor choose the correct box
        switch (box)
        {
            case Boxes.Box1:
                correctBox = "Box1";
                break;
            case Boxes.Box2:
                correctBox = "Box2";
                break;
            case Boxes.Box3:
                correctBox = "Box3";
                break;
        }
    }
}
