//This object is applied singleton pattern
class TimeManager
{
    #pragma warning disable 0649

    //ゲーム全体の時間
    private int worldTime = 30;
    //時間の最大値
    private int maxWorldTime = 360;

    // ----------------------- apply singleton -----------------------
    private static TimeManager timeInstance;
    private TimeManager(){
    }

    public static TimeManager GetTimeInstance(){
        if(timeInstance == null){
            timeInstance = new TimeManager();
        }

        return timeInstance;
    }

    // ----------------------- applied singleton -----------------------

    public int GetWorldTime(){
        return worldTime;
    }

    /**
    引数分worldTimeを進める
    */
    public void ForwardWorldTime(int forwardTime){
        if(worldTime + forwardTime >= maxWorldTime){
            worldTime = (worldTime + forwardTime) - maxWorldTime;
        }else{
            worldTime += forwardTime;
        }
    }
}