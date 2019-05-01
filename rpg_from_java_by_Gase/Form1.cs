using rpg_from_java_by_Gase;
using rpg_from_java_by_Gase.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace winform_clear_project
{
    public partial class Form1 : Form
    {
        #region objects
        List<Image> listimage_FloorWall = new List<Image>();//Колекция картинок с стенами и полом
        List<Image> listimage_main = new List<Image>();//Колекция картинок с основними елементами
        public char[,] room = new char[10, 10];
        public char[,] buffer_room = new char[10, 10];
        public char[,] big_room = new char[15, 15];
        Hero obj_hero;
        Test tester = new Test();
        Fight obj_fight = new Fight();
        Skeleton goblin;
        Boss boss;
        Rooms rooms;
        Label[] blocks = new Label[100];
        LinkLabel randButtom;
        int type_monstr = 0;
        bool in_fight = false;
        bool play = false;
        bool volume = true;
        bool IsBigRoom = false;
        int buf_x = 0, buf_y = 0;//разрушение стены
        int count_play = 0;
        int change_mode = 0;
        #endregion objects
        #region form events
        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            richTextBox1.SelectedText = "The hunt for a ARCHITECTURAL    on the dungeon";
            button6.BringToFront();

            listimage_FloorWall.Add(Resources.wall_crash_1);//0
            listimage_FloorWall.Add(Resources.wall_crash_2);//1
            listimage_FloorWall.Add(Resources.wall_crash_3);//2
            listimage_FloorWall.Add(Resources.wall_crash_4);//3
            listimage_FloorWall.Add(Resources.wall_crash_5);//4
            listimage_FloorWall.Add(Resources.floor);//5
            listimage_main.Add(Resources.hero_sprite);//0
            listimage_main.Add(Resources.hero_sprite_with_sword);//1
            listimage_main.Add(Resources.wall_sprite);//2
            listimage_main.Add(Resources.skeleton);//3
            listimage_main.Add(Resources.boss);//4
            listimage_main.Add(Resources.potion);//5
            listimage_main.Add(Resources.door);//6
            listimage_main.Add(Resources.coin);//7
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            randButtom = new LinkLabel() { Location = new Point(rand.Next(0, tabPage3.Width - 30), rand.Next(110, tabPage3.Height - 30)), Image = Resources.door_clear, Height = 30, Width = 30, BackColor = Color.Transparent };
            randButtom.Click += ButtonOnClick;
            tabPage3.Controls.Add(randButtom);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            char[,] use_room;
            if (!IsBigRoom)
            {
                use_room = room;
            }
            else
            {
                use_room = big_room;
            }
            if (play)
            {
                toolStripStatusLabel1.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
                if (!in_fight)
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            if (tester.walk_up(obj_hero.x, obj_hero.y, use_room))
                            {
                                if (tester.event_up(obj_hero.x, obj_hero.y, use_room))
                                {
                                    switch (use_room[obj_hero.x - 1, obj_hero.y])
                                    {
                                        case 'М':
                                            start_fight_skeleton();
                                            break;
                                        case 'З':
                                            obj_hero.pointions += 1;
                                            break;
                                        case 'Б':
                                            start_fighr_boss();
                                            break;
                                        case 'Л':
                                            go_another_room(ref use_room);
                                            goto draw_metka;
                                        case 'с':
                                            open_chest(ref use_room);
                                            goto draw_metka;
                                        case 'В':
                                            check_win();
                                            return;
                                        case 'К':
                                            obj_hero.coin += 1;
                                            break;
                                    }
                                }
                                obj_hero.x -= 1;
                            }
                            break;
                        case Keys.A:
                            if (tester.walk_left(obj_hero.x, obj_hero.y, use_room))
                            {
                                if (tester.event_left(obj_hero.x, obj_hero.y, use_room))
                                {
                                    switch (use_room[obj_hero.x, obj_hero.y - 1])
                                    {
                                        case 'М':
                                            start_fight_skeleton();
                                            break;
                                        case 'З':
                                            obj_hero.pointions += 1;
                                            break;
                                        case 'Б':
                                            start_fighr_boss();
                                            break;
                                        case 'Л':
                                            go_another_room(ref use_room);
                                            goto draw_metka;
                                        case 'с':
                                            open_chest(ref use_room);
                                            goto draw_metka;
                                        case 'В':
                                            check_win();
                                            return;
                                        case 'К':
                                            obj_hero.coin += 1;
                                            break;
                                    }
                                }
                                obj_hero.y -= 1;
                            }
                            break;
                        case Keys.S:

                            if (tester.walk_down(obj_hero.x, obj_hero.y, use_room))
                            {
                                if (tester.event_down(obj_hero.x, obj_hero.y, use_room))
                                {
                                    switch (use_room[obj_hero.x + 1, obj_hero.y])
                                    {
                                        case 'М':
                                            start_fight_skeleton();
                                            break;
                                        case 'З':
                                            obj_hero.pointions += 1;
                                            break;
                                        case 'Б':
                                            start_fighr_boss();
                                            break;
                                        case 'Л':
                                            go_another_room(ref use_room);
                                            goto draw_metka;
                                        case 'с':
                                            open_chest(ref use_room);
                                            goto draw_metka;
                                        case 'В':
                                            check_win();
                                            return;
                                        case 'К':
                                            obj_hero.coin += 1;
                                            break;
                                    }
                                }
                                obj_hero.x += 1;
                            }
                            break;
                        case Keys.D:
                            if (tester.walk_right(obj_hero.x, obj_hero.y, use_room))
                            {
                                if (tester.event_right(obj_hero.x, obj_hero.y, use_room))
                                {
                                    switch (use_room[obj_hero.x, obj_hero.y + 1])
                                    {
                                        case 'М':
                                            start_fight_skeleton();
                                            break;
                                        case 'З':
                                            obj_hero.pointions += 1; break;
                                        case 'Б':
                                            start_fighr_boss();
                                            break;
                                        case 'Л':
                                            go_another_room(ref use_room); goto draw_metka;
                                        case 'с':
                                            open_chest(ref use_room);
                                            goto draw_metka;
                                        case 'В':
                                            check_win();
                                            return;
                                        case 'К':
                                            obj_hero.coin += 1;
                                            break;
                                    }
                                }
                                obj_hero.y += 1;
                            }
                            break;
                    }
                draw_metka:
                toolStripStatusLabel1.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
                draw_room(ref blocks, obj_hero, use_room);
                if (!IsBigRoom)
                    room = use_room;
                else
                    big_room = use_room;
            }
        }
        #endregion form events
        #region my methods
        private void check_win()
        {
            if (!IsBigRoom)
            {
                win();
            }
            else
            {
                if (obj_hero.coin == 10)
                {
                    win();
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=x8LRYaSukjU");
                    //System.Diagnostics.Process.Start("notepad.exe");
                }
            }
        }

        private void win()
        {
            player_play(volume, Resources.win_sound);
            listimage_main[0] = Resources.hero_sprite;
            timer3.Enabled = false;
            MessageBox.Show("Победа!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage3);
            FormBorderStyle = FormBorderStyle.None;
            Width = 322;
            Height = 225;
            Set_form_in_center();
            in_fight = false;
            play = false;
            room = null;
            timer1.Enabled = true;
            timer2.Enabled = true;
            toolStripProgressBar1.Value = 100;
        }

        private void Set_default_blocks(ref Label[] blocks)
        {
                for (int i = 0; i < blocks.Length; i++)
                {
                    tabPage1.Controls.Remove(blocks[i]);
                }
        }
        private void first_draw(ref Label[] blocks, char[,] room)
        {
            MessageBox.Show("first draw");
            toolStripStatusLabel1.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
            int x = 10, y = 10, k = 0;
            for (int i = 0; i < Math.Sqrt(room.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(room.Length); j++)
                {
                    if (room[i, j] == '+' || room[i, j] == '|' || room[i, j] == '-')
                    {
                        blocks[k] = new Label() { Image = listimage_main[2], Location = new Point(x, y), BackColor = Color.Gray, ForeColor = Color.White };
                        blocks[k].Click += wall_crack;
                        blocks[k].Name = i.ToString() + j.ToString();
                        blocks[k].Tag = 0;
                    }
                    else if (room[i, j] == '*')
                        blocks[k] = new Label() { Image = listimage_main[0], Location = new Point(x, y) };
                    else if (room[i, j] == ' ')
                        blocks[k] = new Label() { Image = listimage_FloorWall[5], Location = new Point(x, y) };
                    else if (room[i, j] == 'З')
                        blocks[k] = new Label() { Image = listimage_main[5], Location = new Point(x, y) };
                    else if (room[i, j] == 'М')
                        blocks[k] = new Label() { Image = listimage_main[3], Location = new Point(x, y) };
                    else if (room[i, j] == 'Б')
                        blocks[k] = new Label() { Image = listimage_main[4], Location = new Point(x, y) };
                    else if (room[i, j] == 'В')
                        blocks[k] = new Label() { Image = listimage_main[6], Location = new Point(x, y) };
                    else if (room[i, j] == 'Л')
                        blocks[k] = new Label() { Image = Resources.downstair, Location = new Point(x, y) };
                    else if (room[i, j] == 'с')
                        blocks[k] = new Label() { Image = Resources.chest_close, Location = new Point(x, y) };
                    else if (room[i, j] == 'С')
                        blocks[k] = new Label() { Image = Resources.chest_open, Location = new Point(x, y) };
                    else if (room[i, j] == 'К')
                        blocks[k] = new Label() { Image = listimage_main[7], Location = new Point(x, y) };
                    else
                        blocks[k] = new Label() { Text = "~", Location = new Point(x, y) };

                    tabPage1.Controls.Add(blocks[k]);
                    blocks[k].Width = 30;
                    blocks[k].Height = 30;
                    blocks[k++].ImageAlign = ContentAlignment.MiddleCenter;
                    //x += tabPage1.Size.Width / 10;
                    x += 30;
                }
                y += 30;
                //y += tabPage1.Size.Height / 10;
                x = 10;
            }
        }
        private void fight_normal(int act)
        {
            string state = "";
            goblin = obj_fight.draw_fight_normal(ref obj_hero, act, goblin, ref state);
            if (goblin.HP_monster <= 0 || obj_hero.health <= 0)
            {
                if (goblin.HP_monster <= 0)
                {
                    end_fight();
                }
                else if (obj_hero.health <= 0)
                {
                    game_end();
                }
            }
            else
            {
                linkLabel3.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
                linkLabel2.Location = new Point((tabPage1.Size.Width / 2) - linkLabel2.Size.Width / 2, linkLabel2.Location.Y);
                linkLabel3.Location = new Point((tabPage1.Size.Width / 2) - linkLabel3.Size.Width / 2, linkLabel3.Location.Y);
                linkLabel5.Text = "HP монстра:" + goblin.HP_monster + ",урон:" + goblin.damage_monster + "," + state;
            }
        }

        private string state_boss_hit()
        {
            Random rand = new Random();
            int change_boss_hit = rand.Next(1, 11);
            if (change_boss_hit >= 4)
            {
                boss.can_boss_hit = true;
                return "ударит";
            }
            else
            {
                boss.can_boss_hit = false;
                return "не ударит";
            }
        }
        private void fight_boss(int act, bool can_boss_hit)
        {
            boss = obj_fight.draw_fight_boss(ref obj_hero, act, boss, can_boss_hit);
            if (boss.HP_monster <= 0 || obj_hero.health <= 0)
            {
                if (boss.HP_monster <= 0)
                {
                    end_fight();
                }
                else if (obj_hero.health <= 0)
                {
                    game_end();
                }
            }
            else
            {
                linkLabel3.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
                linkLabel2.Location = new Point((tabPage1.Size.Width / 2) - linkLabel2.Size.Width / 2, linkLabel2.Location.Y);
                linkLabel3.Location = new Point((tabPage1.Size.Width / 2) - linkLabel3.Size.Width / 2, linkLabel3.Location.Y);
            }
        }
        private void end_fight()
        {
            Random rand = new Random();
            int chance = rand.Next(0, 11);
            if (chance <= 3 && !obj_hero.key && boss.HP_monster >= 0)
            {
                obj_hero.key = true;
                MessageBox.Show("В трупе монстра вы находите: \"Ключ\"");
                toolStripStatusLabel2.Image = Resources.key;
            }
            if (!obj_hero.key && boss.HP_monster <= 0)
            {
                obj_hero.key = true;
                MessageBox.Show("В трупе АРХИТЕКТУРЩИКА вы находите: \"Ключ\"");
                toolStripStatusLabel2.Image = Resources.key;
            }
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Add(tabPage1);
            obj_hero.streng += 1;
            in_fight = false;
            toolStripStatusLabel1.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
            player_play(volume, Resources.background_music);
        }
        private void game_end()
        {
            player_play(volume, Resources.game_over);
            listimage_main[0] = Resources.hero_sprite;
            MessageBox.Show("GAME OVER!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (!IsBigRoom)
                tabControl1.TabPages.Remove(tabPage2);
            else
                tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage3);
            FormBorderStyle = FormBorderStyle.None;
            Width = 322;
            Height = 225;
            Set_form_in_center();
            toolStripStatusLabel2.Image = null;
            in_fight = false;
            play = false;
            room = null;
            big_room = null;
            timer1.Enabled = true;
            timer2.Enabled = true;
        }
        private void draw_room(ref Label[] blocks, Hero obj, char[,] room)
        {
            //if(!IsBigRoom)
            //MessageBox.Show(room.Length.ToString() + ":"+ blocks.Length.ToString());
            int k = 0;
            room[obj.x, obj.y] = '*';
            if (obj.x + 1 < Math.Sqrt(room.Length))
                if (room[obj.x + 1, obj.y] == '*' && obj.x < Math.Sqrt(room.Length))
                    room[obj.x + 1, obj.y] = ' ';
            if (obj.x > 0)
                if (room[obj.x - 1, obj.y] == '*' && obj.x > 0)
                    room[obj.x - 1, obj.y] = ' ';
            if (obj.y + 1 < Math.Sqrt(room.Length))
                if (room[obj.x, obj.y + 1] == '*' && obj.y < Math.Sqrt(room.Length))
                    room[obj.x, obj.y + 1] = ' ';
            if (obj.y > 0)
                if (room[obj.x, obj.y - 1] == '*' && obj.y > 0)
                    room[obj.x, obj.y - 1] = ' ';
            for (int i = 0; i < Math.Sqrt(room.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(room.Length); j++)
                {
                    if (room[i, j] == '+' || room[i, j] == '|' || room[i, j] == '-')
                    {
                        if (obj_hero.room == 0 && blocks[k].Tag != null)
                            switch ((int)(blocks[k].Tag))
                            {
                                case 0: blocks[k].Image = listimage_main[2]; break;
                                case 1: blocks[k].Image = listimage_FloorWall[0]; break;
                                case 2: blocks[k].Image = listimage_FloorWall[1]; break;
                                case 3: blocks[k].Image = listimage_FloorWall[2]; break;
                                case 4: blocks[k].Image = listimage_FloorWall[3]; break;
                                case 5: blocks[k].Image = listimage_FloorWall[4]; break;
                            }
                        else blocks[k].Image = listimage_main[2];


                    }
                    else if (room[i, j] == '*')
                        blocks[k].Image = listimage_main[0];
                    else if (room[i, j] == ' ')
                    {
                        blocks[k].Image = listimage_FloorWall[5];
                    }
                    else if (room[i, j] == 'З')
                        blocks[k].Image = listimage_main[5];
                    else if (room[i, j] == 'М')
                        blocks[k].Image = listimage_main[3];
                    else if (room[i, j] == 'Б')
                        blocks[k].Image = listimage_main[4];
                    else if (room[i, j] == 'В')
                        blocks[k].Image = listimage_main[6];
                    else if (room[i, j] == 'Л')
                        blocks[k].Image = Resources.downstair;
                    else if (room[i, j] == 'с')
                        blocks[k].Image = Resources.chest_close;
                    else if (room[i, j] == 'С')
                        blocks[k].Image = Resources.chest_open;
                    else if (room[i, j] == 'К')
                        blocks[k].Image = listimage_main[7];
                    k++;
                }
            }
        }

        private void wall_crack(object sender, EventArgs e)
        {
            Label clicking_label = sender as Label;
            int x = int.Parse(clicking_label.Name) / 10;
            int y = int.Parse(clicking_label.Name) % 10;
            if (obj_hero.room == 0)
                if ((x != 0 && y != 0) && (x != 9 && y >= 0) && (y != 9 && x >= 0))
                {
                    clicking_label.Tag = (int)clicking_label.Tag + 1;
                    switch ((int)(clicking_label.Tag))
                    {
                        case 1: clicking_label.Image = listimage_FloorWall[0]; break;
                        case 2: clicking_label.Image = listimage_FloorWall[1]; break;
                        case 3: clicking_label.Image = listimage_FloorWall[2]; break;
                        case 4: clicking_label.Image = listimage_FloorWall[3]; break;
                        case 5: clicking_label.Image = listimage_FloorWall[4]; break;
                        default:
                            if (x == 1 && y == 2)
                            {
                                room[x, y] = 'Л';
                                clicking_label.Image = Resources.downstair;
                                clicking_label.Click -= wall_crack;
                            }
                            else
                            {
                                room[x, y] = ' ';
                                clicking_label.Image = listimage_FloorWall[5];
                                clicking_label.Click -= wall_crack;
                            }
                            break;
                    }
                    //draw_room(ref blocks, obj_hero, room);
                }
        }



        private void open_chest(ref char[,] room)
        {
            if (obj_hero.key)
            {
                MessageBox.Show("Вы заполучили МЕЧ БЕСКОНЕЧНОЕСТИ(+10 к силе)");
                obj_hero.key = false;
                obj_hero.streng += 10;
                for (int i = 0; i < Math.Sqrt(room.Length); i++)
                    for (int j = 0; j < Math.Sqrt(room.Length); j++)
                        if (room[i, j] == 'с')
                        {
                            room[i, j] = 'С';
                            goto end;
                        }
                    end:
                listimage_main[0] = Resources.hero_sprite_with_sword;
                toolStripStatusLabel2.Image = null;
            }
        }

        private void go_another_room(ref char[,] room)
        {
            char[,] buffer_buffer_room;
            if (obj_hero.room == 0)
            {
                buffer_buffer_room = room;
                room = buffer_room;
                buffer_room = buffer_buffer_room;
                buf_x = obj_hero.x;
                buf_y = obj_hero.y;
                obj_hero.x = 4;
                obj_hero.y = 1;
                obj_hero.room = 1;
            }
            else
            {
                obj_hero.room = 0;
                buffer_buffer_room = room;
                room = buffer_room;
                buffer_room = buffer_buffer_room;
                obj_hero.x = buf_x;
                obj_hero.y = buf_y;
            }
            draw_room(ref blocks, obj_hero, room);
        }

        private void start_fighr_boss()
        {
            linkLabel4.Text = "АРХИТЕКТУРЩИК";
            linkLabel4.Location = new Point((tabPage1.Size.Width / 2) - linkLabel4.Size.Width / 2, linkLabel4.Location.Y); ;
            linkLabel3.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
            linkLabel3.Location = new Point((tabPage1.Size.Width / 2) - linkLabel3.Size.Width / 2, linkLabel3.Location.Y);
            linkLabel5.Text = "HP монстра:" + boss.HP_monster + ",урон:" + boss.damage_monster + " " + state_boss_hit(); ;
            linkLabel5.Location = new Point((tabPage1.Size.Width / 2) - linkLabel5.Size.Width / 2, linkLabel5.Location.Y);
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            type_monstr = 2;
            label6.Image = Resources.boss;
            label6.Location = new Point((tabPage1.Size.Width / 2) - label6.Size.Width / 2, label6.Location.Y);
            player_play(volume, Resources.music_fight);
        }

        private void start_fight_skeleton()
        {
            goblin.HP_monster = 10;
            linkLabel4.Text = "Скелет";
            linkLabel3.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
            linkLabel5.Text = "HP монстра:" + goblin.HP_monster + ",урон:" + goblin.damage_monster + ",ударит";
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Add(tabPage2);
            in_fight = true;
            type_monstr = 1;
            player_play(volume, Resources.music_fight);
        }
        private void player_play(bool volume, Stream sound)
        {
            if (volume)
            {
                SoundPlayer player = new SoundPlayer(sound);
                player.Load();
                if (player.IsLoadCompleted)
                    player.Play();
            }
        }
        #endregion my methods
        #region button and timer events
        private void ButtonOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Молодец и че даьше");
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=S-SaRKZ4vUE");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (type_monstr == 1)
                fight_normal(1);
            else if (type_monstr == 2)
            {
                fight_boss(1, boss.can_boss_hit);
                linkLabel5.Text = "HP монстра:" + boss.HP_monster + ",урон:" + boss.damage_monster + ", " + state_boss_hit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (type_monstr == 1)
                fight_normal(3);
            else if (type_monstr == 2)
            {
                fight_boss(3, boss.can_boss_hit);
                linkLabel5.Text = "HP монстра:" + boss.HP_monster + ",урон:" + boss.damage_monster + ", " + state_boss_hit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (type_monstr == 1)
                fight_normal(2);
            else if (type_monstr == 2)
            {
                fight_boss(2, boss.can_boss_hit);
                linkLabel5.Text = "HP монстра:" + boss.HP_monster + ",урон:" + boss.damage_monster + ", " + state_boss_hit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!IsBigRoom)
            {
                Width = 325;
                Height = 360;
            }
            else
            {
                Width = 480;
                Height = 510;
            }
            if (!IsBigRoom)
            {
                if (progressBar1.Maximum > progressBar1.Value)
                    obj_hero = new Hero();
                else if (progressBar1.Maximum == progressBar1.Value)
                {
                    obj_hero = new Hero() { health = 100, streng = 20 };
                }
            }
            else
            {
                timer3.Enabled = true;
                obj_hero = new Hero() { x = 14, y = 1 };
            }
            boss = new Boss();
            goblin = new Skeleton();
            rooms = new Rooms();
            room = rooms.room1;
            buffer_room = rooms.secret_room;
            big_room = rooms.big_room;
            if (count_play++ == 0)
            {
                if (!IsBigRoom)
                {
                    //Set_default_blocks(ref blocks);
                    first_draw(ref blocks, room);
                }
                else
                    first_draw(ref blocks, big_room);
                play = true;
            }
            else
            {
                if(change_mode>0)
                {
                    if (!IsBigRoom)
                    {
                        //Set_default_blocks(ref blocks);
                        first_draw(ref blocks, room);
                    }
                    else
                        first_draw(ref blocks, big_room);
                    play = true;
                }
                foreach (var item in blocks)
                {
                    item.Tag = 0;
                    if (item.Controls.Count == 0 && item.Name != "")
                        item.Click += wall_crack;
                }
                if (!IsBigRoom)
                    draw_room(ref blocks, obj_hero, room);
                else
                    draw_room(ref blocks, obj_hero, big_room);
                toolStripStatusLabel1.Text = "HP:" + obj_hero.health + ",урон:" + obj_hero.streng * 2 + ",зелий:" + obj_hero.pointions;
                play = true;
            }
            timer1.Enabled = false;
            timer2.Enabled = false;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Set_form_in_center();
            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Add(tabPage1);
            toolStripStatusLabel2.Image = null;
            player_play(volume, Resources.background_music);
        }

        private void Set_form_in_center()
        {
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            Left = (resolution.Width / 2) - (Size.Width / 2);
            Top = (resolution.Height / 2) - (Size.Height / 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            SoundPlayer beep = new SoundPlayer(Resources.beep);
            beep.Play();
            if (progressBar1.Maximum > progressBar1.Value)
                progressBar1.Value += 1;
            else
            {
                beep.Stream = Resources.menu;
                beep.Play();
                MessageBox.Show("Сила атаки увеличено до 40, здоровье увеличено до 100");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            randButtom.Location = new Point(rand.Next(0, tabPage3.Width - 30), rand.Next(110, tabPage3.Height - 30));
            switch (rand.Next(1, 7))
            {
                case 1: randButtom.Image = Resources.door_clear; break;
                case 2: randButtom.Image = Resources.hero_clear; break;
                case 3: randButtom.Image = Resources.skeleton_clear; break;
                case 4: randButtom.Image = Resources.wall_sprite; break;
                case 5: randButtom.Image = Resources.potion_clear; break;
                case 6: randButtom.Image = Resources.boss_clear; break;
            }
        }

        private void Panel3_Click(object sender, EventArgs e)
        {
            if (volume)
            {
                panel3.BackgroundImage = Resources.volume_off;
                volume = false;
            }
            else
            {
                panel3.BackgroundImage = Resources.volume_on;
                volume = true;
            }
        }

        private void Button4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (MessageBox.Show("Активировать челендж режем", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Set_default_blocks(ref blocks);
                    Array.Resize(ref blocks, 225);
                    IsBigRoom = true;
                    MessageBox.Show("Мужик)");
                    tabPage3.BackColor = richTextBox1.BackColor = Color.Red;
                    linkLabel1.Text = "ЧЕЛЕНДЖ МОД АТИВИРОВАН!!!!!!!";
                    change_mode += 1;
                }
                else
                {
                    Set_default_blocks(ref blocks);
                    Array.Resize(ref blocks, 100);
                    IsBigRoom = false;
                    MessageBox.Show("Слабак(");
                    tabPage3.BackColor = richTextBox1.BackColor = Color.CornflowerBlue;
                    linkLabel1.Text = "Версия 0.0.0.4 Добавлен челендж режим.";
                    change_mode += 1;
                }

            }
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (toolStripProgressBar1.Value != 0)
                toolStripProgressBar1.Value -= 10;
            else
            {
                timer3.Enabled = false;
                game_end();
                toolStripProgressBar1.Value = 100;
            }
        }


        //string text = "Версия 0.0.0.2. Добавлено разрушение блоков.";
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (linkLabel1.Left < this.Width)
            {
                linkLabel1.Left += 2;
            }
            else
            {
                linkLabel1.Left = -linkLabel1.Width;
            }
            //text = text.Substring(1) + text[0];
            //textBox1.Text = text;
        }
        #endregion button and timer events
    }
}
