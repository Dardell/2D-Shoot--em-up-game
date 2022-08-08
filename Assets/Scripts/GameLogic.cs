using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class GameLogic : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();
    private Camera cam;
    private Transform player_rb;
    private float fire_start_time = 0;
    public int totalEnemiesNumber = 500;
    public GameObject enemy_01;
    public Player player;
    public TMP_Text someText;
    public GameObject bullet;
    public GameObject enemiesParent;
    public GameObject bulletsParent;
    public GameObject terrain;
    public Joystick attackJoystick;

    void Start()
    {
        totalEnemiesNumber = 500;
        cam = Camera.main;
        player_rb = player.GetComponent<Transform>();
        InvokeRepeating("SpawnConstEnemiesOutside", 1.0f, 0.1f);
        Vector2 position = new Vector2(player.rb.position.x-12, player.rb.position.y-10);
    }

    void Update()
    {
        //Debug.Log("Memory usage: " + (System.GC.GetTotalMemory(false)/1048576)+ "mb");
        AttackJoystick();
    }

    public static bool IsPointerOverUIObject()
    {
     PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
     eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
     List<RaycastResult> results = new List<RaycastResult>();
     EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
     return results.Count > 0;
    }
    void SpawnConstEnemiesOutside(){
        if (enemyList.Count < totalEnemiesNumber){
        Vector3 spawnPosition = MathUtilities.OutsideTheScreen(cam);
        GameObject instance = (Instantiate(enemy_01, spawnPosition, Quaternion.identity));
        enemyList.Add(instance);
        instance.transform.parent = enemiesParent.transform;
        someText.text = "Enemies: " + enemyList.Count;
        }
    }

    void Attack(Vector2 attackVector){
        bool attackNotOnCooldown = (Time.time - fire_start_time) >= player.attackSpeed;
        if (attackNotOnCooldown){
            Vector3 position = new Vector3(player.rb.position.x,player.rb.position.y,-5);
            GameObject instance = Instantiate(bullet, position, Quaternion.identity);
            instance.transform.parent = bulletsParent.transform;
            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();
            rigidbody.velocity = attackVector * player.bulletSpeed;
            fire_start_time = Time.time;
        }
    }
    void AttackJoystick(){
    if (attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0){
        Vector2 direction = new Vector2 (attackJoystick.Horizontal, attackJoystick.Vertical);
        direction.Normalize();
        Attack(direction);
    }
    }
    void RandomAttack(){
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool attackNotOnCooldown = (Time.time - fire_start_time) >= player.attackSpeed;
        if (attackNotOnCooldown){
            Vector2 rndVect = Random.insideUnitCircle;
            rndVect.Normalize();
            Attack(rndVect);
            fire_start_time = Time.time;
        }
    }
    void AttackPC(){
        if (Input.GetMouseButton(0)){
            bool attackNotOnCooldown = (Time.time - fire_start_time) >= player.attackSpeed;
            if (attackNotOnCooldown && !PointerBlock.blockedByUI){
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = worldMousePosition - player.transform.position;
                direction.Normalize();
                fire_start_time = Time.time;
                Attack(direction);
                }
        }
    }
}

 public static class MathUtilities
 {

     public static Vector3 RandomVector(float minx, float maxx, float miny, float maxy)
     {
         return new Vector3(UnityEngine.Random.Range(minx, maxx), UnityEngine.Random.Range(miny, maxy), 0);
     }

     public static Vector3 InsideTheScreen(Camera cam){
        float spawnX = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).x, cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        float spawnY = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).y, cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        return new Vector3(spawnX, spawnY, 0);
     }

    public static Vector3 OutsideTheScreen(Camera cam){
        float spawnX = 0;
        float spawnY = 0;
        float xMinOffset = 1;
        float xMaxOffset = 15;
        float yMinOffset = 1;
        float yMaxOffset = 15;
        int randPartOfTheScreen = Random.Range(0,8);

        float xScreenLeft = cam.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float xScreenRight = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        float yScreenTop = cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        float yScreenBottom = cam.ScreenToWorldPoint(new Vector2(0, 0)).y;

        List<System.Action> actions = new List<System.Action>();
        actions.Add( ()=> LeftBottom() );
        actions.Add( ()=> LeftCenter() );
        actions.Add( ()=> LeftTop() );
        actions.Add( ()=> TopCenter() );
        actions.Add( ()=> TopRight() );
        actions.Add( ()=> RightCenter() );
        actions.Add( ()=> RightBottom() );
        actions.Add( ()=> BottomCenter() );

        actions[randPartOfTheScreen]();

        void LeftCenter(){
        spawnX = Random.Range(xScreenLeft- xMinOffset, xScreenLeft- xMaxOffset);
        spawnY = Random.Range(yScreenBottom, yScreenTop);
         }
         void LeftTop(){
        spawnX = Random.Range(xScreenLeft- xMinOffset, xScreenLeft- xMaxOffset);
        spawnY = Random.Range(yScreenTop, yScreenTop+yMaxOffset);
         }
         void LeftBottom(){
        spawnX = Random.Range(xScreenLeft- xMinOffset, xScreenLeft- xMaxOffset);
        spawnY = Random.Range(yScreenBottom, yScreenBottom-yMaxOffset);
         }
         void BottomCenter(){
        spawnX = Random.Range(xScreenLeft, xScreenRight);
        spawnY = Random.Range(yScreenBottom - yMinOffset, yScreenBottom - yMaxOffset);
         }
         void TopCenter(){
        spawnX = Random.Range(xScreenLeft, xScreenRight);
        spawnY = Random.Range(yScreenTop + yMinOffset, yScreenTop + yMaxOffset);
         }
         void TopRight(){
        spawnX = Random.Range(xScreenRight + xMinOffset, xScreenRight + xMaxOffset);
        spawnY = Random.Range(yScreenTop + yMinOffset,yScreenTop + yMaxOffset);
         }
         void RightBottom(){
        spawnX = Random.Range(xScreenRight + xMinOffset, xScreenRight + xMaxOffset);
        spawnY = Random.Range(yScreenBottom - yMinOffset,yScreenBottom - yMaxOffset);
         }
         void RightCenter(){
        spawnX = Random.Range(xScreenRight + xMinOffset, xScreenRight + xMaxOffset);
        spawnY = Random.Range(yScreenBottom, yScreenTop);
         }

        return new Vector3(spawnX, spawnY, 0);
     }
 }
