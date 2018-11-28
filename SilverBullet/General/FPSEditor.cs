using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MknGames;
using MknGames.FPSWahtever;
using MknGames.Split_Screen_Dungeon;
using SilverBullet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class FPSEditor
    {
        public bool editOutOfBody = false;
        public EditSmallFPSForm editor;
        Thread editorThread;
        public SmallFPSEditState edit = new SmallFPSEditState();
        //because 1 / 2 = 0.5f so it sorta makes sense to support fractions of 0.5f
        public float editSnapSize = 0.125f;
        //List<Box> clipboardBoxes = new List<Box>();
        //Vector3[] clipboardBoxOffsets = null;
        //Vector3 clipboardBoxContainerSize;
        public Rectangle? mouseUi = null;
        public Gun editHoverGun = null;
        public Gun editGun = null;
        Textbox currentTextbox = null;
        Textbox levelFileTxt;
        Textbox gunFiletxt;
        bool editMultiSelectInclusively = false;
        public bool editUseSelectionBrush = false;
        float editSelectionBrushRadius = 2;
        public bool levelUnedited = true;
        public bool saveRequested = false;
        public bool editBoxesRequestSubdivide;
        public bool editBoxesRequestFocus;
        public string currentLevelFilename = "";
        SmallFPS fps;
        
        Action<Box> RemoveBox
        {
            get { return fps.RemoveBox; }
        }

        public FPSEditor()
        {
            levelFileTxt = new Textbox((string text) => { currentLevelFilename = text; },
                () => { return currentLevelFilename; });
        }

        public void Cleanup()
        {
            if (editor.Visible)
                editorThread.Abort();
        }

        public void LoadContent(SmallFPS fps)
        {
            this.fps = fps;
            editor = new EditSmallFPSForm(fps);
        }

        public void OpenVariableEditor()
        {
            if (editorThread == null || editorThread.ThreadState == System.Threading.ThreadState.Stopped)
            {
                editorThread = new Thread(new ThreadStart(() =>
                {
                    editor.ShowDialog();
                }));
            }
            editorThread.Start();
        }

        public bool RequestText(GameMG game1, Textbox caller)
        {
            game1.restrictedInput = true;
            currentTextbox = caller;
            return true;
        }

        public static float snap(float value, float snapValue)
        {
            float remainder = 0;
            float remaining = snapValue;
            float high = snapValue;
            float low = 0;
            if (value > 0)
            {
                // positive
                // [low, value, high]
                // <-     snap     ->
                // x % y = y(----)x--y
                remainder = value % snapValue;
                remaining = snapValue - remainder;
                high = value + remaining;
                low = value - remainder;
            }
            if (value < 0)
            {
                // negative
                // [-low, -value, -high]
                // quotient = value / -snap = +x
                // +x % 1 = remainderPercentage
                float q = value / -snapValue; //quotient
                float rp = q % 1.0f; //remainder percentage
                remainder = rp * snapValue;
                remaining = snapValue - remainder;
                high = value + remaining;
                low = value - remainder;
            }
            if (remainder < remaining) return low;
            else if (remaining < remainder) return high;
            else return high;
        }

        public float snap(float value)
        {
            return snap(value, editSnapSize);
        }

        public Vector3 snap(Vector3 vector)
        {
            return new Vector3(snap(vector.X), snap(vector.Y), snap(vector.Z));
        }

        public static Vector3 snap(Vector3 vector, float snapValue)
        {
            return new Vector3(snap(vector.X, snapValue), snap(vector.Y, snapValue), snap(vector.Z, snapValue));
        }

        public static float GetVectorComponent(Vector3 vector, int index)
        {
            if (index < 0 || index > 2)
                throw new ArgumentOutOfRangeException();
            return index == 0 ? vector.X : index == 1 ? vector.Y : vector.Z;
        }
        public static void SetVectorComponent(ref Vector3 vector, int index, float value)
        {
            if (index < 0 || index > 2)
                throw new ArgumentOutOfRangeException();
            if (index == 0)
                vector.X = value;
            else if (index == 1)
                vector.Y = value;
            else
                vector.Z = value;
        }

        public void Update(GraphicsDevice GraphicsDevice, GameMG game1, PhysicsState bodyState, CameraState playerCam, Vector3 drop, float dropValue)
        {
            bool controlDown = game1.kdown(Keys.LeftControl) ||
                game1.kdown(Keys.RightControl);
            bool shiftDown =
                game1.kdown(Keys.LeftShift) ||
                game1.kdown(Keys.RightShift);
            bool modifierDown = controlDown || shiftDown;

            // edit form
            if (game1.kclick(Keys.F8))
            {
                OpenVariableEditor();
            }

            if (!game1.IsActive)
                return;
            if (game1.kclick(Keys.E))
            {
                edit.active = !edit.active;
                if (controlDown)
                {
                    bodyState.pos = playerCam.pos - drop;
                }
            }
            if (!edit.active) return;

            //edit pre-edit save
            if (levelUnedited)
            {
                string preEditCopyFilename = "most-recent-backup.txt";
                fps.SaveLevel(preEditCopyFilename);
                levelUnedited = false;
            }

            //edit autosave
            bool wantAutoSave = false;
            string autoSaveFilename = "autosave-" + currentLevelFilename;
            if (!File.Exists(autoSaveFilename))
            {
                wantAutoSave = true;
            }
            else
            {
                var lastSave = File.GetLastWriteTime(autoSaveFilename);
                var elapsed = DateTime.Now - lastSave;
                if (elapsed.TotalMinutes > 5)
                {
                    wantAutoSave = true;
                }
            }
            if (wantAutoSave && File.Exists(currentLevelFilename))
            {
                fps.SaveLevel(autoSaveFilename);
            }

            //edit ui
            // update mouse hit box

            //edit brush
            //toggle brush
            if (game1.kclick(Keys.B))
            {
                editUseSelectionBrush = !editUseSelectionBrush;
            }

            //edit update mouse lock
            if (game1.kclick(Keys.Tab))
            {
                fps.editMouseLocked = !fps.editMouseLocked;
            }

            Ray mouseray = playerCam.ScreenToRay(game1.mouseCurrent.Position.ToVector2(), GraphicsDevice.Viewport);
            float minboxhit = float.MaxValue;

            // edit hover
            edit.hoverbox0 = edit.hoverbox;
            edit.hoverbox = null;
            edit.hoverTarget = -1;
            editHoverGun = null;
            if (mouseUi.HasValue == false)
            {
                //TODO: speedup getZone(ray) to make this faster
                //hover box
                foreach (Box box in fps.allBoxes)
                {
                    //if (box.isnew)
                    //{
                    //    box.isnew = false;
                    //    box.position0 = box.position;
                    //}
                    BoundingBox boxVolume = box.boundingBox;
                    {
                        float? hit = mouseray.Intersects(boxVolume);
                        if (hit.HasValue && hit.Value < minboxhit && hit.Value > 0)
                        {
                            edit.hoverbox = box;
                            minboxhit = hit.Value;
                            Vector3 contact = mouseray.Position + mouseray.Direction * hit.Value;
                            float pen = 0;
                            Vector3 normal;
                            SmallFPS.intersectBoxPoint(contact, boxVolume, out normal, out pen);
                            edit.hoverboxContact = contact;
                            edit.hoverboxNormal = normal;
                        }
                    }
                    //Func<Vector3, Point> gp = (Vector3 pos) =>
                    //{
                    //    return new Point(
                    //    (int)(pos.X / box.size.X),
                    //    (int)(pos.X / box.size.Z)
                    //    );
                    //};
                    //for (int b = 0; b < grenadebullets.Length; ++b)
                    //{
                    //    if (gameTime.TotalGameTime.TotalSeconds % 0.1f > et)
                    //        continue;
                    //    Bullet B = grenadebullets[b];
                    //    resolveDir = Vector3.Zero;
                    //    BoundingSphere bulletSV2 = new BoundingSphere(grenadebullets[b].p.pos, 0.1f);
                    //    if (gp(B.p.pos) == gp(box.position))// Vector3.Distance(B.p.pos, box.position) < box.size.Length() / 100)
                    //    {
                    //        resolveCollision(ref grenadebullets[b].p, bulletSV2, boxVolume);
                    //    }
                    //resolveDirs[b] += resolveDir;
                    //}
                    //if(box.isdoor)
                    //{
                    //    BoundingBox bb = Game1.MakeBox(box.position0, box.size + new Vector3(0, 0, 10));
                    //    box.color = monochrome(0.5f);
                    //    float targY = box.position0.Y;
                    //    if (bb.Contains(bodyState.pos) == ContainmentType.Contains)
                    //    {
                    //        targY = box.position0.Y - box.size.Y + 0.01f;
                    //    }
                    //    box.position.Y = MathHelper.Lerp(box.position.Y, targY, 0.1f);
                    //}
                }
                //hover target
                var targets = fps.targets;
                for (int i = 0; i < targets.Length; ++i)
                {
                    //float? hit = mouseray.Intersects(targets[i]);
                    BoundingSphere bs = new BoundingSphere(targets[i].Center, targets[i].Radius);
                    float? hit = mouseray.Intersects(bs);
                    if (hit.HasValue && hit.Value > 0 && hit.Value < minboxhit)
                    {
                        Vector3 contact = mouseray.Position + mouseray.Direction;
                        Vector3 toContact = contact - targets[i].Center;
                        float len = toContact.Length();
                        if (len > 0)
                        {
                            minboxhit = hit.Value;
                            edit.hoverbox = null;
                            edit.hoverTarget = i;
                            edit.hoverboxNormal = toContact / len;
                            edit.hoverboxContact = contact;
                        }
                    }
                }
                //hover gun
                var allguns = fps.allguns;
                for (int i = 0; i < allguns.Count; ++i)
                {
                    ///gun
                    float? hit = SmallFPS.IntersectRayGun(mouseray, allguns[i]);
                    if (hit.HasValue && hit.Value > 0 && hit.Value < minboxhit)
                    {
                        minboxhit = hit.Value;
                        edit.hoverbox = null;
                        edit.hoverTarget = -1;
                        editHoverGun = allguns[i];
                    }
                }
            }

            if (game1.kclick(Keys.O))
            {
                editOutOfBody = !editOutOfBody;
            }

            //enter text mode
            game1.BeginUnrestrictedInput();
            if (game1.kclick(Keys.OemTilde) && !game1.restrictedInput)
            {
                //filename = "";
                RequestText(game1, levelFileTxt);
            }
            game1.EndUnrestrictedInput();

            //edit selection
            bool ldown = game1.mouseCurrent.LeftButton == ButtonState.Pressed;
            bool ltap = ldown && game1.mouseOld.LeftButton == ButtonState.Released;
            if (ldown && !mouseUi.HasValue)// && clipboardBoxes.Count == 0)
            {
                if (edit.hoverbox != null)
                {
                    List<Box> selectionBoxes = new List<Box>();
                    selectionBoxes.Add(edit.hoverbox);
                    if (editUseSelectionBrush)
                    {
                        BoundingSphere cursor3d = new BoundingSphere(edit.hoverboxContact, editSelectionBrushRadius);
                        Rectangle cursorZone = fps.getzone(cursor3d);
                        List<Box> nearbyBoxes = fps.getBoxesInZone(cursorZone);
                        foreach (Box b in nearbyBoxes)
                        {
                            if (b.boundingBox.Intersects(cursor3d))
                            {
                                selectionBoxes.Add(b);
                            }
                        }
                    }
                    if (!shiftDown && ltap)
                    {
                        edit.boxes.Clear();
                        //edit.boxes.Add(edit.hoverbox);
                        foreach (Box b in selectionBoxes)
                        {
                            if (!edit.boxes.Contains(b))
                                edit.boxes.Add(b);
                        }
                        edit.target = -1;
                        editGun = null;
                    }
                    if (shiftDown)
                    {
                        //if the selection is fresh we can find out if the following
                        //selections should be inclusive or exclusive
                        if (ltap)
                        {
                            if (edit.boxes.Contains(edit.hoverbox))
                                editMultiSelectInclusively = false;
                            else
                                editMultiSelectInclusively = true;

                        }
                        if (edit.hoverbox != edit.hoverbox0 || ltap)
                        {
                            //if (!editMultiSelectInclusively && edit.boxes.Contains(edit.hoverbox))
                            //{
                            //    edit.boxes.Remove(edit.hoverbox);
                            //}
                            //if (editMultiSelectInclusively && !edit.boxes.Contains(edit.hoverbox))
                            //{
                            //    edit.boxes.Add(edit.hoverbox);
                            //}
                            foreach (Box b in selectionBoxes)
                            {
                                if (!editMultiSelectInclusively && edit.boxes.Contains(b))
                                {
                                    edit.boxes.Remove(b);
                                }
                                if (editMultiSelectInclusively && !edit.boxes.Contains(b))
                                {
                                    edit.boxes.Add(b);
                                }
                            }
                            edit.target = -1;
                            editGun = null;
                        }
                    }
                }
                if (edit.hoverTarget > -1)
                {
                    if (!shiftDown)
                    {
                        edit.boxes.Clear();
                        editGun = null;
                    }
                    edit.target = edit.hoverTarget;
                }
                if (editHoverGun != null)
                {
                    if (!shiftDown)
                    {
                        edit.boxes.Clear();
                        edit.target = -1;
                    }
                    editGun = editHoverGun;
                    gunFiletxt = new Textbox((string text) => { editGun.filename = text; },
                        () => { return editGun.filename; });
                }
            }

            //edit deselect
            if (game1.rmouse && !game1.rmouseOld)
            {
                //if (clipboardBoxes.Count > 0)
                //{
                //    clipboardBoxes.Clear();
                //}
                //else
                //{
                edit.boxes.Clear();
                edit.target = -1;
                editGun = null;
                //}
            }

            //edit spawn rotation
            {
                float rotateStepAngle = MathHelper.PiOver4 / 16;
                Vector3 start = fps.playerSpawnEuler;
                if (game1.kclickheld(Keys.OemPlus))
                    fps.playerSpawnEuler.Y -= rotateStepAngle;
                if (game1.kclickheld(Keys.OemMinus))
                    fps.playerSpawnEuler.Y += rotateStepAngle;
                if (start != fps.playerSpawnEuler)
                {
                    edit.saveNeeded = true;
                }
            }

            if (edit.hoverbox != null && !modifierDown)
            {
                Vector3 boxTop = edit.hoverbox.position +
                    Vector3.Up * (edit.hoverbox.size.Y / 2);
                // edit player, edit spawn point
                if (game1.kdown(Keys.D0))
                {
                    fps.playerSpawnPoint = boxTop +
                    Vector3.Up * (fps.height / 2);
                }
                // edit add box
                if (game1.kclick(Keys.N))
                {
                    Box newBox = new Box(Vector3.One, edit.hoverboxContact + edit.hoverboxNormal * 0.5f);
                    newBox.color = fps.monochrome(0.9f);
                    fps.AddBox(newBox);
                }
                //edit place target, edit add target
                if (game1.kclick(Keys.T))
                {
                    fps.AddTarget(snap(edit.hoverboxContact + Vector3.Up * dropValue / 2), dropValue / 2);
                }
                //edit add gun, edit new gun, edit place gun
                if (game1.kclick(Keys.G))
                {
                    Gun gun = new Gun();
                    gun.pos = edit.hoverboxContact + Vector3.Up * gun.size.Y / 2 + edit.hoverboxNormal;
                    //gun.AddBullets(allbullets);
                    //allguns.Add(gun);
                    fps.AddGun(gun);
                }
                //edit add bullet
                if (game1.kclick(Keys.M))
                {
                    Bullet bullet = new Bullet();
                    bullet.lifeSpan = 3;
                    bullet.off = true;
                    bullet.phy.pos = edit.hoverboxContact + Vector3.Up * bullet.size / 2;
                    fps.allbullets.Add(bullet);
                }
            }

            //edit draw box normal, edit draw gizmo
            game1.add3DLine(edit.hoverboxContact + edit.hoverboxNormal, edit.hoverboxContact + edit.hoverboxNormal * 2, Color.Purple);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Right, Color.Red);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Left, Color.Red);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Up, Color.Green);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Down, Color.Green);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Forward, Color.Blue);
            game1.add3DLine(edit.hoverboxContact, edit.hoverboxContact + Vector3.Backward, Color.Blue);

            //edit transformation, edit translate, edit scale
            Vector3 transformation = Vector3.Zero;
            Vector3 translationTransform = Vector3.Zero;
            Vector3 rotationTransformation = Vector3.Zero;
            bool transformationIsTranslation = false;
            {
                transformationIsTranslation = !controlDown;
                Vector3 change = Vector3.Zero;
                if (game1.kclickheld(Keys.Left))
                {
                    change.X--;
                }
                if (game1.kclickheld(Keys.Right))
                {
                    change.X++;
                }
                if (game1.kclickheld(Keys.Up))
                {
                    change.Z++;
                }
                if (game1.kclickheld(Keys.Down))
                {
                    change.Z--;
                }
                float scroll = game1.mouseCurrent.ScrollWheelValue - game1.mouseOld.ScrollWheelValue;
                if (scroll != 0)
                {
                    change.Y += (scroll / 120);
                }
                if (change.LengthSquared() > 0)
                {
                    //float horizontalAmt = widthm;
                    //float lateralAmt = depthm;
                    //float verticalAmt = 1;
                    //Vector3 changeScale = new Vector3(horizontalAmt, verticalAmt, lateralAmt);
                    Vector3 changeScale = new Vector3(editSnapSize * 2 * 2 * 2);
                    if (shiftDown)
                        changeScale /= 2;
                    transformation = change * changeScale / 2;
                    transformation = snap(transformation);
                    translationTransform = snap(transformation / 2);
                    float rotationSnap = MathHelper.ToRadians(15); //45
                    float rotationScale = rotationSnap * 2;
                    rotationTransformation = change * rotationScale;
                    if (shiftDown)
                        rotationTransformation /= 2;
                    rotationTransformation = snap(rotationTransformation, rotationSnap);
                }
            }

            //edit boxes
            bool deleteClick = game1.kclick(Keys.Back);
            if (edit.boxes.Count > 0)
            {
                //edit scale, edit translate, edit move
                if (transformation.LengthSquared() > 0)
                {
                    foreach (Box b in edit.boxes)
                    {
                        if (transformationIsTranslation)
                        {
                            b.position += translationTransform;
                            b.position = snap(b.position, editSnapSize);
                        }
                        else
                        {
                            b.size += transformation;
                            b.size = snap(b.size, editSnapSize * 2);
                        }
                        //if (change.X != 0 || change.Z != 0)
                        fps.RefreshBox(b);
                    }
                }
                if (game1.kclick(Keys.OemQuestion))
                    editBoxesRequestFocus = true;
                if (editOutOfBody && editBoxesRequestFocus)
                {
                    editBoxesRequestFocus = false;
                    playerCam.pos = edit.boxes[0].position;
                }

                // edit split, edit subdivide
                if (game1.kclick(Keys.J))
                {
                    editBoxesRequestSubdivide = true;
                }
                if (editBoxesRequestSubdivide)
                {
                    editBoxesRequestSubdivide = false;
                    for (int b = 0; b < edit.boxes.Count; ++b)
                    {
                        Box B = edit.boxes[b];
                        Vector3 quarterSize = B.size / 2;
                        quarterSize.Y = B.size.Y;
                        Vector3 planarOffset = quarterSize / 2;
                        planarOffset.Y = 0;
                        Vector3 up = Vector3.Up;
                        Vector3 forward = GameMG.Abs(edit.hoverboxNormal);
                        if (forward.Y != 0)
                            up = Vector3.Right;
                        Vector3 right = GameMG.Abs(Vector3.Cross(up, forward));
                        quarterSize = B.size * (up + right) / 2;
                        planarOffset = quarterSize / 2;
                        quarterSize += B.size * forward;
                        Func<Vector3, Vector3, Color, Box> AddBox = fps.AddBox;
                        Box n0 = AddBox(B.position - planarOffset, quarterSize, B.color);
                        Box n1 = AddBox(B.position + planarOffset, quarterSize, B.color);
                        Box n2 = AddBox(B.position - planarOffset + planarOffset * right * 2, quarterSize, B.color);
                        Box n3 = AddBox(B.position - planarOffset + planarOffset * up * 2, quarterSize, B.color);
                        //n0.type = n1.type = n2.type = n3.type = B.type;
                        //n0.embedsBulletOnImpact = 
                        //    n1.embedsBulletOnImpact = 
                        //    n2.embedsBulletOnImpact = 
                        //    n3.embedsBulletOnImpact =
                        //    B.embedsBulletOnImpact;
                        edit.boxes.RemoveAt(b);
                        RemoveBox(B);
                        edit.boxes.Insert(b, n0);
                        edit.boxes.Insert(b, n1);
                        edit.boxes.Insert(b, n2);
                        edit.boxes.Insert(b, n3);
                        b += 3;
                    }
                }

                //edit join/subtract boxes
                //if (game1.kclick(Keys.L))
                //{
                //    if(shiftDown)
                //        editBoxes
                //}
                //if (editBoxesRequestJoin || editBoxesRequestSubtract)
                if (game1.kclick(Keys.L))
                {
                    if (shiftDown)
                    {
                        if (edit.boxes.Count > 1)
                        {
                            List<BoundingBox> exteriorBoxes = new List<BoundingBox>();
                            Box targetBox = edit.boxes.Last();
                            exteriorBoxes.Add(targetBox.boundingBox);
                            foreach (Box chisel in edit.boxes)
                            {
                                if (chisel == targetBox)
                                    continue;
                                List<BoundingBox> positiveBoxes = new List<BoundingBox>();
                                foreach (BoundingBox initialBox in exteriorBoxes)
                                {
                                    BoundingBox editBox = initialBox;
                                    if (editBox.Intersects(chisel.boundingBox) == false)
                                    {
                                        positiveBoxes.Add(editBox);
                                        continue;
                                    }
                                    //clip box on each axis
                                    for (int i = 0; i < 6; ++i)
                                    {
                                        int side = -1;
                                        if (i < 2)
                                            side = 0;
                                        else if (i < 4)
                                            side = 1;
                                        else if (i < 6)
                                            side = 2;
                                        float direction = 1;
                                        if (i % 2 == 1)
                                            direction = -1;
                                        Vector3 chiselReference = chisel.position + chisel.size / 2 * direction;
                                        float d = GetVectorComponent(chiselReference, side);
                                        float min = GetVectorComponent(editBox.Min, side);
                                        float max = GetVectorComponent(editBox.Max, side);
                                        float sidemin = (min - d) * direction;
                                        float sidemax = (max - d) * direction;
                                        if (sidemin < 0 == sidemax < 0) //same sides of the plane
                                        {
                                            if (sidemin > 0 || sidemax > 0) //box is totally exterior on this axis
                                            {
                                                positiveBoxes.Add(editBox);
                                            }
                                        }
                                        else if (sidemin < 0 != sidemax < 0) //opposite sides of the plane
                                        {
                                            BoundingBox minBox = new BoundingBox(editBox.Min, editBox.Max);
                                            SetVectorComponent(ref minBox.Max, side, d);
                                            BoundingBox maxBox = new BoundingBox(editBox.Min, editBox.Max);
                                            SetVectorComponent(ref maxBox.Min, side, d);
                                            if (sidemin > 0)
                                            {
                                                positiveBoxes.Add(minBox);
                                                editBox = maxBox;
                                            }
                                            else if (sidemax > 0)
                                            {
                                                positiveBoxes.Add(maxBox);
                                                editBox = minBox;
                                            }
                                        }
                                    } //end for i
                                      //edit box now models the interior space of the initial box and will be left behind
                                } //end foreach chisel
                                  // positive boxes should now model the exterior space entirely
                                exteriorBoxes.Clear();
                                exteriorBoxes.AddRange(positiveBoxes);
                                //discard this box
                                if (!controlDown)
                                    RemoveBox(chisel);
                            }
                            //at this point exterior boxes should model the entire exterior space
                            edit.boxes.Clear();
                            for (int i = 0; i < exteriorBoxes.Count; ++i)
                            {
                                BoundingBox incomingBox = exteriorBoxes[i];
                                Vector3 size = incomingBox.Max - incomingBox.Min;
                                Vector3 position = incomingBox.Min + size / 2;
                                if (size.LengthSquared() == 0)
                                {
                                    throw new Exception("Zero box created from subtraction!!");
                                }
                                Box newBox = fps.AddBox(position, size, targetBox.color);
                                edit.boxes.Add(newBox);
                            }
                            RemoveBox(targetBox);
                        }
                    }
                    else
                    {
                        Vector3 min = Vector3.Zero, max = Vector3.Zero;
                        bool init = false;
                        foreach (Box b in edit.boxes)
                        {
                            if (!init)
                            {
                                min = b.boundingBox.Min;
                                max = b.boundingBox.Max;
                                init = true;
                            }
                            else
                            {
                                min = Vector3.Min(b.boundingBox.Min, min);
                                max = Vector3.Max(b.boundingBox.Max, max);
                            }
                        }
                        if (init)
                        {
                            Box box = new Box(edit.boxes[0]);
                            box.size = max - min;
                            box.position = min + box.size / 2;
                            for (int i = 0; i < edit.boxes.Count; ++i)
                            {
                                RemoveBox(edit.boxes[i]);
                            }
                            edit.boxes.Clear();
                            fps.AddBox(box);
                            edit.boxes.Add(box);
                        }
                    }
                }

                //edit hollow
                if (game1.kclick(Keys.OemSemicolon))
                {
                    List<Box> boxesToAdd = new List<Box>();
                    for (int i = 0; i < edit.boxes.Count; ++i)
                    {
                        Box B = edit.boxes[i];
                        Box left = new Box(new Vector3(editSnapSize * 2, B.size.Y, B.size.Z),
                            new Vector3(B.boundingBox.Min.X + editSnapSize, B.position.Y, B.position.Z));
                        Box right = new Box(new Vector3(editSnapSize * 2, B.size.Y, B.size.Z),
                            new Vector3(B.boundingBox.Max.X - editSnapSize, B.position.Y, B.position.Z));
                        Box top = new Box(new Vector3(B.size.X, editSnapSize * 2, B.size.Z),
                            new Vector3(B.position.X, B.boundingBox.Max.Y - editSnapSize, B.position.Z));
                        Box bottom = new Box(new Vector3(B.size.X, editSnapSize * 2, B.size.Z),
                            new Vector3(B.position.X, B.boundingBox.Min.Y + editSnapSize, B.position.Z));
                        Box forward = new Box(new Vector3(B.size.X, B.size.Y, editSnapSize * 2),
                            new Vector3(B.position.X, B.position.Y, B.boundingBox.Min.Z + editSnapSize));
                        Box back = new Box(new Vector3(B.size.X, B.size.Y, editSnapSize * 2),
                            new Vector3(B.position.X, B.position.Y, B.boundingBox.Max.Z - editSnapSize));
                        left.color = right.color = top.color = bottom.color = forward.color = back.color = B.color;
                        Action<Box> AddBox = fps.AddBox;
                        AddBox(left);
                        AddBox(right);
                        AddBox(top);
                        AddBox(bottom);
                        AddBox(forward);
                        AddBox(back);
                        RemoveBox(B);
                        boxesToAdd.Add(left);
                        boxesToAdd.Add(right);
                        boxesToAdd.Add(top);
                        boxesToAdd.Add(bottom);
                        boxesToAdd.Add(forward);
                        boxesToAdd.Add(back);
                    }
                    edit.boxes.Clear();
                    foreach (Box b in boxesToAdd)
                    {
                        edit.boxes.Add(b);
                    }
                }

                //edit duplicate
                if (game1.kclick(Keys.V) && !controlDown)
                {
                    List<Box> newBoxes = new List<Box>();
                    foreach (Box b in edit.boxes)
                    {
                        Box newBox = new Box(b);
                        newBox.position += edit.hoverboxNormal * b.size;
                        fps.AddBox(newBox);
                        newBoxes.Add(newBox);
                    }
                    edit.boxes = newBoxes;
                }

                //edit delete
                if (deleteClick)
                {
                    for (int i = 0; i < edit.boxes.Count; ++i)
                    {
                        RemoveBox(edit.boxes[i]);
                        edit.boxes.RemoveAt(i--);
                    }
                }

                //edit copy
                //if(game1.kclick(Keys.C) && controlDown && edit.boxes.Count > 0)
                //{
                //    clipboardBoxes.Clear();
                //    bool containerEdited = false;
                //    Vector3 min = Vector3.Zero;
                //    Vector3 max = Vector3.Zero;
                //    foreach (Box box in edit.boxes)
                //    {
                //        clipboardBoxes.Add(new Box(box));
                //        if (!containerEdited)
                //        {
                //            min = box.boundingBox.Min;
                //            max = box.boundingBox.Max;
                //            containerEdited = true;
                //        }
                //        else
                //        {
                //            min = Vector3.Min(min, box.boundingBox.Min);
                //            max = Vector3.Max(max, box.boundingBox.Max);
                //        }
                //    }
                //    clipboardBoxContainerSize = max - min;
                //    Vector3 center = min + clipboardBoxContainerSize / 2;
                //    clipboardBoxOffsets = new Vector3[clipboardBoxes.Count];
                //    for(int i = 0; i < clipboardBoxes.Count; ++i)
                //    {
                //        Box box = clipboardBoxes[i];
                //        clipboardBoxOffsets[i] = box.position - center;
                //    }
                //    edit.boxes.Clear();
                //}

                //edit color
                if (game1.kclick(Keys.U))
                {
                    foreach (Box b in edit.boxes)
                    {
                        b.color = fps.monochrome(1.0f, 0.25f);
                    }
                }
                if (game1.kclick(Keys.Y))
                {
                    foreach (Box b in edit.boxes)
                    {
                        b.color = fps.monochrome(1.0f, 1.00f);
                    }
                }
            }

            //edit clipboard
            //if (clipboardBoxes.Count > 0)
            //{
            //    //edit clipboard position
            //    float axisOffset = Math.Abs(Vector3.Dot(clipboardBoxContainerSize, edit.hoverboxNormal));
            //    Vector3 axisOffsetVec = edit.hoverboxNormal * axisOffset/2;
            //    Vector3 pivot = snap(edit.hoverboxContact + axisOffsetVec);
            //    for (int i = 0; i < clipboardBoxes.Count;++i)
            //    {
            //        Box box = clipboardBoxes[i];
            //        box.position = snap(pivot + clipboardBoxOffsets[i]);
            //    }

            //    //edit paste
            //    if (game1.lmouse && !game1.lmouseOld && clipboardBoxes.Count > 0)
            //    {
            //        foreach (Box box in clipboardBoxes)
            //        {
            //            AddBox(new Box(box));
            //        }
            //        //clipboardBoxes.Clear();
            //    }

            //    //edit clipboard deselect
            //    if(game1.rmouse && !game1.rmouseOld)
            //    {
            //        clipboardBoxes.Clear();
            //    }
            //}

            //edit targets
            if (edit.target > -1)
            {
                if (transformation.LengthSquared() > 0)
                {
                    var targets = fps.targets;
                    if (transformationIsTranslation)
                    {
                        targets[edit.target].Center += translationTransform;
                        targets[edit.target].Center = snap(targets[edit.target].Center);
                        fps.targetStarts[edit.target] = targets[edit.target].Center;
                    }
                    else
                    {
                        targets[edit.target].Radius += transformation.Y / 2;
                        targets[edit.target].Radius = snap(targets[edit.target].Radius);
                    }
                    edit.saveNeeded = true;
                }
                //delete target
                if (deleteClick)
                {
                    fps.RemoveTargetAt(edit.target);
                    edit.target = -1;
                    edit.hoverTarget = -1;
                    edit.saveNeeded = true;
                }
            }

            //edit gun
            if (editGun != null)
            {
                Gun g = editGun;
                if (transformation.LengthSquared() > 0)
                {
                    if (transformationIsTranslation)
                    {
                        g.pos += translationTransform;
                        g.pos = snap(g.pos);
                    }
                    else
                    {
                        g.rot *= Matrix.CreateFromYawPitchRoll(
                            rotationTransformation.Y,
                            rotationTransformation.X,
                            rotationTransformation.Z);
                    }
                    edit.saveNeeded = true;
                }
                if (deleteClick)
                {
                    fps.RemoveGun(g);
                    editGun = null;
                }
            }
            //if (game1.ltap)
            //{
            //    if (uiGunReload == uiMouseHost)
            //    {
            //        for (int i = 0; i < editGun.bullets.Count; ++i)
            //        {
            //            Bullet b = editGun.bullets[i];
            //            b.off = true;
            //        }
            //    }
            //    if(uiControlButton == uiMouseHost)
            //    {
            //        uiControls.minimized = !uiControls.minimized;
            //    }
            //}
            //if(game1.lmouse)
            //{
            //    if (uiGunJoin == uiMouseHost)
            //    {
            //        editGun.TriggerDown(new PhysicsState(1));
            //    }
            //}

            //edit save
            if (game1.kclick(Keys.S) && controlDown && !shiftDown)
            {
                fps.SaveLevel(currentLevelFilename);
            }

            //edit load
            if (game1.kclick(Keys.L) && controlDown && !shiftDown)
            {
                fps.loadlevel();
            }

            var keys = game1.keyCurrent.GetPressedKeys();
            foreach (Keys k in keys)
            {
                //edit save level, save map, save terrain
                if (game1.kclick(k) && (k >= Keys.D0 && k <= Keys.D9))
                {
                    string newFilename = string.Format("{0}.txt", (int)k);
                    if (game1.kdown(Keys.LeftShift)) //SAVE
                    {
                        currentLevelFilename = newFilename;
                        fps.SaveLevel(currentLevelFilename);
                    }
                    else if (game1.kdown(Keys.LeftControl) && File.Exists(currentLevelFilename)) //LOAD
                    {
                        currentLevelFilename = newFilename;
                        fps.loadlevel();
                    }
                }

                //edit textbox
                if (game1.restrictedInput && currentTextbox == null)
                {
                    game1.restrictedInput = false;
                }
                if (game1.restrictedInput)
                {
                    game1.BeginUnrestrictedInput();
                    string text = currentTextbox.GetText();
                    string text0 = text;
                    //string filename0 = filenam;e
                    if (game1.kclickheld(k))
                    {
                        char c = SmallFPS.KeyToChar(k, shiftDown);
                        if (char.IsLetter(c) || char.IsNumber(c) || char.IsPunctuation(c))
                        {
                            //filename += c;
                            text += c;
                        }
                        if (k == Keys.Back && text.Length > 0)
                        {
                            //filename = filename.Substring(0, filename.Length - 1);
                            text = text.Substring(0, text.Length - 1);
                        }
                        if (k == Keys.Enter)
                        {
                            //edit end text, submit text
                            game1.restrictedInput = false;
                            currentTextbox = null;
                        }
                    }
                    //if(filename0 != filename)
                    //{
                    //    edit.saveNeeded = true;
                    //    if(string.IsNullOrEmpty(filename))
                    //    {
                    //        edit.saveNeeded = false;
                    //    }
                    //}
                    if (text != text0)
                    {
                        currentTextbox.setText(text);
                    }
                    game1.EndUnrestrictedInput();
                }
            }
            //end editupdate
        }
    }
}