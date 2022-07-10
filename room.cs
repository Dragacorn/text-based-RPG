using System;

namespace text_based_RPG
{
    class room 
    {
        public static room EMPTY = new room(room.n, new enemy[] {enemy.NONE});

        public static bool[] n = {false, false, false, false};
        public static bool[] l = {false, false, false, true};
        public static bool[] d = {false, false, true, false};
        public static bool[] dl = {false, false, true, true};
        public static bool[] r = {false, true, false, false};
        public static bool[] h = {false, true, false, true};
        public static bool[] dr = {false, true, true, false};
        public static bool[] tu = {false, true, true, true};
        public static bool[] u = {true, false, false, false};
        public static bool[] ul = {true, false, false, true};
        public static bool[] v = {true, false, true, false};
        public static bool[] tr = {true, false, true, true};
        public static bool[] ur = {true, true, false, false};
        public static bool[] td = {true, true, false, true};
        public static bool[] tl = {true, true, true, false};
        public static bool[] o = {true, true, true, true};
        


        bool[] doors;
        enemy[] enemies;
        public room(bool[] open, enemy[] en) {
            doors = open;
            enemies = en;
        }
        public bool[] getExits() {
            return doors;
        }
        public enemy[] GetEnemies() {
            return enemies;
        }
        public bool cleared() {
            foreach (enemy e in enemies)
            {
                if (!e.isDead()) {
                    return false;
                }
            }

            return true;
        }
        public void loadRoom(character player) {
            player.printStats();
            enemies = this.GetEnemies();
            for(int i = 0; i < enemies.Length; i++) {
                enemies[i].printStats(i);
            }
        }
    }
}