using Microsoft.Xna.Framework;
using MknGames.FPSWahtever;
using SilverBullet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class FPSEditor
    {
        EditSmallFPSForm editor;
        Thread editorThread;
        SmallFPSEditState edit = new SmallFPSEditState();
        //because 1 / 2 = 0.5f so it sorta makes sense to support fractions of 0.5f
        float editSnapSize = 0.125f;
        //List<Box> clipboardBoxes = new List<Box>();
        //Vector3[] clipboardBoxOffsets = null;
        //Vector3 clipboardBoxContainerSize;
        Rectangle? mouseUi = null;
        Gun editHoverGun = null;
        Gun editGun = null;
        Textbox currentTextbox = null;
        Textbox levelFileTxt;
        Textbox gunFiletxt;
        bool editMultiSelectInclusively = false;
        bool editUseSelectionBrush = false;
        float editSelectionBrushRadius = 2;
        bool levelUnedited = true;
        public bool saveRequested = false;
        public bool editBoxesRequestSubdivide;
        public bool editBoxesRequestFocus;
        string currentLevelFilename = "";

        public FPSEditor()
        {
            levelFileTxt = new Textbox((string text) => { currentLevelFilename = text; },
                () => { return currentLevelFilename; });
        }
    }
}
