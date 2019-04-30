using System;
namespace rpg_from_java_by_Gase
{
    public class Hero
    {
        public int health = 20;
        public int streng = 2;
        public int pointions = 1;
        public int x = 4;
        public int y = 0;
        public int room = 0;
        public bool key = false;
    }
    public class Test
    {
        public bool walk_up(int x, int y, char[,] room)
        {
            if (x - 1 < 0)
                return false;
            if (room[x - 1, y] == '-' || room[x - 1, y] == '|' || room[x - 1, y] == 'С' || room[x - 1, y] == '+')
                return false;
            return true;
        }
        public bool walk_down(int x, int y, char[,] room)
        {
            if (x + 1 > Math.Sqrt(room.Length) || x == Math.Sqrt(room.Length))
                return false;
            if (room[x + 1, y] == '-' || room[x + 1, y] == '|' || room[x + 1, y] == 'С' || room[x + 1, y] == '+')
                return false;
            return true;
        }
        public bool walk_right(int x, int y, char[,] room)
        {
            if (y + 1 > Math.Sqrt(room.Length) || y == Math.Sqrt(room.Length))
                return false;
            if (room[x, y + 1] == '-' || room[x, y + 1] == '|' || room[x, y + 1] == 'С' || room[x, y + 1] == '+')
                return false;
            return true;
        }
        public bool walk_left(int x, int y, char[,] room)
        {
            if (y - 1 < 0)
                return false;
            if (room[x, y - 1] == '-' || room[x, y - 1] == '|' || room[x, y - 1] == 'С' || room[x, y - 1] == '+')
                return false;
            return true;
        }
        public bool event_up(int x, int y, char[,] room)
        {
            if (room[x - 1, y] != ' ' || room[x - 1, y] != '-' || room[x - 1, y] != '|')
                return true;
            return false;
        }
        public bool event_down(int x, int y, char[,] room)
        {
            if (room[x + 1, y] != ' ' || room[x + 1, y] != '-' || room[x + 1, y] != '|')
                return true;
            return false;
        }
        public bool event_right(int x, int y, char[,] room)
        {
            if (room[x, y + 1] != ' ' || room[x, y + 1] != '-' || room[x, y + 1] != '|')
                return true;
            return false;
        }
        public bool event_left(int x, int y, char[,] room)
        {
            if (y != 0)
                if (room[x, y - 1] != ' ' || room[x, y - 1] != '-' || room[x, y - 1] != '|')
                    return true;
            return false;
        }
    }
    interface IMonstr
    {
        string name { get; set; }
        int HP_monster { get; set; }
        int damage_monster { get; set; }
        bool hit(int i);
    }
    public class Boss
    {
        public int HP_monster = 50;
        public int damage_monster = 5;
        public int take_dmg = 10;
        public bool can_boss_hit = true;
    }
    public class Skeleton
    {
        public string name = "Скелет";
        public int HP_monster = 10;
        public int damage_monster = 2;
    }
    class Fight
    {
        public int i = 2;
        public Skeleton draw_fight_normal(ref Hero obj, int hero_act, Skeleton monstr, ref string state_monstr)
        {
            if (i % 2 != 0)
                state_monstr = "ударит";
            else
                state_monstr = "не ударит";
            switch (hero_act)
            {
                case 1:
                    monstr.HP_monster -= obj.streng * 2;
                    if (i % 2 == 0)
                        obj.health -= monstr.damage_monster;
                    break;
                case 2: break;
                case 3: if (obj.pointions != 0) { obj.health += 5; obj.pointions--; }; if (i % 2 == 0) obj.health -= monstr.damage_monster; break;
            }
            i++;
            return monstr;
        }
        public Boss draw_fight_boss(ref Hero obj, int hero_act, Boss monstr, bool can_boss_hit)
        {
            switch (hero_act)
            {
                case 1:
                    monstr.HP_monster -= obj.streng * 2;
                    if (can_boss_hit)
                        obj.health -= monstr.damage_monster;
                    break;
                case 2:
                    if (can_boss_hit)
                        obj.health -= monstr.damage_monster / 4;
                    break;
                case 3:
                    if (obj.pointions != 0)
                    {
                        obj.health += 5;

                        obj.pointions--;
                    }
                    if (can_boss_hit)
                        obj.health -= monstr.damage_monster;
                    break;
            }
            return monstr;
        }
    }
    public class Rooms
    {
        public char[,] room1 { get; } = new char[,]{
            {'+','-','-','-','-','-','-','-','В','+'},
            {'|','З','|',' ','М',' ','|',' ','Б','|'},
            {'|',' ','|',' ','|',' ','|',' ','-','|'},
            {'|',' ','|',' ','|',' ','|',' ',' ','|'},
            {'*',' ','|',' ','|','З','|','З',' ','|'},
            {'|',' ','|',' ','|',' ','|','-',' ','|'},
            {'|',' ','|',' ','|',' ','|',' ',' ','|'},
            {'|',' ','|',' ','|',' ','|',' ',' ','|'},
            {'|',' ',' ','М','|',' ','М','М',' ','|'},
            {'+','-','-','-','-','-','-','-','-','+'},
        };
        public char[,] secret_room { get; } = new char[,]{
            {'+','-','-','-','-','-','-','-','-','+'},
            {'|',' ',' ',' ',' ',' ',' ',' ',' ','|'},
            {'|',' ',' ',' ',' ',' ',' ',' ',' ','|'},
            {'|','-',' ',' ',' ',' ',' ',' ',' ','|'},
            {'Л','*',' ',' ',' ',' ',' ','с',' ','|'},
            {'|','-',' ',' ',' ',' ',' ',' ',' ','|'},
            {'|',' ',' ',' ',' ',' ',' ',' ',' ','|'},
            {'|',' ',' ',' ',' ',' ',' ',' ',' ','|'},
            {'|',' ',' ',' ',' ',' ',' ',' ',' ','|'},
            {'+','-','-','-','-','-','-','-','-','+'},
        };
    }
}
