using System;

namespace text_based_RPG
{
    class character {
        int pType, level, hp, mhp, atk, def, mag, spr;
        float exp;
        equipment[] equip = new equipment[6];

        public character(int _pType, int _level, float _exp, equipment head, equipment rHand, equipment lHand, equipment chest, equipment boots, equipment accessory) {
            pType = _pType;
            level = _level;
            exp = _exp;
            equip[0] = head;
            equip[1] = rHand;
            equip[2] = lHand;
            equip[3] = chest;
            equip[4] = boots;
            equip[5] = accessory;
            calcAndSetStats();
            hp = mhp;
        }

        public void doTurn(room curRoom) {
            attackPhase(curRoom.GetEnemies());
        }
        void attackPhase(enemy[] enemies) {
            int curEnemy = 0;
            string resp = "";
            
            attackStart:
                Console.WriteLine("Choose an enemy to target or type h to heal");
                resp = Console.ReadLine();
                if(!int.TryParse(resp, out curEnemy)) {
                    if (resp == "h" || resp == "H")
                    {
                        heal();
                        return;
                    }
                    else { goto attackStart; }
                }
                if (curEnemy - 1 >= enemies.Length) {
                    goto attackStart;
                }

            attackType:
                Console.WriteLine("Turn: \nType 1 to use Melee or type 2 to use Magic");
                resp = Console.ReadLine();
                if (resp.Equals("1")) {
                    attack(0, enemies[curEnemy - 1]);
                } else if (resp.Equals("2")) {
                    attack(1, enemies[curEnemy - 1]);
                }  else {
                    goto attackType;
                }
        }

        public void attack(int moveType, enemy toAttack) {
            if (moveType == 0) {
                toAttack.takeDamage(atk, 0, this);
            } else if(moveType == 1) {
                toAttack.takeDamage(0, mag, this);
            } else if(moveType == 2) {
               toAttack.takeDamage(atk, mag, this);
            }
        }

        public int takeDamage(int atk, int mag) {
            int dmg = 0;
            dmg += (int)Math.Round((float)atk * 1.5f / this.def);
            dmg += (int)Math.Round((float)mag * 1.5f / this.spr);
            hp -= dmg;
            Console.WriteLine("\nYou were dealt " + dmg + " damage!");
            if (hp <= 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You DIED...");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.WriteLine("You are at " + hp + "/" + mhp + " health");
            }
            return dmg;
        }

        public int[] getStats() {
            int[] ret = {hp, atk, def, mag, spr, level};
            return ret;
        }

        public void heal() {
            if (hp < mhp)
            {
                int helathNeeded = mhp - hp;
                int healAmount = (int)Math.Clamp(Math.Round((float)helathNeeded / 2), 1, mhp/8);
                hp += healAmount;
                Console.WriteLine("Healed " + healAmount + " health");
            } else {
                Console.WriteLine("Unable to heal");
            }
        }

        public void gainEXP(float expGained) {
            exp += expGained;
            Console.WriteLine("\nEXP Increased by " + Math.Round(expGained * 100) + "!");
            while (exp >= level + 1) {
                level += 1;
                this.exp -= level;
                Console.WriteLine("Leveled up to level " + (level + 1));
                calcAndSetStats();
                hp = mhp;
            }
            Console.WriteLine("Level: " + (level + 1) + " | EXP: " + Math.Round(exp * 100) + "/" + ((level + 1) * 100) + "\n");
        }

        public void equipItem(int slot, equipment toEquip) {
            equip[slot] = toEquip;
            calcAndSetStats();
        }

        public void printStats() {
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
            Console.WriteLine("\n---YOUR STATS---\nLevel: " + (level + 1) + " | EXP: " + Math.Round(exp * 100) + "/" + ((level + 1) * 100) + " HP: " + hp + "/" + mhp + ", Atk: " + atk + ", Def: " + def + ", Mag: " + mag + ", Spr: " + spr + "\n");
            Console.WriteLine(
                "+------------------------------------------------+\n"+//50
                "| Player | ATK: " + atk + " DEF: " + def + " MAG: " + mag + " SPR: " + spr + statsLine + " |\n"+
                "|   HP: [" + hpLeft + "]     |\n"+
                "| HP: "+ hp + "/" + mhp + spaces + " |\n"+
                "+------------------------------------------------+\n"
            );
        }

        void calcAndSetStats() {
            bool bonus = (pType == equip[0].getType() && pType == equip[1].getType() && pType == equip[2].getType() && pType == equip[3].getType() && pType == equip[4].getType() && pType == equip[5].getType());
            
            int mhp = 10 + (level * 3);
            int atk = equip[0].getAtk() + equip[1].getAtk() + equip[2].getAtk() + equip[3].getAtk() + equip[4].getAtk() + equip[5].getAtk() + (level * 3) + 1;
            int def = equip[0].getDef() + equip[1].getDef() + equip[2].getDef() + equip[3].getDef() + equip[4].getDef() + equip[5].getDef() + (level * 3) + 1;
            int mag = equip[0].getMag() + equip[1].getMag() + equip[2].getMag() + equip[3].getMag() + equip[4].getMag() + equip[5].getMag() + (level * 3) + 1;
            int spr = equip[0].getSpr() + equip[1].getSpr() + equip[2].getSpr() + equip[3].getSpr() + equip[4].getSpr() + equip[5].getSpr() + (level * 3) + 1;
            
            if (bonus)
            {
                switch (pType) {
                    case 0://fighter
                        atk += 5;
                        def += 1;
                        break;
                    case 1://physical tank
                        def += 5;
                        spr += 1;
                        break;
                    case 2://magician
                        mag += 5;
                        spr += 1;
                        break;
                    case 3://magical tank
                        spr += 5;
                        def += 1;
                        break;
                }
            }

            this.mhp = mhp;
            this.atk = atk;
            this.def = def;
            this.mag = mag;
            this.spr = spr;
        }
    }
}