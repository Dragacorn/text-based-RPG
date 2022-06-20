using System;

namespace text_based_RPG
{
    class room 
    {
        public static room EMPTY = new room(new int[] {0, 0, 0, 0}, new enemy[] {enemy.NONE});
        public static room BOSS = new room(new int[]{0,0,1,0}, new enemy[]{new enemy(5, 50, 5, 7, 45, 10, 1.25f)});
        int[] doors;
        enemy[] enemies;
        public room(int[] open, enemy[] en) {
            doors = open;
            enemies = en;
        }
        public int[] getExits() {
            return doors;
        }
        public enemy[] GetEnemies() {
            return enemies;
        }
        public void loadRoom(character player) {
            player.printStats();
            enemies = this.GetEnemies();
            foreach (enemy e in enemies) {
                e.printStats();
            }
        }
    }
}