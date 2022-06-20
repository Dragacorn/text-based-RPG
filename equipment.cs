namespace text_based_RPG
{
    struct equipment 
    {
        public static equipment EMPTY = new equipment(0, -1, 0, 0, 0, 0);


        int level, type, atk, def, mag, spr;

        public equipment(int _level, int _type, int _atk, int _def, int _mag, int _spr) {
            this.level = _level;
            this.type = _type;
            this.atk = _atk;
            this.def = _def;
            this.mag = _mag;
            this.spr = _spr;
        }

        public equipment upgradeEquip() {
            level += 1;
            if (atk != 0) atk += 2 * level;
            if (def != 0) def += 2 * level;
            if (mag != 0) mag += 2 * level;
            if (spr != 0) spr += 2 * level;
            return this;
        }

        public int getLevel() {
            return level;
        }
        public int getType() {
            return type;
        }
        public int getAtk() {
            return atk;
        }
        public int getDef() {
            return def;
        }
        public int getMag() {
            return mag;
        }
        public int getSpr() {
            return spr;
        }
    }
}