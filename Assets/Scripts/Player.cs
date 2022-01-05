using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    public float vida;

    public float velocidade;
    public float forcaPulo;

    bool estaPulando;
    public bool vulneravel;

    private GameController gc;
    float direcao; 

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        direcao = Input.GetAxis("Horizontal");

        if(direcao > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetInteger("Transition", 1);
        }

        if(direcao < 0)
        {        
            transform.eulerAngles = new Vector2(0, 180);

        }

        if(direcao != 0 && estaPulando == false) 
        {
            anim.SetInteger("Transition", 1);
        }
       
        if(direcao == 0 && estaPulando == false)
        {
            anim.SetInteger("Transition", 0);
        }

        Jump();
    }

    private void FixedUpdate()
    {
        rig.velocity = new Vector2(direcao * velocidade, rig.velocity.y);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && estaPulando == false)
        {
            rig.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            anim.SetInteger("Transition", 2); 
            estaPulando = true; 
        }
    }
     
     public void GerarDano()
     {
         if(vulneravel == false)
         {
             gc.PerderVida(vida);
             vida--;
             vulneravel = true;
             StartCoroutine(Respawn());
         }
     }

     IEnumerator Respawn()
     {
        spriteRenderer.enabled = false;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.enabled = true;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.enabled = false;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.enabled = true;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.enabled = false;
         yield return new WaitForSeconds(0.2f);
         spriteRenderer.enabled = true;
         yield return new WaitForSeconds(0.2f);
         vulneravel = false;
     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            estaPulando = false;
        }
    }
}
