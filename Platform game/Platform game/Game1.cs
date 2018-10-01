using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platform_game
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Random random = new Random();
        TimeSpan enemySpawn = new TimeSpan();
        TimeSpan FireTime = new TimeSpan();
        TimeSpan launchedTime = new TimeSpan();
        TimeSpan bossfire = new TimeSpan();
        List<Fireball> fireball = new List<Fireball>();
        List<Snakeman> enemy = new List<Snakeman>();
        List<Platform> platform = new List<Platform>();
        List<Sprite> fire = new List<Sprite>();
        List<FlyingEnemy> plane = new List<FlyingEnemy>();
        Boss boss;
        background Background;
        background Failure;
        background Begining;
        Color bosscolor = Color.White;
        bool Jumping = false;
        int MoveDown = 30;
        int Moveup = -30;
        bool ground = false;
        bool FacingLeft = true;
        bool FacingRight = false;
        KeyboardState prevks;
        bool Alive = true;
        bool OnPlatform = false;
        int health = 100;
        bool rage = false;
        bool Gamestart = false;
        int score = 0;  
        bool fly = false;
        bool BossFight = false;
        bool BossMove = true;
        bool playermoving = false;
        //bool platformMove = true;
        bool platformspawn = false;
        int enemySpawnSpeed = 2000;
        bool PlaneDown = false;
        bool bossmovechange = false;
        bool launched = false;
        bool bossdirectionchange = false;
        SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;

            //graphics.ApplyChanges();
        }


        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            font = (Content.Load<SpriteFont>("font"));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Begining = new background(Content.Load<Texture2D>("Start"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Background = new background(Content.Load<Texture2D>("background"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Failure = new background(Content.Load<Texture2D>("Failure"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            //platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(GraphicsDevice.Viewport.Width - 440, GraphicsDevice.Viewport.Height - 250), Color.White, 1));
            player = new Player(Content.Load<Texture2D>("playerL"), new Vector2(GraphicsDevice.Viewport.Width - 440, GraphicsDevice.Viewport.Height - 350), Color.White);
            boss = new Boss(Content.Load<Texture2D>("BossL"), new Vector2(0, 100000), Color.White, true, false, 20, -25,100);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            
            enemySpawn += gameTime.ElapsedGameTime;
            FireTime += gameTime.ElapsedGameTime;
            if (launched)
            {
                launchedTime += gameTime.ElapsedGameTime;
            }
            if (BossFight&&!playermoving)
            {
                bossfire += gameTime.ElapsedGameTime;
            }
            if (playermoving == true&&bossfire!=TimeSpan.Zero)
            {
                bossfire = TimeSpan.Zero;
            }
            
            for (int i = 0; i < enemy.Count;i++)
            {
                enemy[i].fireTime += gameTime.ElapsedGameTime;
            }
            for (int i = 0; i < plane.Count; i++)
            {
                plane[i].fireTime += gameTime.ElapsedGameTime;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            IsMouseVisible = true;
            
            if (ks.IsKeyDown(Keys.RightAlt))
            {
                enemy.Add(new Snakeman(Content.Load<Texture2D>("enemyL"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 110), Color.White, true, false, -20, 20, true, false));
            }
            if (ks.IsKeyDown(Keys.LeftAlt))
            {
                plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeL"), new Vector2(GraphicsDevice.Viewport.Width, 0), Color.White, true, false, 10));
            }
            if (ks.IsKeyDown(Keys.B))
            {
                BossFight = true;
            }
            if (ks.IsKeyDown(Keys.F)&&prevks.IsKeyUp(Keys.F))
            {
                if (BossMove == true&&!bossmovechange)
                {
                    BossMove = false;
                    bossmovechange = true;
                }
                if(BossMove==false&&!bossmovechange)
                {
                    BossMove = true;
                    bossmovechange = true;
                }
            }
            if (ks.IsKeyDown(Keys.D) && prevks.IsKeyUp(Keys.D))
            {
                if (boss.Left == true && !bossdirectionchange)
                {
                    boss.Left = false;
                    boss.Right = true;
                    bossdirectionchange = true;
                }
                if (boss.Right == true && !bossdirectionchange)
                {
                    boss.Right = false;
                    boss.Left = true;
                    bossdirectionchange = true;
                }
            }
            bossdirectionchange = false;
            bossmovechange = false;
            if (ks.IsKeyDown(Keys.R)&&rage == false)
            {
                if (fire.Count >= 1)
                {
                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                }
                else
                {
                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(0,GraphicsDevice.Viewport.Height-20), Color.White));
                }
                
            }
            if (fire.Count >= 1)
            {
                if (fire[fire.Count - 1].position.X > GraphicsDevice.Viewport.Width)
                {
                    rage = true;
                }
            }
            if (rage == true&&fire.Count == 0)
            {
                rage = false;
            }
            
            
            
            if (Alive == false&& ks.IsKeyDown(Keys.R))
            {
                 Jumping = false;
                 ground = false;
                 FacingLeft = true;
                 FacingRight = false;
                 Alive = true;
                 OnPlatform = false;
                 health = 100;
                 rage = false;
                score = 0;
                fire.Clear();
                fireball.Clear();
                enemy.Clear();
                enemySpawnSpeed = 2000;
                platform.Clear();
                plane.Clear();
                player.position.Y = GraphicsDevice.Viewport.Height - 350;
                player.position.X = GraphicsDevice.Viewport.Width - 440;
                
            }
            
            if (Gamestart == false&&ks.IsKeyDown(Keys.Space))
            {
                Gamestart = true;
            }
            if (Alive&&Gamestart)
            {
                if (ks.IsKeyDown(Keys.LeftShift))
                {
                    
                    if (platformspawn == false)
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(player.position.X, player.position.Y+player.hitbox.Height), Color.White, 1));
                    }
                    platformspawn = true;
                }
                else
                {
                    platform.Clear();
                    platformspawn = false;
                }
                playermoving = false;
                if (ks.IsKeyDown(Keys.Right)&&launched == false)
                {
                    player.MoveRight(GraphicsDevice.Viewport.Width);
                    playermoving = true;
                    FacingLeft = false;
                    FacingRight = true;
                }
                if (ks.IsKeyDown(Keys.Left)&&launched == false)
                {
                    player.MoveLeft();
                    playermoving = true;
                    FacingLeft = true;
                    FacingRight = false;
                }
                if (FireTime > TimeSpan.FromMilliseconds(1000)&&rage == false&&fire.Count < 105)
                {
                    if (fire.Count >= 1)
                    {
                        fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count-1].position.X+10, fire[fire.Count-1].position.Y), Color.White));
                    }
                    else
                    {
                        fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(0, GraphicsDevice.Viewport.Height - 20), Color.White));
                    }
                    if (OnPlatform&&health!=100)
                    {
                        health+=5;
                    }
                    FireTime = TimeSpan.Zero;
                }
                if (ks.IsKeyDown(Keys.Space) && prevks.IsKeyUp(Keys.Space)&&fire.Count>=1||rage)
                {
                    if (FacingLeft)
                    {
                        fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(player.position.X, player.position.Y + 17), Color.White, true, false,false, true));
                    }
                    if (FacingRight)
                    {
                        fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(player.position.X+60, player.position.Y + 17), Color.White, false, true,false, true));
                    }
                    fire.Remove(fire[fire.Count - 1]);
                }
                for (int i = 0; i < fireball.Count; i++)
                {
                    if (fireball[i].Left)
                    {
                        fireball[i].MoveLeft();
                    }
                    if (fireball[i].Right)
                    {
                        fireball[i].MoveRight();
                    }
                    if (fireball[i].down)
                    {
                        fireball[i].MoveDown();
                    }
                }
                if (!fly)
                {
                    if (Jumping == false && ground == false)
                    {
                        player.MoveDown(MoveDown, GraphicsDevice.Viewport.Height);
                        MoveDown++;
                    }
                    if (ks.IsKeyDown(Keys.Up) && Jumping == false && ground == true)
                    {
                        ground = false;
                        Jumping = true;
                        Moveup = -25;
                        MoveDown = 0;
                    }
                    if (Jumping)
                    {
                        player.MoveUp(Moveup);
                        Moveup += 1;
                    }
                    if (Moveup == 0)
                    {
                        Jumping = false;
                    }
                    if (player.position.Y + player.hitbox.Height >= GraphicsDevice.Viewport.Height - 70)
                    {
                        ground = true;
                        player.position.Y = GraphicsDevice.Viewport.Height - player.hitbox.Height - 70;
                    }
                    if (player.position.Y +player.hitbox.Height< 0)
                    {
                        platform.Clear();
                    }
                    
                }
                else
                {
                    if (ks.IsKeyDown(Keys.Up))
                    {
                        player.position.Y-=5;
                    }
                    if (ks.IsKeyDown(Keys.Down))
                    {
                        player.position.Y+=5;
                    }
                }
                for (int i = 0; i < platform.Count; i++)
                {
                    /*if (platformMove == true)
                    {
                        platform[i].Move(GraphicsDevice.Viewport.Width);
                    }
                    if (OnPlatform)
                    {
                        player.position.X += platform[i].movespeed;
                    }*/
                    platform[i].Update();
                    if (player.Hit(platform[i].bottom))
                    {
                        Jumping = false;
                        break;
                    }
                    //if (player.Hit(platform[i].bottom)&&player.position.X < platform[i].bottom.X&&OnPlatform==false)
                    //{
                      //  player.position.X -= 10;
                    //}
                    if (player.Hit(platform[i].top)&&OnPlatform == false)
                    {
                        ground = true;
                        OnPlatform = true;
                        player.position.Y = platform[i].hitbox.Top - player.hitbox.Height+5;
                        MoveDown = 0;
                    }
                    if (OnPlatform == true)
                    {
                        if (player.NoHit(platform[i].top))
                        {
                            ground = false;
                            OnPlatform = false;
                        }
                        
                    }
                }
                if (platform.Count <= 0&&OnPlatform == true)
                {
                    OnPlatform = false;
                    ground = false;
                }
                if (enemySpawn > TimeSpan.FromMilliseconds(enemySpawnSpeed)&&rage == false&&BossFight == false)
                {
                    enemySpawn = TimeSpan.Zero;
                    int ran = random.Next(0, 21);
                    if (ran < 8)
                    {
                        enemy.Add(new Snakeman(Content.Load<Texture2D>("playerL"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 110), Color.White, true, false, -25, 20, true, false));
                    }
                    if (ran >= 3&&ran <= 17)
                    {
                        enemy.Add(new Snakeman(Content.Load<Texture2D>("playerR"), new Vector2(0, GraphicsDevice.Viewport.Height - 110), Color.White, false, true, -25, 20, true, false));
                    }
                    if (ran == 18)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeR"), new Vector2(-10, 0), Color.White, false, true,10));
                    }
                    if (ran == 19)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeL"), new Vector2(GraphicsDevice.Viewport.Width, 0), Color.White, true, false,10));
                    }
                    if (ran == 20)
                    {
                        BossFight = true;
                    }
                }
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (enemy[i].DownyDown())
                    {
                        enemy[i].MoveDown();
                        enemy[i].downspeed += 1;
                    }
                    enemy[i].Ground(GraphicsDevice.Viewport.Height - 70);
                    enemy[i].Sides(GraphicsDevice.Viewport.Width);
                    if (enemy[i].Left && enemy[i].move)
                    {
                        enemy[i].MoveLeft();
                    }
                    if (enemy[i].Right && enemy[i].move)
                    {
                        enemy[i].MoveRight();
                    }
                    if (enemy[i].jumping)
                    {
                        enemy[i].MoveUp();
                        enemy[i].upspeed += 1;
                    }
                    if (enemy[i].upspeed == 0)
                    {
                        enemy[i].upspeed = -25;
                        enemy[i].jumping = false;
                    }
                    if (enemy[i].Fire())
                    {
                        if (enemy[i].Left)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(enemy[i].position.X, enemy[i].position.Y + 17), Color.White, true, false,false, false));
                        }
                        if (enemy[i].Right)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(enemy[i].position.X+60, enemy[i].position.Y + 17), Color.White, false, true,false, false));
                        }
                        enemy[i].fireTime = TimeSpan.Zero;
                    }
                    for (int x = 0; x < fireball.Count; x++)
                    {
                        if (enemy[i].otherHit(fireball[x].hitbox)&&fireball[x].IsPLayer)
                        {
                            if (enemy[i].ground)
                            {
                                enemy[i].jumping = true;
                                enemy[i].downspeed = 0;
                                enemy[i].ground = false;
                            }

                        }
                        if (enemy[i].hit(fireball[x].hitbox) && fireball[x].IsPLayer)
                        {
                            enemy.Remove(enemy[i]);
                            fireball.Remove(fireball[x]);
                            for (int f = 0;f<=10;f++)
                            {
                                if (fire.Count >= 1 && fire.Count < 105)
                                {
                                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                                }
                            }
                            score++;
                            break;
                        }
                        
                    }
                    
                }
                for (int i = 0;i<plane.Count;i++)
                {
                    if (plane[i].Right)
                    {
                        plane[i].MoveRight();
                    }
                    if (plane[i].Left)
                    {
                        plane[i].MoveLeft();
                    }
                    if(plane[i].fire())
                    {
                        plane[i].fireTime = TimeSpan.Zero;
                        if (plane[i].Right)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(plane[i].position.X, plane[i].position.Y), Color.White, false, false, true, false));
                        }
                        if (plane[i].Left)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(plane[i].position.X+plane[i].hitbox.Width-2, plane[i].position.Y), Color.White, false, false, true, false));
                        }
                    }
                    for (int f = 0; f < fireball.Count;f++)
                    {
                        
                        if (fireball[f].Hit(plane[i].hitbox)&&fireball[f].IsPLayer)
                        {
                            if (plane[i].Health >= 1)
                            {
                                plane[i].Health--;
                                fireball.Remove(fireball[f]);
                            }
                            if (plane[i].Health <= 0)
                            {
                                plane.Remove(plane[i]);
                                fireball.Remove(fireball[f]);
                                score += 100;
                                for (int f2 = 0; f2 <= 10; f2++)
                                {
                                    if (fire.Count >= 1 && fire.Count < 105)
                                    {
                                        fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                                    }
                                    
                                }
                                PlaneDown = true;
                                break;
                            }
                        }
                    }
                    if (PlaneDown)
                    {
                        PlaneDown = false;
                        break;
                    }
                    if (plane[i].IsOffScreen(GraphicsDevice.Viewport.Width))
                    {
                        plane.Remove(plane[i]);
                    }
                }
                
                for (int i = 0; i < fireball.Count;i++)
                {
                    if (player.Hit(fireball[i].hitbox) && fireball[i].IsPLayer == false && rage != true)
                    {
                        health -= 10;
                        fireball.Remove(fireball[i]);
                        break;
                    }
                    for (int p = 0; p < platform.Count; p++)
                    {
                        if (fireball[i].Hit(platform[0].hitbox))
                        {
                            fireball.Remove(fireball[i]);
                            break;
                        }
                    }
                    if (i >= fireball.Count)
                    {
                        break;
                    }
                }
                for (int i = 0; i < fireball.Count; i ++)
                {
                    if (fireball[i].IsOffScreen(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height))
                    {
                        fireball.Remove(fireball[i]);
                    }
                    while (i >= fireball.Count)
                    {
                        i--;
                    }
                }
                if (health <= 0)
                {
                    Alive = false;
                }



                if (BossFight)
                {
                    if (enemy.Count == 0)
                    {
                        boss.position.Y = GraphicsDevice.Viewport.Height - 395;
                    }
                    if (boss.smallHitbox.X>player.hitbox.X+player.hitbox.Width&&BossMove)
                    {
                        boss.Left = true;
                        boss.Right = false;
                    }
                    if (boss.smallHitbox.X+boss.smallHitbox.Width < player.hitbox.X&&BossMove)
                    {
                        boss.Right = true;
                        boss.Left = false;
                    }
                    if (bossfire>= TimeSpan.FromMilliseconds(5000))
                    {     
                        fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(player.position.X+30, -20), Color.White, false, false, true, false));
                    }
                    boss.Sides(GraphicsDevice.Viewport.Width);
                    if (BossMove)
                    {
                        if (boss.Right)
                        {
                            boss.MoveRight();
                        }
                        if (boss.Left)
                        {
                            boss.MoveLeft();
                        }
                    }
                    if (boss.topHit(player.hitbox))
                    {
                        Jumping = true;
                        Moveup = -25;
                        MoveDown = 0;
                    }
                    else if (boss.otherHit(player.hitbox))
                    {
                        launched = true;
                        ground = false;
                        Jumping = true;
                        Moveup = -25;
                        MoveDown = 0;
                        launchedTime = TimeSpan.Zero;
                        if (!rage)
                        {
                            health -= 10;
                        }
                        
                    }
                    for (int i = 0;i<fireball.Count;i++)
                    if (boss.otherHit(fireball[i].hitbox)&&fireball[i].IsPLayer)
                    {
                        boss.health--;
                            fireball.Remove(fireball[i]);
                    }
                    if (boss.health<=0)
                    {
                        boss.position.Y = 10000;
                        score += 1000;
                        BossFight = false;
                    }
                }
                
                if (launchedTime < TimeSpan.FromMilliseconds(900)&&launched == true)
                {
                    if (boss.Left)
                    {
                        player.MoveLeft();
                    }
                    if (boss.Right)
                    {
                        player.MoveRight(GraphicsDevice.Viewport.Width);
                    }
                }
                else
                {
                    launched = false;
                }

                
                
            }
            
            prevks = ks;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (Alive&&Gamestart)
            {
                Background.Draw(spriteBatch);
                if (BossFight)
                {
                    if (bossfire >= TimeSpan.FromMilliseconds(4500))
                    {
                        bosscolor = Color.Red;
                    }
                    else
                    {
                        bosscolor = Color.White;
                    }
                    if (boss.Left)
                    {
                        boss.Draw(spriteBatch, Content.Load<Texture2D>("BossL"), bosscolor);
                    }
                    if (boss.Right)
                    {
                        boss.Draw(spriteBatch, Content.Load<Texture2D>("BossR"), bosscolor);
                    }
                    spriteBatch.DrawString(font, "" + boss.health + "", new Vector2(boss.smallHitbox.X + 20, boss.smallHitbox.Y - 20), Color.White);

                }
                if (FacingLeft)
                {
                    if (rage)
                    {
                        player.Draw2(spriteBatch, Content.Load<Texture2D>("PlayerL"), Color.Red);
                    }
                    else
                    {
                        player.Draw2(spriteBatch, Content.Load<Texture2D>("PlayerL"), Color.White);
                    }

                }
                if (FacingRight)
                {
                    if (rage)
                    {
                        player.Draw2(spriteBatch, Content.Load<Texture2D>("playerR"), Color.Red);
                    }
                    else
                    {
                        player.Draw2(spriteBatch, Content.Load<Texture2D>("playerR"), Color.White);
                    }
                }
                for (int i = 0; i < platform.Count; i++)
                {
                    platform[i].Draw(spriteBatch);
                }
                for (int i = 0;i<fire.Count;i++)
                {
                    fire[i].Draw(spriteBatch);
                }
               
                spriteBatch.DrawString(font, "Health = " + health + "", new Vector2(100, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "Enemy's = " + enemy.Count + "", new Vector2(300, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "Fire = " + fire.Count + "", new Vector2(200, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "Score = " + score + "", new Vector2(400, GraphicsDevice.Viewport.Height - 40), Color.White);

                if (plane.Count >= 1)
                {
                    spriteBatch.DrawString(font, "Plane 1 Health = " + plane[0].Health + "", new Vector2(800, GraphicsDevice.Viewport.Height - 40), Color.White);
                }
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (enemy[i].Left)
                    {
                        if (enemy[i].attack == true)
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("playerL"), Color.Red);
                        }
                        else
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("playerL"), Color.SandyBrown);
                        }

                    }
                    if (enemy[i].Right)
                    {
                        if (enemy[i].attack == true)
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("playerR"), Color.Red);
                        }
                        else
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("playerR"), Color.SandyBrown);
                        }
                    }

                }
                
                for (int i = 0; i < plane.Count; i++)
                {
                    plane[i].Draw(spriteBatch);
                }
                    for (int i = 0; i < fireball.Count; i++)
                {
                    fireball[i].Draw(spriteBatch);
                }
            }
            else
            {
                if (Alive == false)
                {
                    Failure.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Press R to Restart", new Vector2(430, 50), Color.DarkOrange);
                    spriteBatch.DrawString(font, "score = " + score + "", new Vector2(460, 80), Color.DarkOrange);
                }
                if (Gamestart == false)
                {
                    Begining.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Press Space to Start", new Vector2(430, 200), Color.DarkOrange);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
