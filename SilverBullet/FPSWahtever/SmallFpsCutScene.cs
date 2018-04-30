//using Game6._2D;
//using Microsoft.Xna.Framework.Media;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;

//namespace Game6.FPSWahtever
//{
//    public class SmallFpsCutScene : fuckwhit_no_cursing
//    {
//        //inst video
//        Video video;
//        VideoPlayer vPlayer;

//        SmallFPS fps;

//        public SmallFpsCutScene(Game1 game) : base(game)
//        {
//            fps = new SmallFPS(game);
//            fps.paused = true;
//            game1.Components.Add(fps);
//        }

//        protected override void LoadContent()
//        {
//            base.LoadContent();
//            // load video
//            video = game1.Content.Load<Video>("moon rover");
//            vPlayer = new VideoPlayer();
//            vPlayer.Play(video);
//        }

//        protected override void UnloadContent()
//        {
//            base.UnloadContent();
//            // unload video
//            video.Dispose();
//            vPlayer.Dispose();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            base.Update(gameTime);

//            //update video
//            if(game1.kclick(Microsoft.Xna.Framework.Input.Keys.Space))
//            {
//                //if (fps.loadContentComplete)
//                //{
//                    vPlayer.Stop();
//                //}
//            }
//            if (vPlayer.State == MediaState.Stopped)
//            {
//                //if (fps.loadContentComplete)
//                //{
//                    game1.Components.Remove(this);
//                    fps.paused = false;
//                    //fps draw will call before the next update so
//                    //slip an update in here.
//                    fps.Update(gameTime);
//                //}
//                //game1.Components.Add(fps);
//                return;
//            }
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            base.Draw(gameTime);
//            //draw video 2d
//            game1.spriteBatch.Begin();
//            //WARN: MEMORY LEAK: vPlayer.GetTexture()
//            game1.drawTexture(
//                vPlayer.GetTexture(),
//                new Vector2(0, 0), 
//                Color.White, 0, 
//                GraphicsDevice.Viewport.Width, 
//                GraphicsDevice.Viewport.Height);
//            string text = "loading";
//            //if (fps.loadContentComplete)
//                text = "Press \'Spacebar\' to Skip";
//            game1.drawString(text,
//                Split_Screen_Dungeon.Backpack.percentageEdges(
//                    GraphicsDevice.Viewport.Bounds, 0.1f, 0.9f, 0.9f, 0.95f),
//                monochrome(1.0f));
//            game1.spriteBatch.End();
//        }
//    }
//}
