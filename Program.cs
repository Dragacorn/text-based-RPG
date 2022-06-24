using System;

namespace text_based_RPG
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool play = true;
            Random rand = new Random();
            

            room[,] level = {
                {new room(new int[]{0,1,1,0}, new enemy[] {enemy.random(), enemy.random(), enemy.random()}), room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY},
                {room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY},
                {room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY},
                {room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY},
                {room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY, room.EMPTY}
            };
            
            room curRoom = level[0,0];

            enemy[] enemies = curRoom.GetEnemies();

            equipment h = new equipment(0, type.ATK, 1, 2, 0, 0);
            equipment r = new equipment(0, type.ATK, 3, 0, 0, 0);
            equipment l = new equipment(0, type.ATK, 1, 3, 0, 1);
            equipment c = new equipment(0, type.ATK, 1, 2, 0, 2);
            equipment b = new equipment(0, type.ATK, 1, 1, 1, 1);
            equipment a = new equipment(2, type.ATK, 2, 2, 0, 0);

            character player = new character(type.ATK,0, 0, h, r, l, c, b, a);
            
            while (play) {

                curRoom.loadRoom(player);
                player.doTurn(curRoom);
                enemy.doTurn(curRoom, player);


                if (player.getStats()[0] <= 0) {
                    play = false;
                    goto end;
                }
    
                Console.WriteLine("\n\nPress Enter to start next turn.");
                Console.ReadKey(true);
                Console.Clear();
            }
            end:
            Console.WriteLine("\n\nPress Enter to Exit Terminal.");
            Console.ReadKey(true);
        }
    }
}