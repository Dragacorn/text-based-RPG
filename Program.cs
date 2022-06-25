using System;
//https://patorjk.com/software/taag/#p=display&f=Doom&t=OVERKILL
namespace text_based_RPG
{
    class Program
    {
        
        static void Main(string[] args)
        {
            setup();
        }

        static void setup() {
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

            equipment h = new equipment(0, type.ATK, 5, 0, 0, 0);
            equipment r = new equipment(0, type.ATK, 5, 0, 0, 0);
            equipment l = new equipment(0, type.ATK, 5, 0, 0, 0);
            equipment c = new equipment(0, type.ATK, 5, 0, 0, 0);
            equipment b = new equipment(0, type.ATK, 5, 0, 1, 0);
            equipment a = new equipment(2, type.ATK, 5, 0, 0, 0);

            character player = new character(type.ATK,0, 0, h, r, l, c, b, a);
            
            gameLoop(curRoom, player);
        }
        static void gameLoop(room curRoom, character player) {
            curRoom.loadRoom(player);
            player.doTurn(curRoom);
            enemy.doTurn(curRoom, player);


            if (player.getStats()[0] <= 0) {
                gameEnd();
            } else {
                Console.WriteLine("\n\nPress Enter to start next turn.");
                Console.ReadKey(true);
                Console.Clear();
                gameLoop(curRoom, player);
            }
        }
        static void gameEnd()
        {
            Console.WriteLine("\n\nPress Enter to Exit Terminal or type \"r\" to restart.");
            string exit = Console.ReadLine();
            switch (exit.ToUpper()) {
                case "R":
                    Console.Clear();
                    setup();
                    break;
                case "": 
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    gameEnd();
                    break;
            }
        }
    }
}