using ConversionHelper;
using MknGames._2D;
using MknGames.FPSWahtever;
using MknGames.Split_Screen_Dungeon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MknGames.FPSWahtever.SmallFPS;

namespace MknGames.NonGames
{
    // Totally associated with bullet
    //      Drawing. (frequent). (simple). O(c),O(n)
    // overview: 2x sphere, 1x cylinder
    // \cylinder\
    // scale horizontal: radius (diameter)
    // scale vertical: height
    // rotation: InvertMatrixLook(zero, direction, up)
    // rotation and position: InvertMatrixLook(center, center + direction, up)
    // position: center
    // \sphere A\
    // scale: radius
    // position: head
    // \sphere B\
    // position: tail
    //      Intersection. (frequent). (complex). O(0)>O(n)>O(n^2). Avoidable through sphere test (radius + center)
    // overview: box, capsule, sphere
    // sphere: closest point on line head>tail, distance < radiusSum^2
    // box: ?
    //      Resolve collision. (frequent). (complex). O(<Intersection)
    // contact, normal, penetration
    //      Construction
    // bullet: Position + Velocity Vector + Radius
    //      Update. (frequent). (simple). O(0)>O(n)>O(n^2)
    // \physicsState\
    // bullet: Position + Velocity Vector + Radius
    public struct DrawingCapsule
    {
        //container = radius + center
        float radius;
        Vector3 center;
        Vector3 head;
        Vector3 tail;
        //Vector3 direction
    }
    // line for closest point
    public struct CollidingCapsule
    {
        BoundingSphere boundingSphere;
        Vector3 head;
        Vector3 tail;
        float length;
        float radius;
        //Vector3 direction;
    }
    public struct BulletCapsule
    {
        BoundingSphere boundingSphere; //center
        Vector3 head;
        Vector3 tail;
        Vector3 direction;
        float length;
        float radius;
        public Vector3 Center
        {
            get { return boundingSphere.Center; }
        }
        public BulletCapsule(Bullet bullet)
        {
            head = bullet.phy.pos;
            tail = head + bullet.phy.vel;
            length = bullet.phy.vel.Length();
            direction = bullet.phy.vel / length;
            radius = bullet.size / 2;
            boundingSphere = new BoundingSphere(head + direction * length / 2, length + radius);
        }
    }

    public class CapsuleCollisionRoom : fuckwhit_no_cursing
    {
        CameraState camera = new CameraState();
        Model cylinder, icoSphere;
        BoundingSphere sphere;
        Vector3 capsuleHead;
        float zoom = 1;
        Vector3 camOffset = Vector3.Zero;
        BulletCapsule capsule = new BulletCapsule();
        Vector3 relativeRight;
        Vector3 relativeForward;
        bool centerOnCapsule = false;
        Vector3 capsuleTail = Vector3.Zero;
        Vector3 capsuleEuler = Vector3.Zero;
        Vector3 capsuleDirection = Vector3.Zero;
        List<object[]> debugPoints = new List<object[]>();
        Vector3[] sphereVertices;
        Vector3[] cylinderVertices;

        float capsuleLength = 2;
        float capsuleRadius = 1;
        

        public CapsuleCollisionRoom(GameMG game) : base(game)
        {
            camera.pos = new Vector3(0, 0, 5);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            cylinder = game1.Content.Load<Model>("cylinder");
            icoSphere= game1.Content.Load<Model>("Models/ico-sphere");
            List<Vector3> vertices = new List<Vector3>();
            foreach (var n in icoSphere.Meshes)
            {
                foreach (var p in n.MeshParts)
                {
                    VertexPosition[] data = new VertexPosition[p.NumVertices];
                    p.VertexBuffer.GetData<VertexPosition>(data);
                    foreach (var v in data)
                    {
                        vertices.Add(v.Position);
                    }
                }
            }
            sphereVertices = vertices.ToArray();
            vertices.Clear();
            foreach (var n in cylinder.Meshes)
            {
                foreach (var p in n.MeshParts)
                {
                    VertexPosition[] data = new VertexPosition[p.NumVertices];
                    p.VertexBuffer.GetData<VertexPosition>(data);
                    foreach (var v in data)
                    {
                        vertices.Add(v.Position);
                    }
                }
            }
            cylinderVertices = vertices.ToArray();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float et = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float tt = (float)gameTime.TotalGameTime.TotalSeconds;

            if (game1.kclick(Keys.B))
            {
                camOffset = new Vector3(-2, 0, 0);
                centerOnCapsule = false;
            }
            if (game1.kclick(Keys.C))
            {
                centerOnCapsule = true;
            }
            if (game1.kclick(Keys.V))
            {
                camOffset = new Vector3(0, 0, 0);
                centerOnCapsule = false;
            }
            if (centerOnCapsule)
            {
                camOffset = capsuleHead;
            }
            Vector2 camMove = game1.makeDirectional(game1.kdown(Keys.Left), game1.kdown(Keys.Right), game1.kdown(Keys.Down), game1.kdown(Keys.Up));
            float camSpeed = 3;
            bool shiftDown = game1.kdown(Keys.LeftShift);
            if (shiftDown)
                camSpeed = 0.05f;
            camera.Euler.Y += camMove.X * camSpeed * et;
            camera.Euler.X += camMove.Y * camSpeed * et;
            camera.Update(gameTime, GraphicsDevice.Viewport);
            Vector3 dir = camera.pos;
            dir.Normalize();
            camera.pos = new Vector3(0, 0, 10);
            Vector3 pos = Vector3.Transform(camera.pos,
                Matrix.CreateRotationX(camera.Euler.X) * Matrix.CreateRotationY(camera.Euler.Y));
            pos *= zoom;
            pos += camOffset;
            camera.view = Matrix.CreateLookAt(pos, camOffset, Vector3.Up);
            Vector3 right = Vector3.Transform(Vector3.Zero, camera.view) - Vector3.Transform(Vector3.Right, camera.view);
            right.Normalize();
            Vector3 forward = Vector3.Transform(Vector3.Zero, camera.view) - Vector3.Transform(Vector3.Forward, camera.view);
            forward.Normalize();
            relativeRight = right;
            //relativeRight.Y = 0;
            //relativeRight.Normalize();
            relativeForward = forward;
            //relativeForward.Y = 0;
            // relativeForward.Normalize();
            float gizdist = 10;
            //game1.add3DLine(camOffset, camOffset + Vector3.Right * 10, Color.Red);
            //game1.add3DLine(camOffset, camOffset + Vector3.Forward * 10, Color.Blue);
            //game1.add3DLine(camOffset, camOffset + Vector3.Up * 10, Color.Green);
            //game1.add3DLine(camOffset, camOffset + relativeForward * gizdist, Color.Cyan);
            //game1.add3DLine(camOffset, camOffset + relativeRight* gizdist, Color.Yellow);

            //zoom
            float zoomSpeed = 0.1f;
            float scrolldelta = game1.mouseCurrent.ScrollWheelValue - game1.mouseOld.ScrollWheelValue;
            zoom += (scrolldelta / 120) * zoomSpeed;
            float zoomRate = 0.4f;
            if (game1.kdown(Keys.OemPlus))
                zoom -= zoomRate * et;
            if (game1.kdown(Keys.OemMinus))
                zoom += zoomRate * et;

            //capsule
            Vector2 move = game1.makeDirectional(game1.kdown(Keys.A), game1.kdown(Keys.D), game1.kdown(Keys.S), game1.kdown(Keys.W));
            float speed = 1;
            if (shiftDown)
                speed = 0.05f;
            capsuleHead.X += move.X * et * speed;
            capsuleHead.Z -= move.Y * et * speed;
            if (game1.kdown(Keys.E))
                capsuleHead.Y += speed * et;
            if (game1.kdown(Keys.Q))
                capsuleHead.Y -= speed * et;
            if (game1.kclick(Keys.Z))
                capsuleHead = Vector3.Zero;
            Vector2 moveRot = game1.makeDirectional(game1.kdown(Keys.J), game1.kdown(Keys.L), game1.kdown(Keys.K), game1.kdown(Keys.I));
            float rotateSpeed = 1;
            if (shiftDown)
                rotateSpeed = 0.1f;
            capsuleEuler.Y += moveRot.X * rotateSpeed * et;
            capsuleEuler.X -= moveRot.Y * rotateSpeed * et;
            //if (game1.kdown(Keys.O))
            //    capsuleTail.Y += speed * et;
            //if (game1.kdown(Keys.U))
            //    capsuleTail.Y -= speed * et;


            //capsule update
            capsuleDirection = Vector3.Transform(Vector3.Up,
                 Matrix.CreateFromYawPitchRoll(capsuleEuler.Y, capsuleEuler.X, capsuleEuler.Z));
            capsuleTail = capsuleHead + capsuleDirection * capsuleLength;

            //sphere update
            sphere = new BoundingSphere(Vector3.Zero, 1);
        }

        RasterizerState wireFrame = new RasterizerState()
        {
            FillMode = FillMode.WireFrame,
            CullMode = CullMode.CullCounterClockwiseFace
        };

        //start draw
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            Vector3 boxSize = new Vector3(2);
            Vector3 boxScale = boxSize / 2;
            Vector3 boxPosition = new Vector3(-3, 0, 0);
            BoundingBox testBox = GameMG.MakeBox(boxPosition, boxSize);

            ContactData data = intersectSphereCapsule(sphere, capsuleHead, capsuleTail);
            bool unrotatedCylinderBoxHit = false;
            for (int i = 0; i < 3; ++i)
            {
                Color wire = new Color(1, 1, 1, 0.3f);
                Color cc = wire;
                Color sc = wire;
                Color bc = wire;
                float radAdd = 0;
                bool lighting = true;
                if (i == 2)
                {
                    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                    cc = Color.Red;
                    sc = Color.Blue;
                    bc = Color.Green;
                    if (data != null)
                    {
                        DrawCapsule(1, capsuleHead + data.norm * data.pen,
                            capsuleTail + data.norm * data.pen,
                            cc, true);
                        cc = Color.Green;
                        cc.A = 200;
                    }
                    else if (unrotatedCylinderBoxHit)
                    {
                        cc = Color.Orange;
                    }
                    //game1.add3DLine(testBox.Min, testBox.Max, Color.CornflowerBlue);
                    //Color sweepColor = Color.Yellow;
                    //{
                    //    Ray r = new Ray(position, Vector3.Up);
                    //    float? hit = r.Intersects(box);
                    //    if (hit.HasValue && hit.Value < 2)
                    //    {
                    //        sweepColor = Color.Pink;
                    //    }
                    //}
                    //sweepColor.A = 100;
                    //DrawBox(boxScale + new Vector3(1), boxPosition, sweepColor, radAdd, false);
                    int iterations = 10;
                    float step = capsuleLength / (float)(iterations - 1);
                    //intsersect capsule
                    //for (int j = 0; j < iterations; ++j)
                    //{
                    //    Vector3 center = capsuleHead + capsuleDirection * step * (float)j;
                    //    BoundingSphere a = new BoundingSphere(center, 1);
                    //    game1.add3DLine(a.Center, a.Center + Vector3.Up * a.Radius, Color.Goldenrod);
                    //    game1.add3DLine(a.Center, a.Center + Vector3.Right * a.Radius, Color.Goldenrod);
                    //    game1.add3DLine(a.Center, a.Center + Vector3.Forward * a.Radius, Color.Goldenrod);
                    //    game1.add3DLine(testBox.Min, a.Center, Color.White);
                    //    game1.add3DLine(testBox.Max, a.Center, Color.White);
                    //    Color scc = Color.White;
                    //    Vector3 vec;
                    //    float pen;
                    //    if (SmallFPS.intersectSphereBox(a, testBox, out vec, out pen))
                    //    {
                    //        scc = Color.Orange;
                    //    }
                    //    game1.DrawModel(game1.sphereModel, 
                    //        Matrix.CreateScale(a.Radius + 0.01f) * 
                    //        Matrix.CreateTranslation(a.Center),
                    //        camera.view, camera.projection, scc, true);
                    //}
                }
                else if (i == 0)
                {
                    continue;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                    radAdd = 0.05f;
                    cc = sc = bc = Color.White;
                    cc.A = sc.A = bc.A = 200;
                    lighting = false;
                }
                else
                {
                    GraphicsDevice.RasterizerState = wireFrame;
                    radAdd = 0.01f;
                    //BoundingBox box = Game1.MakeBox(capsuleHead + Vector3.Up, new Vector3(2, 2, 2));
                    //Color c = Color.White;
                    //if(box.Intersects(testBox))
                    //{
                    //    c = Color.Orange;
                    //    //unrotated
                    //    Vector2 testMin = SmallFPS.xz(testBox.Min);
                    //    Vector2 testMax = SmallFPS.xz(testBox.Max);
                    //    Vector2 cp = SmallFPS.xz(capsuleHead);
                    //    cp = Vector2.Max(cp, testMin);
                    //    cp = Vector2.Min(cp, testMax);
                    //    Vector2 toCp = cp - SmallFPS.xz(capsuleHead);
                    //    if(toCp.LengthSquared() < 1)//radius
                    //    {
                    //        unrotatedCylinderBoxHit = true;
                    //        Vector3 toCp3 = new Vector3(toCp.X, 0, toCp.Y);
                    //        game1.add3DLine(capsuleHead, capsuleHead + toCp3, Color.LightBlue);
                    //    }
                    //}
                    //DrawBox(new Vector3(2, 2, 2) / 2, capsuleHead + Vector3.Up, c, radAdd, false);
                    //BoundingSphere sphere = new BoundingSphere(
                    //    capsuleHead + capsuleDirection * capsuleLength / 2,
                    //    (capsuleLength / 2 + capsuleRadius));
                    //Vector3 dir;
                    //float pen;
                    //Color c = new Color(255, 0, 255, 100);
                    //if (SmallFPS.intersectSphereBox(sphere, testBox, out dir, out pen))
                    //{
                    //    c.G = 255;
                    //    Vector3 boxContact = sphere.Center - dir * (sphere.Radius - pen);
                    //    //Vector3 contact = sphere.Center - dir * sphere.Radius;
                    //    Vector3 contact = boxContact;
                    //    Vector3 line = capsuleTail - capsuleHead;
                    //    float factor = dot(contact - capsuleHead, line) / dot(line, line);
                    //    if (factor > 1) factor = 1;
                    //    if (factor < 0) factor = 0;
                    //    Vector3 eatit = capsuleHead + line * factor;
                    //    game1.add3DLine(sphere.Center, contact, Color.Yellow);
                    //    game1.add3DLine(eatit, contact, Color.Red);
                    //    //Vector3 normal;
                    //    //float pen2;
                    //    //SmallFPS.intersectBoxPoint(boxContact, testBox, out normal, out pen2);
                    //    //game1.add3DLine(boxContact, boxContact + normal, Color.LightBlue);
                    //    Vector3 proxyCenter = eatit;
                    //    //detect face collision
                    //    //if (Math.Abs(normal.X) == 1 || Math.Abs(normal.Y) == 1 || Math.Abs(normal.Z) == 1)
                    //    //{
                    //    //    float dotOrigin = Math.Max(dot(testBox.Min, normal), dot(testBox.Max, normal));
                    //    //    float dotA = dot(capsuleHead, normal) - dotOrigin;
                    //    //    float dotB = dot(capsuleTail, normal) - dotOrigin;
                    //    //    if (dotA < dotB)
                    //    //        proxyCenter = capsuleHead;
                    //    //    else if (dotB < dotA)
                    //    //        proxyCenter = capsuleTail;
                    //    //}
                    //    float minDist2 = (eatit - boxContact).LengthSquared();
                    //    Vector3 cp = boxContact;
                    //    for(int j = 0; j < 2; ++j)
                    //    {
                    //        Vector3 sample = j == 0 ? capsuleHead : capsuleTail;
                    //        Vector3 sampleCp = SmallFPS.getClosestPoint(sample, testBox);
                    //        Vector3 toCP = sampleCp - sample;
                    //        if(toCP.LengthSquared() < minDist2)
                    //        {
                    //            proxyCenter = sample;
                    //            minDist2 = toCP.LengthSquared();
                    //            cp = sampleCp;
                    //        }
                    //    }
                    //    game1.add3DLine(proxyCenter, cp, Color.Green);
                    //    BoundingSphere proxy = new BoundingSphere(proxyCenter, capsuleRadius);
                    //    //BoundingSphere proxy = new BoundingSphere(eatit, sphere.Radius - pen);
                    //    Color proxyColor = Color.White;
                    //    DrawBoundingSphere(proxy, proxyColor, radAdd, false);
                    //    Vector3 proxyNormal;
                    //    float proxyPen;
                    //    if(intersectSphereBox(proxy, testBox, out proxyNormal, out proxyPen))
                    //    {
                    //        data = new ContactData();
                    //        data.norm = proxyNormal;
                    //        data.pen = proxyPen;
                    //    }
                    //}
                    //DrawBoundingSphere(sphere, c, radAdd, false);
                    //var corners = testBox.GetCorners();
                    //    Vector3 line = capsuleTail - capsuleHead;
                    //Vector3 closestPoint = Vector3.Zero;
                    //float closestDist = float.MaxValue;
                    //Vector3 closestCorner = Vector3.Zero;
                    //foreach(Vector3 corner in corners)
                    //{
                    //    float factor = dot(corner - capsuleHead, line) / dot(line, line);
                    //    factor = MathHelper.Clamp(factor, 0, 1);
                    //    Vector3 cp = capsuleHead + line * factor;
                    //    Vector3 toCp = cp - corner;
                    //    float value = toCp.LengthSquared();
                    //    if(value < closestDist)
                    //    {
                    //        closestPoint = cp;
                    //        closestCorner = corner;
                    //        closestDist = value;
                    //    }
                    //}
                    {
                        Vector3 normal = Vector3.Up;
                        Vector3 tangent = Vector3.Right;
                        Vector3 binormal = Vector3.Forward;
                        Vector3 segmentStart = capsuleHead;
                        Vector3 segmentEnd = capsuleTail;
                        BoundingBox intersectionBox = testBox;
                        Vector3 closestPointOnBoxFace = Vector3.Zero;
                        Vector3 closestPointOnSegment = Vector3.Zero;
                        Vector3 line = segmentEnd - segmentStart;
                        {
                            BEPUphysics.Entities.Prefabs.Capsule capsule =
                                new BEPUphysics.Entities.Prefabs.Capsule(
                                    MathConverter.Convert(capsuleHead),
                                    MathConverter.Convert(capsuleTail),
                                    capsuleRadius);
                            capsule.CollisionInformation.WorldTransform = new BEPUutilities.RigidTransform(capsule.Position, capsule.Orientation);
                            BEPUphysics.Entities.Prefabs.Box bepuBox =
                                new BEPUphysics.Entities.Prefabs.Box(
                                    MathConverter.Convert(boxPosition),
                                    boxSize.X, boxSize.Y, boxSize.Z);
                            bepuBox.CollisionInformation.WorldTransform = new BEPUutilities.RigidTransform(bepuBox.Position, bepuBox.Orientation);
                            Func<Vector3, BEPUutilities.Vector3> v3 = (Vector3 vec) =>
                             {
                                 return MathConverter.Convert(vec);
                             };
                            Func<BEPUutilities.Vector3, Vector3> v3b = (BEPUutilities.Vector3 vec) =>
                            {
                                return MathConverter.Convert(vec);
                            };
                            game1.add3DLine(v3b(capsule.Position), v3b(bepuBox.Position), Color.White);

                            BEPUphysics.CollisionTests.CollisionAlgorithms.GeneralConvexPairTester tester =
                                new BEPUphysics.CollisionTests.CollisionAlgorithms.GeneralConvexPairTester();
                            BEPUphysics.CollisionTests.ContactData bpc;



                            tester.Initialize(capsule.CollisionInformation, bepuBox.CollisionInformation);

                            if (tester.GenerateContactCandidate(out bpc))
                            {
                                data = new ContactData();
                                data.contact = new Vector3(bpc.Position.X, bpc.Position.Y, bpc.Position.Z);
                                data.norm = -MathConverter.Convert(bpc.Normal);
                                data.pen = bpc.PenetrationDepth;
                                game1.add3DLine(data.contact, v3b(capsule.Position), Color.Blue);
                                game1.add3DLine(data.contact, v3b(bepuBox.Position), Color.Red);
                                debugPoints.Add(new object[] { "contact", data.contact });
                            }
                            //Func<Vector3, Vector3f> v3f = (Vector3 vector) =>
                            //{
                            //    return new Vector3f(vector.X, vector.Y, vector.Z);
                            //};
                            //Segment3f segment = new Segment3f(new Vector3f(segmentStart), segmentEnd);
                            //DistSegment3Triangle3 distA, distB;
                            //distA = new DistSegment3Triangle3(segment, triangleA);
                            //distB = new DistSegment3Triangle3(segment, triangleB);
                            //closestPointOnBoxFace = segmentClosestPointOnBoxFace(line, normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox);
                            //closestPointOnSegment = pointClosestPointOnSegment(closestPointOnBoxFace, capsuleHead, capsuleTail);
                        }

                        if (true)
                        {
                            //game1.add3DLine(closestPointOnSegment, closestPointOnBoxFace, Color.Red);
                            //game1.add3DLine(closestPointOnFace, closestPointOnSegment, Color.Green);
                            //game1.add3DLine(closestPointOnSegment + Vector3.Up, closestPointOnSegment, Color.Magenta);
                            //game1.add3DLine(capsuleTail, tailResult, Color.Yellow);
                            //game1.add3DLine(capsuleHead, result, Color.Yellow);
                            //game1.add3DLine(tailResult, reprojectTail, Color.Yellow);
                            //game1.add3DLine(reprojectHead, result, Color.Yellow);
                            //debugPoints.Add(new object[] {
                            //string.Format("{0:N2}", (toHead).Length()),
                            //    reprojectHead
                            //});
                            //debugPoints.Add(new object[] {
                            //string.Format("{0:N2}", (toTail).Length()),
                            //    reprojectTail
                            //});
                            //game1.add3DLine(pointOnFace, newSegmentReference, Color.SaddleBrown);
                            //game1.add3DLine(sample, result, Color.Yellow);
                            //game1.add3DLine(pointOnFace, result, Color.Orange);
                        }
                    }
                    if (true)
                    {
                        //game1.add3DLine(closestCorner, closestPoint, Color.White);
                        //DrawCapsule(1 + radAdd, capsuleHead, capsuleTail, cc, lighting);
                    }
                    continue;
                    GraphicsDevice.DepthStencilState = DepthStencilState.None;
                }
                DrawCapsule(capsuleRadius, capsuleHead, capsuleTail, cc, lighting);
                //DrawBoundingSphere(sphere, sc, radAdd, lighting);
                DrawBox(boxScale, boxPosition, bc, radAdd, lighting);
                //Box boxA = new Box(new Vector3(2), capsuleHead);
                //Box boxB = new Box(boxSize, boxPosition);
                //boxA.color = Color.Red;
                //DrawBox(boxA.size / 2, boxA.position, boxA.color, radAdd, lighting);
                //var cornersA = boxA.boundingBox.GetCorners();
                //var cornersB = boxB.boundingBox.GetCorners();
                //for (int j = 0; j < cornersA.Length; ++j)
                //{
                //    for (int k = 0; k < cornersB.Length; ++k)
                //    {
                //        Vector3 difference = cornersA[j] - cornersB[k];
                //        DrawBoundingSphere(new BoundingSphere(difference, 0.05f), Color.Red, radAdd, false);
                //    }
                //}
                //DrawBoundingSphere(new BoundingSphere(Vector3.Zero, 0.1f), Color.Blue, radAdd, false);
                //Matrix[] capsuleTransforms = DrawCapsule(1, new Vector3(2, 0, 0), new Vector3(2, 2, 0), Color.Purple, true);
                //for (int j = 0; j < cornersA.Length; ++j)
                //{
                //    for (int k = 0; k < cylinderVertices.Length; ++k)
                //    {
                //        Vector3 difference = cornersA[j] - Vector3.Transform(cylinderVertices[k], capsuleTransforms[2]);
                //        DrawBoundingSphere(new BoundingSphere(difference, 0.05f), Color.Red, radAdd, false);
                //    }
                //    for (int k = 0; k < sphereVertices.Length; ++k)
                //    {
                //        Vector3 difference = cornersA[j] - Vector3.Transform(sphereVertices[k], capsuleTransforms[0]);
                //        DrawBoundingSphere(new BoundingSphere(difference, 0.05f), Color.Red, radAdd, false);
                //        Vector3 difference2 = cornersA[j] - Vector3.Transform(sphereVertices[k], capsuleTransforms[1]);
                //        DrawBoundingSphere(new BoundingSphere(difference2, 0.05f), Color.Red, radAdd, false);
                //    }
                //}
                //game1.DrawModel(game1.cubeModel,
                //    Matrix.CreateScale((obbSize + new Vector3(radAdd))/2) *
                //    Matrix.CreateTranslation(obbPosition),
                //    camera.view,
                //    camera.projection,
                //    bc,
                //    lighting);
                if (i == 0)
                {
                    GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.TransparentBlack, 1, 0);
                }
            }

            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            game1.DrawAll3dLines(camera.view, camera.projection);
            game1.Flush3dLines();

            BoundingFrustum frust = new BoundingFrustum(camera.view * camera.projection);
            spriteBatch.Begin();
            if (true)
            {
                foreach (object[] objs in debugPoints)
                {
                    if (frust.Contains((Vector3)objs[1]) == ContainmentType.Contains)
                    {
                        game1.drawString(
                            objs[0].ToString(),
                            game1.centeredRect(camera.worldToScreen((Vector3)objs[1], GraphicsDevice.Viewport), 50, 20),
                            Color.White,
                            new Vector2(0.5f),
                            false);
                    }
                }
            }
            spriteBatch.End();
            debugPoints.Clear();
        }

        private Vector3 segmentClosestPointOnBoxFace(Vector3 line, Vector3 normal, Vector3 tangent, Vector3 binormal, Vector3 segmentStart, Vector3 segmentEnd, BoundingBox intersectionBox)
        {
            Vector3 closestPointOnBoxFace = Vector3.Zero;
            float dotStart = dot(segmentStart, normal);
            float dotFace = Math.Max(dot(intersectionBox.Max, normal), dot(intersectionBox.Min, normal));
            float dotStartSeparation = dotStart - dotFace;
            Vector3 flatStart = segmentStart - normal * dotStartSeparation;
            Vector3 flatLine = line - dot(line, normal) * normal;
            float startTng = GetAxisClamp(intersectionBox, tangent, flatStart);
            float startBin = GetAxisClamp(intersectionBox, binormal, flatStart);
            bool containsA = startTng == 0 &&
                startBin == 0;
            Vector3 flatEnd = flatStart + flatLine;
            float endTng = GetAxisClamp(intersectionBox, tangent, flatEnd);
            float endBin = GetAxisClamp(intersectionBox, binormal, flatEnd);
            bool containsB = endTng == 0 &&
                endBin == 0;
                float dotEndSeparation = dot(segmentEnd, normal) - dotFace;

            int sidednessCounter = 0;
            int oppositeSides = sidednessCounter++;
            int sameSides = sidednessCounter++;
            int coplanar = sidednessCounter++;
            int sidedness = -1;

            if (dotEndSeparation >= 0 != dotStartSeparation >= 0) {
                sidedness = oppositeSides;
            }
            if (dotEndSeparation >= 0 == dotStartSeparation >= 0) {
                sidedness = sameSides;
            }
            if (dotEndSeparation == 0 && dotStartSeparation == 0) {
                sidedness = coplanar;
            }

            int solutionCount = 0;
            int doRay = solutionCount++;
            int slideCloser = solutionCount++;
            int slideBoth = solutionCount++;
            int slideOuter = solutionCount++;
            int solution = -1;
            int fallback = -1;


            int comparisonCounter = 0;
            int equidistant = comparisonCounter++;
            int inequality = comparisonCounter++;
            int oneIsZero = comparisonCounter++;
            int comparison = -1;

            float dotStartDistance = Math.Abs(dotStartSeparation);
            float dotEndDistance = Math.Abs(dotEndSeparation);
            if (dotStartDistance == dotEndDistance)
                comparison = equidistant;
            if (dotStartDistance != dotEndDistance)
                comparison = inequality;
            if (dotStartDistance == 0 ^ dotEndDistance == 0)
                comparison = oneIsZero;

            int stateCounter = 0;
            int containsBoth = stateCounter++;
            int containsOne = stateCounter++;
            int containsNeither = stateCounter++;
            int state = -1;
            if (containsA && containsB)
                state = containsBoth;
            else if (containsA ^ containsB)
                state = containsOne;
            else
                state = containsNeither;

            if (sidedness == coplanar)
            {
                solution = slideBoth;
            }
            else if(state == containsBoth)
            {
                if (sidedness == oppositeSides)
                {
                    solution = doRay;
                    fallback = slideCloser;
                }
                else if (sidedness == sameSides)
                {
                    if (comparison == equidistant)
                    {
                        solution = slideBoth;
                    }
                    else if (comparison == inequality || comparison == oneIsZero)
                    {
                        solution = slideCloser;
                    }
                }
            }
            else if(state == containsOne)
            {
                if(sidedness == oppositeSides)
                {
                    solution = doRay;
                    if(comparison == oneIsZero)
                    {
                        fallback = slideCloser;
                    }
                    else if(comparison == equidistant || comparison == inequality)
                    {
                        fallback = slideOuter;
                    }
                }
                else if(sidedness == sameSides)
                {
                    if(comparison == equidistant)
                    {
                        solution = slideBoth;
                    }
                    else if(comparison == inequality || comparison == oneIsZero)
                    {
                        solution = slideCloser;
                    }
                }
            }
            else if(state == containsNeither)
            {
                if(sidedness == oppositeSides)
                {
                    solution = doRay;
                    fallback = slideCloser;
                }
                else if(sidedness == sameSides)
                {
                    if(comparison == equidistant)
                    {
                        solution = slideBoth;
                    }
                    else if(comparison == inequality || comparison == oneIsZero)
                    {
                        solution = slideCloser;
                    }
                }
            }
            
            float segmentLength = line.Length();
            Vector3 segmentDirection = line / segmentLength;
            Vector3 outter = segmentStart;
            Vector3 outterFlat = flatStart;
            Vector3 outterDirection = segmentDirection;
            float outterDist = dotStartSeparation;
            Vector3 inner = segmentEnd;
            Vector3 innerFlat = flatEnd;
            float innerDist = dotEndSeparation;
            if (containsA)
            {
                outter = segmentEnd;
                outterFlat = flatEnd;
                outterDist = dotEndSeparation;
                outterDirection = -segmentDirection;
                inner = segmentStart;
                innerFlat = flatStart;
                innerDist = dotStartSeparation;
            }
            Vector3 closer = segmentStart;
            Vector3 closerFlat = flatStart;
            Vector3 closerDirection = segmentDirection;
            float closerDist = dotStartSeparation;
            if(dotStartDistance > dotEndDistance)
            {
                closer = segmentEnd;
                closerFlat = flatEnd;
                closerDirection = -segmentDirection;
                closerDist = dotEndSeparation;
            }

            bool requestFallback = false;
            for (int i = 0; i < 2; ++i)
            {
                int currentSolution = solution;
                if(i == 1)
                {
                    currentSolution = fallback;
                    if(!requestFallback)
                    {
                        break;
                    }
                }
                if (currentSolution == doRay)
                {
                    Ray ray = new Ray(segmentStart, segmentDirection);
                    if (dotStartSeparation < 0)
                    {
                        ray.Position = segmentEnd;
                        ray.Direction = -ray.Direction;
                    }
                    float? rayhit = ray.Intersects(new Plane(-normal, dotFace));
                    if (rayhit.HasValue && rayhit.Value < segmentLength)
                    {
                        Vector3 contact = ray.Position + ray.Direction * rayhit.Value;
                        game1.add3DLine(contact, contact + normal, Color.Goldenrod);
                        game1.add3DLine(contact, pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, contact, segmentDirection, segmentLength, false), Color.Goldenrod);
                        if (GetAxisClamp(intersectionBox, tangent, contact) == 0 &&
                            GetAxisClamp(intersectionBox, binormal, contact) == 0)
                        {
                            closestPointOnBoxFace = contact;
                        }else
                        {
                            requestFallback = true;
                        }
                    }
                    else
                    {
                        requestFallback = true;
                    }
                }
                else if(currentSolution == slideCloser)
                {
                    closestPointOnBoxFace = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, closer, closerDirection, segmentLength, true);
                }
                else if (currentSolution == slideOuter)
                {
                    closestPointOnBoxFace = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, outter, outterDirection, segmentLength, true);
                }
                else if (currentSolution == slideBoth)
                {
                    Vector3 slidA = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentStart, segmentDirection, segmentLength, true);
                    Vector3 slidB = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentStart, segmentDirection, segmentLength, true);
                    closestPointOnBoxFace = slidA + (slidB - slidA) / 2;
                }
            }

            //float segmentLength = line.Length();
            //Vector3 segmentDirection = line / segmentLength;
            //bool hitRay = false;
            //Vector3 rayHit = Vector3.Zero;
            //string entry = "no entry?";
            //if (containsA && containsB)
            //{
            //    entry = "A & B contained: ";
            //    if (dotStartSeparation == 0 && dotEndSeparation == 0) //both sit on face
            //    {
            //        entry += "both on plane.";
            //        closestPointOnBoxFace = flatStart + flatLine / 2;
            //    }
            //    else if (dotStartSeparation == 0) //start sits on face
            //    {
            //        entry += "A on plane";
            //        closestPointOnBoxFace = flatStart;
            //    }
            //    else if (dotEndSeparation == 0) //end sits on face
            //    {
            //        entry += "B on plane";
            //        closestPointOnBoxFace = flatEnd;
            //    }
            //    else if (dotStartSeparation < 0 != dotEndSeparation < 0) //intersecting
            //    {
            //        entry += "opposing sides";
            //        Ray ray = new Ray(segmentStart, segmentDirection);
            //        if (dotStartSeparation < 0)
            //        {
            //            ray.Position = segmentEnd;
            //            ray.Direction = -segmentDirection;
            //        }
            //        float? hit = ray.Intersects(new Plane(-normal, dotFace));
            //        if (hit.HasValue && hit.Value > 0 && hit.Value < segmentLength)
            //        {
            //            rayHit = ray.Position + ray.Direction * hit.Value;
            //            hitRay = true;
            //            closestPointOnBoxFace = rayHit;
            //        }
            //    }
            //    else if (Math.Abs(dotStartSeparation) < Math.Abs(dotEndSeparation))
            //    {
            //        entry += "A closer";
            //        closestPointOnBoxFace = flatStart;
            //    }
            //    else
            //    {
            //        entry += "B closer";
            //        closestPointOnBoxFace = flatEnd;
            //    }
            //}
            //else if(containsA ^ containsB)
            //{
            //    entry = "A outside: ";
            //    Vector3 outter = segmentStart;
            //    Vector3 outterFlat = flatStart;
            //    float outterDist = dotStartSeparation;
            //    Vector3 inner = segmentEnd;
            //    Vector3 innerFlat = flatEnd;
            //    float innerDist = dotEndSeparation;
            //    if(containsA)
            //    {
            //        entry = "B outside: ";
            //        outter = segmentEnd;
            //        outterFlat = flatEnd;
            //        outterDist = dotEndSeparation;
            //        inner = segmentStart;
            //        innerFlat = flatStart;
            //        innerDist = dotStartSeparation;
            //    }
            //    if((dotStartSeparation == 0 && dotEndSeparation == 0) ||
            //        dotStartSeparation == dotEndSeparation)
            //    {
            //        entry += "equidistant, slide outter";
            //        closestPointOnBoxFace = pointClosestPointOnBoxFace(intersectionBox,
            //            normal,
            //            tangent,
            //            binormal,
            //            outterFlat,
            //            -segmentDirection,
            //            segmentLength,
            //            true);
            //    }
            //    else if(innerDist > 0 == outterDist > 0)
            //    {
            //        entry += "same side, ";
            //        if (Math.Abs(innerDist) < Math.Abs(outterDist))
            //        {
            //            entry += "inner closer";
            //            closestPointOnBoxFace = innerFlat;
            //        }else
            //        {
            //            entry += "outter closer, slide outter";
            //            closestPointOnBoxFace = pointClosestPointOnBoxFace(intersectionBox,
            //                normal,
            //                tangent,
            //                binormal,
            //                outterFlat,
            //                -segmentDirection,
            //                segmentLength,
            //                true);
            //        }
            //    }
            //    else
            //    {
            //        entry += "opposite sides, ";
            //        Vector3 ourLine = outter - inner;
            //        float ourLength = ourLine.Length();
            //        Vector3 ourDir = ourLine / ourLength;
            //        Ray ray = new Ray(inner, ourDir);
            //        if(innerDist < 0)
            //        {
            //            ray.Position = outter;
            //            ray.Direction = -ray.Direction;
            //        }
            //        float? hit = ray.Intersects(new Plane(-normal, dotFace));
            //        bool hitConfirmed = false;
            //        if(hit.HasValue && hit.Value < ourLength)
            //        {
            //            Vector3 contact = ray.Position + hit.Value * ray.Direction;
            //            if (GetAxisClamp(intersectionBox, tangent, contact) == 0 &&
            //                GetAxisClamp(intersectionBox, binormal, contact) == 0)
            //            {
            //                entry += " ray hit";
            //                closestPointOnBoxFace = contact;
            //                hitConfirmed = true;
            //            }
            //        }
            //        if(!hitConfirmed)
            //        {
            //            entry += " slide outter";
            //            closestPointOnBoxFace = pointClosestPointOnBoxFace(
            //                intersectionBox,
            //                normal, tangent, binormal, outterFlat, -segmentDirection, segmentLength, true);
            //        }
            //    }
            //}
            //else
            //{

            //}
            //float closestPointDistSq = float.MaxValue;
            ////start point
            //Vector3 startDirect = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentStart, segmentDirection, segmentLength, false);
            //SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, startDirect);
            //Vector3 startSlide = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentStart, segmentDirection, segmentLength, true);
            //SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, startSlide);
            ////end point
            //Vector3 endDirect = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentEnd, -segmentDirection, segmentLength, false);
            //SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, endDirect);
            //Vector3 endSlide = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, segmentEnd, -segmentDirection, segmentLength, true);
            //SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, endSlide);
            ////point along segment
            //float dotLine = dot(line, normal);
            //if (dotLine != 0)
            //{
            //    Ray segmentRay = new Ray();
            //    segmentRay.Direction = segmentDirection;
            //    segmentRay.Position = segmentStart;
            //    if (dotLine > 0) //points away from plane
            //    {
            //        segmentRay.Position = segmentEnd;
            //        segmentRay.Direction = -segmentRay.Direction;
            //    }
            //    Plane plane = new Plane(normal, Math.Min(dot(intersectionBox.Max, normal), dot(intersectionBox.Min, normal)));
            //    float? hit = segmentRay.Intersects(plane);
            //    Vector3 onPlane = segmentRay.Position;
            //    if (hit.HasValue) //ray is not exactly on plane
            //        onPlane = segmentRay.Position + hit.Value * segmentRay.Direction;
            //    Vector3 onFaceDirect = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, onPlane, segmentRay.Direction, segmentLength, false);
            //    SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, onFaceDirect);
            //    Vector3 onFaceSlide = pointClosestPointOnBoxFace(intersectionBox, normal, tangent, binormal, onPlane, segmentRay.Direction, segmentLength, true);
            //    SampleSetClosestPointOnFace(normal, tangent, binormal, segmentStart, segmentEnd, intersectionBox, ref closestPointOnBoxFace, ref closestPointDistSq, onFaceSlide);
            //    if (true)
            //    {
            //        //game1.add3DLine(segmentRay.Position, onFace, Color.Gray);
            //        //debugPoints.Add(new object[] { "projection", segmentRay.Position + Vector3.Up / 8 });
            //    }
            //}
            //debug
            if (true)
            {
                Color c = Color.Magenta;
                Vector3 co = normal / 8;
                game1.add3DLine(flatStart, flatStart + co, containsA ? Color.Red : c);
                game1.add3DLine(flatEnd, flatEnd + co, containsB ? Color.Red : c);
                game1.add3DLine(closestPointOnBoxFace,
                    pointClosestPointOnSegment(closestPointOnBoxFace, segmentStart, segmentEnd), 
                    Color.Blue);
                game1.add3DLine(closestPointOnBoxFace,
                    closestPointOnBoxFace + normal/2, Color.Blue);
                debugPoints.Add(new object[] { "A", segmentStart+normal/2});
                debugPoints.Add(new object[] { "B", segmentEnd +normal/2});
                string entry = "";
                entry += state == containsBoth ? "Contains Both" : state == containsOne ? "Contains One" : state == containsNeither ? "Contains Neither" : "ERR_STATE";
                entry += ": ";
                entry += sidedness == -1 ? "ERR_SIDE" : new string[]{ "Opposite Sides", "Same Sides", "Coplanar"}[sidedness];
                entry += ", ";
                entry += comparison == -1 ? "ERR_COMP" : new string[] { "Equidistant", "Inequality", "One is Zero" }[comparison];
                debugPoints.Add(new object[] { entry, closestPointOnBoxFace + normal });
                //game1.add3DLine(segmentStart, result, Color.Magenta);
                //game1.add3DLine(segmentEnd, tailResult, Color.Magenta);
                //debugPoints.Add(new object[] { "start", segmentStart });
                //debugPoints.Add(new object[] { "end", segmentEnd });
            }
            return closestPointOnBoxFace;
        }

        private void SampleSetClosestPointOnFace(Vector3 normal, Vector3 tangent, Vector3 binormal, Vector3 segmentStart, Vector3 segmentEnd, BoundingBox intersectionBox, ref Vector3 closestPointOnBoxFace, ref float closestPointDistSq, Vector3 onFace)
        {
            Vector3 onSeg = pointClosestPointOnSegment(onFace, segmentStart, segmentEnd);
            Vector3 toFace = onFace - onSeg;
            float lenSq = toFace.LengthSquared();
            if (lenSq < closestPointDistSq)
            {
                closestPointDistSq = lenSq;
                closestPointOnBoxFace = onFace;
            }
            if (false)
            {
                game1.add3DLine(onSeg, onFace, Color.Yellow);
                debugPoints.Add(new object[] { lenSq, onFace });
            }
        }

        public Vector3 pointClosestPointOnBoxFace(BoundingBox testBox, Vector3 normal, Vector3 tangent, Vector3 binormal, Vector3 sample, Vector3 sampleDirection, float segmentLength, bool doSlide)
        {
            Vector3 result;
            //project
            float dotOrigin = Math.Max(dot(testBox.Max, normal), dot(testBox.Min, normal));
            float dotA = dot(normal, sample) - dotOrigin;
            //float dotB = dot(normal, capsuleTail) - dotOrigin;
            Vector3 pointOnPlane = sample - dotA * normal;
            Vector3 offset = Vector3.Zero;

            Vector3 FLAT = sampleDirection * segmentLength - dot(sampleDirection * segmentLength, normal) * normal;
            float flatLength = FLAT.Length();
            if(flatLength> 0)
                FLAT /= flatLength;
            //tangent
            float tngOffset = GetAxisClamp(testBox, tangent, pointOnPlane);
            float tngFactor = dot(tangent, FLAT);
            //if (tngFactor <= 0 || (1 / tngFactor) * tngOffset > flatLength)
            if (tngFactor == 0 || !doSlide)
                offset += tangent * tngOffset;
            else
                offset += FLAT / tngFactor * tngOffset;
            //binormal
            float binOffset = GetAxisClamp(testBox, binormal, pointOnPlane + offset);
            float binFactor = dot(binormal, FLAT);
            //if (binFactor <= 0 || (1 / binFactor) * binOffset > flatLength)
            if (binFactor == 0 || !doSlide)
            offset += binormal * binOffset;
            else
                offset += FLAT / binFactor * binOffset;
            if (false)
            {
                Vector3 tng = tngOffset * tangent;
                Vector3 bin = binOffset * binormal;
                game1.add3DLine(sample, pointOnPlane, Color.Red);
                game1.add3DLine(pointOnPlane, pointOnPlane + offset, Color.Blue);
                game1.add3DLine(pointOnPlane + offset + normal / 8, pointOnPlane + offset, Color.Green);
                game1.add3DLine(pointOnPlane + tng, pointOnPlane, Color.Orange);
                game1.add3DLine(pointOnPlane, pointOnPlane + bin, Color.Cyan);
                game1.add3DLine(pointOnPlane, pointOnPlane + bin + tng, new Color(80, 160, 80));
                //debugPoints.Add(new object[] { "plane", pointOnPlane });
                debugPoints.Add(new object[] { "face", pointOnPlane + offset + normal/4 });
                debugPoints.Add(new object[] { "tng", pointOnPlane + tng });
                debugPoints.Add(new object[] { "bin", pointOnPlane + bin });
                debugPoints.Add(new object[] { "off", pointOnPlane + bin + tng + normal / 8 });
            }
            return pointOnPlane + offset;
        }
        public Vector3 pointClosestPointOnSegment(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd)
        {
            Vector3 line = segmentEnd - segmentStart;
            float faceLineFactor = dot(point - segmentStart, line) / dot(line, line);
            return segmentStart + line * MathHelper.Clamp(faceLineFactor, 0, 1);
        }

        public float GetAxisClamp(BoundingBox bounds, Vector3 axis, Vector3 sample)
        {
            float inMax = dot(bounds.Max, axis);
            float inMin = dot(bounds.Min, axis);
            float zMax = Math.Max(inMax, inMin);
            float zMin = Math.Min(inMax, inMin);
            float zHead = dot(axis, sample);
            if (zHead > zMax)
            {
                return (zMax - zHead);
            }
            else if (zHead < zMin)
            {
                return (zMin - zHead);
            }
            return 0;
        }

        private void DrawBoundingSphere(BoundingSphere bsphere, Color sc, float radAdd, bool lighting)
        {
            game1.DrawModel(game1.sphereModel, Matrix.CreateScale(bsphere.Radius + radAdd) *
                Matrix.CreateTranslation(bsphere.Center), camera.view, camera.projection, sc, lighting);
        }

        private void DrawBox(Vector3 boxScale, Vector3 boxPosition, Color bc, float radAdd, bool lighting)
        {
            game1.DrawModel(game1.cubeModel,
                Matrix.CreateScale(boxScale + new Vector3(radAdd)) *
                Matrix.CreateTranslation(boxPosition),
                camera.view,
                camera.projection,
                bc,
                lighting);
        }

        //bool intersectBoxBox(BoundingBox a, BoundingBox b, out Vector3 contact)
        //{
        //    float penetration = float.MaxValue;
        //    penetration = Math.Min(a.Min.X - b.Max.X);
        //    if(a.Min.X - b.Max.X > 0)
        //    {
        //        return false;
        //    }
        //}

        bool duringDistasterUseCapsuleLineAsNormal = true;
        ContactData intersectSphereCapsule(BoundingSphere sphere, Vector3 head, Vector3 tail)
        {
            Vector3 start = head;
            Vector3 end = tail;
            game1.add3DLine(start, end, Color.White);
            float small = 0.1f;
            //game1.add3DLine(sphere.Center + Vector3.Down * small, sphere.Center + Vector3.Up * small, Color.White);
            float radius = 1;

            ContactData data = null;
            Vector3 line = end - start;
            float ll = dot(line, line);
            float dsl = dot(sphere.Center - start, line);
            float distanceFactor = dsl / ll;
            if (distanceFactor < 0) distanceFactor = 0; //clamp
            if (distanceFactor > 1) distanceFactor = 1;
            Vector3 closestPoint = start + (line * distanceFactor);

            Vector3 collisionVector = sphere.Center - closestPoint;
            float distance = collisionVector.Length();
            Vector3 normal = collisionVector / distance;
            if (distance == 0)
            {
                if (duringDistasterUseCapsuleLineAsNormal)
                {
                    float trueFactor = dot(sphere.Center - start, Vector3.Normalize(line));
                    normal = Vector3.Normalize(line);
                    if (distanceFactor > 0.5f)
                    {
                        normal = -normal;
                    }
                    //if(distanceFactor > )
                }
            }
            //game1.add3DLine(closestPoint, sphere.Center, Color.Cyan);

            float radiusSum = sphere.Radius + radius;
            if (distance < radiusSum)
            {
                data = new ContactData();
                data.norm = -normal;
                data.contact = sphere.Center - sphere.Radius * normal;
                data.pen = radiusSum - distance;
                Vector3 contactA, contactB;
                //game1.add3DLine(sphere.Center - sphere.Radius * normal,
                //    closestPoint + sphere.Radius * normal, Color.Yellow);
            }
            return data;
        }
        public static float dot(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a, b);
        }

        void DrawBox(float width, float height, float depth, Vector3 position, Color color, bool lighting)
        {

        }
        Matrix[] DrawCapsule(float radius, Vector3 position, Vector3 tailPos, Color color, bool lighting)
        {
            Vector3 head = position;
            Vector3 tail = tailPos;
            //sphere a
            Matrix matrixA =
                Matrix.CreateScale(radius, radius, radius) * Matrix.CreateTranslation(head);
            game1.DrawModel(icoSphere,
            matrixA,
                camera.view, camera.projection, color,
                lighting);
            //sphere b
            Matrix matrixB = Matrix.CreateScale(radius, radius, radius) * Matrix.CreateTranslation(tail);
            game1.DrawModel(icoSphere,
                matrixB,
                camera.view, camera.projection, color,
                lighting);
            Vector3 line = tail - head;
            float height = line.Length();
            Vector3 center = head + line / 2;
            Vector3 direction = line / height;
            //game1.add3DLine(head, head + direction * 5, Color.Yellow, Color.Cyan);
            Vector3 up = Vector3.Up;
            if (Math.Abs(dot(up, direction)) > 0.98f)
                up = Vector3.Right;
            Matrix look = Matrix.CreateLookAt(head, tail, up);
            float length = height;
            float s = radius * 2;
            Matrix orientation = Matrix.Invert(look);
            Matrix world =
                Matrix.CreateTranslation(0, 0, -1) *
                Matrix.CreateScale(s / 2, s / 2, length / 2) *
                orientation;
            //cylinder
            game1.DrawModel(cylinder,
                world
                , camera.view, camera.projection, color,
                lighting);
            return new Matrix[] { matrixA, matrixB, world };
        }
    }
}
