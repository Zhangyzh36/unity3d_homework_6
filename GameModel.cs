using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

public class GameModel : SSActionManager, ISSActionCallback {
	//运行之前将预制拖入这些属性中
    public GameObject PatrolInstance, PlayerInstance, backgroundInstance, uiInstance;

    private SceneController scene;
    private GameObject zyzPlayer, background, gameText;
	//巡逻兵对象列表
    private List<GameObject> PatrolList;
	//巡逻兵巡逻的方向
    private List<int> PatrolDirection;
	//巡逻兵的巡逻速度
    private const float NORMAL_SPEED = 0.5f;
	//巡逻兵追逐玩家时的速度
    private const float CHASING_SPEED = 0.7f;

    void Awake() {
		//用一个预制初始化工厂中用于生产巡逻兵的对象
        PatrolFactory.getInstance().initPatrolInatance(PatrolInstance);
    }

    protected new void Start () {
        scene = SceneController.getInstance();
        scene.setGameModel(this);
		//生产玩家（同样是用之前做好的预制）
        createPlayer();
		//用工厂制作6个巡逻兵
        createPatrols();
		//背景
        background = Instantiate(backgroundInstance);
        gameText = Instantiate(uiInstance);
    }

    protected new void Update() {
        base.Update();
    }

    void createPlayer() {
        zyzPlayer = Instantiate(PlayerInstance);
    }

    void createPatrols() {
        PatrolList = new List<GameObject>(6);
        PatrolDirection = new List<int>(6);
        Vector3[] locations = PatrolFactory.getInstance().getPatrolLocations();
        for (int i = 0; i < 6; i++) {
			//使用工厂模式初始化游戏对象
            GameObject newPatrol = PatrolFactory.getInstance().getPatrol();
            newPatrol.transform.position = locations[i];
            newPatrol.name = "Patrol" + i;
			//初始化巡逻方向为空
            PatrolDirection.Add(-2);
            PatrolList.Add(newPatrol);
			//开始让巡逻兵随机移动
            moveRandomly(newPatrol, true);
        }
    }

    //玩家的移动
    public void movePlayer(int dir) {
		//欧拉角直接旋转90度
        zyzPlayer.transform.rotation = Quaternion.Euler(new Vector3(0, dir * 90, 0));
		if (dir == Diretion.UP)
			zyzPlayer.transform.position += new Vector3(0, 0, 0.15f);
		else if (dir == Diretion.DOWN)
			zyzPlayer.transform.position += new Vector3(0, 0, -0.15f);
		else if (dir == Diretion.LEFT)
			zyzPlayer.transform.position += new Vector3(-0.15f, 0, 0);
		else if (dir == Diretion.RIGHT)
			zyzPlayer.transform.position += new Vector3(0.15f, 0, 0);
    }

	
    public void SSActionEvent(SSAction source, SSActionEventType eventType = SSActionEventType.Completed, 
        SSActiontargetPositionType intParam = SSActiontargetPositionType.Normal, string strParam = null, object objParam = null) {
        //若是正常模式下就随机巡逻
		if (intParam == SSActiontargetPositionType.Normal)
            moveRandomly(source.gameObject, true);
		//否则就追逐玩家
        else
            moveToPlayer(source.gameObject);
    }

    //isSelfTurn说明是否主动变向（动作结束）
    public void moveRandomly(GameObject gameObj, bool isSelfTurn) {
        int index = getIndex(gameObj);
        int moveDir = getmoveDirection(index, isSelfTurn);
        PatrolDirection[index] = moveDir;

        gameObj.transform.rotation = Quaternion.Euler(new Vector3(0, moveDir * 90, 0));
        Vector3 targetPosition = gameObj.transform.position;
        
		if (movedir == Diretion.UP)
            targetPosition += new Vector3(0, 0, 1);     
        else if (movedir == Diretion.DOWN)
            targetPosition += new Vector3(0, 0, -1);         
        else if (movedir == Diretion.LEFT)
            targetPosition += new Vector3(-1, 0, 0);       
        else if (movedir == Diretion.RIGHT)
            targetPosition += new Vector3(1, 0, 0);
           
        addSingleMoving(gameObj, targetPosition, NORMAL_SPEED, false);
    }
	
	//根据巡逻兵的名字判断巡逻兵的编号
    int getIndex(GameObject gameObj) {
        string name = gameObj.name;
        return name[name.Length - 1] - '0';
    }
	
	//获取巡逻兵的巡逻方向，isSelfTurn表明是正常换方向还是撞围栏的情况
    int getmoveDirection(int index, bool isSelfTurn) {
        int moveDir = Random.Range(-1, 3);
        //如果要碰到墙
		if (!isSelfTurn) {
			//不能走相同的方向并且不能迈出界限
            while (PatrolDirection[index] == moveDir || isPatrolOut(index, moveDir)) {
                moveDir = Random.Range(-1, 3);
            }
        }
		//如果是正常的换巡逻方向，下一时刻的巡逻方向不能相同或相反
        else {    
		    /*      0
			 *      |
			 * -1---|--- 1
			 *      |
			 *      2
			 *若两个方向相反且不等，则和为2或0
			 */
			int sumOfMovingDirection = PatrolDirection[index] + moveDir;
            while ( PatrolDirection[index] == moveDir || sumOfMovingDirection == 2 || sumOfMovingDirection == 0
                || isPatrolOut(index, moveDir)) {
                moveDir = Random.Range(-1, 3);
            }
        }
        
        return moveDir;
    }
	
    //判断巡逻兵是否走出了自己的区域
    bool isPatrolOut(int index, int moveDir) {
        Vector3 patrolPos = PatrolList[index].transform.position;
        float x = patrolPos.x;
        float z = patrolPos.z;
		
		bool falg = false;
       
		if  (index == 0 && (moveDir == 1 && x + 1 > FenchLocs.left
			    || moveDir == 2 && z - 1 < FenchLocs.center))
			flag = true;
		else if (index == 1 && (x + 1 > FenchLocs.right
				|| moveDir == -1 && x - 1 < FenchLocs.left
				|| moveDir == 2 && z - 1 < FenchLocs.center))
			flag = true;   
        else if (index == 2 && (moveDir == -1 && x - 1 < FenchLocs.right
			    || moveDir == 2 && z - 1 < FenchLocs.center))
			flag = true;
        else if (index == 3 && (moveDir == 1 && x + 1 > FenchLocs.left
                || moveDir == 0 && z + 1 > FenchLocs.center))
			flag = true;
        else if (index == 4 && (moveDir == 1 && x + 1 > FenchLocs.right
                || moveDir == -1 && x - 1 < FenchLocs.left
                || moveDir == 0 && z + 1 > FenchLocs.center))
            flag = true;   
        else if (moveDir == -1 && x - 1 < FenchLocs.right
                || moveDir == 0 && z + 1 > FenchLocs.center)
            flag = true;
          
        
        return flag;
    }

    //追逐玩家
    public void moveToPlayer(GameObject gameObj) {
		//追逐之前先将巡逻方向置为空
        int index = getIndex(gameObj);
        PatrolDirection[index] = -2;

        gameObj.transform.LookAt(gameObj.transform);
		//计算追逐的方向
        Vector3 chasingDirection = zyzPlayer.transform.position - gameObj.transform.position;
		//计算移动的目标位置
        Vector3 targetPosition = new Vector3(chasingDirection.x , 0, chasingDirection.z);
        targetPosition += gameObj.transform.position;
        this.runAction(gameObj, CCMoveToAction.GetSSAction(targetPosition, CHASING_SPEED, true), this);
    }

    //获取玩家所在的区域
    public int getPlayerArea() {
        return zyzPlayer.GetComponent<PlayerStatus>().whichArea;
    }
}
