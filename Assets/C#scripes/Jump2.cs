using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jump2 : MonoBehaviour {

    public Rigidbody Rigidbody;
    public GameObject Effect;
    public Text ScoreText;
    public GameObject[] Stage;
    private Animator anim;

    public GameObject Camera;
    public GameObject Player;
    public GameObject StartStage;
    private Vector3 Direction=new Vector3(1,0,0);//方向
    private int number = 1;//接触方块次数;
    private bool isOn = false;
    private int numSpace = 0;
   

    public float JumpTime = 0;
    public float Force = 5;
    private int Score = 0;

   
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Direct();
        Jump();
    }
    private void Direct()//实例化方向
    {
        if ( number >= 3 && isOn==true )
        {
            if (number % 2 == 0)
            {
                Direction = new Vector3(0, 0, 1);
            }
            else if (number % 2 == 1)
            {
                Direction = new Vector3(1, 0, 0);
            }
        }
    }
    private void Jump()//跳跃and实例化
    {
        if (number >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int num = Random.Range(0, 8);//生成方块随机种类
                float RandomLocal = Random.Range(2, 4);//生成方块随机大小；
                Stage[num].transform.localScale = new Vector3(RandomLocal, 2, RandomLocal);
                Instantiate(Stage[num], StartStage.transform.position +=
                    Direction * Random.Range(5, 10),Quaternion .identity );
                //实例化stage；
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Effect, transform.position, transform.rotation);
            //anim.SetBool("isJump", true);
            JumpTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var elapse = Time.time - JumpTime;
            //anim.SetBool("isJump", false);
            Rigidbody.AddForce((new Vector3(0, 1, 0) + Direction) *
                elapse * Force, ForceMode.Impulse);//给玩家添加啊一个力；
            //添加力；
        }
        ScoreText.text = "Score:" + Score;
        Camera.transform.position = Player.transform.position + new Vector3(-10, 10, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")//失败
        {
            SceneManager.LoadScene(0);
        }
        if (collision.collider.tag == "Box")
        {
            Score++;
            number++;
            isOn = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Box")
        {
            isOn = false;
           
        }
    }

}
