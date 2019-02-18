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

        TimeSpan enemySpawn = new TimeSpan();
        TimeSpan FireTime = new TimeSpan();
        TimeSpan launchedTime = new TimeSpan();
        TimeSpan bossfire = new TimeSpan();
        TimeSpan second = new TimeSpan();
        TimeSpan shieldblinking = new TimeSpan();

        List<Fireball> fireball = new List<Fireball>();
        List<Enemy> enemy = new List<Enemy>();
        List<Platform> platform = new List<Platform>();
        List<Sprite> fire = new List<Sprite>();
        List<FlyingEnemy> plane = new List<FlyingEnemy>();
        List<Sprite> Warning = new List<Sprite>();
        List<Crates> Crate = new List<Crates>();
        List<Moveythingy> Clouds = new List<Moveythingy>();

        Player player;
        Random random = new Random();
        Boss boss;
        background Background;
        background Failure;
        background Begining;
        PlayerSprite LegsL;
        PlayerSprite TurretL;
        PlayerSprite TurretR;
        PlayerSprite LegsR;
        Bars HealthBar;
        Bars ShieldBar;
        Color bosscolor = Color.White;
        Color playercolor = Color.White;
        Sprite BackgroundPlatform;
        Sprite Arrow;

        bool Jumping = false;
        int MoveDown = 30;
        int Moveup = -30;
        bool Ground = false;
        bool FacingLeft = true;
        bool FacingRight = false;
        bool TurretFaceLeft = true;
        bool TurretFaceRight = false;
        KeyboardState prevks;
        bool Alive = true;
        bool OnPlatform = false;
        int health = 100;
        int shield = 100;
        bool rage = false;
        bool Gamestart = false;
        int score = 0;
        int Leftspeed = 5;
        int Rightspeed = 5;
        bool BossFight = false;
        bool BossMove = true;
        bool playershield = true;
        //bool platformspawn = false;
        int enemySpawnSpeed = 2000;
        bool PlaneDown = false;
        bool launched = false;
        bool playershieldblinking = false;
        bool playercolorchange = false;
        bool BossPlatformsSpawned = false;

        bool makeenemies = true;

        bool BossAirstrike = false;
        int bossfirelocation = 0;
        bool BossAttack = false;
        bool BossLazerbeam = false;
        bool Bosschargelazer = false;
        bool IsPlayerMoving = true;
        bool playermoveleft = true;
        bool playermoveright = true;
        bool BossPlatformsInPosition = false;
        int CLoudDownSpeed = 0;
        //bool OnEnemy = false;
        int TurretMovement = 1;
        //Rectangle Trolololololol;
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
            Arrow = new Sprite(Content.Load<Texture2D>("PixelArrow"), new Vector2(0, -50), Color.White);
            Begining = new background(Content.Load<Texture2D>("Start"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Background = new background(Content.Load<Texture2D>("background"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Failure = new background(Content.Load<Texture2D>("Failure"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            //platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(GraphicsDevice.Viewport.Width - 440, GraphicsDevice.Viewport.Height - 250), Color.White, 1));
            player = new Player(Content.Load<Texture2D>("playerL"), new Vector2(GraphicsDevice.Viewport.Width - 440, GraphicsDevice.Viewport.Height - 350), Color.White);
            boss = new Boss(Content.Load<Texture2D>("BossL"), new Vector2(-1000, GraphicsDevice.Viewport.Height - 395), Color.White, true, false, 20, -25, 100);
            TurretL = new PlayerSprite(Content.Load<Texture2D>("TurretL"), new Vector2(0, 0), Color.White);
            LegsL = new PlayerSprite(Content.Load<Texture2D>("LegsL"), new Vector2(0, 0), Color.White);
            TurretR = new PlayerSprite(Content.Load<Texture2D>("TurretR"), new Vector2(0, 0), Color.White);
            LegsR = new PlayerSprite(Content.Load<Texture2D>("LegsR"), new Vector2(0, 0), Color.White);
            HealthBar = new Bars(Content.Load<Texture2D>("HealthBar"), new Vector2(0, GraphicsDevice.Viewport.Height - 40), Color.White);
            ShieldBar = new Bars(Content.Load<Texture2D>("ShieldBar"), new Vector2(0, GraphicsDevice.Viewport.Height - 40), Color.White);
            BackgroundPlatform = new Sprite(Content.Load<Texture2D>("BackgroundPlatform"), new Vector2(0,GraphicsDevice.Viewport.Height-75), Color.White);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < platform.Count; i++)
            {
                platform[i].Update();
            }
            enemySpawn += gameTime.ElapsedGameTime;
            FireTime += gameTime.ElapsedGameTime;
            second += gameTime.ElapsedGameTime;
            shieldblinking += gameTime.ElapsedGameTime;
            
            if (launched)
            {
                launchedTime += gameTime.ElapsedGameTime;
            }

            if (BossFight)
            {
                bossfire += gameTime.ElapsedGameTime;
            }
            if (bossfire >= TimeSpan.FromMilliseconds(4500) && !BossAttack)
            {
                int ran = random.Next(0, 2);
                if (ran == 0)
                {
                    BossAirstrike = true;
                }
                if (ran == 1)
                {
                    Bosschargelazer = true;
                }
                bossfire = TimeSpan.Zero;
            }

            if (second >= TimeSpan.FromMilliseconds(1000))
            {
                second = TimeSpan.Zero;
                if (shield <= 100 && playershield)
                {
                    shield += 1;
                }
                if (!BossPlatformsInPosition)
                {
                    int CloudStartPosition = random.Next(0, GraphicsDevice.Viewport.Height - 105);
                    Clouds.Add(new Moveythingy(Content.Load<Texture2D>("cloud1"), new Vector2(-130, CloudStartPosition), Color.White));
                }
                else
                {
                    int CloudStartPosition = random.Next(0, GraphicsDevice.Viewport.Width - 105);
                    Clouds.Add(new Moveythingy(Content.Load<Texture2D>("cloud1"), new Vector2(CloudStartPosition, -150), Color.White));
                    
                    CLoudDownSpeed++;
                }
            }

            
            if (Bosschargelazer)
            {
                BossMove = false;
                if (bossfire >= TimeSpan.FromMilliseconds(500))
                {
                    BossLazerbeam = true;
                    Bosschargelazer = false;
                    bosscolor = Color.Red;
                }
            }
            if (BossAirstrike || BossLazerbeam)
            {
                if (bossfire >= TimeSpan.FromMilliseconds(2000))
                {
                    BossAirstrike = false;
                    BossLazerbeam = false;
                    BossMove = true;
                    Warning.Clear();
                    bossfire = TimeSpan.Zero;
                }
            }
            if (BossAirstrike || BossLazerbeam || Bosschargelazer)
            {
                BossAttack = true;
            }
            else
            {
                BossAttack = false;
                bosscolor = Color.White;
            }
            for (int i = 0; i < enemy.Count; i++)
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


            if (ks.IsKeyDown(Keys.B))
            {
                BossFight = true;
            }


            if (ks.IsKeyDown(Keys.R) && rage == false)
            {
                if (fire.Count >= 1)
                {
                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                }
                else
                {
                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(0, GraphicsDevice.Viewport.Height - 20), Color.White));
                }

            }
            if (fire.Count >= 1)
            {
                if (fire[fire.Count - 1].position.X - fire[fire.Count - 1].hitbox.Width > GraphicsDevice.Viewport.Width)
                {
                    rage = true;
                }
            }
            if (rage == true && fire.Count == 0)
            {
                rage = false;
            }



            if (Alive == false && ks.IsKeyDown(Keys.R))
            {
                Jumping = false;
                Ground = false;
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
                boss.position.X = -1000;
                boss.health = 100;
                BossFight = false;
                playershield = true;
                shield = 100;
                BossMove = false;
                Warning.Clear();
                BossAirstrike = false;
                BossLazerbeam = false;
                BossAttack = false;
                Crate.Clear();
                
            }

            if (Gamestart == false && ks.IsKeyDown(Keys.Space))
            {
                Gamestart = true;
            }
            if (Alive && Gamestart)
            {
               
                for (int i = 0; i < Clouds.Count; i ++)
                {                  
                    Clouds[i].MoveDown(CLoudDownSpeed);
                    Clouds[i].MoveRight(5);
                    if (Clouds[i].position.Y+Clouds[i].hitbox.Height>GraphicsDevice.Viewport.Height-75)
                    {
                        Clouds.Remove(Clouds[i]);
                        break;
                    }
                    if (Clouds[i].position.X > GraphicsDevice.Viewport.Width)
                    {
                        Clouds.Remove(Clouds[i]);
                    }
                    
                }
                player.UpdateotherHitbox();
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].UpdateHitbox();
                }
                if (ks.IsKeyDown(Keys.RightShift)&&prevks.IsKeyUp(Keys.RightShift))
                {
                    platform.Clear();
                }
                if (ks.IsKeyDown(Keys.LeftShift) && prevks.IsKeyUp(Keys.LeftShift))
                {
                    if (ks.IsKeyDown(Keys.Down))
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(player.position.X, player.position.Y + player.hitbox.Height), Color.White, 1, false, 1, false, false));
                    }
                    else if (IsPlayerMoving == false && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Right))
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(player.position.X, player.position.Y - player.hitbox.Height), Color.White, 1, false, 1, false, false));
                    }
                    else
                    {
                        if (ks.IsKeyDown(Keys.Left))
                        {
                            platform.Add(new Platform(Content.Load<Texture2D>("sidePlatform"), new Vector2(player.position.X - player.hitbox.Width, player.position.Y), Color.White, 1, true, 1, false, false));
                        }
                        if (ks.IsKeyDown(Keys.Right))
                        {
                            platform.Add(new Platform(Content.Load<Texture2D>("sidePlatform"), new Vector2(player.position.X + player.hitbox.Width * 2, player.position.Y), Color.White, 1, true, 1, false, false));
                        }
                    } 
                }
                IsPlayerMoving = false;
                if (ks.IsKeyDown(Keys.Right) && launched == false && playermoveright)
                {
                    player.MoveRight(GraphicsDevice.Viewport.Width, Rightspeed);

                    FacingLeft = false;
                    FacingRight = true;
                    IsPlayerMoving = true;
                }
                else if (ks.IsKeyDown(Keys.Left) && launched == false && playermoveleft)
                {
                    player.MoveLeft(Leftspeed);

                    FacingLeft = true;
                    FacingRight = false;
                    IsPlayerMoving = true;
                }
                
                if (ks.IsKeyDown(Keys.W) && prevks.IsKeyUp(Keys.W))
                {
                    TurretMovement *= -1;
                }
                if (TurretMovement == -1)
                {
                    if (ks.IsKeyDown(Keys.A))
                    {
                        TurretFaceLeft = true;
                        TurretFaceRight = false;
                    }
                    if (ks.IsKeyDown(Keys.D))
                    {
                        TurretFaceLeft = false;
                        TurretFaceRight = true;
                    }
                }
                if (player.position.Y + player.hitbox.Height >= GraphicsDevice.Viewport.Height - 70)
                {
                    Ground = true;
                    player.position.Y = GraphicsDevice.Viewport.Height - player.hitbox.Height - 70;
                }
                if (Jumping == false && Ground == false)
                {
                    player.MoveDown(MoveDown, GraphicsDevice.Viewport.Height);
                    MoveDown++;
                }
                if (ks.IsKeyDown(Keys.Up) && Jumping == false && Ground == true)
                {
                    Ground = false;
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

                Arrow.position.X = player.position.X;
                if (player.position.Y+player.hitbox.Height<0&&Arrow.position.Y<0)
                {
                    Arrow.position.Y ++;
                }
                else if(Arrow.position.Y+Arrow.hitbox.Height>=0)
                {
                    Arrow.position.Y--;
                }
                if (FireTime > TimeSpan.FromMilliseconds(1000) && rage == false && fire.Count < 105)
                {
                    if (fire.Count >= 1)
                    {
                        fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                    }
                    else
                    {
                        fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(0, GraphicsDevice.Viewport.Height - 20), Color.White));
                    }

                    FireTime = TimeSpan.Zero;
                }
                if (rage == true)
                {
                    int ran = random.Next(0, 1000);
                    fire.Remove(fire[fire.Count - 1]);
                    fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(ran, -10), Color.White, false, false, true, true));
                    if (fire.Count <= 1)
                    {
                        rage = false;
                    }
                }
                if (ks.IsKeyDown(Keys.Space) && prevks.IsKeyUp(Keys.Space) && fire.Count >= 1)
                {
                    if (TurretFaceLeft)
                    {
                        fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(player.position.X, player.position.Y + 17), Color.White, true, false, false, true));
                    }
                    if (TurretFaceRight)
                    {
                        fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(player.position.X + 60, player.position.Y + 17), Color.White, false, true, false, true));
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



                playermoveright = true;
                playermoveleft = true;
                OnPlatform = false;
                Ground = false;
                for (int i = 0; i < platform.Count; i++)
                {
                    if (platform[i].DoMove)
                    {
                        platform[i].Move();
                        if (platform[i].BossPlatforms)
                        {
                            if (platform[i].position.Y < GraphicsDevice.Viewport.Height-75)
                            {
                                platform[i].DoMove = false;
                                BossPlatformsInPosition = true;
                            }
                        }
                        if (platform[i].OnSide)
                        {
                            if (platform[i].position.Y < 0 || platform[i].position.Y > GraphicsDevice.Viewport.Height)
                            {
                                platform[i].Movedirection *= -1;
                            }
                        }
                        else
                        {
                            if (platform[i].position.X < 0 || platform[i].position.X > GraphicsDevice.Viewport.Width)
                            {
                                platform[i].Movedirection *= -1;
                            }
                        }
                    }

                    platform[i].Update();
                    if (player.Hit(platform[i].bottom))
                    {
                        Jumping = false;
                        
                    }
                    if (BossFight)
                    {
                        
                        if (!platform[i].BossPlatforms)
                        {
                            platform[i].MoveDown(CLoudDownSpeed);
                            if (launched && player.Hit(platform[i].hitbox))
                            {
                                platform.Remove(platform[i]);
                                break;
                            }
                            if (boss.otherHit(platform[i].hitbox))
                            {
                                platform.Remove(platform[i]);
                                boss.health -= 1;
                                break;

                            }
                        }
                        
                    }
                    if (player.Hit(platform[i].RightSide))
                    {
                        playermoveright = false;
                    }
                    
                    if (player.Hit(platform[i].LeftSide))
                    {
                        playermoveleft = false;
                    }
                    
                    if (player.Hit(platform[i].top) && OnPlatform == false)
                    {
                        Ground = true;
                        OnPlatform = true;
                        player.position.Y = platform[i].hitbox.Top - player.hitbox.Height + 5;
                        MoveDown = 0;
                    }
                    
                }
                if (platform.Count <= 0 && OnPlatform == true)
                {
                    OnPlatform = false;
                    Ground = false;
                }
                if (enemySpawn > TimeSpan.FromMilliseconds(enemySpawnSpeed) && !BossFight && makeenemies)
                {
                    enemySpawn = TimeSpan.Zero;
                    int ran = random.Next(0, 21);
                    if (ran < 8)
                    {
<<<<<<< HEAD
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerL"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 110), Color.White, false, false, -25, 20, true, false, false, false));
                    }
                    if (ran >= 3 && ran <= 17)
                    {
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerR"), new Vector2(0, GraphicsDevice.Viewport.Height - 110), Color.White, false, false, -25, 20, true, false, false, false));
=======
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerL"), new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 110), Color.White, true, false, -25, 20, true, false, false, false));
                    }
                    if (ran >= 3 && ran <= 17)
                    {
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerR"), new Vector2(0, GraphicsDevice.Viewport.Height - 110), Color.White, false, true, -25, 20, true, false, false, false));
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                    }
                    if (ran == 18)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeR"), new Vector2(-10, 0), Color.White, false, true, 2));
                    }
                    if (ran == 19)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeL"), new Vector2(GraphicsDevice.Viewport.Width, 0), Color.White, true, false, 2));
                    }
                    if (ran == 20)
                    {
                        BossFight = true;
                        BossMove = true;
                    }
                }
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (enemy[i].hit(player.hitbox))
                    {
                        enemy[i].jumping = true;
                        enemy[i].downspeed = 0;
<<<<<<< HEAD
                        enemy[i].upspeed = -25;
=======
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                        enemy[i].ground = false;
                    }
                    if (enemy[i].DownyDown())
                    {
                        enemy[i].MoveDown();
                        enemy[i].downspeed += 1;
                    }
                    enemy[i].UpdateHitbox();
                    enemy[i].Ground(GraphicsDevice.Viewport.Height - 70, player.otherHitbox);
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
<<<<<<< HEAD
                    
=======
                    if (player.hitbox.X < enemy[i].hitbox.X && enemy[i].Right&&!enemy[i].InPlayerArea)
                    {
                        enemy[i].Right = false;
                        enemy[i].Left = true;
                    }
                    if (player.hitbox.X > enemy[i].hitbox.X && enemy[i].Left&&!enemy[i].InPlayerArea)
                    {
                        enemy[i].Right = true;
                        enemy[i].Left = false;
                    }
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                    /*if (!enemy[i].otherHit(player.otherHitbox))
                    {
                        enemy[i].move = true;
                    }
                    if (!enemy[i].hit(player.hitbox)&&Ground)
                    {
                        enemy[i].downspeed = 0;
                        enemy[i].ground = false;
                    }*/
                    for (int p = 0; p < platform.Count; p++)

                    {
                        if (enemy[i].hit(platform[p].bottom))
                        {
                            enemy[i].jumping = false;
                        }
                        if (enemy[i].hit(platform[p].top) && enemy[i].OnPlatform == false)
                        {
                            enemy[i].OnPlatform = true;
                            enemy[i].ground = true;
                            enemy[i].downspeed = 0;
                        }
                        if (enemy[i].OnPlatform == true)
                        {
                            enemy[i].position.Y = platform[p].position.Y - enemy[i].hitbox.Height;
                            if (!enemy[i].hit(platform[p].top))
                            {
                                enemy[i].ground = false;
                                enemy[i].OnPlatform = false;
                            }
                        }
                        
                        
                        if (enemy[i].hit(platform[p].RightSide)&&enemy[i].Right)
<<<<<<< HEAD
                        {
                            enemy[i].move = false;
                        }
                        if (enemy[i].hit(platform[p].LeftSide)&&enemy[i].Left)
                        {
                            enemy[i].move = false;
                        }
                        else if (!enemy[i].hit(platform[p].hitbox))
                        {
=======
                        {
                            enemy[i].move = false;
                        }
                        if (enemy[i].hit(platform[p].LeftSide)&&enemy[i].Left)
                        {
                            enemy[i].move = false;
                        }
                        else if (!enemy[i].hit(platform[p].hitbox))
                        {
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                            enemy[i].move = true;
                        }

                    }
                    
                        health = 100;
                    if (enemy[i].Fire())
                    {
                        if (enemy[i].Left)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(enemy[i].position.X, enemy[i].position.Y + 17), Color.White, true, false, false, false));
                        }
                        if (enemy[i].Right)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(enemy[i].position.X + 60, enemy[i].position.Y + 17), Color.White, false, true, false, false));
                        }
                        enemy[i].fireTime = TimeSpan.Zero;
                    }
                    for (int x = 0; x < fireball.Count; x++)
                    {
                        if (enemy[i].otherHit(fireball[x].hitbox) && fireball[x].IsPLayer)
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
                            int ran = random.Next(0, 2);
                            if (ran == 0)
                            {
                                Crate.Add(new Crates(Content.Load<Texture2D>("Crate"), new Vector2(enemy[i].position.X, enemy[i].position.Y + 20), Color.White));
                            }
                            enemy.Remove(enemy[i]);
                            fireball.Remove(fireball[x]);
                            score++;
                            break;
                        }

                    }

                }
<<<<<<< HEAD
                
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].UpdateHitbox();
=======
                for (int i = 0; i < enemy.Count; i++)
                {
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                    if (enemy[i].otherHit(player.hitbox))
                    {
                        enemy[i].InPlayerArea = true;
                    }
                    if (enemy[i].InPlayerArea)
                    {
                        enemy[i].state = Enemy.State.Run;
                    }
                    if (!enemy[i].otherHit(player.hitbox))
                    {
                        enemy[i].InPlayerArea = false;
                        enemy[i].state = Enemy.State.Follow;
                    }
                    if (enemy[i].state == Enemy.State.Follow)
                    {
<<<<<<< HEAD
                        if (enemy[i].hitbox.X<player.hitbox.X)
                        {
                            enemy[i].Left = false;
                            enemy[i].Right = true;
                        }
                        if (enemy[i].hitbox.X>player.hitbox.X)
                        {
                            enemy[i].Right = false;
                            enemy[i].Left = true;
                        }
                    }
                    if (enemy[i].state == Enemy.State.Run)
                    {
                        if (enemy[i].hitbox.X < player.hitbox.X)
                        {
                            enemy[i].Left = true;
                            enemy[i].Right = false;
                        }
                        if (enemy[i].hitbox.X > player.hitbox.X)
                        {
                            enemy[i].Right = true;
                            enemy[i].Left = false;
                        }
=======

>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                    }
                }
                for (int i = 0; i < plane.Count; i++)
                {
                    if (plane[i].Right)
                    {
                        plane[i].MoveRight();
                    }
                    if (plane[i].Left)
                    {
                        plane[i].MoveLeft();
                    }
                    if (plane[i].fire())
                    {
                        plane[i].fireTime = TimeSpan.Zero;
                        if (plane[i].Right)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(plane[i].position.X, plane[i].position.Y), Color.White, false, false, true, false));
                        }
                        if (plane[i].Left)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(plane[i].position.X + plane[i].hitbox.Width - 2, plane[i].position.Y), Color.White, false, false, true, false));
                        }
                    }
                    for (int f = 0; f < fireball.Count; f++)
                    {

                        if (fireball[f].Hit(plane[i].hitbox) && fireball[f].IsPLayer)
                        {
                            if (plane[i].Health >= 1)
                            {
                                plane[i].Health--;
                                fireball.Remove(fireball[f]);
                            }
                            if (plane[i].Health <= 0)
                            {
                                plane.Remove(plane[i]);
                                score += 100;
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

                for (int i = 0; i < fireball.Count; i++)
                {
                    if (player.Hit(fireball[i].hitbox) && fireball[i].IsPLayer == false && rage != true)
                    {
                        if (!playershield)
                        {
                            health -= 10;
                        }
                        if (playershield)
                        {
                            shield -= 10;
                        }
                        fireball.Remove(fireball[i]);
                        break;
                    }
                    
                    for (int p = 0; p < platform.Count; p++)
                    {
                        if (fireball[i].Hit(platform[p].hitbox))
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
                for (int i = 0; i < fireball.Count; i++)
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
                if (BossFight&&BossPlatformsSpawned==false)
                {
                    for (int i = 0; i < GraphicsDevice.Viewport.Width; i += 20)
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("sidePlatform"), new Vector2(i, GraphicsDevice.Viewport.Height), Color.White, 1, true, 1, true, true));
                    }
                    BossPlatformsSpawned = true;
                }
                
                if (BossFight==false&&BossPlatformsSpawned==true)
                {
                    int Bossplatfromscount = 0;
                    for (int i = 0; i < platform.Count;i++)
                    {
                        if (platform[i].BossPlatforms==true)
                        {
                            Bossplatfromscount++;
                            platform[i].DoMove = true;
                            platform[i].Movedirection = 1;
                            if (platform[i].position.Y>GraphicsDevice.Viewport.Height)
                            {
                                platform.Remove(platform[i]);
                            }
                        }
                    }
                    if (Bossplatfromscount==0)
                    {
                        BossPlatformsSpawned = false;
                        BossPlatformsInPosition = false;
                    }
                    Bossplatfromscount = 0;
                    CLoudDownSpeed = 0;
                }
                boss.SmallHitbox();
                if (BossFight)
                {
                    
                    if (enemy.Count == 0)
                    {
                        boss.position.Y = GraphicsDevice.Viewport.Height - 395;
                    }
                    if (boss.smallHitbox.X > player.hitbox.X + player.hitbox.Width && BossMove)
                    {
                        boss.Left = true;
                        boss.Right = false;
                    }
                    if (boss.smallHitbox.X + boss.smallHitbox.Width < player.hitbox.X && BossMove)
                    {
                        boss.Right = true;
                        boss.Left = false;
                    }

                    if (BossAirstrike)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            fireball.Add(new Fireball(Content.Load<Texture2D>("FireballDown"), new Vector2(bossfirelocation + i * 10, -500), Color.White, false, false, true, false));
                        }
                        Warning.Add(new Sprite(Content.Load<Texture2D>("Warning"), new Vector2(bossfirelocation, 0), Color.White));
                    }
                    if (!BossAirstrike)
                    {
                        bossfirelocation = player.hitbox.X;
                    }
                    if (BossLazerbeam)
                    {
                        BossMove = false;
                        for (int i = 0; i < 23; i++)
                        {
                            if (boss.Left)
                            {
                                fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(boss.smallHitbox.X, GraphicsDevice.Viewport.Height - 100 - i * 10), Color.White, true, false, false, false));
                            }
                            if (boss.Right)
                            {
                                fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(boss.smallHitbox.X + 50, GraphicsDevice.Viewport.Height - 100 - i * 10), Color.White, false, true, false, false));
                            }
                        }
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
                        Ground = false;
                        Jumping = true;
                        shield -= 10;
                        Moveup = -25;
                        MoveDown = 0;
                        launchedTime = TimeSpan.Zero;
                        if (!rage)
                        {
                            health -= 10;
                        }

                    }
                    for (int i = 0; i < fireball.Count; i++)
                    {
                        if (boss.otherHit(fireball[i].hitbox) && fireball[i].IsPLayer)
                        {
                            boss.health--;
                            fireball.Remove(fireball[i]);
                            int ran = random.Next(0, 10);
                            if (ran == 0)
                            {
                                Crate.Add(new Crates(Content.Load<Texture2D>("Crate"), new Vector2(boss.position.X, boss.position.Y + 20), Color.White));
                            }
                        }
                    }
                    if (boss.health <= 0)
                    {
                        boss.position.X = -1000;
                        score += 1000;
                        BossFight = false;
                        Warning.Clear();
                        boss.health = 100;
                    }



                }
                if (BossFight)
                {
                    BossMove = true;
                }
                else
                {
                    BossMove = false;
                }
                if (launchedTime < TimeSpan.FromMilliseconds(900) && launched == true)
                {
                    if (boss.Left)
                    {
                        player.MoveLeft(20);

                    }
                    if (boss.Right)
                    {
                        player.MoveRight(GraphicsDevice.Viewport.Width, 20);

                    }
                }
                else
                {
                    launched = false;
                }

                if (shield <= 0)
                {
                    playershield = false;
                    playershieldblinking = false;
                }
                else
                {
                    playershield = true;
                }
                if (shield <= 30 && playershield)
                {
                    playershieldblinking = true;
                }
                if (shield > 30 && playershield && playershieldblinking)
                {
                    playershieldblinking = false;
                }
                for (int i = 0; i < Crate.Count; i++)
                {
                    Crate[i].MoveDown(GraphicsDevice.Viewport.Height - 70);
                    if (Crate[i].Intersect(player.hitbox))
                    {
                        int ran = random.Next(0, 2);
                        if (ran == 0)
                        {
                            for (int fi = 0; fi <= 10; fi++)
                            {
                                if (fire.Count >= 1)
                                {
                                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(fire[fire.Count - 1].position.X + 10, fire[fire.Count - 1].position.Y), Color.White));
                                }
                                else
                                {
                                    fire.Add(new Sprite(Content.Load<Texture2D>("FireballLeft"), new Vector2(0, GraphicsDevice.Viewport.Height - 20), Color.White));
                                }
                            }
                        }
                        if (ran == 1)
                        {
                            if (health == 100)
                            {
                                shield += 50;
                            }
                            else if (playershield == false && health < 100)
                            {
                                health = 100;
                            }

                        }
                        Crate.Remove(Crate[i]);
                        break;
                    }
                }

                if (FacingLeft)
                {
                    TurretL.position = player.position;
                    TurretR.position = player.position;
                    TurretR.position.X = player.position.X + 28;
                }
                if (FacingRight)
                {
                    TurretR.position = player.position;
                    TurretR.position.X = player.position.X + 18;
                    TurretL.position = player.position;
                    TurretL.position.X = player.position.X - 18;
                }

                LegsR.position = player.position;
                LegsL.position = player.position;

                if (shield > 100)
                {
                    shield = 100;
                }
                if (shield < 0)
                {
                    shield = 0;
                }
                if (HealthBar.position.X != health - 100)
                {
                    if (HealthBar.position.X > health - 100)
                    {
                        HealthBar.MoveLeft();
                    }
                    else
                    {
                        HealthBar.MoveRight();
                    }
                }
                if (ShieldBar.position.X != shield - 100)
                {
                    if (ShieldBar.position.X > shield - 100)
                    {
                        ShieldBar.MoveLeft();
                    }
                    else
                    {
                        ShieldBar.MoveRight();
                    }
                }

            }

            prevks = ks;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (Alive && Gamestart)
            {
                Background.Draw(spriteBatch);
                for (int i = 0; i < Clouds.Count;i++)
                {
                    Clouds[i].Draw(spriteBatch);
                }
                BackgroundPlatform.Draw(spriteBatch);
                if (boss.Left)
                {
                    boss.Draw(spriteBatch, Content.Load<Texture2D>("BossL"), bosscolor);
                }
                if (boss.Right)
                {
                    boss.Draw(spriteBatch, Content.Load<Texture2D>("BossR"), bosscolor);
                }
                spriteBatch.DrawString(font, "" + boss.health + "", new Vector2(boss.smallHitbox.X + 20, boss.smallHitbox.Y - 20), Color.White);

                for (int i = 0; i < Crate.Count; i++)
                {
                    Crate[i].Draw(spriteBatch);
                }
                for (int i = 0; i < Warning.Count; i++)
                {
                    Warning[i].Draw(spriteBatch);
                }

                if (playershield && !playershieldblinking)
                {
                    playercolor = Color.Blue;
                }
                if (rage)
                {
                    playercolor = Color.Red;
                }
                if (!playershield && !rage)
                {
                    playercolor = Color.White;
                }
                if (shieldblinking > TimeSpan.FromMilliseconds(100))
                {
                    shieldblinking = TimeSpan.Zero;
                    if (playershieldblinking)
                    {
                        if (playercolor == Color.Blue && !playercolorchange)
                        {
                            playercolor = Color.White;
                            playercolorchange = true;
                        }
                        if (playercolor == Color.White && !playercolorchange)
                        {
                            playercolor = Color.Blue;
                            playercolorchange = true;
                        }
                    }
                    playercolorchange = false;
                }

                player.Draw2(spriteBatch, Content.Load<Texture2D>("BoxR"), playercolor);
                //player.Draw2(spriteBatch, Content.Load<Texture2D>("BoxR"), playercolor);

                if (FacingLeft)
                {
                    LegsL.OtherDraw(spriteBatch, playercolor);
                }
                if (FacingRight)
                {
                    LegsR.OtherDraw(spriteBatch, playercolor);
                }
                if (TurretMovement == 1)
                {
                    if (FacingLeft)
                    {
                        TurretFaceLeft = true;
                        TurretFaceRight = false;
                    }
                    if (FacingRight)
                    {
                        TurretFaceRight = true;
                        TurretFaceLeft = false;
                    }
                }

                if (TurretFaceLeft)
                {
                    TurretL.OtherDraw(spriteBatch, playercolor);
                }
                if (TurretFaceRight)
                {
                    TurretR.OtherDraw(spriteBatch, playercolor);
                }
                for (int i = 0; i < platform.Count; i++)
                {
                    if (platform[i].OnSide == false)
                    {
                        platform[i].Draw(spriteBatch);
                    }

                    if (platform[i].OnSide)
                    {
                        platform[i].Draw2(spriteBatch, Content.Load<Texture2D>("SidePlatform"));
                    }
                }

                for (int i = 0; i < fire.Count; i++)
                {
                    fire[i].Draw(spriteBatch);
                }
               
                //spriteBatch.DrawString(font, "Health = " + health + "", new Vector2(100, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "width = " + player.hitbox.Width + "", new Vector2(200, GraphicsDevice.Viewport.Height - 40), Color.White);
<<<<<<< HEAD
                spriteBatch.DrawString(font, "CloudDownSpeed = " + CLoudDownSpeed + "", new Vector2(300, GraphicsDevice.Viewport.Height - 40), Color.White);
                if (enemy.Count > 0)
                {
                    spriteBatch.DrawString(font, "Enemy Ground = " + enemy[0].ground + "", new Vector2(500, GraphicsDevice.Viewport.Height - 40), Color.White);
                    spriteBatch.DrawString(font, "EnemyInPlayerArea = " + enemy[0].InPlayerArea + "", new Vector2(700, GraphicsDevice.Viewport.Height - 40), Color.White);
                }
                
=======
                spriteBatch.DrawString(font, "CloudDownSpeed = " + CLoudDownSpeed + "", new Vector2(200, GraphicsDevice.Viewport.Height - 60), Color.White);
>>>>>>> 712ce9998d453a8dbc039cd7a7fef61edff11243
                HealthBar.Draw(spriteBatch);
                ShieldBar.Draw(spriteBatch);

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
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("PlayerL"), Color.Red);
                        }
                        else
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("PlayerL"), Color.SandyBrown);
                        }

                    }
                    if (enemy[i].Right)
                    {
                        if (enemy[i].attack == true)
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("PlayerR"), Color.Red);
                        }
                        else
                        {
                            enemy[i].Draw(spriteBatch, Content.Load<Texture2D>("PlayerR"), Color.SandyBrown);
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
                Arrow.Draw(spriteBatch);
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
