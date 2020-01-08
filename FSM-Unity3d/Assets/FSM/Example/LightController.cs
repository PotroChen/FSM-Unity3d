using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLToolKits.FSM;
public class LightController : MonoBehaviour
{
    public float MaxIntensity = 1f;
    public float fadeSpeed= 1f;
    public bool isChangeColor;
    private FSMMachine lightFSM;
    private FSMMachine open;
    private State close;
    private Transition open2Close;
    private Transition close2Open;
    private State changeIntensity;
    private State changeColor;

    private Transition color2Intensity;
    private Transition intensity2color;


    private Light myLight;
    private bool isOpen = true;
    //------灯光忽明忽暗动画参数 Start----
    private bool isAnimation;
    private bool isReset;
    private float target;
    private Color startColor;
    private Color targetColor;
    private float colorTimer;
    
    //------End----
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        InitFSM();
    }

    // Update is called once per frame
    void Update()
    {
        lightFSM.UpdateCallback(Time.deltaTime);
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(150f,30f,55f,28f),"Open"))
        {
            isOpen = true;
        }

        if(GUI.Button(new Rect(150f,65f,55f,28f),"Close"))
        {
            isOpen = false;
        }
    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    private void InitFSM()
    {
        changeIntensity = new State("ChangeIntensity");
        changeIntensity.OnEnter+=(IState state)=>{
            isReset = true;
            isAnimation = true;
        };
        changeIntensity.OnUpdate +=(float f)=>{
            if(isAnimation)
            {
                if(isReset)
                {
                    if(FadeTo(MaxIntensity))
                    {
                        isReset = false;
                        isAnimation = false;
                    }
                }
                else
                {
                    if(FadeTo(target))
                        isReset = true;
                }
            }
            else
            {
                target = Random.Range(0.3f,0.7f);
                isAnimation = true;
            }
        };

        changeColor = new State("ChangeColor");
        changeColor.OnEnter+=(IState state)=>{
            isAnimation = false;
        };
        changeColor.OnUpdate+=(float f)=>{
            if(isAnimation)
            {
                if( colorTimer>=1f)
                {
                    isAnimation = false;
                }
                else
                {
                    colorTimer+=Time.deltaTime*1f;
                    myLight.color=Color.Lerp(startColor,targetColor,colorTimer);
                }
            }
            else
            {
                float r = Random.Range(0f,1f);
                float g = Random.Range(0f,1f);
                float b = Random.Range(0f,1f);
                targetColor = new Color(r,g,b);
                startColor = myLight.color;
                colorTimer = 0f;
                isAnimation = true;
            }
        };
        
        color2Intensity = new Transition(changeColor,changeIntensity);
        color2Intensity.OnCheck+=()=>{
            return !isChangeColor;
        };
        changeColor.AddTransition(color2Intensity);

        intensity2color = new Transition(changeIntensity,changeColor);
        intensity2color.OnCheck+=()=>{
            return isChangeColor;
        };
        changeIntensity.AddTransition(intensity2color);

        open = new FSMMachine("Open",changeIntensity);
        open.AddState(changeColor);

        open.OnEnter +=(IState state)=>{
            myLight.intensity = MaxIntensity;
        };
        close = new State("Close");
        close.OnEnter +=(IState state)=>{
            myLight.intensity = 0f;
        };

        open2Close = new Transition (open,close);
        open2Close.OnCheck+=()=>
        {
            return !isOpen;
        };
        open2Close.OnTransition+= ()=>
        {
            return FadeTo(0f);
        };
        open.AddTransition(open2Close);
        

        close2Open = new Transition(close,open);
        close2Open.OnCheck+=()=>
        {
            return isOpen;
        };
        close2Open.OnTransition+= ()=>
        {
            return FadeTo(MaxIntensity);
        };
        close.AddTransition(close2Open);

        lightFSM = new FSMMachine("LightFSM",open);
        lightFSM.AddState(close);
    }

    private bool FadeTo(float f)
    {
        if(Mathf.Abs(myLight.intensity - f)<= 0.05f)
        {
            myLight.intensity = f;
            return true;
        }
        else
        {
            int flag = myLight.intensity>f?-1:1;
            myLight.intensity+=Time.deltaTime *fadeSpeed*flag;
            return false;
        }

    }
}
