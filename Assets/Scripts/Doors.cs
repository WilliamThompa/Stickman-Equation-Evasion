using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{

    public bool isLevelSelectDoor;
    public int doorNumber;
    private bool inExitZone;
    public string levelToLoad;
    public Sprite lockSprite;
    public SpriteRenderer doorNumberRender;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField] Animator anim;

    public UIManager uim;

    // Start is called before the first frame update
    void Start()
    {
        inExitZone = false;
        uim = GameObject.Find("UIManager").GetComponent<UIManager>();
        anim = GameObject.Find("SceneTransition").GetComponentInChildren<Animator>();
        if (isLevelSelectDoor)
        {
            Debug.Log("DOOR CHECK" + doorNumber.ToString());
            levelManager = LevelManager.self;
            if(doorNumber == 2 && !levelManager.enabledLevel2)
            {
                //gameObject.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                doorNumberRender.sprite = lockSprite;
                
            }
            if (doorNumber == 3 && !levelManager.enabledLevel3)
            {
                //gameObject.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                doorNumberRender.sprite = lockSprite;
            }
            if (doorNumber == 4 && !levelManager.enabledLevel4)
            {
                //gameObject.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                doorNumberRender.sprite = lockSprite;
            }
            if (doorNumber == 5 && !levelManager.enabledLevel5)
            {
                //gameObject.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
                doorNumberRender.sprite = lockSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && inExitZone)
        {
            if (!isLevelSelectDoor)
            {
                LevelManager levelManger = FindObjectOfType<LevelManager>();
                if(doorNumber == 1)
                {
                    levelManger.enabledLevel2 = true;
                }
                if (doorNumber == 2)
                {
                    levelManger.enabledLevel3 = true;
                }
                if (doorNumber == 3)
                {
                    levelManger.enabledLevel4 = true;
                }
                if (doorNumber == 4)
                {
                    levelManger.enabledLevel5 = true;
                }

            }
            StartCoroutine(LoadLevel());
            // Transition method
            //SceneManager.LoadSceneAsync(levelToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player Variant")
        {
            inExitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player Variant")
        {
            inExitZone = false;
        }
    }

    IEnumerator LoadLevel()
    {
        anim.SetTrigger("Finish");
        //Save
        uim.Save();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelToLoad);
        anim.SetTrigger("Start");
    }
}
