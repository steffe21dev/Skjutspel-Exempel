using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D[] main_character;
        Texture2D[] enemies_tex;
        List<Bullet> bullets;
        List<Enemy> fiender;
        Texture2D background,bullet;
        Character player;
        bool isShoot;
        float lastShot;
        Random random;

        public Game1()
        {
            main_character = new Texture2D[2];
            enemies_tex = new Texture2D[2];
            bullets = new List<Bullet>();
            isShoot = false;
            fiender = new List<Enemy>();
            random = new Random();

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            base.Initialize();

            player = new Character(new Vector2(100, 640), new Rectangle(100, 640, 200, 150), true);

        }

  
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            main_character[0] = Content.Load<Texture2D>("Höger-Gubbe");
            main_character[1] = Content.Load<Texture2D>("Vänster-Gubbe");
            enemies_tex[0] = Content.Load<Texture2D>("Höger-Fiende");
            enemies_tex[1] = Content.Load<Texture2D>("Vänster-Fiende");
            background = Content.Load<Texture2D>("Background");
            bullet = Content.Load<Texture2D>("bullet");




        }

        protected override void UnloadContent()
        {
        }

      
        protected override void Update(GameTime gameTime)
        {



            if(fiender.Count < 2)
            {
                int rn_x = random.Next(-100,1380);
                fiender.Add(new Enemy(new Vector2(rn_x, 640), new Rectangle(rn_x, 640, 200, 150), true));
            }

            HitDetection();

            Listen_Input();

            ///Skottlogiken
            UpdateBullet();



            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin();

            


            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 768), Color.White);



            for (int i = 0; i < bullets.Count; i++)
            {
                spriteBatch.Draw(bullet, bullets[i].rectangle, Color.White);
            }



            for (int i = 0; i < fiender.Count; i++)
            {
                if(fiender[i].isRight)
                    spriteBatch.Draw(enemies_tex[0], fiender[i].rec, Color.White);
                else
                    spriteBatch.Draw(enemies_tex[1], fiender[i].rec, Color.White);

            }




            if (player.isRight)
                spriteBatch.Draw(main_character[0],player.rec, Color.White);
            else
                spriteBatch.Draw(main_character[1], player.rec, Color.White);



           
            spriteBatch.End();



            base.Draw(gameTime);
        }


        protected void HitDetection()
        {
            try
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    for (int j = 0; j < fiender.Count; j++)
                    {
                        if (bullets[i].rectangle.Intersects(fiender[j].rec))
                        {

                            bullets.RemoveAt(i);
                            fiender[j].hp -= 20;

                            if (fiender[j].hp <= 0)
                                fiender.RemoveAt(j);
                        }

                    }

                }
            }
            catch (Exception)
            {

            }
        }

        protected void UpdateBullet()
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].pos = new Vector2(bullets[i].pos.X + bullets[i].speed, bullets[i].pos.Y);
                bullets[i].rectangle = new Rectangle((int)bullets[i].pos.X, (int)bullets[i].pos.Y,10,10);

            }

        }


        protected void Listen_Input()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.pos = new Vector2(player.pos.X + 3, player.pos.Y);
                player.rec = new Rectangle((int)player.pos.X, (int)player.pos.Y, 200, 150);
                player.isRight = true;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.pos = new Vector2(player.pos.X - 3, player.pos.Y);
                player.rec = new Rectangle((int)player.pos.X, (int)player.pos.Y, 200, 150);
                player.isRight = false;

            }


            //Shoot
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (!isShoot)
                {
                    if (player.isRight)
                    {
                        bullets.Add(new Bullet(new Vector2(player.pos.X + 170, player.pos.Y + 63), new Rectangle((int)player.pos.X, (int)player.pos.Y, 10, 10), 20));
                    }
                    else
                    {
                        bullets.Add(new Bullet(new Vector2(player.pos.X + 30, player.pos.Y + 63), new Rectangle((int)player.pos.X, (int)player.pos.Y, 10, 10), -20));

                    }
                }

                isShoot = true;
            }


            if (Keyboard.GetState().IsKeyUp(Keys.Space))
                isShoot = false;

        }
    }
}
