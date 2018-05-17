//using Game6._2D;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using static Game6.Split_Screen_Dungeon.Backpack;
//using Game6.Split_Screen_Dungeon;

//namespace Game6.FPSWahtever
//{
//    public class SmallFPSMenu : fuckwhit_no_cursing
//    {
//        SmallFPS fpsGame;
//        string[][] texts = { 
//            new string[]{ "New Game", "Continue", "Multiplayer", "Quit" },
//            new string[]{ "Find Lobby", "Create Lobby", "Back" }
//        };
//        Rectangle[][] rects;
//        int menuI = 0;

//        public SmallFPSMenu(Game1 game) : base(game)
//        {
//            game1.IsMouseVisible = true;
//        }

//        protected override void LoadContent()
//        {
//            base.LoadContent();
//            rects = new Rectangle[texts.Length][];// new Rectangle[texts.Length];
//            for (int i = 0; i < texts.Length; ++i)
//            {
//                string[] options = texts[i];
//                rects[i] = new Rectangle[options.Length];
//                for (int j = 0; j < options.Length; ++j)
//                {
//                    rects[i][j] = Backpack.percentage(
//                    GraphicsDevice.Viewport.Bounds,
//                    0.1f,
//                    0.1f + 0.2f * (float)j,
//                    0.8f,
//                    0.15f);
//                }
//            }
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);
//            if (game1.mouseCurrent.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
//                game1.mouseOld.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
//            {
//                switch (menuI)
//                {
//                    case 0: // MAIN MENU
//                        if (rects[0][0].Contains(game1.mouseCurrent.Position)) //NEW GAME
//                        {
//                            fpsGame = new SmallFPS(game1);
//                            fpsGame.menu = this;
//                            fpsGame.requestNewGame = true;
//                            fpsGame.reload();
//                            game1.Components.Add(fpsGame);
//                            game1.Components.Remove(this);
//                        }
//                        if (rects[0][1].Contains(game1.mouseCurrent.Position)) //CONTINUE
//                        {
//                            if (fpsGame == null)
//                            {
//                                fpsGame = new SmallFPS(game1);
//                                fpsGame.menu = this;
//                            }
//                            game1.Components.Add(fpsGame);
//                            game1.Components.Remove(this);
//                        }
//                        if (rects[0][2].Contains(game1.mouseCurrent.Position)) //MP
//                        {
//                            menuI = 1;
//                        }
//                        if (rects[0][3].Contains(game1.mouseCurrent.Position)) //quit
//                        {
//                            //if (fpsGame != null)
//                            //    fpsGame.SaveGame();
//                            game1.Exit();
//                        }
//                        break;
//                    case 1: //MULTIPLAYER
//                        //if (rects[1][0].Contains(game1.mouseCurrent.Position)) //NEW GAME
//                        //{
//                        //}
//                        //if (rects[1][1].Contains(game1.mouseCurrent.Position)) //CONTINUE
//                        //{
//                        //}
//                        if (rects[1][2].Contains(game1.mouseCurrent.Position)) //CONTINUE
//                        {
//                            menuI = 0;
//                        }
//                        break;
//                }
//            }
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            base.Draw(gameTime);
//            spriteBatch.Begin();
//            for (int i = 0; i < texts[menuI].Length; ++i)
//            {
//                game1.drawSquare(rects[menuI][i], monochrome(0.2f), 0);
//                game1.drawString(texts[menuI][i], rects[menuI][i], monochrome(1.0f));
//            }
//            spriteBatch.End();
//        }
//    }
//}
