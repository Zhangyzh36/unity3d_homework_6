using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Patrols;

//工厂模式生成巡逻兵
namespace Com.Patrols {
    public class PatrolFactory : System.Object {
        private static PatrolFactory instance;
        private GameObject PatrolInstance;
		
		//巡逻兵的初始位置
        private Vector3[] PatrolLocations = new Vector3[] { new Vector3(-4, 0, 10), new Vector3(-1, 0, 10),
            new Vector3(4, 0, 10), new Vector3(-4, 0, 4), new Vector3(0, 0, 4), new Vector3(4, 0, 4)};
	
		//单例
        public static PatrolFactory getInstance() {
            if (instance == null)
                instance = new PatrolFactory();
            return instance;
        }
	
		//初始化用于生产的模具
        public void initPatrolInatance(GameObject PatrolInstance) {
            this.PatrolInstance = PatrolInstance;
        }
		
		
        public GameObject getPatrol() {
            GameObject newPatrol = Camera.Instantiate(PatrolInstance);
            return newPatrol;
        }

        public Vector3[] getPatrolLocations() {
            return PatrolLocations;
        }
    }
}

