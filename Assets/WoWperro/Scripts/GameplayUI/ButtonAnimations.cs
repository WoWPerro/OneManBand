using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonAnims
{
    ScaleUp,
    ScaleDown,
    MoveFromLeft,
    MoveFromRight,
    MoveFromUp,
    MoveFromDown,
    ScaleFromCenter,
    CustomScale,
    CustomPos,
}

public class ButtonAnimations : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = .5f;
    private Vector3 scale;
    private Vector3 pos;

    public Vector3 customFromScale;
    public Vector3 customFromPos;
    public ButtonAnims anims;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        scale = gameObject.transform.localScale;
        pos = gameObject.transform.position;
        SwitcherPrep();
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        SwitcherAnimation();
    }


    public void SwitcherPrep()
    {
        switch(anims)
        {
            case ButtonAnims.ScaleUp:
                gameObject.transform.localScale = Vector3.zero;
            return;

            case ButtonAnims.ScaleDown:
                
            return;

            case ButtonAnims.MoveFromLeft:
                gameObject.transform.position = new Vector3(pos.x - Screen.width, pos.y, pos.z);
            return;

            case ButtonAnims.MoveFromRight:
                gameObject.transform.position = new Vector3(pos.x + Screen.width, pos.y, pos.z);
            return;

            case ButtonAnims.MoveFromUp:
                gameObject.transform.position = new Vector3(pos.x, pos.y + Screen.height, pos.z);
            return;

            case ButtonAnims.MoveFromDown:
                gameObject.transform.position = new Vector3(pos.x, pos.y - Screen.height, pos.z);
            return;

            case ButtonAnims.ScaleFromCenter:
                gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
                gameObject.transform.localScale = new Vector3(0, scale.y, scale.z);
            return;

             case ButtonAnims.CustomPos:
                gameObject.transform.position = customFromPos;
            return;

            case ButtonAnims.CustomScale:
                gameObject.transform.localScale = customFromScale;
            return;

            default:
                Debug.Log("No Se eligió un ButtonAnims Valido");
            return;

        }
    }

    public void SwitcherAnimation()
    {
        switch(anims)
        {
            case ButtonAnims.ScaleUp:
                ScaleUp();
            return;

            case ButtonAnims.ScaleDown:
                ScaleDown();
            return;

            case ButtonAnims.MoveFromLeft:
                MoveFromLeft();
            return;

            case ButtonAnims.MoveFromRight:
                MoveFromRight();
            return;

            case ButtonAnims.MoveFromUp:
                MoveFromUp();
            return;

            case ButtonAnims.MoveFromDown:
                MoveFromDown();
            return;

            case ButtonAnims.ScaleFromCenter:
                ScaleFromCenter();
            return;

             case ButtonAnims.CustomPos:
                MoveFromCustom();
            return;

            case ButtonAnims.CustomScale:
                ScaleFromCustom();
            return;

            default:
                Debug.Log("No Se eligió un ButtonAnims Valido");
            return;

        }
    }

    public void ScaleDown()
    {
        LeanTween.scale(gameObject, Vector3.zero, duration).setEase(curve);
    }

    public void ScaleUp()
    {
        //gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, scale, duration).setEase(curve);
    }

    public void MoveFromLeft()
    {
        //gameObject.transform.position = new Vector3(pos.x - 1080, pos.y, pos.z);
        LeanTween.move(gameObject, pos, duration).setEase(curve);
    }
    
    public void MoveFromRight()
    {
        //gameObject.transform.position = new Vector3(pos.x + 1080, pos.y, pos.z);
        LeanTween.move(gameObject, pos, duration).setEase(curve);
    }

    public void MoveFromUp()
    {
        //gameObject.transform.position = new Vector3(pos.x, pos.y + 1920, pos.z);
        LeanTween.move(gameObject, pos, duration).setEase(curve);
    }
    
    public void MoveFromDown()
    {
        //gameObject.transform.position = new Vector3(pos.x, pos.y - 1920, pos.z);
        LeanTween.move(gameObject, pos, duration).setEase(curve);
    }

    public void ScaleFromCenter()
    {
        //gameObject.transform.position = new Vector3(pos.x, pos.y - 1920, pos.z);
        LeanTween.scale(gameObject, scale, duration).setEase(curve);
    }

    public void MoveFromCustom()
    {
        //gameObject.transform.position = customFromPos;
        LeanTween.move(gameObject, pos, duration).setEase(curve);
    }
    
    public void ScaleFromCustom()
    {
        //gameObject.transform.localScale = customFromScale;
        LeanTween.scale(gameObject, scale, duration).setEase(curve);
    }
}
