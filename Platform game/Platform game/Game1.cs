using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Platform_game
{
    //ToDo List:
    //Make everything move in relation to player movements(done exept boss fight)
    //Create Sprites so I can make animations(not happening)
    //Create Levels
    //Clean up code
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
        TimeSpan CloudSpeed = new TimeSpan();

        List<Fireball> fireball = new List<Fireball>();
        List<Enemy> enemy = new List<Enemy>();
        List<Platform> platform = new List<Platform>();
        List<OtherPlatforms> walls = new List<OtherPlatforms>();
        List<Sprite> fire = new List<Sprite>();
        List<FlyingEnemy> plane = new List<FlyingEnemy>();
        List<Sprite> Warning = new List<Sprite>();
        List<Crates> Crate = new List<Crates>();
        List<Moveythingy> Clouds = new List<Moveythingy>();
        List<Sprite> BackgroundPlatform = new List<Sprite>();
        List<Sprite> Sawblade = new List<Sprite>();

        Player player;
        Random random = new Random();
        Boss boss;
        background Background;
        background Failure;
        background Begining;
        background Winner;
        PlayerSprite LegsL;
        PlayerSprite TurretL;
        PlayerSprite TurretR;
        PlayerSprite LegsR;
        Bars HealthBar;
        Bars ShieldBar;
        Color bosscolor = Color.White;
        Color playercolor = Color.White;
        Sprite Arrow;
        Sprite Portal;

        int level = 1;
        int portalspawnx = 4500;
        bool Jumping = false;
        int MoveDown = 25;
        int Moveup = 0;
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
        int MoveSpeed = 5;
        int LeftSide = 0;
        int RightSide = 5000;
        bool BossFight = false;
        bool BossMove = true;
        bool playershield = true;
        int enemySpawnSpeed = 2000;
        bool PlaneDown = false;
        bool launched = false;
        bool playershieldblinking = false;
        bool playercolorchange = false;
        bool BossPlatformsSpawned = false;
        bool Win = false;

        bool makeenemies = true;

        bool MoveScreenLeft = false;
        bool MoveScreenRight = false;
        bool BossAirstrike = false;
        int bossfirelocation = 0;
        bool BossAttack = false;
        bool BossLazerbeam = false;
        bool Bosschargelazer = false;
        bool IsPlayerMoving = true;
        bool playermoveleft = true;
        bool playermoveright = true;
        //bool BossPlatformsInPosition = false;
        bool screenmoveUp = false;
        bool screenmoveDown = false;
        //int CLoudDownSpeed = 0;
        int TurretMovement = 1;
        float tempmovespeedUp = 0;
        //bool Level1 = true;
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
            for (int i = 0; i < 3; i++)
            {
                int CloudY = random.Next(0, GraphicsDevice.Viewport.Height - 105);
                int CloudX = random.Next(5, GraphicsDevice.Viewport.Width - 5);
                Clouds.Add(new Moveythingy(Content.Load<Texture2D>("cloud1"), new Vector2(CloudX, CloudY), Color.White));
            }
            font = (Content.Load<SpriteFont>("font"));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Arrow = new Sprite(Content.Load<Texture2D>("PixelArrow"), new Vector2(0, -50), Color.White);
            Portal = new Sprite(Content.Load<Texture2D>("blue portal"), new Vector2(RightSide - 83, GraphicsDevice.Viewport.Height - 175), Color.White);
            Begining = new background(Content.Load<Texture2D>("Start"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Background = new background(Content.Load<Texture2D>("background"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Failure = new background(Content.Load<Texture2D>("Failure"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            Winner = new background(Content.Load<Texture2D>("Win Screen"), Vector2.One, Color.White, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
           
        
            player = new Player(Content.Load<Texture2D>("playerL"), new Vector2(GraphicsDevice.Viewport.Width - 548, GraphicsDevice.Viewport.Height - 175), Color.White);
            boss = new Boss(Content.Load<Texture2D>("BossL"), new Vector2(-1000, GraphicsDevice.Viewport.Height - 395), Color.White, true, false, 20, -25, 100);
            TurretL = new PlayerSprite(Content.Load<Texture2D>("TurretL"), new Vector2(0, 0), Color.White);
            LegsL = new PlayerSprite(Content.Load<Texture2D>("LegsL"), new Vector2(0, 0), Color.White);
            TurretR = new PlayerSprite(Content.Load<Texture2D>("TurretR"), new Vector2(0, 0), Color.White);
            LegsR = new PlayerSprite(Content.Load<Texture2D>("LegsR"), new Vector2(0, 0), Color.White);
            HealthBar = new Bars(Content.Load<Texture2D>("HealthBar"), new Vector2(0, GraphicsDevice.Viewport.Height - 40), Color.White);
            ShieldBar = new Bars(Content.Load<Texture2D>("ShieldBar"), new Vector2(0, GraphicsDevice.Viewport.Height - 40), Color.White);

            //enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerL"), new Vector2(1500, BackgroundPlatform[0].position.Y - 2), Color.White, false, false, -25, 20, true, false, false, false));
            for (int i = 0; i <= RightSide; i += 1150)
            {
                BackgroundPlatform.Add(new Sprite(Content.Load<Texture2D>("BackgroundPlatform"), new Vector2(i, GraphicsDevice.Viewport.Height - 76), Color.White));
            }
            //BackgroundPlatform.Add(new Sprite(Content.Load<Texture2D>("BackgroundPlatform"), new Vector2(-1105, GraphicsDevice.Viewport.Height - 76), Color.White));

            for (int i = 0; i < RightSide; i += 140)
            {
                walls.Add(new OtherPlatforms(Content.Load<Texture2D>("platform"), new Vector2(i, -200), Color.Brown, false));
            }
            for (int i = GraphicsDevice.Viewport.Height-75; i > -200;i-=140)
            {
                walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(0, i), Color.Brown, true));
            }
            for (int i = GraphicsDevice.Viewport.Height - 75; i > -200; i -= 140)
            {
                walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(4970, i), Color.Brown, true));
            }


            
            //Generate Lvl 1


            /*for (int i = 800; i < RightSide-1500; i += 140)
            {
                walls.Add(new OtherPlatforms(Content.Load<Texture2D>("platform"), new Vector2(i, 200), Color.Brown, false));
            }       
            walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(800, 230), Color.Brown, true));
            walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(600, GraphicsDevice.Viewport.Height-230), Color.Brown, true));*/
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
            CloudSpeed += gameTime.ElapsedGameTime;
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
            if (CloudSpeed >= TimeSpan.FromMilliseconds(10))
            {
                CloudSpeed = TimeSpan.Zero;
                for (int i = 0;i<Clouds.Count;i++)
                {
                    Clouds[i].MoveRight(2);
                    Clouds[i].MoveDown(1);
                }
            }
            if (second >= TimeSpan.FromMilliseconds(1000))
            {
                second = TimeSpan.Zero;
                if (shield <= 100 && playershield)
                {
                    shield += 1;
                }
                    if (FacingLeft)
                    {
                        int CloudStartPositionY = random.Next(-120, GraphicsDevice.Viewport.Height - 75);
                        int CloudStartPositionX = -120;
                        Clouds.Add(new Moveythingy(Content.Load<Texture2D>("cloud1"), new Vector2(CloudStartPositionX, CloudStartPositionY), Color.White));
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
                Win = false;
                OnPlatform = false;
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
                player.position.Y = GraphicsDevice.Viewport.Height - 175;
                player.position.X = GraphicsDevice.Viewport.Width - 440;
                boss.position.X = -1000;
                boss.health = 100;
                BossFight = false;
                BossPlatformsSpawned = false;
                playershield = true;
                shield = 100;
                BossMove = false;
                Warning.Clear();
                BossAirstrike = false;
                BossLazerbeam = false;
                BossAttack = false;
                Crate.Clear();
                MoveDown = 25;
                Moveup = 0;
                LeftSide = 0;
                RightSide = 5000;
                Portal.position.X = RightSide - 83;
                Portal.position.Y = GraphicsDevice.Viewport.Height - 175;
                BackgroundPlatform.Clear();
                for (int i = 0; i <= RightSide; i += 1150)
                {
                    BackgroundPlatform.Add(new Sprite(Content.Load<Texture2D>("BackgroundPlatform"), new Vector2(i, GraphicsDevice.Viewport.Height - 76), Color.White));
                }
                boss.SmallHitbox();
                walls.Clear();
                for (int i = 0; i < RightSide; i += 140)
                {
                    walls.Add(new OtherPlatforms(Content.Load<Texture2D>("platform"), new Vector2(i, -130), Color.Brown, false));
                }
                for (int i = GraphicsDevice.Viewport.Height - 75; i > -200; i -= 140)
                {
                    walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(0, i), Color.Brown, true));
                }
                for (int i = GraphicsDevice.Viewport.Height - 75; i > -200; i -= 140)
                {
                    walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(4970, i), Color.Brown, true));
                }
                if (level == 1)
                {

                   /* for (int i = 800; i < RightSide - 1500; i += 140)
                    {
                        walls.Add(new OtherPlatforms(Content.Load<Texture2D>("platform"), new Vector2(i, 200), Color.Brown, false));
                    }
                    walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(800, 230), Color.Brown, true));
                    walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sidePlatform"), new Vector2(600, GraphicsDevice.Viewport.Height - 230), Color.Brown, true));*/
                }
            }
            
            if (Gamestart == false && ks.IsKeyDown(Keys.Space))
            {
                Gamestart = true;
            }
            if (Alive && Gamestart&&!Win)
            {
               
                for (int i = 0; i < Clouds.Count; i ++)
                {                  
                    if (Clouds[i].position.Y+Clouds[i].hitbox.Height>GraphicsDevice.Viewport.Height+240)
                    {
                        Clouds.Remove(Clouds[i]);
                        break;
                    }
                    if (Clouds[i].position.Y+240<0)
                    {
                        Clouds.Remove(Clouds[i]);
                        break;
                    }
                    if (Clouds[i].position.X > GraphicsDevice.Viewport.Width+240)
                    {
                        Clouds.Remove(Clouds[i]);
                        break;
                    }
                    if (Clouds[i].position.X+Clouds[i].hitbox.Width+240<0)
                    {
                        Clouds.Remove(Clouds[i]);
                        break;
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
                if (ks.IsKeyDown(Keys.LeftShift) && prevks.IsKeyUp(Keys.LeftShift)&&BossFight)
                {
                    if (ks.IsKeyDown(Keys.Down))
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(player.position.X, player.position.Y + player.hitbox.Height), Color.White, false));
                    }
                    else if (IsPlayerMoving == false && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Right))
                    {
                        platform.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(player.position.X, player.position.Y - player.hitbox.Height), Color.White, false));
                    }
                    else
                    {
                        if (ks.IsKeyDown(Keys.Left))
                        {
                            platform.Add(new Platform(Content.Load<Texture2D>("sidePlatform"), new Vector2(player.position.X - player.hitbox.Width, player.position.Y), Color.White, false));
                        }
                        if (ks.IsKeyDown(Keys.Right))
                        {
                            platform.Add(new Platform(Content.Load<Texture2D>("sidePlatform"), new Vector2(player.position.X + player.hitbox.Width * 2, player.position.Y), Color.White, false));
                        }
                    } 
                }
                if (player.position.X<200)
                {
                    MoveScreenLeft = true;
                    MoveScreenRight = false;
                }
                if (player.position.X+player.hitbox.Width>800)
                {
                    MoveScreenRight = true;
                    MoveScreenLeft = false;
                }
                if (LeftSide >= 0)
                {
                    MoveScreenLeft = false;
                }
                if (RightSide <= GraphicsDevice.Viewport.Width)
                {
                    MoveScreenRight = false;
                }
                IsPlayerMoving = false;
                if (ks.IsKeyDown(Keys.Right) && playermoveright && !MoveScreenRight)
                {
                    player.MoveRight(MoveSpeed,GraphicsDevice.Viewport.Width);
                    MoveScreenRight = false;
                    MoveScreenLeft = false;
                    FacingLeft = false;
                    FacingRight = true;
                    IsPlayerMoving = true;
                }
                if (ks.IsKeyDown(Keys.Left) && playermoveleft &&!MoveScreenLeft)
                {
                    player.MoveLeft(MoveSpeed);
                    MoveScreenLeft = false;
                    MoveScreenRight = false;
                    FacingLeft = true;
                    FacingRight = false;
                    IsPlayerMoving = true;
                }
                boss.SmallHitbox();
                if (ks.IsKeyDown(Keys.Right) && MoveScreenRight && playermoveright && launched == false)
                {
                    for (int i = 0; i < Clouds.Count; i++)
                    {
                        Clouds[i].MoveLeft(MoveSpeed);
                    }
                    for (int i = 0;i<platform.Count;i++)
                    {
                        platform[i].MoveLeft(MoveSpeed);
                    }
                    for (int i = 0; i < enemy.Count; i++)
                    {
                        enemy[i].othermoveLeft(MoveSpeed);
                    }
                    for (int i = 0; i < fireball.Count; i++)
                    {
                        fireball[i].otherMoveLeft(MoveSpeed);
                    }
                    for (int i = 0; i < Crate.Count;i++)
                    {
                        Crate[i].MoveLeft(MoveSpeed);
                    }
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].position.X -= MoveSpeed;
                    }
                    RightSide -= MoveSpeed;
                    LeftSide -= MoveSpeed;
                    if (BossFight)
                    {
                        boss.position.X -= MoveSpeed;
                        bossfirelocation -= MoveSpeed;
                        
                        for (int i = 0;i<Warning.Count;i++)
                        {
                            Warning[i].position.X -= MoveSpeed;
                        }
                    }
                    Portal.position.X -= MoveSpeed;
                    for (int  i = 0;i<walls.Count;i++)
                    {
                        walls[i].position.X -= MoveSpeed;
                    }
                    portalspawnx -= MoveSpeed;
                    FacingLeft = false;
                    FacingRight = true;
                    IsPlayerMoving = true; 
                }
                else if (ks.IsKeyDown(Keys.Left) && playermoveleft && MoveScreenLeft&&launched == false)
                {
                    for (int i = 0;i<Clouds.Count;i++)
                    {
                        Clouds[i].MoveRight(MoveSpeed);
                    }
                    for (int i = 0;i<platform.Count;i++)
                    {
                        platform[i].MoveRight(MoveSpeed);
                    }
                    for (int i = 0;i<enemy.Count;i++)
                    {
                        enemy[i].othermoveRight(MoveSpeed);
                    }
                    for (int i = 0;i<fireball.Count;i++)
                    {
                        fireball[i].otherMoveRight(MoveSpeed);
                    }
                    for (int i = 0; i < Crate.Count; i++)
                    {
                        Crate[i].MoveRight(MoveSpeed);
                    }
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].position.X += MoveSpeed;
                    }
                    RightSide += MoveSpeed;
                    LeftSide += MoveSpeed;
                    if (BossFight)
                    {
                        boss.position.X += MoveSpeed;
                        bossfirelocation += MoveSpeed;
                        
                        for (int i = 0; i < Warning.Count; i++)
                        {
                            Warning[i].position.X += MoveSpeed;
                        }
                    }
                    Portal.position.X += MoveSpeed;
                    for (int i = 0; i < walls.Count; i++)
                    {
                        walls[i].position.X += MoveSpeed;
                    }
                    portalspawnx += MoveSpeed;
                    FacingLeft = true;
                    FacingRight = false;
                    IsPlayerMoving = true;
                }

                if (launchedTime < TimeSpan.FromMilliseconds(900) && launched == true)
                {
                    if (boss.Right)
                    {
                        for (int i = 0; i < Clouds.Count; i++)
                        {
                            Clouds[i].MoveLeft(MoveSpeed * 2);
                        }
                        for (int i = 0; i < platform.Count; i++)
                        {
                            platform[i].MoveLeft(MoveSpeed * 2);
                        }
                        for (int i = 0; i < enemy.Count; i++)
                        {
                            enemy[i].othermoveLeft(MoveSpeed * 2);
                        }
                        for (int i = 0; i < fireball.Count; i++)
                        {
                            fireball[i].otherMoveLeft(MoveSpeed * 2);
                        }
                        for (int i = 0; i < Crate.Count; i++)
                        {
                            Crate[i].MoveLeft(MoveSpeed * 2);
                        }
                        for (int i = 0; i < BackgroundPlatform.Count; i++)
                        {
                            BackgroundPlatform[i].position.X -= MoveSpeed * 2;
                        }
                        RightSide -= MoveSpeed * 2;
                        LeftSide -= MoveSpeed * 2;
                        if (BossFight)
                        {
                            boss.position.X -= MoveSpeed * 2;
                            bossfirelocation -= MoveSpeed * 2;
                            
                            for (int i = 0; i < Warning.Count; i++)
                            {
                                Warning[i].position.X -= MoveSpeed * 2;
                            }
                        }
                        Portal.position.X -= MoveSpeed * 2;
                        for (int i = 0; i < walls.Count; i++)
                        {
                            walls[i].position.X -= MoveSpeed * 2;
                        }
                        portalspawnx -= MoveSpeed * 2;
                        FacingLeft = false;
                        FacingRight = true;
                        IsPlayerMoving = true;
                    }
                    if (boss.Left)
                    {
                        for (int i = 0; i < Clouds.Count; i++)
                        {
                            Clouds[i].MoveRight(MoveSpeed * 2);
                        }
                        for (int i = 0; i < platform.Count; i++)
                        {
                            platform[i].MoveRight(MoveSpeed * 2);
                        }
                        for (int i = 0; i < enemy.Count; i++)
                        {
                            enemy[i].othermoveRight(MoveSpeed * 2);
                        }
                        for (int i = 0; i < fireball.Count; i++)
                        {
                            fireball[i].otherMoveRight(MoveSpeed * 2);
                        }
                        for (int i = 0; i < Crate.Count; i++)
                        {
                            Crate[i].MoveRight(MoveSpeed * 2);
                        }
                        for (int i = 0; i < BackgroundPlatform.Count; i++)
                        {
                            BackgroundPlatform[i].position.X += MoveSpeed * 2;
                        }
                        RightSide += MoveSpeed * 2;
                        LeftSide += MoveSpeed * 2;
                        if (BossFight)
                        {
                            boss.position.X += MoveSpeed * 2;
                            bossfirelocation += MoveSpeed * 2;
                            
                            for (int i = 0; i < Warning.Count; i++)
                            {
                                Warning[i].position.X += MoveSpeed * 2;
                            }
                        }
                       
                        for (int i = 0; i < walls.Count; i++)
                        {
                            walls[i].position.X += MoveSpeed * 2;
                        }
                        portalspawnx += MoveSpeed * 2;
                        FacingLeft = true;
                        FacingRight = false;
                        IsPlayerMoving = true;
                    }
                }
                else
                {
                    launched = false;
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

                if (player.position.Y<50)
                {
                    screenmoveUp = true;
                    screenmoveDown = false;
                }
                else
                {
                    screenmoveUp = false;
                }
                if (player.position.Y+player.hitbox.Height>GraphicsDevice.Viewport.Height-75)
                {
                   
                    screenmoveDown = true;
                    screenmoveUp = false;
                }
                else
                {
                    screenmoveDown = false;
                }
                
                
                if (!Jumping&&!Ground&&!screenmoveDown)
                {
                    Moveup--;
                    player.position.Y -= Moveup;
                }
                if (Jumping&&!screenmoveUp)
                {
                    MoveDown--;
                    player.position.Y -= MoveDown;
                }
                

                if (!Jumping&&!Ground&&screenmoveDown)
                {
                    Moveup--;
                    for (int i = 0; i < Clouds.Count; i++)
                    {
                        Clouds[i].position.Y += Moveup;
                    }
                    for (int i = 0; i < platform.Count; i++)
                    {
                        platform[i].position.Y += Moveup;
                    }
                    for (int i = 0; i < enemy.Count; i++)
                    {
                        enemy[i].position.Y += Moveup;
                    }
                    for (int i = 0; i < fireball.Count; i++)
                    {
                        fireball[i].position.Y += Moveup;
                    }
                    for (int i = 0; i < Crate.Count; i++)
                    {
                        Crate[i].position.Y += Moveup;
                    }
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].position.Y += Moveup;
                    }
                    if (BossFight)
                    {
                        boss.position.Y += Moveup;
                        bossfirelocation += Moveup;
                        for (int i = 0; i < Warning.Count; i++)
                        {
                            Warning[i].position.Y += Moveup;
                        }
                    }
                    Portal.position.Y += Moveup;
                    for (int i = 0; i < walls.Count; i++)
                    {
                        walls[i].position.Y += Moveup;
                    }
                }
                if (ks.IsKeyDown(Keys.Up) && Jumping == false && Ground == true)
                {

                    Ground = false;
                    Jumping = true;
                    Moveup = 0;
                    MoveDown = 25;
                }
                
                if (Jumping&&screenmoveUp)
                {
                    MoveDown--;
                    for (int i = 0; i < Clouds.Count; i++)
                    {
                        Clouds[i].position.Y += MoveDown;
                    }
                    for (int i = 0; i < platform.Count; i++)
                    {
                        platform[i].position.Y += MoveDown;
                    }
                    for (int i = 0; i < enemy.Count; i++)
                    {
                        enemy[i].position.Y += MoveDown;
                    }
                    for (int i = 0; i < fireball.Count; i++)
                    {
                        fireball[i].position.Y += MoveDown;
                    }
                    for (int i = 0; i < Crate.Count; i++)
                    {
                        Crate[i].position.Y += MoveDown;
                    }
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].position.Y += MoveDown;
                    }
                    if (BossFight)
                    {
                        boss.position.Y += MoveDown;
                        bossfirelocation += MoveDown;
                        for (int i = 0; i < Warning.Count; i++)
                        {
                            Warning[i].position.Y += MoveDown;
                        }
                    }
                    Portal.position.Y += MoveDown;
                    for (int i = 0; i < walls.Count; i++)
                    {
                        walls[i].position.Y += MoveDown;
                    }
                }
                if (player.position.Y + player.hitbox.Height > GraphicsDevice.Viewport.Height - 75&&Ground)
                {
                   tempmovespeedUp = player.position.Y + player.hitbox.Height - BackgroundPlatform[0].position.Y;
                }
                if (MoveDown == 0)
                {
                    Jumping = false;
                    Ground = false;
                    MoveDown = 25;
                }
                if (Moveup < -20)
                {
                    Moveup = -20;
                }
                playermoveright = true;
                playermoveleft = true;
                OnPlatform = false;
                OnPlatform = false;
                for (int w = 0; w < walls.Count; w++)
                {

                    walls[w].Update();
                    if (player.Hit(walls[w].bottom))
                    {
                        Jumping = false;
                        launched = false;
                    }


                    if (player.Hit(walls[w].RightSide))
                    {
                        playermoveright = false;
                        player.position.X = walls[w].position.X-player.hitbox.Width;
                        launched = false;
                    }

                    if (player.Hit(walls[w].LeftSide))
                    {
                        playermoveleft = false;
                        launched = false;
                        player.position.X = walls[w].position.X + walls[w].hitbox.Width;
                    }



                    if (player.Hit(walls[w].top) && OnPlatform == false)
                    {
                        Ground = true;
                        OnPlatform = true;


                        player.position.Y = walls[w].position.Y - player.hitbox.Height;
                        Moveup = 0;
                    }

                }
                

                for (int i = 0; i < platform.Count; i++)
                {
                   

                    platform[i].Update();
                    if (player.Hit(platform[i].bottom))
                    {
                        Jumping = false;

                    }
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
                        

                    
                    if (player.position.Y > 10000)
                    {
                        Alive = false;
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

                      
                        player.position.Y = platform[i].position.Y - player.hitbox.Height;
                        Moveup = 0;
                    }

                    
                }

                


                if (!OnPlatform)
                {
                    Ground = false;
                }
                
                

                for (int bp = 0; bp < BackgroundPlatform.Count; bp++)
                {
                    if (player.hitbox.Intersects(BackgroundPlatform[bp].hitbox))
                    {
                        Ground = true;
                        if (!screenmoveDown)
                        {
                            player.position.Y = BackgroundPlatform[0].position.Y - player.hitbox.Height;
                        }
                        else
                        {
                            tempmovespeedUp = player.position.Y + player.hitbox.Height - BackgroundPlatform[0].position.Y;
                        }
                        
                    }
                }
                if (tempmovespeedUp!=0)
                {
                    for (int i = 0; i < Clouds.Count; i++)
                    {
                        Clouds[i].position.Y += tempmovespeedUp;
                    }
                    for (int i = 0; i < platform.Count; i++)
                    {
                        platform[i].position.Y += tempmovespeedUp;
                    }
                    for (int i = 0; i < enemy.Count; i++)
                    {
                        enemy[i].position.Y += tempmovespeedUp;
                    }
                    for (int i = 0; i < fireball.Count; i++)
                    {
                        fireball[i].position.Y += tempmovespeedUp;
                    }
                    for (int i = 0; i < Crate.Count; i++)
                    {
                        Crate[i].position.Y += tempmovespeedUp;
                    }
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].position.Y += tempmovespeedUp;
                    }
                    if (BossFight)
                    {
                        boss.position.Y += tempmovespeedUp;
                        for (int i = 0; i < Warning.Count; i++)
                        {
                            Warning[i].position.Y += tempmovespeedUp;
                        }
                    }
                    Portal.position.Y += tempmovespeedUp;
                    for (int i = 0; i < walls.Count; i++)
                    {
                        walls[i].position.Y += tempmovespeedUp;
                    }
                    tempmovespeedUp = 0;
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



                
                //enemy spawning
                if (enemySpawn > TimeSpan.FromMilliseconds(enemySpawnSpeed) && !BossFight && makeenemies)
                {
                    enemySpawn = TimeSpan.Zero;
                    int ran = random.Next(0, 21);
                    if (ran < 8)
                    {
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerL"), new Vector2(GraphicsDevice.Viewport.Width, -100), Color.White, false, false, -25, 20, true, false, false, false));
                    }
                    if (ran >= 3 && ran <= 17)
                    {
                        enemy.Add(new Enemy(Content.Load<Texture2D>("PlayerR"), new Vector2(-98, -100), Color.White, false, false, -25, 20, true, false, false, false));
                    }
                    if (ran == 18)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeR"), new Vector2(-10, 0), Color.White, false, true, 2));
                    }
                    if (ran == 19)
                    {
                        plane.Add(new FlyingEnemy(Content.Load<Texture2D>("planeL"), new Vector2(GraphicsDevice.Viewport.Width, 0), Color.White, true, false, 2));
                    }
                }
                
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (enemy[i].hit(player.hitbox))
                    {
                        enemy[i].jumping = true;
                        enemy[i].downspeed = 0;
                        enemy[i].upspeed = -25;
                        enemy[i].ground = false;
                    }
                    if (enemy[i].DownyDown())
                    {
                        enemy[i].MoveDown();
                        enemy[i].downspeed += 1;
                    }
                    enemy[i].UpdateHitbox();
                    for (int bp = 0; bp < BackgroundPlatform.Count; bp++)
                    {
                        enemy[i].Ground(GraphicsDevice.Viewport.Height - 70, BackgroundPlatform[bp].hitbox);
                    }
                    if (enemy[i].Left && enemy[i].move)
                    {
                        enemy[i].MoveLeft();
                    }
                    if (enemy[i].Right && enemy[i].move)
                    {
                        enemy[i].MoveRight();
                    }
                    if (enemy[i].position.X<LeftSide)
                    {
                        enemy[i].position.X = LeftSide;
                    }
                    if (enemy[i].position.X+enemy[i].hitbox.Width>RightSide)
                    {
                        enemy[i].position.X = RightSide-enemy[i].hitbox.Width;
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
                        {
                            enemy[i].position.X = platform[p].position.X - 2 - enemy[i].hitbox.Width;
                        }
                        if (enemy[i].hit(platform[p].LeftSide)&&enemy[i].Left)
                        {
                            enemy[i].position.X = platform[p].position.X + platform[p].hitbox.Width + 2;
                        }
                       

                    }
                    for (int p = 0; p < walls.Count; p++)
                    {
                        
                        if (enemy[i].hit(walls[p].bottom))
                        {
                            enemy[i].jumping = false;
                        }
                        if (enemy[i].hit(walls[p].top) && enemy[i].OnPlatform == false)
                        {
                            enemy[i].OnPlatform = true;
                            enemy[i].ground = true;
                            enemy[i].downspeed = 0;
                        }
                        if (enemy[i].OnPlatform == true)
                        {
                            enemy[i].position.Y = walls[p].position.Y - enemy[i].hitbox.Height;
                            if (!enemy[i].hit(walls[p].top))
                            {
                                enemy[i].ground = false;
                                enemy[i].OnPlatform = false;
                            }
                        }


                        if (enemy[i].hit(walls[p].RightSide) && enemy[i].Right)
                        {
                            enemy[i].position.X = walls[p].position.X - 2 - enemy[i].hitbox.Width;
                        }
                        if (enemy[i].hit(walls[p].LeftSide) && enemy[i].Left)
                        {
                            enemy[i].position.X = walls[p].position.X + walls[p].hitbox.Width + 2;
                        }


                    }


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
                        if (enemy[i].BigHit(fireball[x].hitbox) && fireball[x].IsPLayer)
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
                
                for (int i = 0; i < enemy.Count; i++)
                {
                    enemy[i].UpdateHitbox();
                    if (enemy[i].BigHit(player.hitbox))
                    {
                        enemy[i].state = Enemy.State.Run;
                    }
                    if (enemy[i].otherHit(player.hitbox)&&!enemy[i].BigHit(player.hitbox))
                    {
                        enemy[i].state = Enemy.State.Shoot;
                    }
                    if (!enemy[i].otherHit(player.hitbox))
                    {
                        enemy[i].state = Enemy.State.Follow;
                    }
                    if (enemy[i].state == Enemy.State.Follow)
                    {
                        enemy[i].move = true;
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
                        enemy[i].move = true;
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
                    }
                    if (enemy[i].state == Enemy.State.Shoot)
                    {
                        enemy[i].move = false;
                        if (enemy[i].hitbox.X > player.hitbox.X)
                        {
                            enemy[i].Left = true;
                            enemy[i].Right = false;
                        }
                        if (enemy[i].hitbox.X < player.hitbox.X)
                        {
                            enemy[i].Right = true;
                            enemy[i].Left = false;
                        }
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
                    for (int p = 0; p < walls.Count; p++)
                    {
                        if (fireball[i].Hit(walls[p].hitbox))
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
                        break;
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
                
                if (RightSide == 1600&&BossPlatformsSpawned==false)
                {
                    BossFight = true;
                    boss.position.X = 1240;
                    for (int y = GraphicsDevice.Viewport.Height - 75; y > -200; y -= 140)
                    {
                        walls.Add(new OtherPlatforms(Content.Load<Texture2D>("sideplatform"), new Vector2(210, y), Color.Brown, true));
                    }
                    Portal.position.X = 80000;
                    BossPlatformsSpawned = true;
                }

                
                
                if (BossFight)
                {
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

                    /*if (BossAirstrike)
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
                    }*/
                    if (BossLazerbeam)
                    {
                        BossMove = false;
                        for (int i = 0; i < 23; i++)
                        {
                            if (boss.Left)
                            {
                                fireball.Add(new Fireball(Content.Load<Texture2D>("FireballLeft"), new Vector2(boss.smallHitbox.X, boss.smallHitbox.Y+50+i*10), Color.White, true, false, false, false));
                            }
                            if (boss.Right)
                            {
                                fireball.Add(new Fireball(Content.Load<Texture2D>("FireballRight"), new Vector2(boss.smallHitbox.X + 50, boss.smallHitbox.Y+50+i*10), Color.White, false, true, false, false));
                            }
                        }
                    }
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
                        Moveup = 0;
                        MoveDown = 25;
                    }
                    else if (boss.otherHit(player.hitbox))
                    {
                        launched = true;
                        Ground = false;
                        Jumping = true;
                        Moveup = 0;
                        MoveDown = 25;
                        launchedTime = TimeSpan.Zero;
                        if (!rage)
                        {
                            if (shield > 0)
                            {
                                shield -= 10;
                            }
                            else
                            {
                                health -= 10;
                            }
                            
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
                        Portal.position.X = portalspawnx;
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
                    Crate[i].MoveDown(BackgroundPlatform[0].position.Y);
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

                if (player.Hit(Portal.hitbox))
                {
                    Win = true;
                }

            }

            prevks = ks;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (Alive && Gamestart&&!Win)
            {
                Background.Draw(spriteBatch);
                for (int i = 0; i < Clouds.Count;i++)
                {
                    Clouds[i].Draw(spriteBatch);
                }
              
                    for (int i = 0; i < BackgroundPlatform.Count; i++)
                    {
                        BackgroundPlatform[i].Draw(spriteBatch);
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


                for (int i = 0; i < Crate.Count; i++)
                {
                    Crate[i].Draw(spriteBatch);
                }
                for (int i = 0; i < Warning.Count; i++)
                {
                    Warning[i].Draw(spriteBatch);
                }
                Portal.Draw(spriteBatch);
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

                for (int i = 0; i < walls.Count; i++)
                {
                    if (walls[i].OnSide == false)
                    {
                        walls[i].Draw(spriteBatch);
                    }

                    if (walls[i].OnSide)
                    {
                        walls[i].Draw2(spriteBatch, Content.Load<Texture2D>("SidePlatform"));
                    }
                }

                for (int i = 0; i < fire.Count; i++)
                {
                    fire[i].Draw(spriteBatch);
                }
                MouseState ms = Mouse.GetState();
                spriteBatch.DrawString(font, "boss smallhitbox x = " + boss.smallHitbox.X + "", new Vector2(500, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "Boss Position x = " + boss.position.X + "", new Vector2(200, GraphicsDevice.Viewport.Height - 40), Color.White);
                spriteBatch.DrawString(font, "portal x = " + Portal.hitbox.X + "", new Vector2(800, GraphicsDevice.Viewport.Height - 40), Color.White);
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
                
                for (int i = 0;i < Sawblade.Count; i++)
                {
                    Sawblade[i].Draw(spriteBatch);
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
                if (Win)
                {
                    Winner.Draw(spriteBatch);
                    //spriteBatch.DrawString(font, "Press R to Play Again", new Vector2(430, 50), Color.DarkOrange);
                    spriteBatch.DrawString(font, "score = " + score + "", new Vector2(460, 80), Color.FloralWhite);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
