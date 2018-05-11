using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Patrols {
	//巡逻兵进行巡逻的方向
    public class Diretion {
		public const int LEFT = -1;
        public const int UP = 0;
		public const int RIGHT = 1;
        public const int DOWN = 2;
    }
	
	//围栏的位置，这里的地图中没有设置这些围栏，但是我们可以通过位置判断巡逻兵是否碰到
    public class FenchLocs {
        public const float center = 12f;
        public const float left = -3.0f;
        public const float right = 3.0f;
    }
	
	//玩家移动的接口
    public interface IUserAction {
        void movePlayer(int dir);
    }
	
	//巡逻兵的移动模式
    public interface PatrolMove {
        void moveRandomly(GameObject gameObj, bool isSelfTurn);
        void moveToPlayer(GameObject gameObj);
    }

    public interface GameStatus {
        int getPlayerArea();
        void playerScore();
        void gameOver();
    }

    public class SceneController : System.Object, IUserAction, PatrolMove, GameStatus {
        private static SceneController instance;
        private GameModel zyzGameModel;
        private GameEventManager zyzGameEventManager;

        public static SceneController getInstance() {
            if (instance == null)
                instance = new SceneController();
            return instance;
        }

        internal void setGameModel(GameModel zyzGameModel) {
            if (zyzGameModel == null) {
                zyzGameModel = zyzGameModel;
            }
        }

        internal void setGameEventManager(GameEventManager zyzGameEventManager) {
            if (zyzGameEventManager == null) {
                zyzGameEventManager = zyzGameEventManager;
            }
        }

        public void movePlayer(int dir) {
            zyzGameModel.movePlayer(dir);
        }

        public void moveRandomly(GameObject gameObj, bool isSelfTurn) {
            zyzGameModel.moveRandomly(gameObj, isSelfTurn);
        }

        public void moveToPlayer(GameObject gameObj) {
            zyzGameModel.moveToPlayer(gameObj);
        }

        public int getPlayerArea() {
            return zyzGameModel.getPlayerArea();
        }

        public void playerScore() {
            zyzGameEventManager.playerScore();
        }

        public void gameOver() {
            zyzGameEventManager.gameOver();
        }
    }
}

