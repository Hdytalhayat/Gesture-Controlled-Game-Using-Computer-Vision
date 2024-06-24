using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gun_Script : MonoBehaviour
{
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject Barrel;
    public GameObject handle;
    public GameObject impact;
    public GameObject bullet;
    public float recoilSnapiness;
    public float recoilReturnTime;
    private float timeElapsed;
    private bool recoilReturn;
    private bool performRecoil;
    private bool performShoot=true;
    public float recoilValue=20f;
    private AudioSource gunSound;
    public TMP_Text text_Score;
    public  int score;
    public event EventHandler on_TargetHit;
    bool timeRemaining;
    public static float totalBulletShoot;
    public static float totalBulletHitTarget;

    UDPReceive uDPReceive;
    string data;
    string[] points;


    // Start is called before the first frame update
    void Start()
    {
        uDPReceive = GetComponent<UDPReceive>();
        data = uDPReceive.data;
        //Cursor.lockState = CursorLockMode.Locked;

        timeRemaining = true;
        //GameObject.Find("GameHandler").GetComponent<Timer_Script>().on_Time_finished += onTimeFinished;
    }

    // Update is called once per frame
    void Update()
    {
        DataHandle();

        if (timeRemaining)
        {
            if (Menu_Script.gameIsPlayable)
            {
                if (!Menu_Script.gameIsPaused)
                {
                    if (Input.GetButtonDown("Fire1") || points[1] == "True")
                    {
                        totalBulletShoot++;
                        StartCoroutine("shoot");
                    }
                }
            }
        }
    }

    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }

    IEnumerator shoot()
    {
        if (performShoot)
        {
            gunSound = GetComponent<AudioSource>();
            gunSound.Play();
            muzzleFlash.Play();
            performRecoil = true;
            performShoot = false;
            StartCoroutine("recoil");
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit))
            {

                Target target = hit.transform.GetComponent<Target>();
                GameObject bulletTrail = UnityEngine.Object.Instantiate(bullet, fpsCamera.transform.position, Quaternion.identity);
                bulletTrail.GetComponent<Bullet_Script>().moveBullet(Barrel.transform.position, hit.point);
                Destroy(bulletTrail, .3f);


                if (target != null)
                {
                    //on_TargetHit += target.targetHit;
                    on_TargetHit?.Invoke(this, EventArgs.Empty);
                    //target.targetHit();
                    score++;
                    text_Score.text = score.ToString();
                    totalBulletHitTarget++;
                }

                GameObject impactEffect = UnityEngine.Object.Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactEffect, 1f);
            }
            yield return new WaitForSeconds(recoilSnapiness + recoilReturnTime);
            performShoot = true;
        }
    }

    IEnumerator recoil()
    {
        while (performRecoil)
        {
            Vector3 endRotation = new Vector3(-recoilValue, 0, 0);
            Vector3 startRotation = new Vector3(0, 0, 0);

            if (recoilReturn)
            { //returning gun to its original position after recoil  
                Debug.Log("recoil Return");
                timeElapsed += Time.deltaTime;
                float interpolationValue = timeElapsed / recoilReturnTime;
                transform.localEulerAngles=(Vector3.Lerp(endRotation, startRotation, interpolationValue));
                if (interpolationValue >= 1)
                {
                    recoilReturn = false;
                    timeElapsed = 0;
                    performRecoil = false;
                }
                yield return null;
            }
            else
            { //rotating gun upwards to make a recoil
                timeElapsed += Time.deltaTime;
                float interpolationValue = timeElapsed / recoilSnapiness;
                transform.localEulerAngles=(Vector3.Lerp(startRotation, endRotation, interpolationValue));
                if (interpolationValue >= 1)
                {
                    recoilReturn = true;
                    timeElapsed = 0;                  
                }
                yield return null;

            }
        }
    }

    void onTimeFinished()
    {
        Cursor.lockState = CursorLockMode.Confined;
        timeRemaining = false;
    }
}
