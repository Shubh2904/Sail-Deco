using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    [SerializeField] float steerSpeed;
    [SerializeField] float idleSpeed;
    [SerializeField] float topSpeed;
    [Header("Time in which boat reaches top speed:")]
    [SerializeField] float accelTime;
    [Header("Time in which boat comes to stop:")]
    [SerializeField] float deaccelTime;
    
    
    float speed;
    float steerFactor;
    Rigidbody rb;
    bool controlsEnabled;
    MakeCameraFollow cameraFollow;


    GameObject passenger;
    Interactable interactable;



    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraFollow = GetComponent<MakeCameraFollow>();
        interactable = GetComponent<Interactable>();
    }


    void Update()
    {
        if(!controlsEnabled)
            return;

        if(Input.GetKeyDown(KeyCode.E))
            findLanding();

        checkBoostControl();

        checkSteerControl();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        boostBoat(); //to deaccelerate boat even if not being controlled

        if(!controlsEnabled)
            return;

        
        steerBoat();
        
    }

    void onInteract()
    {

        if(GM.playersCurrentBoat != null)
            GM.playersCurrentBoat.exitToNewBoat();

        GM.playersCurrentBoat = this;


        passenger = GM.player;
        passenger.SetActive(false);

        GM.interactRange.setTarget(this.transform);
        GM.interactRange.removeFromRange(this.interactable);

        this.interactable.disable();


        cameraFollow.enabled = true;

        controlsEnabled = true;

        speed = idleSpeed;
    }








    ///////////
    //BOAT EXIT
    ///////////


    void findLanding()
    {
        var hitObjs = Physics.OverlapSphere(transform.position, GM.interactRange.radius);
    
        foreach(var hitObj in hitObjs)
        {
            if(hitObj.tag != "Land")
                continue;
            
            var landDir = hitObj.transform.position - this.transform.position;
            landDir = landDir.normalized;

            Physics.SphereCast(origin: this.transform.position, radius: 2f, direction: landDir, out var landHitInfo);

            var landEdge = landHitInfo.point;
            landEdge = landEdge + landDir * 2f; //move 2f farther into land's edge

            if(landEdge.y > 3) //land too high
                continue;
           
            

            var ray = new Ray(landEdge+new Vector3(0,3,0), Vector3.down);
            
            if(Physics.Raycast(ray, out landHitInfo))
            {    
                exitBoat(landingPoint: landHitInfo.point);
                return;
            } 


        }


        interactable.showInteractPrompt("No Land Nearby...");
        interactable.hideInteractPrompt(delay: 3, resetText: true);
    }

    void exitBoat(Vector3 landingPoint)
    {
        cameraFollow.enabled = false;

        controlsEnabled = false; 
        
        deaccelerateToZero();

    

        passenger.transform.position = landingPoint;
        passenger.SetActive(true);

        GM.interactRange.setTarget(passenger.transform);



        passenger = null;

        this.interactable.enable();

        GM.interactRange.refresh();

        

    }

    public void exitToNewBoat()
    {
        cameraFollow.enabled = false;

        controlsEnabled = false; 
        
        deaccelerateToZero();




        passenger = null;

        this.interactable.enable();

        GM.interactRange.refresh();
    }










    ///////////
    //MOVEMENT CONTROL
    ///////////

    void checkSteerControl()
    {
        if(Input.GetKey(KeyCode.A))
            steerFactor = -1;
        else if(Input.GetKey(KeyCode.D))
            steerFactor = 1;
        else 
            steerFactor = 0;
    }


    void checkBoostControl()
    {
        // if(Input.GetKey(KeyCode.Space))
        //     rb.position += moveSpeed * transform.forward * Time.fixedDeltaTime; 

        if(Input.GetKeyDown(KeyCode.Space))
            accelerate();
        else if(Input.GetKeyUp(KeyCode.Space))
            deaccelerate();
    }












    

    ///////////
    //MOVEMENT LOGIC
    ///////////

    void boostBoat()
    {
        rb.position += Time.fixedDeltaTime * speed * transform.forward;
    }

    void steerBoat()
    {
        rb.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, steerSpeed * steerFactor * Time.deltaTime, 0));
    }




    int accelRef;
    void accelerate()
    {
        if(deaccelarating())
            stopDeaccelarating();
            
        accelRef = LeanTween.value(this.gameObject, setSpeed, speed, topSpeed, accelTime).id;
    }

    int deaccelRef;
    void deaccelerate()
    {
        if(accelarating())
            stopAccelarating();
            
        deaccelRef = LeanTween.value(this.gameObject, setSpeed, speed, idleSpeed, deaccelTime).id;
    }


    void deaccelerateToZero()
    {   
        if(accelarating())
            stopAccelarating();

        LeanTween.value(this.gameObject, setSpeed, speed, 0, 4);
        
    }



    
    bool accelarating()
    {
       return accelRef != 0 && LeanTween.isTweening(accelRef); 
    }    

    bool deaccelarating()
    {
        return deaccelRef != 0 && LeanTween.isTweening(deaccelRef); 
    }





    void stopDeaccelarating()
    {
        LeanTween.cancel(deaccelRef);
    }

    void stopAccelarating()
    {
        LeanTween.cancel(accelRef);
    }



    void setSpeed(float value)
    {
        speed = value;
    }






}
