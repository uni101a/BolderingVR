class WallConf
{
    private static WallConf instance;

    private float WALL_X = 30;
    private int WALL_Y = 25;
    private int EXTEND_Y_LENGTH = 5;
    private int WALL_Z_POSITION = 5;

    private WallConf(){

    }

    public static WallConf GetInstance(){
        if(instance == null){
            instance = new WallConf();
        }
        
        return instance;
    }

    public float GetWALL_X(){
        return WALL_X;
    }

    public int GETWALL_Y(){
        return WALL_Y;
    }

    public int GetEXTEND_Y_LENGTH(){
        return EXTEND_Y_LENGTH;
    }

    public int Get_WALL_Z_POSITION(){
        return WALL_Z_POSITION;
    }
}