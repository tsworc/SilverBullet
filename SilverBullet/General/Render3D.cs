using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MknGames;
using MknGames.FPSWahtever;
using MknGames.Split_Screen_Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class Render3D
    {
        float renderLightAmbientValue = 0f;
        Vector3 renderLightDir = Vector3.Zero;
        public RenderTarget2D deferredBuffer0;
        public RenderTarget2D deferredBufferDepth;
        //inst shadows
        public const int shadowMapCount = 0;
        //RenderTarget3D cascadeShadowMaps;
        public RenderTarget2D[] cascadeShadowMaps = new RenderTarget2D[shadowMapCount];
        public Matrix[] shadowViews = new Matrix[shadowMapCount];
        public Matrix[] shadowProjections = new Matrix[shadowMapCount];
        Matrix[] shadowViewProjections = new Matrix[shadowMapCount];
        float[] cascadeShadowDepthBiasWorld = new float[] { 0.1f, 0.2f, 0.4f, 0.8f };
        float[] cascadeShadowSoftSampleDist = new float[] { 1, 1, 1, 1 };
        Vector3 lightOffset;
        float[] shadowDistances = new float[] { 300, 300, 300, 300 };
        float[] shadowProjSizes = new float[] { 15, 30, 100, 300 };
        bool[] shadowCullCounterclockwise = new bool[] { true, true, true, false };
        float shadowNonLinearCutoff = 1;
        float[] cascadingNonLinearCutoffs = new float[4] { 1, 3, 27, 59 };
        Vector2[] nonLinearMinsPlusRanges = new Vector2[4];
        Matrix[] cascadingNonLinearProjections = new Matrix[4];
        Matrix[] cascadingNonLinearViews = new Matrix[4];
        Matrix[] cascadingNonLinearVPs = new Matrix[4];
        Matrix[] cascadingInverseNonLinearProjections = new Matrix[4];

        public Effect basicfx;
        public Effect deferfx;
        public Effect processfx;
        public Effect shadowfx;
        public Effect clearfx;

        VertexDeclaration instanceDecl = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(Single) * 4, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1)
            //new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 0),
            //new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1),
            //new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 2),
            //new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 3)
            );
        public VertexBuffer instanceVertexBuffer;
        //Matrix[] instanceTransforms;
        const int instanceMax = 10000;
        public SilverBullet.General.InstanceData[] instanceTransforms = new SilverBullet.General.InstanceData[instanceMax];
        public int instanceCubeIterator;
        public VertexBuffer instanceCubeVertexBuffer;
        public IndexBuffer instanceCubeIndexBuffer;
        GraphicsDevice GraphicsDevice;

        VertexPositionNormalTexture[] quadVertices = new VertexPositionNormalTexture[]
        {
            new VertexPositionNormalTexture(new Vector3(-1,1,0), Vector3.Backward, new Vector2(0,0)),
            new VertexPositionNormalTexture(new Vector3(1,1,0),Vector3.Backward, new Vector2(1,0)),
            new VertexPositionNormalTexture(new Vector3(1,-1,0),Vector3.Backward, new Vector2(1,1)),
            new VertexPositionNormalTexture(new Vector3(-1,-1,0),Vector3.Backward, new Vector2(0,1)),
        };
        int[] quadIndices = new int[] { 0, 1, 2, 0, 2, 3 };

        Dictionary<string, SaveLoadVariable> saveLoadVariables;
        private void AddSaveLoadVariable(string name, Func<string> save, Action<string> load)
        {
            if (saveLoadVariables.ContainsKey(name) == false)
            {
                SaveLoadVariable slv = new SaveLoadVariable(name, save, load);
                saveLoadVariables.Add(name, slv);
            }
        }
        private void AddSaveLoadVariable<T>(Dictionary<string, SaveLoadVariable<T>> collection, string name, Func<T, string> save, Action<T, string> load)
        {
            if (collection.ContainsKey(name) == false)
            {
                SaveLoadVariable<T> slv = new SaveLoadVariable<T>(name, save, load);
                collection.Add(name, slv);
            }
        }
        public void AddVariables(Dictionary<string, SaveLoadVariable> variables)
        {
            saveLoadVariables = variables;
            AddSaveLoadVariable("renderLightAmbientValue",
                () => { return renderLightAmbientValue.ToString(); },
                (string text) => { renderLightAmbientValue = float.Parse(text); });
            AddSaveLoadVariable("shadowNonLinearCutoff",
                () => { return shadowNonLinearCutoff.ToString(); },
                (string text) => { shadowNonLinearCutoff = float.Parse(text); });
            AddSaveLoadVariable("renderLightDir",
            () => { return SmallFPS.csvWriteV3(renderLightDir); },
                (string text) => { renderLightDir = SmallFPS.csvParseV3(text); });
            AddSaveLoadVariable("lightOffset",
                () => { return SmallFPS.csvWriteV3(lightOffset); },
                (string text) => { lightOffset = SmallFPS.csvParseV3(text); });
            AddSaveLoadVariable("shadowDistances",
                () => {
                    return SmallFPS.writeObjectArray(shadowDistances);
                },
                (string text) => {
                    shadowDistances = SmallFPS.readArrayFloat(text);
                });
            AddSaveLoadVariable("cascadingNonLinearCutoffs",
                () => { return SmallFPS.writeObjectArray(cascadingNonLinearCutoffs); },
                (string line) =>
                {
                    cascadingNonLinearCutoffs = SmallFPS.readArrayFloat(line);
                });
            AddSaveLoadVariable("shadowProjSizes",
                () => {
                    return SmallFPS.writeObjectArray(shadowProjSizes);
                },
                (string text) => {
                    shadowProjSizes = SmallFPS.readArrayFloat(text);
                });
            AddSaveLoadVariable("shadowDistances",
                () => {
                    return SmallFPS.writeObjectArray(shadowDistances);
                },
                (string text) => {
                    shadowDistances = SmallFPS.readArrayFloat(text);
                });
            AddSaveLoadVariable("cascadeShadowDepthBiasWorld",
                () => {
                    return SmallFPS.writeObjectArray(cascadeShadowDepthBiasWorld);
                },
                (string text) => {
                    cascadeShadowDepthBiasWorld = SmallFPS.readArrayFloat(text);
                });
            AddSaveLoadVariable("cascadeShadowSoftSampleDist",
                () => {
                    return SmallFPS.writeObjectArray(cascadeShadowSoftSampleDist);
                },
                (string text) => {
                    cascadeShadowSoftSampleDist = SmallFPS.readArrayFloat(text);
                });
            AddSaveLoadVariable("shadowCullCounterclockwise",
                () => {
                    return SmallFPS.writeObjectArray(shadowCullCounterclockwise);
                },
                (string text) => {
                    shadowCullCounterclockwise = SmallFPS.readArrayBool(text);
                });
        }

        public void Clear()
        {
            clearfx.CurrentTechnique.Passes[0].Apply();
            DrawScreenQuad();
        }

        public void DrawScreenQuad()
        {
            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                quadVertices,
                0,
                4,
                quadIndices,
                0,
                2);
        }

        private Vector3[] GetFarFrustumCorners(Matrix view, Matrix projection)
        {
            BoundingFrustum frustum = new BoundingFrustum(view * projection);
            var cornersWS = frustum.GetCorners();
            Vector3[] cornersVS = new Vector3[cornersWS.Length];
            Vector3.Transform(cornersWS, ref view, cornersVS);
            Vector3[] farFrustumCorners = new Vector3[4];
            for (int i = 0; i < 4; ++i)
            {
                //farFrustumCorners[i] = Vector3.Transform(cornersVS[i + 4], Matrix.Invert(playerCam.view));
                farFrustumCorners[i] = cornersVS[i + 4];
            }
            return farFrustumCorners;
        }

        public void LoadContent(GraphicsDevice gd, GameMG game1)
        {
            GraphicsDevice = gd;
            deferredBuffer0 = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8);
            deferredBufferDepth = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24Stencil8);
            for (int i = 0; i < shadowMapCount; ++i)
            {
                cascadeShadowMaps[i] =
                    new RenderTarget2D(
                    GraphicsDevice,
                    2048,
                    2048,
                    false,
                    SurfaceFormat.Single,
                    DepthFormat.Depth24Stencil8);
            }

            //load effects, load fx
            basicfx = game1.Content.Load<Effect>("Shaders/basic");
            deferfx = game1.Content.Load<Effect>("Shaders/deferred");
            processfx = game1.Content.Load<Effect>("Shaders/process");
            //shadowfx = game1.Content.Load<Effect>("shadow");
            clearfx = game1.Content.Load<Effect>("Shaders/clear");

            //load instancing
            instanceVertexBuffer = new VertexBuffer(GraphicsDevice, instanceDecl, instanceMax, BufferUsage.WriteOnly);
            //instanceTransforms = new Matrix[instanceMax];
            Model cubeModel = game1.Content.Load<Model>("cube");
            instanceCubeIndexBuffer = cubeModel.Meshes[0].MeshParts[0].IndexBuffer;
            instanceCubeVertexBuffer = cubeModel.Meshes[0].MeshParts[0].VertexBuffer;
        }

        public void PreRenderShadowSetup(Vector3 desiredOffset, Matrix desiredView, float desiredNear, float desiredFOV)
        {
            Vector3 lightDir = Vector3.Normalize(renderLightDir);
            for (int i = 0; i < shadowMapCount; ++i)
            {
                Vector3 offset = lightOffset;
                if (i < shadowMapCount - 1)
                {
                    offset = desiredOffset;
                }
                shadowViews[i] = Matrix.CreateLookAt(offset - lightDir * shadowDistances[i] / 2, offset + lightDir * shadowDistances[i] / 2, Math.Abs(Vector3.Dot(lightDir, Vector3.Up)) > 0.9f ? Vector3.Right : Vector3.Up);
                //Matrix shadowView = Matrix.CreateLookAt(bodyState.pos - lightDir * shadowDistance / 2, bodyState.pos, Vector3.Up);
                //Matrix shadowProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(65), 1, 1, shadowDistance);
                float step = (float)(i + 1) / (float)shadowMapCount;
                shadowProjections[i] = Matrix.CreateOrthographic(shadowProjSizes[i], shadowProjSizes[i], 1, shadowDistances[i]);
                shadowViewProjections[i] = shadowViews[i] * shadowProjections[i];
            }
            for (int i = 0; i < cascadingNonLinearCutoffs.Length; ++i)
            {
                float near = desiredNear;
                if (i > 0)
                {
                    near = cascadingNonLinearCutoffs[i - 1];
                }
                float far = Math.Max(cascadingNonLinearCutoffs[i], near + 0.001f);
                nonLinearMinsPlusRanges[i] = new Vector2(near, far - near);
                cascadingNonLinearProjections[i] = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(desiredFOV),
                    GraphicsDevice.Viewport.AspectRatio, near, far);
                cascadingInverseNonLinearProjections[i] = Matrix.Invert(cascadingNonLinearProjections[i]);
                cascadingNonLinearVPs[i] = desiredView * cascadingNonLinearProjections[i];
            }
        }

        public void Render(Action<int, int> action, CameraState playerCam)
        {
            Vector3 lightDir = Vector3.Normalize(renderLightDir);
            const int finalPass = Render3D.shadowMapCount;
            for (int i = 0; i < shadowMapCount + 1; ++i)
            {
                int shadow = -1;
                int pass = -1;
                if (i > 0)
                {
                    GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.TransparentBlack, 1, 0);
                }
                if (i == finalPass)
                {
                    pass = 0; //color
                    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                    GraphicsDevice.SetRenderTargets(deferredBuffer0, deferredBufferDepth);
                }
                else
                {
                    pass = 1;//shadow
                    shadow = i;
                    GraphicsDevice.SetRenderTarget(cascadeShadowMaps[shadow]);
                    if (shadowCullCounterclockwise[shadow])
                        GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                    else
                        GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
                    SetDeferredParametersShadows(shadow);
                }
                action(i, pass);
            }
            GraphicsDevice.SetRenderTargets(null);
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            processfx.Parameters["diffusetx"].SetValue(deferredBuffer0);
            processfx.Parameters["tempNegLightDir"].SetValue(-lightDir);
            processfx.Parameters["tempAmbient"].SetValue(renderLightAmbientValue);

            processfx.Parameters["svp"].SetValue(shadowViewProjections);
            processfx.Parameters["sv"].SetValue(shadowViews);
            processfx.Parameters["sp"].SetValue(shadowProjections);
            processfx.Parameters["sfar"].SetValue(shadowDistances);
            processfx.Parameters["inverseView"].SetValue(Matrix.Invert(playerCam.view));
            processfx.Parameters["inverseProjection"].SetValue(Matrix.Invert(playerCam.projection));
            processfx.Parameters["inverseViewRotation"].SetValue(Matrix.Invert(Matrix.Invert(playerCam.rotation3D)));
            processfx.Parameters["inverseNonLinearMatrices"].SetValue(cascadingInverseNonLinearProjections);
            processfx.Parameters["nonLinearMatrices"].SetValue(cascadingNonLinearVPs);
            processfx.Parameters["nonLinearCutoffs"].SetValue(cascadingNonLinearCutoffs);
            processfx.Parameters["far"].SetValue(playerCam.far);
            Vector3[] farFrustumCorners = GetFarFrustumCorners(playerCam.view, playerCam.projection);
            Vector3[][] farFrustomCornersPlus = new Vector3[4][];
            for (int i = 0; i < 4; ++i)
            {
                farFrustomCornersPlus[i] = new Vector3[4];
                farFrustomCornersPlus[i] = GetFarFrustumCorners(cascadingNonLinearViews[i], cascadingNonLinearProjections[i]);
                processfx.Parameters["farFrustumCornersPlus" + i].SetValue(farFrustomCornersPlus[i]);
            }
            processfx.Parameters["farFrustumCorners"].SetValue(farFrustumCorners);
            processfx.Parameters["camPos"].SetValue(playerCam.pos);
            processfx.Parameters["linearMinsPlusRanges"].SetValue(nonLinearMinsPlusRanges);
            processfx.Parameters["nonlineartx"].SetValue(deferredBufferDepth);
            processfx.Parameters["diffuseWidth"].SetValue((float)deferredBuffer0.Width);
            processfx.Parameters["diffuseHeight"].SetValue((float)deferredBuffer0.Height);
            processfx.Parameters["shadowDepthBias"].SetValue(cascadeShadowDepthBiasWorld);
            processfx.Parameters["nonLinearCutoff"].SetValue(shadowNonLinearCutoff);
            processfx.Parameters["softSampleDists"].SetValue(cascadeShadowSoftSampleDist);
            processfx.CurrentTechnique.Passes[0].Apply();
            for (int i = 0; i < shadowMapCount; ++i) //this must go after apply
            {
                GraphicsDevice.Textures[i] = cascadeShadowMaps[i];
                //processfx.Parameters["shadowtx"].SetValue(cascadeShadowMaps[i]);
            }
            DrawScreenQuad();
        }

        public void SetDeferredParametersGeometry(CameraState playerCam, Matrix world, Matrix orientation)
        {
            deferfx.Parameters["far"].SetValue(playerCam.far);
            deferfx.Parameters["orientation"].SetValue(orientation);
            deferfx.Parameters["world"].SetValue(world);
            deferfx.Parameters["view"].SetValue(playerCam.view);
            deferfx.Parameters["projection"].SetValue(playerCam.projection);
            deferfx.Parameters["nonLinearMatrices"].SetValue(cascadingNonLinearProjections);
            deferfx.Parameters["viewRotation"].SetValue(playerCam.rotation3D);
            deferfx.Parameters["linearMinsPlusRanges"].SetValue(nonLinearMinsPlusRanges);
        }

        public void SetDeferredParametersShadows(int cascade)
        {
            deferfx.Parameters["sw"].SetValue(Matrix.Identity);
            deferfx.Parameters["sv"].SetValue(shadowViews[cascade]);
            deferfx.Parameters["sp"].SetValue(shadowProjections[cascade]);
            deferfx.Parameters["shadowFar"].SetValue(shadowDistances[cascade]);
        }
    }
}
