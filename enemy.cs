using System;

namespace text_based_RPG
{
    class enemy 
    {
        public static enemy NONE = new enemy(0, 0, 0, 0, 0, 0, 0);
        public static enemy random() {
            Random rand = new Random();
            return new enemy(1, rand.Next(10, 25), rand.Next(1, 7), rand.Next(1, 7), rand.Next(1, 7), rand.Next(1, 7), (float)rand.NextDouble() + (1 / 5));
        }

        public static enemy[] randGroup(int size) {
            if(size == 0) {
                return new enemy[] {enemy.NONE};
            }
            enemy[] ret = new enemy[size];
            for (int i = 0; i < size; i++) {
                ret[i] = enemy.random();
            }
            return ret;
        }

        int level, hp, mhp, atk, def, mag, spr;
        float exp;

        public enemy(int level, int hp, int atk, int def, int mag, int spr, float rewardEXP) {
            this.level = level;
            this.mhp = hp;
            this.hp = mhp;
            this.atk = atk;
            this.def = def;
            this.mag = mag;
            this.spr = spr;
            this.exp = rewardEXP;
        }

        public bool isDead() {
            return hp <= 0;
        }

        public static void doTurn(room curRoom, character player) {
            enemy[] enemies = curRoom.GetEnemies();
            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i].isDead()) {
                    enemies[i] = enemy.NONE;
                } else {
                    enemies[i].attack(player);
                }
            }
        }

        public void attack(character toAttack) {
            if (this.Equals(enemy.NONE) || toAttack.isDead()) {
                return;
            }
            int moveType = mag * 1.5f / toAttack.getStats()[4] > atk * 1.5f / toAttack.getStats()[2] ? 1 : 0;

            if (moveType == 0) {
                toAttack.takeDamage(atk, 0);
            } else if(moveType == 1) {
                toAttack.takeDamage(0, mag);
            }
        }

        public void printStats(int pos) {
            if (this.Equals(enemy.NONE)) {
                Console.WriteLine(
" _____ _   _  ________  ____   __ ______ _____  ___ ______ \n"+
"|  ___| \\ | ||  ___|  \\/  \\ \\ / / |  _  \\  ___|/ _ \\|  _  \\\n"+
"| |__ |  \\| || |__ | .  . |\\ V /  | | | | |__ / /_\\ \\ | | |\n"+
"|  __|| . ` ||  __|| |\\/| | \\ /   | | | |  __||  _  | | | |\n"+
"| |___| |\\  || |___| |  | | | |   | |/ /| |___| | | | |/ / \n"+
"\\____/\\_| \\_/\\____/\\_|  |_/ \\_/   |___/ \\____/\\_| |_/___/  \n");
                return;
            }
            string spaces = "";
            string hpLeft = "";
            string statsLine = "";
            int num;
            
            num = 14 - (atk > 99 ? 3 : (atk > 9 ? 2 : 1)) - (def > 99 ? 3 : (def > 9 ? 2 : 1)) - (mag > 99 ? 3 : (mag > 9 ? 2 : 1)) - (spr > 99 ? 3 : (spr > 9 ? 2 : 1));
            for (int i = 0; i < num; i++)
            {
                statsLine += " ";
            }

            int hpRem = (hp * 100/mhp);

            num = 41 - (mhp > 99 ? 3 : (mhp > 9 ? 2 : 1)) - (hp > 99 ? 3 : (hp > 9 ? 2 : 1));
            for (int i = 0; i < num; i++) {
                spaces += " ";
            }
            int j = 0;
            while (j < (hpRem * 0.34)) {
                j++;
                hpLeft += "=";
            }
            while (j < 34) {
                j++;
                hpLeft += " ";
            }
            
            switch (pos) {
                case 0:
                    Console.WriteLine(
                        " __   \n"+
                        "/  |  +------------------------------------------------+\n"+//50
                        " | |  | Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        " | |  |   HP: [" + hpLeft + "]     |\n"+
                        "_| |_ | HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "\\___/ +------------------------------------------------+\n"
                    );
                    break;
                case 1:
                    Console.WriteLine(
                        " _____  \n"+
                        "/___  \\ +------------------------------------------------+\n"+//50
                        "   / /  | Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        "  / /   |   HP: [" + hpLeft + "]     |\n"+
                        " / /___ | HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "\\_____/ +------------------------------------------------+\n"
                    );
                    break;
                case 2:
                    Console.WriteLine(
                        " _____  \n"+
                        "|____ | +------------------------------------------------+\n"+//50
                        "    / / | Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        "    \\ \\ |   HP: [" + hpLeft + "]     |\n"+
                        " ___/ / | HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "\\____/  +------------------------------------------------+\n"
                    );
                    break;
                case 3:
                    Console.WriteLine(
                        "   ___  \n"+
                        "  /   | +------------------------------------------------+\n"+//50
                        " / /| | | Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        "/ /_| | |   HP: [" + hpLeft + "]     |\n"+
                        "\\___  | | HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "    |_| +------------------------------------------------+\n"
                    );
                    break;
                case 4:
                    Console.WriteLine(
                        " _____  \n"+
                        "|  ___| +------------------------------------------------+\n"+//50
                        "|___ \\  | Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        "    \\ \\ |   HP: [" + hpLeft + "]     |\n"+
                        "/\\__/ / | HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "\\____/  +------------------------------------------------+\n"
                    );
                    break;
                default:
                    Console.WriteLine(
                        "\n"+
                        "+------------------------------------------------+\n"+//50
                        "| Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                        "|   HP: [" + hpLeft + "]     |\n"+
                        "| HP: "+ hp + "/" + mhp + spaces + " |\n"+
                        "+------------------------------------------------+\n"
                    );
                    break;
            }
            
            
            
        }

        public int takeDamage(int atk, int mag, character attacker) {
            if (this.Equals(enemy.NONE)) {
                return 0;
            }
            int dmg = 0;
            dmg += (int)Math.Round((float)atk * 1.5f / this.def);
            dmg += (int)Math.Round((float)mag * 1.5f / this.spr);
            hp -= dmg;
            Console.WriteLine("\nEnemy was dealt " + dmg + " damage!");
            if (hp <= 0) {
                Console.WriteLine("Defeated Enemy!");
                if (hp < -mhp/2){
                    exp *= 2;
                    // Console.WriteLine("☻☻OVERKILL BONUS☻☻");
                    Console.WriteLine("\n\n"+
                        " _____  _   _ ___________ _   _______ _      _     \n"+
                        "|  _  || | | |  ___| ___ \\ | / /_   _| |    | |    \n"+
                        "| | | || | | | |__ | |_/ / |/ /  | | | |    | |    \n"+
                        "| | | || | | |  __||    /|    \\  | | | |    | |    \n"+
                        "\\ \\_/ /\\ \\_/ / |___| |\\ \\| |\\  \\_| |_| |____| |____\n" +
                        " \\___/  \\___/\\____/\\_| \\_\\_| \\_/\\___/\\_____/\\_____/\n\n");
                }
                attacker.gainEXP(exp);
            } else {
                Console.WriteLine("Enemy is at " + hp + "/" + mhp + " health!");
            }
            return dmg;
        }

        public static enemy operator *(enemy e, int x) {
            return new enemy(e.level * x, e.hp * x, e.atk * x, e.def * x, e.mag * x, e.spr * x, e.exp * x);
        } 
    }
}