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
            if (this.Equals(enemy.NONE)) {
                return;
            }
            int moveType = mag * 1.5f / toAttack.getStats()[4] > atk * 1.5f / toAttack.getStats()[2] ? 1 : 0;

            if (moveType == 0) {
                toAttack.takeDamage(atk, 0);
            } else if(moveType == 1) {
                toAttack.takeDamage(0, mag);
            }
        }

        public void printStats() {
            if (this.Equals(enemy.NONE)) {
                Console.WriteLine(
                "+------------------------------------------------+\n"+//50
                "| Enemy is defeated                              |\n"+
                "|   HP: [                                  ]     |\n"+
                "| HP: 0/0                                        |\n"+
                "+------------------------------------------------+\n");
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
            //Console.WriteLine("\n---ENEMY STATS---\nHP: " + hp + "/" + mhp + ", Atk: " + atk + ", Def: " + def + ", Mag: " + mag + ", Spr: " + spr + "\n");
            Console.WriteLine(
                "+------------------------------------------------+\n"+//50
                "| Enemy | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + "  |\n"+
                "|   HP: [" + hpLeft + "]     |\n"+
                "| HP: "+ hp + "/" + mhp + spaces + " |\n"+
                "+------------------------------------------------+\n"
            );
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