using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sprite3D : MonoBehaviour
{

    public GameObject target;
    public bool vertical;
    public string spritesheetName;
    private GameObject character;
    private Object[] sprites;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    void Start()
    {
        character = this.gameObject.transform.parent.gameObject;
        sprites = Resources.LoadAll("sprite_sheets/mario_kart", typeof(Sprite));//in Resources/
        foreach (Object sprite in sprites)
        {
            spriteDictionary.Add(sprite.name, (Sprite)sprite);
        }
        var x = 1;
    }

    void Update()
    {
        lookAtTarget();
        updateSprite();
    }

    //from http://wiki.unity3d.com/index.php?title=CameraFacingBillboard
    void lookAtTarget()
    {
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward,
            target.transform.rotation * Vector3.up);
        if (!vertical)
        {
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        }
    }

    void updateSprite()
    {
        Vector3 target_to_character = target.transform.position - character.transform.position;
        float angle = Vector3.Angle(target_to_character, character.transform.forward);
        Vector3 cross_prod = Vector3.Cross(target_to_character, character.transform.forward);
        bool invertedX = false;
        if (cross_prod.y < 0)
            invertedX = true;

        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        //front_view --0--0
        if (angle >= 0 && angle < 22.5)
        {
            if (spriteRenderer.sprite.name == spritesheetName + "--0--0")
                return;
            spriteRenderer.sprite = spriteDictionary[spritesheetName + "--0--0"];
        }

        //45 view --45--0
        else if (angle >= 22.5 && angle < 67.5)
        {
            if (spriteRenderer.sprite.name == spritesheetName + "--45--0")
                return;
            spriteRenderer.sprite = spriteDictionary[spritesheetName + "--45--0"];
        }

        //90 view --90--0
        else if (angle >= 67.5 && angle < 112.5)
        {
            if (spriteRenderer.sprite.name == spritesheetName + "--90--0")
                return;
            spriteRenderer.sprite = spriteDictionary[spritesheetName + "--90--0"];
        }

        //135 view --135--0
        else if (angle >= 112.5 && angle < 157.5)
        {
            if (spriteRenderer.sprite.name == spritesheetName + "--135--0")
                return;
            spriteRenderer.sprite = spriteDictionary[spritesheetName + "--135--0"];
        }

        //180 view --180--0
        else if (angle >= 157.5 && angle <= 180)
        {
            if (spriteRenderer.sprite.name == spritesheetName + "--180--0")
                return;
            spriteRenderer.sprite = spriteDictionary[spritesheetName + "--180--0"];
        }
        else {
            print("sprite not found!");
            return;
        }

        if (invertedX)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

    }

}
