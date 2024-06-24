
using UnityEngine;


public class Target : MonoBehaviour
{
    public GameObject gun;
    Gun_Script gun_script;
    
    void Start()
    {
        Debug.Log("called");
        gun_script =GameObject.Find("Gun").GetComponent<Gun_Script>();
        gun_script.on_TargetHit += targetHit;
    }
    public GameObject destroyed_Particles;
    public void targetHit(object sender,System.EventArgs e)
    {
        Destroy(gameObject);
        
        GameObject particles = UnityEngine.Object.Instantiate(destroyed_Particles, transform.position, Quaternion.identity);
        Destroy(particles, 1f);
        gun_script.on_TargetHit -= targetHit;
    }
}
