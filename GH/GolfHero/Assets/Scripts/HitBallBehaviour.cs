using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HitBallBehaviour : MonoBehaviour {

    private class Position {
        public Vector3 pos;
        public Position prev;
        public Position next;

        public Position(Vector3 pos, Position prev = null, Position next = null) {
            this.pos = pos;
            this.prev = prev;
            this.next = next;
        }
    }

    private Position curr;
    private Camera cam;
    private static Rigidbody rb;
	private float power = 1f;
    private float jumpPower = 25f;
    private float maxPower = 36f;
    private bool increase = true;
    private static Vector3 startPos;
    private Vector3 force;
    public GameObject arrow;
	private Renderer[] arrowRend;
    private int stage = 0;
    private int[] windSpd = { 0, 0, 0 };
    public static Vector3 windDir;
    private static bool pause = false;
    private static bool resume = false; // ignore click if just resuming
    private static bool calcWind = true;
    private bool cheated = false;
    private bool shoot = false;
    private Component windTxt;
    private float time;
    private float wait = .08f;
    private int shot = 0;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        startPos = rb.transform.position;
		arrowRend = arrow.GetComponentsInChildren<Renderer> ();
        curr = new Position(startPos);
        checkStage();
        Cursor.lockState = CursorLockMode.Locked;
        time = wait;
    }

    public static void jump(float force){
        rb.AddForce(Vector3.up * force);
    }

    public static void boostCurrentForce(float multiplier, float addition){
        Vector3 normalizedVelocity = Vector3.Normalize(rb.velocity);//Get the current velocity
        float prevMag = rb.velocity.magnitude;//Use for multiplier
        Debug.Log("Entered a speed pad with force + " + rb.velocity.ToString() + "\n magnitude: " + prevMag.ToString());
    
        rb.AddForce(normalizedVelocity * (addition));//Perform addition first then multiply
        Debug.Log("Adding force: " + (normalizedVelocity * (addition)).ToString());
        rb.AddForce(normalizedVelocity * ((multiplier -1f) * prevMag));
        Debug.Log("Adding force: " + (normalizedVelocity * (multiplier -1f) * prevMag).ToString());
    }
    
    // Update is called once per frame
    void Update() {
        if (resume || pause) {
            resume = false;
            return;
        }

        if (calcWind && rb.velocity == Vector3.zero) {
            windDir = new Vector3(UnityEngine.Random.Range(-1f, 1.1f), 0, UnityEngine.Random.Range(-1f, 1.1f));
            windSpd[2] = UnityEngine.Random.Range(windSpd[0], windSpd[1] + 1);
            //((Text)windTxt).text = "Wind Speed: " + windSpd[2] +"\nWind Direction:" + windDir;
            WindText.SetText("Wind Speed: " + windSpd[2] + "km/h\nWind Direction: " + windDir +
                "\nPower: " + power + "\nMax Power: " + maxPower + "\nCoins: " + CoinBehaviour.Collected);
            calcWind = false;
            updatePos();
        }

        if (!shoot && rb.velocity == Vector3.zero && Input.GetKeyUp("mouse 0")) {
            force = cam.transform.forward;
            shoot = true;
            BallCamController.Disabled(true);
        } else if (shoot && Input.GetKeyUp("mouse 0")) {
            shoot = false;
            BallCamController.Disabled(false);
            shot++;
            calcWind = true;
            /*Possibly account for ball being on slope */
            //Vector3 planeNorm = rb.getPlane();
            Vector3 planeNorm = new Vector3(0, 1, 0); //Use the norm of the x,z plane
            force = Vector3.ProjectOnPlane(force, planeNorm).normalized; //Project onto a flat surface as we dont care about camera height
            Debug.Log(force);
            rb.AddForce(force * power * power + windDir * windSpd[2] * windSpd[2]);
            power = 1f;
        } else if (shoot && Input.GetKeyUp("mouse 1")) {
            shoot = false;
            BallCamController.Disabled(false);
            power = 1f;
        }

        if (Input.GetKeyDown("s")) {
            stopBall();
        } else if (Input.GetKeyDown("r")) {
            resetBall();
        } else if (Input.GetKeyDown("j")) {
            jump(jumpPower*jumpPower);
        } else if (Input.GetKeyDown("d")) {
            if (curr.prev != null) {
                stopBall();
                rb.transform.position = curr.prev.pos;
                curr = curr.prev;
                shot--;
                calcWind = true;
                cheated = true;
            }
        } else if (Input.GetKeyDown("f")) {
            if (curr.next != null) {
                stopBall();
                rb.transform.position = curr.next.pos;
                curr = curr.next;
                shot++;
                calcWind = true;
                cheated = true;
            }
        }

        // Hide arrow when ball is in motion
        if (rb.velocity == Vector3.zero) {
			foreach (Renderer rend in arrowRend) {
				rend.enabled = true;
			}

		} else {
			foreach (Renderer rend in arrowRend) {
				rend.enabled = false;
			}
		}
    }

    private void updatePos() {
        if (shot > 0) {
            if (cheated) {
                cheated = false;
                return;
            }
            curr = new Position(rb.transform.position, curr);
            curr.prev.next = curr;
            //curr.next = newPos;
            //curr = newPos;
        }
        Debug.Log(shot);
    }

    private void FixedUpdate() {
        if (shoot) {
            if (increase) {
                if (power > maxPower) {
                    if (time >= 0) {
                        power = maxPower;
                        time -= Time.fixedUnscaledDeltaTime;
                        return;
                    }
                    else {
                        increase = false;
                        time = wait;
                        return;
                    }
                }
                power += .67f;
            } else {
                if (power < 1f) {
                    if (time >= 0) {
                        power = 1f;
                        time -= Time.fixedUnscaledDeltaTime;
                        return;
                    } else {
                        increase = true;
                        time = wait;
                        return;
                    }
                }
                power -= .67f;
            }
        }
        WindText.SetText("Wind Speed: " + windSpd[2] + "km/h\nWind Direction: " + windDir +
                "\nPower: " + power + "\nMax Power: " + maxPower + "\nCoins: " + CoinBehaviour.Collected);
    }

    public static void Pause() {
        pause = true;
        BallCamController.Disabled(true);
    }

    public static void Resume() {
        resume = true;
        pause = false;
        BallCamController.Disabled(false);
    }

    public static void stopBall() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public static void resetBall() {
        stopBall();
        rb.transform.position = startPos;
    }

    private void checkStage()
    {
        switch (stage)
        {
            case 0:
                windSpd[0] = Stage0.windSpdMin;
                windSpd[1] = Stage0.windSpdMax;
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            default:
                break;
        }
    }
}
