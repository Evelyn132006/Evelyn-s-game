using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class player : MonoBehaviour
{
    // local variables for the 3 sliders
    public Slider healthSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;

    public Animator anim;
    public GameObject cross;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            hungerSlider.value -= 0.5f;
            thirstSlider.value -= 1;
            //check if hunger or thirst is zero
            if (hungerSlider.value == 0 || thirstSlider.value == 0)
            {
                healthSlider.value -= 1;
            }
            if (healthSlider.value == 0)
            {
                //gameOver
                SceneManager.LoadScene("GameOver");
            }
            timer = 0;
        }

        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //when the raycasthits an objects
        if(Physics.Raycast(ray, out hit, 4))
        {
            //if we are looking at a potato and we press E
            if(hit.transform.gameObject.tag.Equals("potato") && Input.GetKeyDown(KeyCode.E))
            {
                hungerSlider.value += 25;
                Destroy(hit.transform.gameObject);
            }
            else if(hit.transform.gameObject.tag.Equals("lettuce") && Input.GetKeyDown(KeyCode.E))
            {
                hungerSlider.value += 10;
                thirstSlider.value += 25;
                Destroy(hit.transform.gameObject);
            }

            if(hit.transform.gameObject.tag.Equals("potato") || hit.transform.gameObject.tag.Equals("lettuce"))
            {
                cross.transform.localScale = new Vector3(2, 2, 2);
            }
            else
            {
                cross.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            cross.transform.localScale = new Vector3(1, 1, 1);
        }


    }
    
}
