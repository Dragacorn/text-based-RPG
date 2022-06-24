using System;

namespace text_based_RPG
{
    class room 
    {
        public static room EMPTY = new room(new int[] {0, 0, 0, 0}, new enemy[] {enemy.NONE});

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