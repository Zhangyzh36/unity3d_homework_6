using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

/*这个脚本放在玩家的游戏对象身上，用来判断玩家处于哪一个区域中
 *      FenchLocs:
 *
 *      left  right
 * |-----|-----|-----|
 * |  0  |  1  |  2  |
 * |-----|-----|-----|center
 * |  3  |  4  |  5  |
 * |-----|-----|-----|
 *
 */
public class PlayerStatus : MonoBehaviour {
    public int whichArea = -1;

	void Start () {
		
	}
	
	void Update () {
		//地图如下
		
        float x = this.gameObject.transform.position.x;
        float z = this.gameObject.transform.position.z;
        //玩家处于上半区域的：
		if (z >= FenchLocs.center) {
			//左边
            if (x < FenchLocs.left)
                whichArea = 0;
            //右边
			else if (x > FenchLocs.right)
                whichArea = 2;
            //中间
			else
                whichArea = 1;
        }
	    //玩家处于下半区域的：
        else {
			//左边
            if (x < FenchLocs.left)
                whichArea = 3;
            //右边
			else if (x > FenchLocs.right)
                whichArea = 5;
            //中间
			else
                whichArea = 4;
        }
	}
}
