
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCharacter : MonoBehaviour
{
    public int Health = 100;
    public float WalkSpeed = 1.0f;

    enum eFacingDirection
    {
        Left,
        Right
    };
    eFacingDirection FacingDirection = eFacingDirection.Left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector;
        if (FacingDirection == eFacingDirection.Left)
        {
            gameObject.transform.position += new Vector3(-WalkSpeed * Time.deltaTime, 0.0f);
            if (gameObject.transform.position.x < 100)
            {
                FacingDirection = eFacingDirection.Right;
                gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            gameObject.transform.position += new Vector3(WalkSpeed * Time.deltaTime, 0.0f);
            if (gameObject.transform.position.x > 700)
            {
                FacingDirection = eFacingDirection.Left;
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}
