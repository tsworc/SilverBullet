using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MknGames.FPSWahtever
{
    //REALLY DISORIENTING -12/17/17
    public class FPSWhatever : DrawableGameComponent
    {
        GameMG game1;
        Camera camera = new Camera();
        Vector3 cameraPosition;
        Vector3 cameraEuler;
        Matrix cameraOrientation = Matrix.Identity;
        bool isLocked;
        bool mouseEnabledOld;

        public FPSWhatever(GameMG game) : base(game)
        {
            game1 = game;
        }
        protected override void LoadContent()
        {
            game1.IsMouseVisible = true;
            base.LoadContent();
            CenterMouse();
        }
        void CenterMouse()
        {
            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }
        public override void Update(GameTime gameTime)
        {
            float et = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool mouseEnabled = false;
            if (game1.IsActive && !isLocked)
            {
                mouseEnabled = true;
            }
            game1.IsMouseVisible = !mouseEnabled;
            if (mouseEnabled)
            {
                Vector2 mouseDelta = (game1.mouseCurrent.Position - game1.screenCenter().ToPoint()).ToVector2();
                //Mouse.SetPosition()
                CenterMouse();
                if (mouseEnabledOld)
                {
                    float mouseSensitivity = 180;
                    cameraEuler.X -= mouseDelta.Y / mouseSensitivity;
                    cameraEuler.Y -= mouseDelta.X / mouseSensitivity;
                    cameraOrientation = Matrix.CreateFromYawPitchRoll(-mouseDelta.X / mouseSensitivity ,
                        - mouseDelta.Y / mouseSensitivity,
                        0) * cameraOrientation;
                }
            }
            mouseEnabledOld = mouseEnabled;
            float rotationRate = 3;
            if (game1.kdown(Keys.Right))
                cameraEuler.Y -= rotationRate * et;
            if (game1.kdown(Keys.Left))
                cameraEuler.Y += rotationRate * et;
            if (game1.kdown(Keys.Up))
                cameraEuler.X += rotationRate * et;
            if (game1.kdown(Keys.Down))
                cameraEuler.X -= rotationRate * et;

            //cameraOrientation = Matrix.CreateFromYawPitchRoll(cameraEuler.Y, cameraEuler.X, cameraEuler.Z);

            Vector3 cameraForward = Vector3.Transform(Vector3.Forward, cameraOrientation);
            Vector3 cameraRight = Vector3.Transform(Vector3.Right, cameraOrientation);
            Vector2 translationInput = game1.makeDirectional(game1.kdown(Keys.A), game1.kdown(Keys.D), game1.kdown(Keys.S),
                game1.kdown(Keys.W));
            float translationRate = 10;
            if(game1.kdown(Keys.LeftShift) || game1.kdown(Keys.RightShift))
            {
                translationRate = 100;
            }
            cameraPosition += translationInput.Y * cameraForward * translationRate * et;
            cameraPosition += translationInput.X * cameraRight * translationRate * et;

            camera.view = Matrix.CreateTranslation(-cameraPosition) * Matrix.Invert(cameraOrientation);
            camera.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(65),
                GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                1000f);

            isLocked = game1.kdown(Keys.Space);
            if(true && cameraPosition != Vector3.Zero)//game1.kclick(Keys.F))
            {
                Vector3 toCenter = cameraPosition;
                toCenter.Normalize();
                cameraOrientation = Matrix.CreateWorld(Vector3.Zero, cameraForward, toCenter);
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            DrawModel(
                game1.cubeModel,
                Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds) *
                Matrix.CreateTranslation(0, 0, -5),
                Color.Red);
            //FLOOR
            DrawModel(
                game1.cubeModel,
                Matrix.CreateScale(10,1f,10) *
                Matrix.CreateTranslation(0, -2, 0),
                Color.Blue);
            //CIELING
            DrawModel(
                game1.cubeModel,
                Matrix.CreateScale(10,1f,10) *
                Matrix.CreateTranslation(0, 10, 0),
                Color.Blue);
            //Pillars
            Matrix scale = Matrix.CreateScale(1, 5, 1);
            DrawModel(
                game1.cubeModel,
                scale *
                Matrix.CreateTranslation(-9, 4, -9),
                Color.Blue);
            DrawModel(
                game1.cubeModel,
                scale *
                Matrix.CreateTranslation(9, 4, -9),
                Color.Blue);
            DrawModel(
                game1.cubeModel,
                scale *
                Matrix.CreateTranslation(-9, 4, 9),
                Color.Blue);
            DrawModel(
                game1.cubeModel,
                scale *
                Matrix.CreateTranslation(9, 4, 9),
                Color.Blue);
            Model sphereModel = game1.sphereModel;
            for (int i = 0; i < 10; ++i)
            {
                DrawModel(sphereModel,
                    Matrix.CreateScale(100 * (i+1)),
                    Color.Green);
                DrawModel(sphereModel,
                    Matrix.CreateScale(-100 * (i + 1)),
                    Color.Purple);
            }

            game1.spriteBatch.Begin();
            if (isLocked)
            {
                Vector2 screenSize = game1.screenCenter() * 2;
                game1.drawFrame(game1.screenCenter(), Color.Red, screenSize.X, screenSize.Y, 10);
            }
            game1.spriteBatch.End();

            base.Draw(gameTime);
        }
        public void DrawModel(Model model, Matrix world, Color color)
        {
            game1.DrawModel(model, world, camera.view, camera.projection, color);
        }
    }
}
