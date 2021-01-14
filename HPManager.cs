using System;

//This object is applied singleton pattern
class HPManager
{
    #pragma warning disable 0649
    
    //左手のhpの値
    private float leftHP;
    //右手のhpの値
    private float rightHP;
    //hpを回復する値
    private float continuouslyHealVal = 0.05f;
    //hpを減らす値
    private float continuouslyReduceVal = 0.1f;

    // ----------------------- apply singleton -----------------------
    private static HPManager hpInstance;

    private HPManager(){
    }

    public static HPManager GetHPInstance(){
        if(hpInstance == null){
            hpInstance = new HPManager();
        }

        return hpInstance;
    }

    // ----------------------- applied singleton -----------------------


    // ----------------------- start Getter Setter -----------------------

    public float GetLeftHP(){
        return leftHP;
    }
    
    public float GetRightHP(){
        return rightHP;
    }

    public float GetContinuouslyHealVal(){
        return continuouslyHealVal;
    }

    public float GetContinuouslyReduceVal(){
        return continuouslyReduceVal;
    }

    public void SetLeftHP(float leftHP){
        this.leftHP = leftHP;
    }
    
    public void SetRightHP(float rightHP){
        this.rightHP = rightHP;
    }

    public void SetContinuouslyHealVal(float continuouslyHealVal){
        this.continuouslyHealVal = continuouslyHealVal;
    }

    public void SetContinuouslyReduceValC(float continuouslyReduceVal){
        this.continuouslyReduceVal = continuouslyReduceVal;
    }

    // ----------------------- end Getter Setter -----------------------

    
    /**
    判定したい値が閾値より大きいときtrueを返す

    num: 判定したい値
    threshold: 閾値
    */
    private bool IsAboveThreshold(float num, float threshold){
        if(num > threshold){
            return true;
        }else
            return false;
    }

    /**
    引数に与えられた手のhpを減少

    controllerType: left or right
    */
    public void ReduceGrabbingHandsHP(string controllerType, float reduceVal){
        if(controllerType == "left"){
            float accReduceVal = Math.Abs(reduceVal - leftHP);

            if(IsAboveThreshold(accReduceVal, reduceVal)){
                leftHP -= reduceVal;
            }else if(IsAboveThreshold(accReduceVal, reduceVal)){
                leftHP -= accReduceVal;
            }

            SetLeftHP(leftHP);
        }

        if(controllerType == "right"){
            float accReduceVal = Math.Abs(reduceVal - rightHP);

            if(IsAboveThreshold(accReduceVal, reduceVal)){
                rightHP -= reduceVal;
            }else if(IsAboveThreshold(accReduceVal, reduceVal)){
                rightHP -= accReduceVal;
            }

            SetRightHP(rightHP);
        }
    }

    /**
    引数に与えられた手のhpを回復

    controllerType: left or right
    */
    public void HealHandsHP(string controllerType, float healVal){
        if(controllerType == "left"){
            float accHealVal = 100.00f - leftHP;

            if(IsAboveThreshold(accHealVal, healVal)){
                leftHP += healVal;
            }else if(IsAboveThreshold(accHealVal, healVal)){
                leftHP += accHealVal;
            }

            SetLeftHP(leftHP);
        }
        
        if(controllerType == "right"){
            float accHealVal = 100.00f - rightHP;
            
            if(IsAboveThreshold(accHealVal, healVal)){
                rightHP += healVal;
            }else if(IsAboveThreshold(accHealVal, healVal)){
                rightHP += accHealVal;
            }

            SetRightHP(rightHP);
        }
    }

    public static void DestroyInstance(){
        hpInstance = null;
    }
}