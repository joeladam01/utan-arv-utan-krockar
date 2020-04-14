using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;





namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Mine> enemies;
        List<Tripod> enemies2;
        SpriteFont Arial;
        static ExcelWorksheet sheet;
        static ExcelPackage package;
        static FileInfo file;
        int raknare;





        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 1500;
            graphics.ApplyChanges();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            file = new FileInfo(@"C:\Users\Joel\Desktop\Uanarvutankrockar.xlsx");

            package = new ExcelPackage(file);

            sheet = package.Workbook.Worksheets.Add("blad1");

            raknare = 1;

            sheet.Cells[$"A{raknare}"].Value = "Update";
            sheet.Cells[$"B{raknare++}"].Value = "Draw";

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies2 = new List<Tripod>();
            enemies = new List<Mine>();
            Random random = new Random();
            Texture2D tmpSprite =
                Content.Load<Texture2D>("mine");
            int posX = 0;
            int posY = 0; 
            for (int i = 0; i < 100; i++)
            {
                if(posX < 1350)
                {
                    posX += 150;
                }
                else
                {
                    posX = 75;
                    posY += 150;
                }
                Mine temp = new Mine(tmpSprite, posX, posY, 6f, 1f);
                enemies.Add(temp);
                
            }
            tmpSprite = Content.Load<Texture2D>("tripod");
            int posX1 = 75;
            int posY1 = 75;
            for (int i = 0; i <100; i++)
            {
                if (posX1 < 1350)
                {
                    posX1 += 150;
                }
                else
                {
                    posX1 = 75;
                    posY1 += 150;
                }
                Tripod temp = new Tripod(tmpSprite, posX1, posY1, 0f, 3f);
                enemies2.Add(temp);
            }
            Arial = Content.Load<SpriteFont>("Fonts/Arial");

            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (Mine m in enemies.ToList())
            {
                if (m.IsAlive)
                { 
                    m.Update(Window);
                }
                
            }
            foreach (Tripod t in enemies2.ToList())
            {
                if (t.IsAlive)
                {
                    t.Update(Window);
                }

            }



            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
   
 
            //Kontrollera ifall rymdskeppet har åkt ut från kanten, om det //
            //har det, så återställ dess position.
            //Har det åkt ut till vänster:



            base.Update(gameTime);
            watch.Stop();
            var elapsedMS = watch.ElapsedTicks;
            if (raknare <= 1200)
            {
                sheet.Cells[$"A{raknare}"].Value = elapsedMS;
            }
            else
            {
                package.Save();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Mine m in enemies)
                m.Draw(spriteBatch);
            foreach (Tripod t in enemies2)
                t.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(Arial, "antal fiender" + enemies.Count, new Vector2(0, 0), Color.White);

           
            spriteBatch.End();
            watch.Stop();
            var elapsedMS = watch.ElapsedTicks;
            if (raknare <= 1200)
            {
                sheet.Cells[$"B{raknare++}"].Value = elapsedMS;
            }
        }
    }
}



