//definitions
//#define ADD_CLOSED_CAPTIONS

using MknGames._2D;
using MknGames.Split_Screen_Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static MknGames.Split_Screen_Dungeon.Backpack;
using Microsoft.Xna.Framework.Input;
using MknGames.Split_Screen_Dungeon.Environment;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using NAudio;
using NAudio.Wave;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;
using System.Reflection;
using SilverBullet.Common;
using SilverBullet.General;

namespace MknGames.FPSWahtever
{
    //  Introduction: This is a large file. Many lines of code.
    //Primary functions: constructor, LoadContent, Update, and Draw.
    //Key Terms: networking, edit, bullets, boxes, guns, player, helper, collision
    //Search Example: "update bullets", "helper csv"
    public class SmallFPS : DrawableGameComponentMG
    {
        //public class PhysicsStateWrapper
        //{
        //    public PhysicsState physics;
        //}
        //public enum BoxType
        //{
        //    REFLECT = 0,
        //    STICK,
        //    FLATTEN,
        //    SLIDE,
        //    SLOW,
        //    DEFAULT,
        //    COUNT
        //}
        //public class ZombieState
        //{
        //    public Vector3 pos, vel, force;
        //    //public int targ;
        //    public int pathProgress;
        //    public Gun gun;
        //    public List<square> path;
        //    public List<int> visibleTargets = new List<int>();
        //    public float pathfindElapsed;
        //    public float pathfindWaitDurationS = 1;
        //    //public int vitalTarg = -1;

        //    public ZombieState()
        //    {
        //    }

        //    public void reload(List<Bullet> allBullets, List<Gun> allGuns, Vector3 spawn)
        //    {
        //        visibleTargets.Clear();
        //        pathfindElapsed = 0;
        //        gun = new Gun();
        //        gun.bulletSpeed = 63;
        //        gun.init(allBullets, 0.15f, 6, false);
        //        allGuns.Add(gun);
        //        pos = spawn;
        //        //vitalTarg = -1;
        //    }
        //}
        //public class Person
        //{
        //    public Vector3 pos;
        //    //public Vector3 offset;
        //    public Vector3 euler;
        //    public bool detected;
        //    //public float dt;
        //}
        //class UdpState
        //{
        //    public IPEndPoint e;
        //    public UdpClient u;
        //}
        //public class NetworkPlayer
        //{
        //    public Vector3 pos;
        //    public CameraState camera;
        //    public Gun gun;
        //    public IPEndPoint endpoint;
        //    //public int selfTarget = -1;
        //    //public FPSInput input0;
        //    public FPSInput input;
        //}
        //public class Unit
        //{
        //    public Vector3 pos;
        //    public int team = -1;
        //    public int selfTarget = -1;
        //    public int target = -1;
        //    public Gun gun;
        //    public bool stationary;
        //}

        //START INSTANTIATION
        //public List<Unit> units = new List<Unit>();
        //int totalCount;
        //int allyCount;
        //int enemyCount;

        //inst networking
        //public static Dictionary<IPAddress, NetworkPlayer> networkplayerdata = new Dictionary<IPAddress, NetworkPlayer>();
        //float packetT;
        //public UdpClient sendClient;
        //public Dictionary<IPEndPoint, List<string>> udpResults = new Dictionary<IPEndPoint, List<string>>();
        //int listenPort = 11000; // our listen port
        //int sendPort = 12000; // our sending port
        //int remotePort = 11000; // other players' listen port
        //string pingAddress = "66.31.222.168"; // manual connection address

        //public class UIElement
        //{
        //    public float x = 300;
        //    public float y = 300;
        //    public float width = 300;
        //    public float height = 300;
        //    public string labelText;
        //    public bool minimized = false;
        //}

        //public struct ClosedCaption
        //{
        //    public Vector3 pos;
        //    public string text;
        //    public string textPlural;
        //    public float duration;
        //    public float elapsed;
        //}

        //inst ui
        //UIElement uiGunWindow = new UIElement();
        //UIElement uiMouseHost = null;
        //UIElement uiGunReload = new UIElement() { labelText = "reload" };
        //UIElement uiGunJoin = new UIElement() { labelText = "join to target" };
        //List<UIElement> allUIElements = new List<UIElement>();
        //UIElement uiControls = new UIElement();
        //UIElement uiControlButton = new UIElement();

        int testetseteststest;
        //inst player
        Player player = new Player();
        //Vector2 gv; //player aim center
        //Matrix untransformedProjection;
        //public float jetfuel = 50;
        //public float jetfuelrechargerate = 5;
        //public float playerFriction = 1;
        //public float gunpowder = 
        CameraState playerCam = new CameraState();
        PhysicsState bodyState = new PhysicsState(1);
        public Vector3 playerSpawnPoint;
        public Vector3 playerSpawnEuler;
        //public bool wasInContact;
        public bool is3rdPerson = false;
        public float height = 3;
        float dropValue;
        Vector3 drop;
        //float turnElapsed = 0;
        //float turnCap = 0.5f;
        int sphereBodyCount = 6;
        int bodyc;
        float moveSpeed;
        int stance = 0;
        int stance0 = 0;
        bool stanceChanged = false;
        bool stancekeyheld = false;
        FPSInput localInput0;
        FPSInput localInput;
        int myGunLimit = 1;
        Vector3 returnLoc;
        public float groundJumpSpeed = 0;
        public float jumpSpeed = 0;
        public float playerFriction = 0;
        public float playerFrictionStopped = 0;
        public float playerFrictionAir = 0;
        public float playerFrictionOverride = 0;
        public float playerInputAccelRate = 0;
        public float playerInputDecelRate = 0;
        public float playerMoveInputElapsed = 0;
        public float playerTerminalVelocity = 0;
        public float playerWalkBoost = 0;
        public float playerRunBoost = 0;
        public float playerFlyBoost = 0;
        public float playerGunForwardOffset = 0;
        bool playerRequestFrictionOverride = false;
        //bool playerLeftHandy = false;
        bool playerCanSwapToEmpty = false;
        const int playerHolsterOcto = 0;
        const int playerHolsterAdventurer = 1;
        int playerGunHolsterFormation = playerHolsterOcto;

        //inst save
        Dictionary<string, SaveLoadVariable> saveLoadVariables = new Dictionary<string, SaveLoadVariable>();
        Dictionary<string, SaveLoadVariable<Gun>> gunSaveLoadVariables = new Dictionary<string, SaveLoadVariable<Gun>>();
        bool requestForceReloadSettings = false;
        bool requestSkipReloadSettings = false;
        bool disableSavingSettings = false;

        //inst ghost
        //struct GhostFrame
        //{
        //    public Vector3 gunPosition;
        //    public Matrix gunRotation;
        //    public bool shooting;
        //}
        //GhostFrame[] ghostFrames = new GhostFrame[20000];
        //int ghostFrame = 0;
        //Gun ghostGun;
        //int ghostPlaybackFrame;

        //inst terrain
        bool terrainActive = true;
        List<Box>[,] terrain;
        public List<Box> allBoxes = new List<Box>();
        //List<Box> extraBoxes = new List<Box>();
        public const float MAP_SCALE = 2;
        float widthm = 2 * MAP_SCALE;
        float depthm = 2 * MAP_SCALE;
        float heightm = 6 * MAP_SCALE;
        //Vector3 obbPos;
        //Matrix obbRot;
        //Vector3 obbSize;

        //inst environment
        Vector3 gravity = Vector3.Zero;

        //inst rendering
        Render3D render3d = new Render3D();

        //inst bullets
        public List<Bullet> allbullets = new List<Bullet>();
        //Box boxBullet = null;
        //Bullet bulletBox = null;

        //inst targets
        //BoundingSphere[] targets;
        public Target[] targets;
        public Vector3[] targetStarts;
        List<int> capturedTargets = new List<int>();
        //int teleportationTarget = -1;
        //int teleportationTarget2 = -1;
        //int teleportationTarget3 = -1;
        //Vector3 teleportationLocation;
        //List<BoundingSphere> targets = new List<BoundingSphere>();

        //Target[] targets;
        //List<Target> sphereTargets = new List<Target>();
        //List<Target> boxTargets = new List<Target>();
        //Vector3[] targetSpawns;
        //Vector3[,] targetHistory;
        float targetRestY;
        float targetHitY;
        //float target2HitY;
        //float target2y;
        //float target2r;
        //Texture2D traceImage;
        //Vector3 gunpos;
        //Vector3 grenadepos;
        //Bullet[] grenadebullets = new Bullet[64];
        //Vector3[] gbp;
        //Vector3 guneuler;
        //Vector3 recoile, _recoile;
        //Matrix gunrot;
        //Vector3 gunsize = new Vector3(.05f, .08f, 1.1f) * 2;
        //float bsz = 0.05f;
        StarrySkyEnv sky;

        //inst models
        Model surfaceSphere;
        Model surfaceCylinder;
        Model cylinder;

        //inst textures
        Texture2D basetx;//, normaltx;
        Texture2D soiltx;
        Texture2D metaltx;
        Texture2D crackedDirttx;
        Texture2D spheresTx;
        Texture2D diagonalTx;
        Texture2D tileTx;
        Texture2D checker4Tx;
        Texture2D frameTx;

        //inst sound, inst audio
        WaveOut waveOut;
        MixingSampleProvider mixer;
        VolumeSampleProvider mixerVolume;
        SignalADSRState shootSound = new SignalADSRState();
        bool enableSound = true;
#if ADD_CLOSED_CAPTIONS
        public Dictionary<string, float> closedCaptions = new Dictionary<string, float>();
#endif

        //AdsrSampleProvider adsr; 
        //OffsetSampleProvider signalOffset;

        //inst effect, inst fx

        //inst instancing

        //float spread = 0;
        //float cooktime;
        //bool grenadexploding;

        //inst edit
        public FPSEditor editor = new FPSEditor();
        //EditSmallFPSForm editor;
        //Thread editorThread;
        //SmallFPSEditState edit = new SmallFPSEditState();
        ////because 1 / 2 = 0.5f so it sorta makes sense to support fractions of 0.5f
        //float editSnapSize = 0.125f;
        ////List<Box> clipboardBoxes = new List<Box>();
        ////Vector3[] clipboardBoxOffsets = null;
        ////Vector3 clipboardBoxContainerSize;
        //Rectangle? mouseUi = null;
        //Gun editHoverGun = null;
        //Gun editGun = null;
        //Textbox currentTextbox = null;
        //Textbox levelFileTxt;
        //Textbox gunFiletxt;
        //bool editMultiSelectInclusively = false;
        //bool editUseSelectionBrush = false;
        //float editSelectionBrushRadius = 2;
        //bool levelUnedited = true;
        //public bool saveRequested = false;
        //public bool editBoxesRequestSubdivide;
        //public bool editBoxesRequestFocus;
        ////public bool loadRequested = false;

        //string currentLevelFilename = "";

        //inst guns
        //FieldInfo[] gunFields = typeof(Gun).GetFields();
        Gun currentGun;
        Gun fastGun;
        Gun slowGun;
        Gun shotGun;
        Gun chargeShot;
        Gun retractorGun;
        Gun blankGun;
        object retractorTarget = null;
        float chargeShotElapsed = 0;
        float chargeShotDelay;
        int guni;
        List<Gun> myGuns = new List<Gun>();
        List<Gun> gunsIveHad = new List<Gun>();
        bool gunsAutofireGunsIveHad = false;
        //Gun gun2;
        public List<Gun> allguns = new List<Gun>();
        List<Gun> customGuns = new List<Gun>();
        FPSAccumulator updatefps = new FPSAccumulator(), drawfps = new FPSAccumulator();
        public bool paused;
        public bool stepOver;
        //public bool pauseMenuShowing = false;
        //public bool skipToDesktopQuit = true;
        //string[] pauseTexts = {
        //    "quit to menu",
        //    "quit to desktop",
        //    //"save",
        //    //"load"
        //};
        //Rectangle[] pauseRects;
        //public Person person = new Person();
        //inst pathfinding
        //FPSAStarPathfinding pathfinding = new FPSAStarPathfinding();
        //Vector3 clevergirl;
        //Vector3 clevergirlvel;
        //Vector3 clevergirlforce;
        //ZombieState[] zombies = new ZombieState[1];
        //Vector3 cleverboy;
        //int girlnexttarg = -1;
        //int girlpathprogress = 0;
        //int boynexttarg = -1;
        //Gun girlgun;
        //Gun boygun;
        //List<square> cleverpath;
        //List<square> cleverpathboy;
        //information given
        //List<int> visibleTargetsGirl = new List<int>();
        //List<int> visibleTargetsBoy = new List<int>();
        public bool wasActive;
        bool wasMouseLocked = false;
        //public PhysicsState tankps = new PhysicsState(1);
        //public float tankr = 3;
        //public Gun tankgun;
        //public bool intank;
        //public bool canEnterTank;
        //public Vector3 tankeu;

        //inst explosions
        //public Bullet[] explosionBullets = new Bullet[64];
        //public Vector3[] explosionBulletLocations;
        //public Bullet explosionHostBullet = null;
        //public Bullet[] bombBullets = new Bullet[64];
        //public float bombt = -1;

        //public List<Vector3> spawns = new List<Vector3>();
        //inst screen quad
        VertexDeclaration vertexDeclQuad = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(Single) * 4, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
            new VertexElement(sizeof(Single) * 6, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 2)
            );
        //VertexPositionNormalTexture[] quadVertices = new VertexPositionNormalTexture[]
        //{
        //    new VertexPositionNormalTexture(new Vector3(-1,1,0), Vector3.Backward, new Vector2(0,0)),
        //    new VertexPositionNormalTexture(new Vector3(1,1,0),Vector3.Backward, new Vector2(1,0)),
        //    new VertexPositionNormalTexture(new Vector3(1,-1,0),Vector3.Backward, new Vector2(1,1)),
        //    new VertexPositionNormalTexture(new Vector3(-1,-1,0),Vector3.Backward, new Vector2(0,1)),
        //};
        //public struct QuadVertex
        //{
        //    public Vector4 position;
        //    public Vector2 uv;
        //    public float frust;
        //    public QuadVertex(Vector3 position, Vector2 uv, float frust)
        //    {
        //        this.position = new Vector4(position, 1);
        //        this.uv = uv;
        //        this.frust = frust;
        //    }
        //}
        //QuadVertex[] quadVertices = new QuadVertex[]
        //{
        //    new QuadVertex(new Vector3(-1,1,0), new Vector2(0,0), 0),
        //    new QuadVertex(new Vector3(1,1,0), new Vector2(1,0), 1),
        //    new QuadVertex(new Vector3(1,-1,0), new Vector2(1,1), 3),
        //    new QuadVertex(new Vector3(-1,-1,0), new Vector2(0,1), 2),
        //};

        //Effect doffx, blurfx;
        //RenderTarget2D backbuffrt;
        //RenderTarget2D depthrt;
        //RenderTarget2D blurrt;
        public bool aimDownSight;
        //BoundingBox turret;
        //Gun turretGun;
        //public Bullet meleeBullet;
        //public float meleeTime, meleeDuration;
        //public bool meleeing;
        public bool playOutOfBody = false;
        //int[][] coreTargetsTable;
        //int[] zombieVTargs;
        //float pathfindElapsed = 0;
        //float pathfindWaitDurationS = 1;
        //Vector3 spawnA, spawnB;
        //List<Vector3> occlusionCircles = new List<Vector3>();
        //float hearingRadius = 1;
        //inst koth
        //Vector3 kothCenter;
        //float kothRadius;
        //NetworkPlayer enemyPlayer = new NetworkPlayer();
        //List<square> kothEnemyPath;
        //int kothPathProgress;
        //float kothPathElapsed;
        //float kothPathDuration = 2;
        //int networkTargetIncrementer = 0;
        //int[] breachTargets;
        //float jumpDuration = 1;
        //float jumpElapsed = 0;
        //int difficultyLevel = 0;
        string singlePlayerSaveFileName = "single-player-save.txt";
        bool saveLoaded = false;
        public bool requestNewGame = false;
        //public SmallFPSMenu menu;
        //ContactData[] boxCH = new ContactData[20];//box contact history
        //Box mostRecentBoxHit = null;
        //int boxCHI = -1;
        //int playerSelfTarget = -1;
        float crawlSpeed = 200;
        float crouchSpeed = 500;
        float standSpeed = 1000;
        //string levelFilenameGroundWar = "level-ground-war.txt";

        //inst mechanisms
        //Gun gunMech; //gun mechanism
        //int gunMechTarget = -1;
        //Gun reloadMechGun;
        //BoundingSphere reloadMech;
        //Bullet reloadMechBullet;
        //List<Bullet> expiredBullets = new List<Bullet>();

        // instance scripting

        // instance video
        //Video video;
        //VideoPlayer vPlayer;
        //Vector3 videoTutorialPosition;
        //Matrix videoTutorialRot;
        BasicEffect screenQuadEffect;
        //Texture2D vPlayerTexture;

        //inst dialog
        //string dialogText = "Hello World";
        //RenderTarget2D dialogRt;
        //Rectangle dialogBox;
        //Box narrativeBox;
        //float narrativeBoxExcitement;
        //float narrativeBoxForce;
        //float narrativeBoxRotation;
        //string[] dialog =
        //{
        //    "W: Hi!",
        //    "T: Yo.",
        //    "W: Will that be all?",
        //    "T: Yeah.",
        //    "...",
        //    "T: Hey did you see the moon is full?",
        //    "W: No, I didnt!",
        //    "T: Yeah. It's beautiful.",
        //    "W: Oh..."
        //};
        //int dialogIndex = -1;
        //public bool loadContentComplete = false;
        //inst travel
        //Box americanFlagBox;
        //Texture2D flagTx, americanFlagTx, mexianFlagTx;
        //BoundingSphere planeSV;
        //int travelScriptState = 0;
        //float travelScriptTime = -1;
        //Color planeColor;
        //inst training
        //StateMachine missionState = new StateMachine();
        //StateMachine gaz = new StateMachine();
        //Vector3 gazPosition;
        //Gun g36c;
        //StateMachine g36State = new StateMachine();
        //Vector3 fireRangePosition;
        //Box pvtLootz;
        //Box ceilBox;

        //inst console, inst debug
        //List<string> debugOutput = new List<string>();

        bool pauseBullets = false;
        Color bulletTrailColor = Color.Magenta;
        public  bool editMouseLocked = true;
        bool gameMouseLocked = true;


        //END INSTANTIATION

        //smallfps constructor
        public SmallFPS(GameMG game) : base(game)
        {
            ////construct args
            //string[] args = Environment.GetCommandLineArgs();
            //string exe = args[0];
            ////construct network args
            //if (args.Length > 1)
            //    listenPort = int.Parse(args[1]);
            //if (args.Length > 2)
            //    sendPort = int.Parse(args[2]);
            //if(args.Length > 3)
            //    remotePort = int.Parse(args[3]);
            //if (args.Length > 4)
            //    pingAddress = args[4];
            height = 3;
            dropValue = height / (float)sphereBodyCount;
            drop = Vector3.Down * dropValue;
            bodyc = 6;
            game.enableEscapeKeyToQuit = false;
            //game1.Components.Add(new SmallFPSExpansion(game1, this));


            ////construct networking
            ////https://msdn.microsoft.com/en-us/library/system.net.sockets.udpclient.beginreceive(v=vs.110).aspx
            //IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
            //UdpClient u = new UdpClient(e);
            //BeginReceive(u, e);
            //sendClient = new UdpClient(sendPort);
        }
        //helper networking
        //public void BeginReceive(UdpClient u, IPEndPoint e)
        //{
        //    UdpState s = new UdpState();
        //    s.e = e;
        //    s.u = u;

        //    //Console.WriteLine("listening for messages");
        //    u.BeginReceive(new AsyncCallback(ReceiveCallback), s);
        //}
        //public void ReceiveCallback(IAsyncResult ar)
        //{
        //    //our client
        //    UdpClient localClient = (UdpClient)((UdpState)(ar.AsyncState)).u;
        //    //their endpoint
        //    IPEndPoint remoteEndpoint = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

        //    Byte[] receiveBytes = localClient.EndReceive(ar, ref remoteEndpoint);
        //    string receiveString = Encoding.ASCII.GetString(receiveBytes);
        //    if (!udpResults.ContainsKey(remoteEndpoint))
        //        udpResults.Add(remoteEndpoint, new List<string>());
        //    udpResults[remoteEndpoint].Add(receiveString);

        //    //Console.WriteLine("Received: {0}", receiveString);
        //    BeginReceive(localClient, remoteEndpoint);

        //    //Byte[] receiveBytes = sendClient.EndReceive(ar, ref sendClientEndpoint);
        //    //string receiveString = Encoding.ASCII.GetString(receiveBytes);
        //    //if (!udpResults.ContainsKey(e))
        //    //    udpResults.Add(e, new List<string>());
        //    //udpResults[e].Add(receiveString);

        //    //Console.WriteLine("Received: {0}", receiveString);
        //    //BeginReceive(u, e);
        //}
        //void InitNetworkPlayer(NetworkPlayer player, IPEndPoint e)
        //{
        //    player.endpoint = e;
        //    player.gun = new Gun();
        //    player.gun.init(allbullets);
        //    player.selfTarget = networkTargetIncrementer++;
        //    allguns.Add(player.gun);
        //    networkplayerdata.Add(e.Address, player);
        //}
        // process network message
        //public void ProcessMessage(IPEndPoint e, string msg)
        //{
        //    NetworkPlayer player = null;

        //    //get network player, add if neccessary
        //    if (!networkplayerdata.ContainsKey(e.Address))
        //    {
        //        player = new NetworkPlayer();
        //        player.endpoint = e;
        //        player.gun = new Gun();
        //        player.gun.init(allbullets);
        //        player.camera = new CameraState();
        //        player.camera.far = 2000;
        //        //player.selfTarget = networkTargetIncrementer++;
        //        allguns.Add(player.gun);
        //        networkplayerdata.Add(e.Address, player);
        //    }
        //    else
        //    {
        //        player = networkplayerdata[e.Address];
        //    }

        //    //process message
        //    //HACK: not sure why msg is null but crashing is inconvenient here
        //    if (msg == null)
        //        return;
        //    string[] tokens = msg.Split(' ');
        //    if (tokens.Length > 0)
        //    {
        //        switch (tokens[0])
        //        {
        //            case "p":
        //                if (tokens.Length > 1)
        //                {
        //                    Vector3 position = csv_parsev3(tokens[1]);
        //                    player.pos = position;
        //                }
        //                if (tokens.Length > 2)
        //                {
        //                    Vector3 euler = csv_parsev3(tokens[2]);
        //                    player.camera.Euler = euler;
        //                }
        //                if(tokens.Length > 3)
        //                {
        //                    FPSInput input = csv_parseFPSInput(tokens[3]);
        //                    //player.input0 = player.input;
        //                    player.input = input;
        //                }
        //                break;
        //        }
        //    }
        //}

            // start load content
        protected override void LoadContent()
        {
            base.LoadContent();

            //game1.graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //game1.graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //game1.graphics.IsFullScreen = true;
            //game1.IsFixedTimeStep = true;
            //game1.TargetElapsedTime = TimeSpan.FromMilliseconds(16);
            //game1.graphics.SynchronizeWithVerticalRetrace = false; //<<<<<<<<<<Unlock FPS
            game1.graphics.ApplyChanges();

            game1.Exiting += OnGameExit;

            //load editor
            editor.LoadContent(this);

            //load travel
            //americanFlagTx = game1.Content.Load<Texture2D>("1280px-Flag_of_the_United_States.svg");
            //mexianFlagTx = game1.Content.Load<Texture2D>("mexico-hi");
            //flagTx = americanFlagTx;
            //new Thread(new ThreadStart(() =>
            //{
            //load video
            //video = game1.Content.Load<Video>("cg-video_tutorial_MakingGamesWindows.vshost 2018-04-01 16-13-02-353");
            //vPlayer = new VideoPlayer();
            //vPlayer.Play(video);
            //vPlayerTexture = vPlayer.GetTexture();
            screenQuadEffect = new BasicEffect(GraphicsDevice);
            //traceImage = game1.Content.Load<Texture2D>("300px-CoD4_Mile_High_Club_Room_3");
            //basetx = game1.Content.Load<Texture2D>("concrete_06");// "bumpspacey");
            //basetx = game1.Content.Load<Texture2D>("43449944-seamless-pattern-classical-abstract-small-textured-background-simple-texture-with-regularly-repeatin");// "bumpspacey");
            basetx = game1.Content.Load<Texture2D>("noise");// "bumpspacey");
                                                            //basetx = game1.Content.Load<Texture2D>("shingles_white");// "bumpspacey");
                                                            //normaltx = game1.Content.Load<Texture2D>("concrete_06_NormalMap");
                                                            //load texture
            soiltx = game1.Content.Load<Texture2D>("soil");// "bumpspacey");
            frameTx = game1.Content.Load<Texture2D>("frame-512");// "bumpspacey");
            metaltx = game1.Content.Load<Texture2D>("metal");
            crackedDirttx = game1.Content.Load<Texture2D>("cracked-dirt");
            spheresTx = game1.Content.Load<Texture2D>("sphere-pattern-small");
            diagonalTx = game1.Content.Load<Texture2D>("diagonal-pattern");
            tileTx = game1.Content.Load<Texture2D>("tile-simple-2048");
            checker4Tx = game1.Content.Load<Texture2D>("checkerboard-512");

            //load sound, load audio
            //click = game1.Content.Load<SoundEffect>("fast_snap - 177494__snapper4298__snap-3");
            shootSound = loadSignalASDRState("shoot-sound.csv");
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 1));
            mixer.ReadFully = true;
            mixerVolume = new VolumeSampleProvider(mixer);
            mixerVolume.Volume = enableSound ? 1 : 0;
            waveOut = new WaveOut();
            waveOut.DesiredLatency = 150;
            waveOut.NumberOfBuffers = 2;
            waveOut.Init(mixerVolume);
            waveOut.Play();

            //load models
            surfaceSphere = game1.Content.Load<Model>("surface-sphere");
            cylinder= game1.Content.Load<Model>("flat-normal-cylinder");
            surfaceCylinder = game1.Content.Load<Model>("surface-cylinder");


            //load rendering
            render3d.LoadContent(GraphicsDevice, game1);
            //deferredBuffer0 = new RenderTarget2D(
            //    GraphicsDevice,
            //    GraphicsDevice.PresentationParameters.BackBufferWidth,
            //    GraphicsDevice.PresentationParameters.BackBufferHeight,
            //    false,
            //    SurfaceFormat.Color,
            //    DepthFormat.Depth24Stencil8);
            //deferredBufferDepth = new RenderTarget2D(
            //    GraphicsDevice,
            //    GraphicsDevice.PresentationParameters.BackBufferWidth,
            //    GraphicsDevice.PresentationParameters.BackBufferHeight,
            //    false,
            //    SurfaceFormat.Color,
            //    DepthFormat.Depth24Stencil8);
            //for (int i = 0; i < shadowMapCount; ++i)
            //{
            //    cascadeShadowMaps[i] =
            //        new RenderTarget2D(
            //        GraphicsDevice,
            //        2048,
            //        2048,
            //        false,
            //        SurfaceFormat.Single,
            //        DepthFormat.Depth24Stencil8);
            //}
            //doffx = game1.Content.Load<Effect>("dof");
            //blurfx = game1.Content.Load<Effect>("blur");
            //backbuffrt = new RenderTarget2D(
            //    GraphicsDevice, 
            //    GraphicsDevice.Viewport.Width,
            //    GraphicsDevice.Viewport.Height,
            //    false,
            //    SurfaceFormat.Color,
            //    DepthFormat.Depth24);
            //depthrt = new RenderTarget2D(
            //    GraphicsDevice,
            //    GraphicsDevice.Viewport.Width,
            //    GraphicsDevice.Viewport.Height,
            //    false,
            //    SurfaceFormat.Single,
            //    DepthFormat.Depth24);
            //blurrt = new RenderTarget2D(
            //    GraphicsDevice,
            //    GraphicsDevice.Viewport.Width,
            //    GraphicsDevice.Viewport.Height,
            //    false,
            //    SurfaceFormat.Color,
            //    DepthFormat.Depth24);
            //load pause rects
            //pauseRects = new Rectangle[pauseTexts.Length];
            //for (int i = 0; i < pauseTexts.Length; ++i)
            //{
            //    pauseRects[i] = Backpack.percentage(GraphicsDevice.Viewport.Bounds,
            //        0.1f, 0.1f + 0.25f * (float)i, 0.5f, 0.2f);
            //}
            //filename = levelFilenameGroundWar;
            //filename = "53.txt";
            //load ui
            //UIAddElement(uiControlButton);
            //uiControlButton.x = Backpack.percentageW(ViewBounds, 0.9f);
            //uiControlButton.y = Backpack.percentageH(ViewBounds, 0.9f);
            //uiControlButton.width = Backpack.percentageW(ViewBounds, 0.05f);
            //uiControlButton.height = Backpack.percentageH(ViewBounds, 0.05f);
            //UIAddElement(uiControls);
            //uiControls.x = Backpack.percentageW(ViewBounds, 0.7f);
            //uiControls.y = Backpack.percentageH(ViewBounds, 0.2f);
            //uiControls.width = Backpack.percentageW(ViewBounds, 0.2f);
            //uiControls.height = Backpack.percentageH(ViewBounds, 0.7f);
            //UIAddElement(uiGunWindow);
            //UIAddElement(uiGunReload);
            //UIAddElement(uiGunJoin);

            //update rectangle in update for good input checking
            //
            reload();
            //loadContentComplete = true;
            //})).Start();
        }

        // on game exit
        private void OnGameExit(object sender, EventArgs e)
        {
            editor.Cleanup();
            SaveSettings(singlePlayerSaveFileName);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            //unload audio, dispose audio, dispose sound, unload sound
            waveOut.Dispose();
        }
        //save settings
        public void SaveSettings(string fileName)
        {
            if (disableSavingSettings)
                return;
            using (StreamWriter writer = new StreamWriter(File.Create(fileName)))
            {
                //writer.WriteLine("bodyStatePos " + csvWriteV3(bodyState.pos));
                writer.WriteLine("filename " + editor.currentLevelFilename);
                writer.WriteLine("enableSound " + enableSound);
                writer.WriteLine("editOutOfBody " + editor.editOutOfBody);
                writer.WriteLine("editMouseLocked " + editMouseLocked);
                writer.WriteLine("editUseSelectionBrush " + editor.editUseSelectionBrush);
                writer.WriteLine("crouchSpeed " + crouchSpeed);
                writer.WriteLine("crawlSpeed " + crawlSpeed);
                writer.WriteLine("standSpeed " + standSpeed);
                writer.WriteLine("groundJumpSpeed " + groundJumpSpeed);
                writer.WriteLine("jumpSpeed " + jumpSpeed);
                writer.WriteLine("playerFriction " + playerFriction);
                foreach (var pair in saveLoadVariables)
                {
                    writer.WriteLine(pair.Value.name + " " + pair.Value.write());
                }
            }
        }

        public void AddSaveLoadVariable(string name, Func<string> save, Action<string> load)
        {
            if (saveLoadVariables.ContainsKey(name) == false)
            {
                SaveLoadVariable slv = new SaveLoadVariable(name, save, load);
                saveLoadVariables.Add(name, slv);
            }
        }
        public void AddSaveLoadVariable<T>(Dictionary<string, SaveLoadVariable<T>> collection, string name, Func<T, string> save, Action<T, string> load)
        {
            if (collection.ContainsKey(name) == false)
            {
                SaveLoadVariable<T> slv = new SaveLoadVariable<T>(name, save, load);
                collection.Add(name, slv);
            }
        }

        //static void LaunchCommandLineApp()
        //{
        //    // For the example
        //    const string ex1 = "C:/Users/tlawr/Documents/SplitScreenDungeonAndroid/MakinGames/MakinGames/Content/basic.hlsl";
        //    const string ex2 = "C:/Users/tlawr/Documents/SplitScreenDungeonAndroid/MakingGamesWindows/bin/Windows/x86/Debug/Content/basic.mgfx";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.CreateNoWindow = false;
        //    startInfo.UseShellExecute = false;
        //    startInfo.FileName = "C:/Program Files (x86)/MSBuild/MonoGame/v3.0/Tools/2MGFX.exe";
        //    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;
        //    startInfo.Arguments = ex1 + " " + ex2 + " /Profile:DirectX_11";

        //    try
        //    {
        //        // Start the process with the info we specified.
        //        // Call WaitForExit and then the using statement will close.
        //        using (Process exeProcess = Process.Start(startInfo))
        //        {
        //            exeProcess.WaitForExit();
        //        }
        //    }
        //    catch
        //    {
        //        // Log error.
        //    }
        //}

        public void AddGun(Gun gun)
        {
            if (allguns.Contains(gun))
                throw new Exception("Gun already registered");
            allguns.Add(gun);
            for (int i = 0; i < gun.bullets.Count; ++i)
            {
                allbullets.Add(gun.bullets[i]);
            }
            editor.edit.saveNeeded = true;
        }

        public void RemoveGun(Gun gun)
        {
            if (!allguns.Contains(gun))
                throw new Exception("Gun hasn't been registered");
            allguns.Remove(gun);
            for (int i = 0; i < gun.bullets.Count; ++i)
            {
                allbullets.Remove(gun.bullets[i]);
                gun.bullets.RemoveAt(i--);
            }
            editor.edit.saveNeeded = true;
        }

        //drop gun
        public bool DropGun(int i)
        {
            Gun G = myGuns[i];
            //Ray dropTo = new Ray(bodyState.pos, Vector3.Down);
            //float dist = 200;
            //Vector3 start = dropTo.Position;
            //Vector3 end = start + dropTo.Direction * dist;
            //Rectangle zone = getzone(start, end);
            //List<Box> boxes = getBoxesInZone(zone);
            //float minDist = float.MaxValue;
            //foreach (Box b in boxes)
            //{
            //    float? hit = dropTo.Intersects(b.boundingBox);
            //    if (hit.HasValue && hit.Value <= dist && hit.Value < minDist)
            //    {
            //        minDist = hit.Value;
            //    }
            //}
            //if(minDist < float.MaxValue)
            //{
            //    myGuns[i].pos = dropTo.Position + dropTo.Direction * minDist + Vector3.Up * myGuns[i].size.Y;
            //    myGuns.RemoveAt(i);
            //    return true;
            //}
            myGuns.Remove(G);
            if (G.isTriggerDown)
                G.fireAutomatically = true;
            G.pos += G.MakeForward() * G.size.Z;
            if (currentGun == G)
            {
                currentGun = null;
            }
            if (i == guni)
            {
                guni = MathHelper.Clamp(guni, 0, myGuns.Count - 1);
            }
            return false;
        }

        //pickup gun
        public bool PickupGun(Gun gun)
        {
            if (myGuns.Count == myGunLimit)
                return false;
            //foreach(Gun g in customGuns)
            //{
            //if (g == gun) continue;
            //RemoveGun(g);
            //}
            //customGuns.Clear();
            gun.fireAutomatically = false;
            if (gunsIveHad.Contains(gun) == false)
                gunsIveHad.Add(gun);
            myGuns.Add(gun);
            return true;
        }

        //reload
        public void reload()
        {
            //in case you want to add a variable at runtime
            //reload save variables
            AddSaveLoadVariable("gunsAutofireGunsIveHad",
                () => { return gunsAutofireGunsIveHad.ToString(); },
                (string text) => { gunsAutofireGunsIveHad = bool.Parse(text); });
            AddSaveLoadVariable("playerCanSwapToEmpty",
                () => { return playerCanSwapToEmpty.ToString(); },
                (string text) => { playerCanSwapToEmpty = bool.Parse(text); });
            AddSaveLoadVariable("playerInputAccelRate",
                () => { return playerInputAccelRate.ToString(); },
                (string text) => { playerInputAccelRate = float.Parse(text); });
            AddSaveLoadVariable("playerInputDecelRate",
                () => { return playerInputDecelRate.ToString(); },
                (string text) => { playerInputDecelRate = float.Parse(text); });
            AddSaveLoadVariable("playerTerminalVelocity",
                () => { return playerTerminalVelocity.ToString(); },
                (string text) => { playerTerminalVelocity = float.Parse(text); });
            AddSaveLoadVariable("playerWalkBoost",
                () => { return playerWalkBoost.ToString(); },
                (string text) => { playerWalkBoost = float.Parse(text); });
            AddSaveLoadVariable("playerRunBoost",
                () => { return playerRunBoost.ToString(); },
                (string text) => { playerRunBoost = float.Parse(text); });
            AddSaveLoadVariable("playerFlyBoost",
                () => { return playerFlyBoost.ToString(); },
                (string text) => { playerFlyBoost = float.Parse(text); });
            AddSaveLoadVariable("playerStoppedFriction",
                () => { return playerFrictionStopped.ToString(); },
                (string text) => { playerFrictionStopped = float.Parse(text); });
            AddSaveLoadVariable("playerAirFriction",
                () => { return playerFrictionAir.ToString(); },
                (string text) => { playerFrictionAir = float.Parse(text); });
            AddSaveLoadVariable("playerOverrideFriction",
                () => { return playerFrictionOverride.ToString(); },
                (string text) => { playerFrictionOverride = float.Parse(text); });
            AddSaveLoadVariable("playerGunForwardOffset",
                () => { return playerGunForwardOffset.ToString(); },
                (string text) => { playerGunForwardOffset = float.Parse(text); });
            AddSaveLoadVariable("playerCam.far",
                () => { return playerCam.far.ToString(); },
                (string text) => { playerCam.far = float.Parse(text); });
            AddSaveLoadVariable("playerCam.near",
                () => { return playerCam.near.ToString(); },
                (string text) => { playerCam.near = float.Parse(text); });
            //for (int i = 0; i < cascadingNonLinearCutoffs.Length; ++i)
            //{
            //    AddSaveLoadVariable("cascadingNonLinearCutoffs[" + i + "]",
            //        () => { return cascadingNonLinearCutoffs[i].ToString(); },
            //        (string text) => { cascadingNonLinearCutoffs[i] = float.Parse(text); });
            //}
            AddSaveLoadVariable("gv",
                () => { return csvWriteV2(player.gv); },
                (string text) => { player.gv = csvParseV2(text); });
            AddSaveLoadVariable("gravity",
                () => { return csvWriteV3(gravity); },
                (string text) => { gravity = csvParseV3(text); });
            AddSaveLoadVariable("game1.clearColor",
                () => { return csvWriteV3(game1.clearColor.ToVector3()); },
                (string text) => { game1.clearColor = new Color(csvParseV3(text)); });
            AddSaveLoadVariable("myGunLimit",
                () => { return myGunLimit.ToString(); },
                (string text) => { myGunLimit = int.Parse(text); });
            AddSaveLoadVariable("playerGunHolsterFormation",
                () => { return playerGunHolsterFormation.ToString(); },
                (string text) => { playerGunHolsterFormation = int.Parse(text); });

            render3d.AddVariables(saveLoadVariables);
            //guns

            //public float deathDuration;
            //public bool canRespawn;
            //public bool fireAutomatically;
            AddSaveLoadVariable<Gun>(gunSaveLoadVariables,
                    "canRespawn",
                    (Gun g) => { return g.canRespawn.ToString(); },
                    (Gun g, String s) => { g.canRespawn = bool.Parse(s); });
            AddSaveLoadVariable<Gun>(gunSaveLoadVariables,
                    "deathDuration",
                    (Gun g) => { return g.deathDuration.ToString(); },
                    (Gun g, String s) => { g.deathDuration = float.Parse(s); });
            AddSaveLoadVariable<Gun>(gunSaveLoadVariables,
                    "size",
                    (Gun g) => { return csvWriteV3(g.size); },
                    (Gun g, String s) => { g.size = csvParseV3(s); });
            //AddSaveLoadVariable<Gun>(gunSaveLoadVariables,
            //        "deathDuration",
            //        (Gun g) => { return "deathDuration:" + g.canRespawn; },
            //        (Gun g, String s) => { g.deathDuration = float.Parse(s); });

            //hearingRadius = 20;
            //pathfindElapsed = 0;
            //bool isSaveReload = false;
            //load settings
            if (requestForceReloadSettings || (!requestSkipReloadSettings && !saveLoaded && !requestNewGame && File.Exists(singlePlayerSaveFileName)))
            {
                using (StreamReader reader = new StreamReader(File.Open(singlePlayerSaveFileName, FileMode.Open)))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] tokens = line.Split(' ');
                        if (tokens.Length > 0)
                        {
                            switch (tokens[0])
                            {
                                //case "bodyStatePos":
                                //    bodyState.pos = csvParseV3(tokens[1]);
                                //    break;
                                case "filename":
                                    editor.currentLevelFilename = tokens[1];
                                    break;
                                case "enableSound":
                                    enableSound = bool.Parse(tokens[1]);
                                    mixerVolume.Volume = enableSound ? 1 : 0;
                                    break;
                                case "editOutOfBody":
                                    editor.editOutOfBody = bool.Parse(tokens[1]);
                                    break;
                                case "editMouseLocked":
                                    editMouseLocked = bool.Parse(tokens[1]);
                                    break;
                                case "editUseSelectionBrush":
                                    if (tokens.Length > 1)
                                    {
                                        editor.editUseSelectionBrush = bool.Parse(tokens[1]);
                                    }
                                    break;
                                case "crouchSpeed":
                                    crouchSpeed = float.Parse(tokens[1]);
                                    break;
                                case "crawlSpeed":
                                    crawlSpeed = float.Parse(tokens[1]);
                                    break;
                                case "standSpeed":
                                    standSpeed = float.Parse(tokens[1]);
                                    break;
                                case "groundJumpSpeed":
                                    groundJumpSpeed = float.Parse(tokens[1]);
                                    break;
                                case "jumpSpeed":
                                    jumpSpeed = float.Parse(tokens[1]);
                                    break;
                                case "playerFriction":
                                    playerFriction = float.Parse(tokens[1]);
                                    break;
                            }
                            if (saveLoadVariables.ContainsKey(tokens[0]))
                            {
                                saveLoadVariables[tokens[0]].read(tokens[1]);
                            }
                        }
                    }
                    saveLoaded = true;
                    //isSaveReload = true;
                }

                //save settings backup
                if(!requestForceReloadSettings)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        int hours = 2 * (i+1);
                        string backupFilename = hours + "-hour-backup-" + singlePlayerSaveFileName;
                        bool requestBackup = false;
                        if (!File.Exists(backupFilename))
                            requestBackup = true;
                        else
                        {
                            TimeSpan timeSinceLastWrite = DateTime.UtcNow - File.GetLastWriteTimeUtc(backupFilename);
                            if (timeSinceLastWrite.Hours > hours)
                            {
                                requestBackup = true;
                            }
                        }
                        if (requestBackup)
                        {
                            SaveSettings(backupFilename);
                        }
                    }
                }
                requestForceReloadSettings = false;
            }
            //pathfinding.walkablePredicate = (Point p) =>
            //{
            //    if (p.X < 0 || p.X >= terrain.GetLength(0) || p.Y < 0 ||
            //    p.Y >= terrain.GetLength(1))
            //        return false;
            //    var boxes = terrain[p.X, p.Y];

            //    bool hasBox = false;
            //    foreach (var box in boxes)
            //    {
            //        Vector2 pos = new Vector2(box.position.X, box.position.Z);
            //        Vector2 size = new Vector2(box.size.X, box.size.Z);
            //        Vector2 min = pos - size / 2;
            //        Vector2 max = pos + size / 2;
            //        Vector2 bucketSize = new Vector2(widthm, depthm);
            //        Vector2 center = p.ToVector2() * bucketSize;
            //        if (!(center.X < min.X || center.Y < min.Y ||
            //        center.X > max.X || center.Y > max.Y))
            //        {
            //            float top = box.position.Y + box.size.Y;
            //            if (top < 0.6f && top > 0.4f)
            //            {
            //                hasBox = true;
            //            }
            //        }
            //    }
            //    return hasBox;
            //};
            //person.pos.Y = height / 2;
            //if (basicfx != null)
            //{
            //    basicfx.Dispose();
            //}
            //basicfx = game1.Content.Load<Effect>("basic");
            //must be mgfx file
            //LaunchCommandLineApp();
            //using (StreamReader reader = new StreamReader(File.Open("Content/basic.mgfx", FileMode.Open)))
            //{
            //    string file = "";
            //    while (!reader.EndOfStream)
            //    {
            //        file += (char)reader.Read();
            //    }
            //    byte[] fileBytes = Encoding.ASCII.GetBytes(file);
            //    basicfx = new Effect(GraphicsDevice, fileBytes);
            //}
            //playerCam.far =100;
            //playerCam.near = 2;// dropValue/2;
            //reload lists
            //expiredBullets.Clear();
            allBoxes.Clear();
            allguns.Clear();
            customGuns.Clear();
            //extraBoxes.Clear();
            allbullets.Clear();
            myGuns.Clear();
            //boxTargets.Clear();
            //sphereTargets.Clear();
            //visibleTargetsGirl.Clear();
            //visibleTargetsBoy.Clear();
            //spawns.Clear();

            //reload targets
            //targets = new BoundingSphere[0];
            targets = new Target[0];
            targetStarts = new Vector3[0];
            capturedTargets.Clear();

            //reload terrain buckets
            if (terrainActive && terrain == null)
            {
                //terrain = new List<Box>[10, 10]; //>60 FPS
                terrain = new List<Box>[35, 35]; //>60 FPS
                //terrain = new List<Box>[70, 70]; //20 FPS
                //terrain = new List<Box>[140, 140]; //3-6 FPS
                for (int x = 0; x < terrain.GetLength(0); ++x)
                {
                    for (int z = 0; z < terrain.GetLength(1); ++z)
                    {
                        terrain[x, z] = new List<Box>();
                    }
                }
            }


            //reload explosion
            //explosionBulletLocations = new Vector3[explosionBullets.Length];
            ////bombt = -1;
            //for (int i = 0; i < explosionBullets.Length; ++i)
            //{
            //    explosionBullets[i] = new Bullet();
            //    explosionBullets[i].size = 1.0f;
            //    explosionBulletLocations[i] = new Vector3(game1.randf(-1, 1), game1.randf(-1, 1), game1.randf(-1, 1));
            //    explosionBulletLocations[i].Normalize();
            //    //bombBullets[i] = new Bullet();
            //    //bombBullets[i].size = 1.0f;
            //    allbullets.Add(explosionBullets[i]);
            //    //allbullets.Add(bombBullets[i]);
            //}

            //allguns.Add(gun2);
            //for (int i = 0; i < 100; ++i)
            //{
            //    Gun newg = new Gun();
            //    newg.init(allbullets);
            //    newg.pos = new Vector3(game1.randf(0, widthm * terrain.GetLength(0)), 1, game1.randf(0, depthm * terrain.GetLength(1)));
            //    newg.rot = Matrix.CreateFromYawPitchRoll(game1.randa(), game1.randa()/2, game1.randa());
            //    allguns.Add(newg);
            //}
            //for (int b = 0; b < grenadebullets.Length; ++b)
            //{
            //    grenadebullets[b] = new Bullet();
            //}
            //gbp = new Vector3[grenadebullets.Length];
            //for(int i = 0; i < gbp.Length; ++i)
            //{
            //    gbp[i] = Vector3.Transform(Vector3.Forward, Matrix.CreateFromYawPitchRoll(game1.randa(),
            //        game1.randa(), game1.randa()));
            //}
            //tankps.pos = new Vector3(20, 20, 20);
            //tankps.vel = Vector3.Zero;

            // reload terrain data
            if (terrainActive)
            {
                if (!string.IsNullOrWhiteSpace(editor.currentLevelFilename) && File.Exists(editor.currentLevelFilename))
                {
                    loadlevel();
                }
                else
                {
                    playerSpawnPoint = Vector3.Up * height / 2;
                    playerSpawnEuler = new Vector3(0, -MathHelper.Pi / 1.5f, 0);
                    //Dictionary<Point, int> shapes = new Dictionary<Point, int>();
                    //{
                    //    shapes.Add(new Point(5, 5), 0);
                    //    shapes.Add(new Point(6, 5), 2);
                    //    shapes.Add(new Point(7, 5), 0);
                    //    shapes.Add(new Point(8, 5), 0);
                    //    shapes.Add(new Point(9, 5), 2);
                    //    shapes.Add(new Point(10, 5), 0);
                    //    shapes.Add(new Point(7, 7), 1);
                    //    shapes.Add(new Point(8, 7), 1);
                    //    shapes.Add(new Point(7, 10), 1);
                    //    shapes.Add(new Point(8, 10), 1);
                    //};
                    for (int x = 0; x < terrain.GetLength(0); ++x)
                    {
                        for (int y = 0; y < terrain.GetLength(1); ++y)
                        {
                            if (terrain[x, y] == null)
                                terrain[x, y] = new List<Box>();
                            else
                                terrain[x, y].Clear();
                            Box b = new Box();
                            //Color color = (x + y) % 2 == 0 ? monochrome(1.0f) : monochrome(0.9f);
                            Color color = monochrome(0.9f);
                            //Color dirtColor = monochrome(0.6f);
                            b.color = color;
                            int locx = x % 12;
                            int locy = y % 12;
                            //if (locx < 4 && locy < 4)
                            //{
                            //    b.embedsBulletOnImpact = true;
                            //}
                            //else if (locx < 8 && locy < 8)
                            //{
                            //    //b.restitution = 1;
                            //    b.embedsBulletOnImpact = false;
                            //}
                            //else
                            //{
                            //    b.embedsBulletOnImpact = false;
                            //}
                            Vector3 finalSize = new Vector3(widthm, 1, depthm);
                            Vector3 finalPosition = new Vector3(x * finalSize.X, -0.5f, y * finalSize.Z);

                            // cleanup
                            //b.size = new Vector3(widthm, 1, depthm);
                            //b.position = new Vector3(x * b.size.X, -0.5f, y * b.size.Z);

                            //b.embedsBulletOnImpact = true;
                            //if (game1.rand.Next(2) == 0)
                            //{
                            //b.isdoor = true;
                            //b.type = (BoxType)game1.rand.Next((int)BoxType.COUNT);
                            //}
                            if (x != 0 || y != 0)
                            {
                                int value = 20;
                                //int value = game1.rand.Next(20);
                                //if (shapes.ContainsKey(new Point(x, y)))
                                //{
                                //    value = shapes[new Point(x, y)];
                                //}
                                switch (value)
                                {
                                    case 0:
                                    case 2:
                                        //b.size = new Vector3(mw, mh * game1.rand.Next(1, 3), md);
                                        int heightShift = game1.rand.Next(0, 2);
                                        if (value == 2)
                                            heightShift = 1;
                                        if (value == 0)
                                            heightShift = 0;
                                        if (heightShift > 0 && game1.rand.Next(2) == 0)
                                        {
                                            Box floorBox = new Box(b.size, b.position);
                                            floorBox.color = color;
                                            //floorBox.embedsBulletOnImpact = true;
                                            //extraBoxes.Add(floorBox);
                                            allBoxes.Add(floorBox);
                                            terrain[x, y].Add(floorBox);
                                            //if(game1.rand.Next(5) == 0)
                                            //{
                                            //    spawns.Add(floorBox.position + Vector3.Up * (floorBox.size.Y/2));
                                            //}
                                        }
                                        finalSize = new Vector3(widthm, heightm, depthm);
                                        finalPosition.Y = b.size.Y * ((float)heightShift * 0.66f + 0.5f);

                                        //b.embedsBulletOnImpact = false;
                                        break;
                                    case 1:

                                        if (game1.rand.Next(2) == 0)
                                        {
                                            finalSize = new Vector3(widthm, heightm, depthm);
                                            finalPosition.Y = -(b.size.Y / 10);

                                            //b.embedsBulletOnImpact = false;
                                        }
                                        break;
                                }
                            }
                            b.size = finalSize;
                            b.position = finalPosition;
                            terrain[x, y].Add(b);
                            allBoxes.Add(b);
                            //Box ub = new Box();
                            //Color ucolor = (x + y) % 2 == 0 ? monochrome(1.0f) : monochrome(0.9f);
                            //ub.color = ucolor;
                            //ub.size = new Vector3(widthm, 1, depthm);
                            //ub.position = new Vector3(x * ub.size.X, -1.5f, y * ub.size.Z);
                            //terrain[x, y].Add(ub);
                            //allBoxes.Add(ub);
                            //{
                            //    Box ceilBox = new Box(new Vector3(mw, 1, md), new Vector3(b.position.X, mh + 0.5f - 0.5f, b.position.Z));
                            //    ceilBox.color = color;
                            //    extraBoxes.Add(ceilBox);
                            //    allBoxes.Add(ceilBox);
                            //}
                        }
                    }
                }
            }

            //reload guns (gotta put it after load level as that will clear guns not in inventory)
            float gunracky = 0;
            Point gunrackpt = new Point(20, 6);
            float gunrackystart = .5f;
            float gunrackgap = 0.5f;
            Action<Gun> gunrackify = (Gun gun) =>
            {
                gun.pos = coordToWorld(gunrackpt, gunrackystart + gunracky++ * gunrackgap);
                gun.pos += Vector3.Right * gunracky;
                gun.rot = Matrix.CreateRotationY(MathHelper.Pi);
                AddGun(gun);
                if (customGuns.Contains(gun) == false)
                    customGuns.Add(gun);
                //PickupGun(gun);
            };

            //reload ghost
            //ghostGun = new Gun();
            //AddGun(ghostGun);
            //customGuns.Add(ghostGun);
            //ghostPlaybackFrame = ghostFrame = 0;

            //boxBullet = new Box();
            //boxBullet.size = new Vector3(1);
            //boxBullet.type = 0;
            //boxBullet.color = monochrome(1.0f, 1.0f);
            //AddBox(boxBullet);
            //fastGun = new Gun();
            //fastGun.bulletSpeed = 500;
            //fastGun.init(allbullets, .05f, 2, true);
            //fastGun.automaticFireDelayS = 1.5f;
            //fastGun.bulletSpeed = 500;

            slowGun = new Gun(0.05f, 12, false, 4);
            slowGun.filename = "slowgun.txt";
            gunrackify(slowGun);
            slowGun.bulletSpeed = 50;
            PickupGun(slowGun);

            ////blankGun = new Gun();
            ////AddGun(blankGun);
            ////PickupGun(blankGun);

            ////slowGun.AddBullets(allbullets);
            ////myGuns.Add(slowGun);
            ////allguns.Add(slowGun);
            ////turret = Game1.MakeBox(new Vector3(0, 2, 0), new Vector3(2, 4, 2));
            ////turretGun = new Gun();
            ////turretGun.automaticFireDelayS = 0.1f;
            ////turretGun.init(allbullets, 0.03f, 32, true);
            ////turretGun.pos = new Vector3(0, 2.5f, 0);

            //Gun rapidFire = new Gun(0.075f, 12, false);
            //rapidFire.filename = "rapidFire.txt";
            //gunrackify(rapidFire);
            ////rapidFire.size.Z *= 5;
            //rapidFire.automaticFireDelayS = 0.15f;
            //rapidFire.bulletSpeed = 50;
            ////rapidFire.AddBullets(allbullets);
            ////myGuns.Add(rapidFire);
            ////allguns.Add(rapidFire);

            //shotGun = new Gun(0.05f, 20, false);
            //shotGun.filename = "shotGun.txt";
            //gunrackify(shotGun);
            //shotGun.automaticFireDelayS = 0.3f;
            //shotGun.bulletSpeed = 20;
            //shotGun.spreadConeAngle = MathHelper.ToRadians(2);
            //shotGun.bulletsPerShot = 10;

            //chargeShot = new Gun();
            //chargeShot.filename = "chargeShot.txt";
            //gunrackify(chargeShot);

            //retractorGun = new Gun();
            //retractorGun.filename = "retractorGun.txt";
            //gunrackify(retractorGun);
            //shotGun.AddBullets(allbullets);
            //myGuns.Add(shotGun);
            //allguns.Add(shotGun);
            //Gun flamethrower = new Gun();
            //flamethrower.automaticFireDelayS = 0.1f;
            //flamethrower.init(allbullets, 0.2f, 200, true);
            //flamethrower.bulletSpeed = 20;
            //flamethrower.spreadConeAngle = MathHelper.ToRadians(7);
            //flamethrower.bulletsPerShot = 5;
            //myGuns.Add(flamethrower);
            //allguns.Add(flamethrower);
            //girlgun = new Gun();
            //girlgun.bulletSpeed = 63;
            //girlgun.init(allbullets, 0.15f, 6, false);
            //boygun = new Gun();
            //boygun.init(allbullets, 0.05f, 1, false);
            //tankgun = new Gun();
            //tankgun.bulletSpeed = 50;
            //tankgun.init(allbullets, 0.2f, 1);
            //Gun bulkyGun = new Gun();
            //bulkyGun.bulletSpeed = 10;
            //bulkyGun.init(allbullets, 0.8f, 1);
            //gun2 = new Gun();
            //gun2.init(allbullets);
            //myGuns.Add(fastGun);
            //allguns.Add(fastGun);
            //allguns.Add(turretGun);
            //allguns.Add(girlgun);
            //allguns.Add(boygun);
            //clevergirl = new Vector3(0, 1, 0);
            //clevergirl = new Vector3(widthm * terrain.GetLength(0)/2, 3, depthm * terrain.GetLength(1) / 2);
            //cleverboy = new Vector3(10, 3, 0);
            //allguns.Add(tankgun);
            //allguns.Add(bulkyGun);
            guni = 0;
            //currentGun = myGuns[guni];
            //meleeBullet = new Bullet();
            //allbullets.Add(meleeBullet);
            //meleeDuration = 0.33f;
            //meleeing = false;
            //meleeTime = 0;

            //if (terrainActive)
            //{
            //    sky = new StarrySkyEnv(game1, widthm * terrain.GetLength(0) * 2, depthm * terrain.GetLength(1) * 2, 0.5f, 1, 60);
            //}
            //else
            //{
            //    sky = new StarrySkyEnv(game1, 40, 40, 0.5f, 1, 60);
            //}

            //float mapWidth = mw * terrain.GetLength(0);
            //float mapDepth = md * terrain.GetLength(1);
            //Box ceilBox = new Box(new Vector3(mapWidth, 1, mapDepth), new Vector3(mapWidth/2, mh + 0.5f - 0.5f, mapDepth/2));
            //extraBoxes.Add(ceilBox);
            //allBoxes.Add(ceilBox);
            targetRestY = height - dropValue / 2;
            //targetRestY = dropValue/2;
            //targetHitY = height * 2;
            targetHitY = 0.5f;
            //target2HitY = 0;
            //targets = new BoundingSphere[10*3];
            //targets = new BoundingSphere[10];
            //targets = new Target[10];
            //targetSpawns = new Vector3[targets.Length];
            //targetHistory = new Vector3[targets.Length, 5];
            //for (int i = 0; i < targets.Length; i+=3)
            //teleportationTarget = -1;

            //Func<Vector3> randv3 = () =>
            //{
            //    return new Vector3(
            //        game1.randf(widthm * terrain.GetLength(0)),
            //        targetRestY,
            //        game1.randf(depthm * terrain.GetLength(1)));
            //};

            //reload targets
            //List<BoundingSphere> newtargs = new List<BoundingSphere>();
            //for (int i = 0; i < sky.stars.Length; ++i)
            //{
            //    newtargs.Add(new BoundingSphere(new Vector3(
            //        sky.stars[i].X,
            //        sky.stars[i].Y,
            //        sky.stars[i].Z), sky.stars[i].W));
            //}

            // reload health targets
            //for (int i = 0; i < 5 * 3; i += 3)
            ////for (int i = 0; i < targets.Length; ++i)
            //{
            //    //if (i == 3)
            //    //    break;
            //    Vector3 center = randv3();
            //    float radius = dropValue / 2;
            //    float grow = 0.1f;
            //    //targets[i] = new BoundingSphere(center, radius);
            //    //targets[i+1] = new BoundingSphere(center, radius + grow);
            //    //targets[i+2] = new BoundingSphere(center, radius + grow * 2);
            //    newtargs.Add(new BoundingSphere(center, radius));
            //    newtargs.Add(new BoundingSphere(center, radius + grow));
            //    newtargs.Add(new BoundingSphere(center, radius + grow * 2));
            //    //if (teleportationTarget == -1)
            //    //{
            //    //    teleportationTarget = i;
            //    //    teleportationTarget2 = i + 1;
            //    //    teleportationTarget3 = i + 2;
            //    //    teleportationLocation = center;
            //    //}
            //    //targets[i] = new Target(new Vector3(
            //    //    game1.randf(widthm * terrain.GetLength(0)),
            //    //    targetRestY,
            //    //    game1.randf(depthm * terrain.GetLength(1))),
            //    //    dropValue / 2);
            //    //if(game1.rand.Next(2) == 0)
            //    //{
            //    //    targets[i].isCube = true;
            //    //    boxTargets.Add(targets[i]);
            //    //}else
            //    //{
            //    //    sphereTargets.Add(targets[i]);
            //    //}
            //    //targetSpawns[i] = targets[i].Center - Vector3.Up * targetRestY;
            //    //for (int j = 0; j < targetHistory.GetLength(1); ++j)
            //    //{
            //    //    targetHistory[i, j] = targets[i].Center + new Vector3(game1.randf(-1, 1), game1.randf(-1, 1), game1.randf(-1, 1));
            //    //}
            //}

            //reload cores
            //int coreLayerCount = 10;
            //int coreCount = 2;
            ////coreTargetsTable = new int[coreCount][];
            //Vector3 mapSize = new Vector3(terrain.GetLength(0) * widthm,
            //    0,
            //    terrain.GetLength(1) * depthm);
            //for (int i = 0; i < coreCount; ++i)
            //{
            //    //coreTargetsTable[i] = new int[coreLayerCount];
            //    for (int j = 0; j < coreLayerCount; ++j)
            //    {
            //        Vector3 mapCenter = mapSize / 2;
            //        Vector3 center = (mapSize / 4) + (mapSize / 2) * (float)i;// new Vector3(20, targetRestY + 2, 20);
            //        center.Y = 5;
            //        float radius = 1.5f + (float)j * 0.1f;
            //        newtargs.Add(new BoundingSphere(center, radius)); //add layer of core
            //        //coreTargetsTable[i][j] = newtargs.Count - 1;
            //    }
            //}
            //reload koth (king of the hill)
            //spawnA = mapSize / 4 - mapSize / 8;
            //spawnB = mapSize / 2 + mapSize / 4 + mapSize / 8;
            //spawnA.Y = spawnB.Y = 3;
            //clevergirl = spawnA;
            //if (!isSaveReload)
            //{
            //    bodyState.pos = spawnB;
            //}
            //networkTargetIncrementer = 0;
            //kothCenter = mapSize / 2;
            //kothRadius = 10;
            //if (enemyPlayer == null)
            //{
            //    enemyPlayer = new NetworkPlayer();
            //}
            //if(!networkplayerdata.ContainsValue(enemyPlayer))
            //    InitNetworkPlayer(enemyPlayer, new IPEndPoint(IPAddress.None, 0));
            //enemyPlayer.pos = spawnB;
            ////"spare" targets
            //for (int i = 0; i < 10; ++i)
            //    newtargs.Add(new BoundingSphere(new Vector3(0, -200, 0), dropValue / 2));

            //reload zombies
            //zombieVTargs = new int[zombies.Length];
            //for (int i = 0; i < zombies.Length; ++i)
            //{
            //    zombies[i] = new ZombieState();
            //    Vector3 spawn = spawnA;
            //    if(i >= 4)
            //    {
            //        spawn = spawnB;
            //    }
            //    zombies[i].reload(allbullets, allguns, spawn + new Vector3(
            //        game1.randf(-1,1),
            //        0,
            //        game1.randf(-1,1)));
            //}
            //applyDifficulty();
            //clevergirl.Y = bodyState.pos.Y = 3;
            //HACK for siege
            //breachTargets = new int[50];
            //for (int i = 0; i < breachTargets.Length; ++i)
            //{
            //    breachTargets[i] = newtargs.Count;
            //    Vector3 center = coordToWorld(new Point(12, 15)) + Vector3.Up;
            //    float radius = 3.5f + 0.1f * (float)i;
            //    newtargs.Add(new BoundingSphere(center, radius));
            //}

            //reload targets extra
            //for (int i = 0; i < 12;++i)
            ////for (int i = 0; i < zombies.Length + 1; ++i)
            //{
            ////    if (i < zombies.Length)
            ////    {
            ////        //zombies[i].vitalTarg = newtargs.Count;
            ////        zombieVTargs[i] = newtargs.Count;
            ////    }
            //    Vector3 center = randv3();
            //    float radius = dropValue / 2;
            //    newtargs.Add(new BoundingSphere(center, radius));
            //}

            //reload player
            moveSpeed = standSpeed;
            //bodyState.pos = unitSpawn(true, -1);
            bodyState.mass = 45;
            bodyState.pos = playerSpawnPoint;
            playerCam.Euler = playerSpawnEuler;
            bodyState.vel = Vector3.Zero;
            bodyState.force = Vector3.Zero;
            //playerSelfTarget = newtargs.Count;

            //reload travel
            //americanFlagBox = new Box(new Vector3(widthm, height, depthm),
            //    coordToWorld(new Point(30, 30), height / 2));
            //americanFlagBox.color = monochrome(1.0f);
            //AddBox(americanFlagBox);
            //{
            //    float radius = height;
            //    planeSV = new BoundingSphere(coordToWorld(new Point(30, 33), radius), radius);
            //}

            //reload training
            //gaz.ChangeState(0);
            //gazPosition = coordToWorld(new Point(2, 2), height/2);
            //missionState.ChangeState(0);
            //if (g36c != null)
            //    allguns.Remove(g36c);
            //g36c = new Gun();
            //g36c.automaticFireDelayS = 0.1f;
            //g36c.init(allbullets, 0.03f, 32, false);
            //allguns.Add(g36c);
            //g36State.ChangeState(0);
            //if(pvtLootz == null)
            //{
            //    pvtLootz = new Box(new Vector3(1, 1, 1),
            //        coordToWorld(new Point(9, 4), height / 2));
            //    pvtLootz.color = monochrome(0.4f);
            //}
            //allBoxes.Add(pvtLootz);
            //Box invisCeil = new Box(new Vector3(10000, 2, 10000), new Vector3(0, height + 1.5f, 0));
            //invisCeil.ignoresBullets = true;
            //allBoxes.Add(invisCeil);
            //foreach(var b in terrain)
            //{
            //    b.Add(invisCeil);
            //}
            //Box ceil = new Box(new Vector3(1000, 1, 1000), new Vector3(0, widthm * 8, 0));
            //ceil.color = monochrome(1.0f);
            //allBoxes.Add(ceil);
            //ceilBox = ceil;
            //foreach (var b in terrain)
            //{
            //    b.Add(ceil);
            //}

            //reload scripted sequence

            //reload video

            // reload dialog
            //dialogText = "He stood there. I was left uninspired";
            //dialogBox = new Rectangle(0, 0, 500, 50);
            //if(dialogRt == null)
            //    dialogRt = new RenderTarget2D(GraphicsDevice, dialogBox.Width, dialogBox.Height);
            //newtargs.Add(new BoundingSphere(coordToWorld(new Point(4, 8), 43), dropValue/2));
            //narrativeBox = new FPSWahtever.SmallFPS.Box(new Vector3(dropValue), coordToWorld(new Point(4, 3), dropValue/2));
            //narrativeBox.color = monochrome(0.5f);
            //narrativeBoxExcitement = 0;
            //narrativeBoxForce = 0;
            //dialogIndex = -1;

            //reload ui

            //reload units
            //totalCount = 16;
            //allyCount = 3;
            //enemyCount = totalCount - allyCount;
            //units.Clear();
            //for (int i = 0; i < totalCount; ++i)
            //{
            //    Unit unit = new Unit();
            //    unit.gun = new Gun();
            //    unit.gun.init(allbullets, 0.1f, 6, true);
            //    allguns.Add(unit.gun);
            //    unit.stationary = true;
            //    if (i < allyCount)
            //    {
            //        unit.pos = unitSpawn(true, i);
            //        unit.team = 0;
            //        unit.gun.rot = Matrix.CreateRotationY(MathHelper.Pi);
            //        //if (i < 2)
            //        //{
            //            //unit.target = coreTargetsTable[1].First();
            //        //    unit.stationary = false;
            //        //}
            //    }
            //    else
            //    {
            //        int j = i - allyCount;
            //        unit.team = 1;
            //        unit.pos = unitSpawn(false, j);
            //        //if (j < 2)
            //        //{
            //            //unit.target = coreTargetsTable[0].First();
            //        //    unit.stationary = false;
            //        //}
            //    }
            //    unit.selfTarget = newtargs.Count;
            //    newtargs.Add(new BoundingSphere(unit.pos, dropValue / 2));
            //    units.Add(unit);
            //}

            ////reload mechanisms
            //gunMech = new Gun();
            //gunMech.bulletSpeed = 40;
            //gunMech.automaticFireDelayS = 1.0f;
            //gunMech.init(allbullets, .2f, 6);
            //gunMech.pos = coordToWorld(new Point(5, 5));
            //gunMech.pos.Y = 2f;
            //gunMech.rot = Matrix.CreateRotationY(MathHelper.Pi);
            //allguns.Add(gunMech);
            //gunMechTarget = targets.Length;
            //Vector3 targetPosition = gunMech.pos;
            //targetPosition.Y = targetRestY;
            ////AddTarget(new BoundingSphere(targetPosition, dropValue / 2));
            //AddTarget(new Target(targetPosition, dropValue / 2));
            //reloadMech = new BoundingSphere(gunMech.pos, dropValue / 2);
            //reloadMech.Center.Y = 0;
            //reloadMechBullet = null;
            //reloadMechGun = gunMech;

            //newtargs.Clear();
            //targets = new BoundingSphere[newtargs.Count];
            //for(int i = 0; i < targets.Length; ++i)
            //target2y = targets[0].Radius;
            //target2r = targets[0].Radius / 1.5f;
            if (requestNewGame)
            {
                SaveSettings(singlePlayerSaveFileName);
                requestNewGame = false;
            }
            //END RELOAD
        }

        //helper input
        public bool mouseHover(Rectangle rect)
        {
            return rect.Contains(game1.mouseCurrent.Position);
        }
        public static char KeyToChar(Keys key, bool shiftModifier)
        {
            int code = (int)key;
            char c = (char)0;
            if (code >= 65 && code <= 90) //A-Z
            {
                c = (char)key;
                if (!shiftModifier)
                {
                    c = char.ToLower(c);
                }
            }
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                int number = key - Keys.D0;
                number += 48; //Ascii[48, 57] = [0, 9]
                c = (char)number;
            }
            switch (key)
            {
                case Keys.OemPeriod:
                    c = '.';
                    if (shiftModifier)
                        c = '>';
                    break;
                case Keys.OemMinus:
                    c = '-';
                    if (shiftModifier)
                        c = '_';
                    break;
            }
            return c;
        }

        //helper fps
        //public virtual void OnTargetHit(Bullet b, Target t, ContactData c)
        //{

        //}

        //helper misc
        public bool OutOfBody()
        {
            return (editor.edit.active && editor.editOutOfBody) || (!editor.edit.active && playOutOfBody);
        }
        public static Vector2 xz(Vector3 input)
        {
            return new Vector2(input.X, input.Z);
        }
        public void AddTarget(Vector3 position, float radius)
        {
            //AddTarget(new BoundingSphere(position, radius));
            AddTarget(new Target(position, radius));
        }
        //public void AddTarget(BoundingSphere target)
        public void AddTarget(Target target)
        {
            Target[] larger = new Target[targets.Length + 1];
            //BoundingSphere[] larger = new BoundingSphere[targets.Length + 1];
            Vector3[] largerStarts = new Vector3[larger.Length];
            for (int i = 0; i < larger.Length; ++i)
            {
                if (i < targets.Length)
                {
                    larger[i] = targets[i];
                    largerStarts[i] = targetStarts[i];
                }
                else
                {
                    larger[i] = target;
                    largerStarts[i] = target.Center;
                }
            }
            targets = larger;
            targetStarts = largerStarts;
            editor.edit.saveNeeded = true;
        }
        public void RemoveTargetAt(int t)
        {
            Target[] smaller = new Target[targets.Length - 1];
            //BoundingSphere[] smaller = new BoundingSphere[targets.Length - 1];
            Vector3[] smallerStarts = new Vector3[smaller.Length];
            int j = 0;
            for (int i = 0; i < targets.Length; ++i)
            {
                if (i != t && j < smaller.Length)
                {
                    smaller[j] = targets[i];
                    smallerStarts[j] = targetStarts[i];
                    j++;
                }
            }
            targets = smaller;
            targetStarts = smallerStarts;
            editor.edit.saveNeeded = true;
        }
        //Vector3 unitSpawn(bool ally, int i)
        //{
        //    Vector3 pos = Vector3.Zero;
        //    if(ally)
        //            pos = Vector3.Backward * depthm + Vector3.Right * 2 + Vector3.Right * widthm * (i + 1);
        //    else
        //        pos = Vector3.Backward * (terrain.GetLength(1) - 1) * depthm +
        //        Vector3.Right * widthm * (terrain.GetLength(0) - 1) + Vector3.Left * widthm * i;
        //    pos.Y = height - dropValue / 2;
        //    return pos;
        //}
        public static void UpdateGunFromCam(Gun gun, CameraState cam, Vector2 normScreen, GraphicsDevice gd, bool aimDownSight, SmallFPS fps)
        {
            Vector3 camForward = Vector3.Transform(Vector3.Forward, cam.rotation3D);
            Ray toGunRay = cam.ScreenToRay(
                    Backpack.percentageLocation(gd.Viewport.Bounds, normScreen.X, normScreen.Y),
                    gd.Viewport);
            Vector3 top = fps.bodyState.pos + Vector3.Up * (fps.bodyc / 2 * fps.dropValue - fps.dropValue / 2);
            if (aimDownSight)
                gun.pos = top + toGunRay.Direction * fps.playerGunForwardOffset + Vector3.Down * gun.BulletSize;
            else
                gun.pos = top + toGunRay.Direction * fps.playerGunForwardOffset;
            Vector2 screens = gd.Viewport.Bounds.Size.ToVector2();
            Vector2 screenc = screens / 2;
            Vector3 d = cam.ScreenToWorld(screenc, 1, gd.Viewport);
            Vector3 gf = d - gun.pos;
            gf.Normalize();
            Vector3 gr = Vector3.Cross(Vector3.Up, gf);
            gr.Normalize();
            Vector3 gu = Vector3.Cross(gf, gr);
            gu.Normalize();
            //guneuler = Vector3.Lerp(guneuler, Vector3.Zero, 0.1f);
            //Matrix gunrotlocal = Matrix.CreateFromYawPitchRoll(guneuler.Y, guneuler.X, guneuler.Z);
            gun.rot =
                //gunrotlocal *
                Matrix.Invert(Matrix.CreateLookAt(Vector3.Zero, gf, gu));
            camForward = Vector3.Transform(Vector3.Forward, cam.rotation3D); //refresh foward
            Vector3 up = Vector3.Transform(Vector3.Up, cam.rotation3D); //get up
                                                                        ////Vector3 gfa = Vector3.Transform(Vector3.Forward, gunrot);
                                                                        ////Vector3 gua = Vector3.Transform(Vector3.Up, gunrot);
                                                                        //for (int b = 0; b < bullets.Length; ++b)
                                                                        //{
                                                                        //    if (bullets[b].off)
                                                                        //    {
                                                                        //        //Vector3 offset = gua / 10 + gfa * (float)b * 0.1f;
                                                                        //        Vector3 offset = gu / 10 + gf * (float)b * 0.1f;
                                                                        //        //offset = Vector3.Transform(offset, gunrotlocal);
                                                                        //        bullets[b].p.pos = gunpos + offset;
                                                                        //    }
                                                                        //}
        }
        //ContactData boxIntersectBox(BoundingBox a, BoundingBox b)
        //{
        //    float 
        //    if(a.Min )
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="destination"></param>
        /// <param name="delta"></param>
        /// <returns>Whether the target has reached the destination yet, or not</returns>
        bool travel(ref Vector3 target, Vector3 destination, float delta)
        {
            Vector3 toDestination = destination - target;
            float distance = toDestination.Length();
            if (distance <= delta)
            {
                target = destination;
                return true;
            }
            else
            {
                target += (toDestination / distance) * delta;
            }
            return false;
        }
        //hack for progression of difficult
        //void applyDifficulty()
        //{
        //    //level 0 easy!
        //    //level 1 easy
        //    //level 2 wait
        //    //level 3 dam!
        //    //level 4 fuck!
        //    //level 5 impossible
        //    //level 6 IMPOSSIBLE
        //    bool grav = true;
        //    float speed = 40;
        //    float size = 0.15f;
        //    float slow = 20, medium = 50, fast = 100, superfast = 200;
        //    float small = 0.15f, tiny = 0.1f, mediumsize = 0.25f, large = 0.4f;
        //    int ammo = 2;
        //    int lowammo = 2, mediumammo = 6, highammo = 10, infiniteammo = 30;
        //    switch (difficultyLevel)
        //    {
        //        case 0:
        //            grav = true;
        //            speed = slow;
        //            size = large;
        //            ammo = lowammo;
        //            break;
        //        case 1:
        //            grav = true;
        //            speed = slow;
        //            size = large;
        //            ammo = lowammo;
        //            break;
        //        case 2:
        //            grav = false;
        //            speed = fast;
        //            size = mediumsize;
        //            ammo = lowammo;
        //            break;
        //        case 3:
        //            grav = false;
        //            speed = fast;
        //            size = small;
        //            ammo = mediumammo;
        //            break;
        //        case 4:
        //            grav = false;
        //            speed = fast;
        //            size = small;
        //            ammo = mediumammo;
        //            break;
        //        case 5:
        //            grav = false;
        //            speed = superfast;
        //            size = small;
        //            ammo = highammo;
        //            break;
        //        default:
        //            grav = false;
        //            speed = superfast;
        //            size = tiny;
        //            ammo = infiniteammo;
        //            break;
        //    }
        //    zombies[0].gun.bulletSpeed = speed;
        //    zombies[0].gun.init(allbullets, 0.15f, ammo, grav);
        //}
        //public float GetMapWidth()
        //{
        //    return terrain.GetLength(0) * widthm;
        //}
        //public float GetMapDepth()
        //{
        //    return terrain.GetLength(1) * depthm;
        //}
        //int tclamp(int value)
        //{
        //    return MathHelper.Clamp(value, 0, terrain.GetLength(0) - 1); //HACK assumes width = depth
        //}
        //public bool RayHitsBox(Vector3 start, Vector3 end)
        //{
        //    Vector3 dir = end - start;
        //    float length = dir.Length();
        //    dir /= length;
        //    return RayHitsBox(new Ray(start, dir), length);
        //}
        //public bool RayHitsBox(Ray ray, float length)
        //{
        //    Vector3 end = ray.Position + ray.Direction * length;
        //    Point[] zone = getzone(ray, length);
        //    //Rectangle zone = getzone(
        //    //    Vector3.Min(ray.Position, end),
        //    //    Vector3.Max(ray.Position, end));
        //    //for (int x = tclamp(zone.Left); x <= tclamp(zone.Right); ++x)
        //    //{
        //    //    for (int z = tclamp(zone.Top); z <= tclamp(zone.Bottom); ++z)
        //    //    {
        //    for (int i = 0; i < zone.Length; ++i)
        //    {
        //        foreach (Box box in terrain[zone[i].X, zone[i].Y])
        //        {
        //            float? hit = ray.Intersects(Game1.MakeBox(box.position, box.size));
        //            if (hit.HasValue && hit.Value <= length)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    //    }
        //    //}
        //    return false;
        //}

        //update
        public override void Update(GameTime gameTime)
        { //start update
            //if (!loadContentComplete)
            //    return;
            if(editor.editor.SetReady())
            {
                editor.editor.PerformSet();
            }
            if(editor.saveRequested)
            {
                SaveSettings(singlePlayerSaveFileName);
                editor.saveRequested = false;
            }
            base.Update(gameTime);
            float et = (float)gameTime.ElapsedGameTime.TotalSeconds;
            updatefps.Update(et);
            float tt = (float)gameTime.TotalGameTime.TotalSeconds;
            StateMachine.totalGameTime = tt;
            if (game1.kclick(Keys.U))
            {
                Console.WriteLine("nada");
            }
            //update ui
            //uiGunWindow.x = (float)ViewBounds.Width * 0.5f;
            //uiGunWindow.y = (float)ViewBounds.Height * 0.2f;
            //uiGunWindow.width = (float)ViewBounds.Width * 0.2f;
            //uiGunWindow.height = (float)ViewBounds.Height * 0.70f;
            //UIFollow(uiGunReload, uiGunWindow, 0, 0, 1, 0.1f);
            //UIFollow(uiGunJoin, uiGunWindow, 0, 0.1f, 1, 0.1f);
            //update ui element
            //Vector2 mousePointer = game1.mouseCurrent.Position.ToVector2();
            //uiMouseHost = null;
            //for (int i = 0; i < allUIElements.Count; ++i)
            //{
            //    UICaptureMouse(allUIElements[i], mousePointer);
            //}
            //update pause
            if (game1.kclick(Keys.Escape))
            {
                //if (skipToDesktopQuit)
                //{
                game1.Exit();
                //}
                //else
                //{
                //    if (!pauseMenuShowing)
                //    {
                //        pauseMenuShowing = true;
                //        paused = true;
                //        game1.IsMouseVisible = true;
                //    }
                //    else
                //    {
                //        pauseMenuShowing = false;
                //        paused = false;
                //        game1.IsMouseVisible = false;
                //    }
                //}
            }
            //if (pauseMenuShowing &&
            //    game1.mouseCurrent.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
            //    game1.mouseOld.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            //{
            //    if (pauseRects[0].Contains(game1.mouseCurrent.Position)) //quit to menu
            //    {
            //        SaveGame();
            //        if (menu != null)
            //        {
            //            game1.Components.Remove(this);
            //            game1.Components.Add(menu);
            //        }
            //        else
            //        {
            //            game1.Exit();
            //        }
            //    }
            //    if (pauseRects[1].Contains(game1.mouseCurrent.Position)) //quit to menu
            //    {
            //        SaveGame();
            //        game1.Exit();
            //    }
            //}
            bool controlDown = game1.kdown(Keys.LeftControl) ||
                game1.kdown(Keys.RightControl);
            bool shiftDown =
                game1.kdown(Keys.LeftShift) ||
                game1.kdown(Keys.RightShift);
            bool modifierDown = controlDown || shiftDown;

            bool wasPaused = paused;
            if (game1.kclick(Keys.P))
            {
                paused = !paused;
            }
            if (game1.kclick(Keys.F10) || (game1.kdown(Keys.LeftShift) &&
                game1.kdown(Keys.F10)))
            {
                stepOver = true;
            }
            if (paused && !stepOver)
                return;
            stepOver = false;

            //float turnRate = 3;
            //float turnProgress = turnElapsed / turnCap;
            //bool turnInputEntered = false;
            //if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.D))
            //{
            //    camera.Euler.Y -= turnRate * et * turnProgress;
            //    turnInputEntered = true;
            //}
            //if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.A))
            //{
            //    camera.Euler.Y += turnRate * et * turnProgress;
            //    turnInputEntered = true;
            //}
            //if (turnInputEntered)
            //{
            //    turnElapsed += et;
            //}
            //else
            //{
            //    turnElapsed -= et;
            //}
            //turnElapsed = MathHelper.Clamp(turnElapsed, 0, turnCap);
            //person.pos.Y = 2.75f;
            //person.pos.X = 20;
            //person.pos.Z = 20;

            //update network incoming
            //for (int i = 0; i < udpResults.Keys.Count; ++i)
            //{
            //    var endpoint = udpResults.Keys.ElementAt(i);
            //    var messages = udpResults[endpoint];
            //    for (int j = 0; j < messages.Count; ++j)
            //    {
            //        ProcessMessage(endpoint, messages[j]);
            //    }
            //    messages.Clear();
            //}
            //foreach (var pair in networkplayerdata)
            //{
            //    Vector3 pairTop = pair.Value.pos + (height / 2) * Vector3.Up;
            //    pair.Value.camera.fov = 35;
            //    pair.Value.camera.pos = pairTop + drop / 2;
            //    pair.Value.camera.Update(gameTime, GraphicsDevice.Viewport);
            //    UpdateGunFromCam(pair.Value.gun, pair.Value.camera,
            //        new Vector2(0.7f, 0.8f),
            //        GraphicsDevice,
            //        false
            //        );
            //    //Vector3 tonp = pair.Value.pos - bodyState.pos;
            //    //tonp.Normalize();
            //    //game1.add3DLine(bodyStte.pos + tonpa, bodyState.pos + tonp * 3, monochrome(1.0f, 0.5f));
            //    //game1.add3DLine(pair.Value.camera.pos,
            //    //    pair.Value.camera.pos + Vector3.Up, monochrome(1.0f, 0.5f));
            //    //pair.Value.gun.rot = Matrix.CreateFromYawPitchRoll(pair.Value.camera.Euler.Y,
            //    //    pair.Value.camera.Euler.X,
            //    //    pair.Value.camera.Euler.Z);
            //    //Vector3 gunright = Vector3.Transform(Vector3.Right, pair.Value.gun.rot);
            //    //pair.Value.gun.pos = 
            //    //    pair.Value.pos + 
            //    //    //Vector3.Up * height / 8 + 
            //    //    gunright * dropValue/1.5f;
            //    //targets[pair.Value.selfTarget].Center = pair.Value.pos + Vector3.Up * (height / 2 - dropValue / 2);
            //    //game1.add3DLine(pair.Value.pos, pair.Value.pos + Vector3.Transform(Vector3.Forward, pair.Value.gun.rot),
            //    //    Color.Green);
            //    if (pair.Value.input.shoot)
            //    {
            //        pair.Value.gun.TriggerDown(new PhysicsState(1));
            //    }
            //}

            //if(turret.Contains(bodyState.pos) == ContainmentType.Contains)
            //{
            //    currentGun = turretGun;
            //    //bodyState.pos = turretGun.pos;
            //}

            //update player input
            //if (game1.kdown(Keys.LeftAlt))
            //    playerFriction = 0;
            //else
            //    playerFriction = 1;

            //drop gun
            if (game1.kclick(Keys.LeftControl) && !editor.edit.active)
                playerRequestFrictionOverride = !playerRequestFrictionOverride;
            if (game1.kclick(Keys.Z))
            {
                if (guni < myGuns.Count)
                    DropGun(guni);
            }
            if (game1.kclick(Keys.F))
                is3rdPerson = !is3rdPerson;
            if (game1.kclick(Keys.Q))
            {
                gameMouseLocked = !gameMouseLocked;
            }
            //update mouse
            bool mouseLocked = true;
            if (!editMouseLocked && editor.edit.active)
                mouseLocked = false;
            if (!editor.edit.active && !gameMouseLocked)
                mouseLocked = false;
            if (game1.IsActive)
            {
                if (mouseLocked)
                {
                    Point c = GraphicsDevice.Viewport.Bounds.Center;
                    if (wasActive && !wasPaused && wasMouseLocked)
                    {
                        float mdx, mdy;
                        Point md = game1.mouseCurrent.Position - c;
                        mdx = md.X;
                        mdy = md.Y;
                        //update camera look
                        playerCam.Euler.Y -= mdx / (float)GraphicsDevice.Viewport.Width;
                        playerCam.Euler.X -= mdy / (float)GraphicsDevice.Viewport.Width;
                    }

                    //update mouse position, update mouse visibility
                    Mouse.SetPosition(c.X, c.Y);
                    game1.IsMouseVisible = false;
                }
                else if (!editor.edit.active)
                {
                    Vector2 mouse = game1.mouseCurrent.Position.ToVector2();
                    int thresh = 5;
                    mouse.X = MathHelper.Clamp(mouse.X, thresh, ViewBounds.Width - thresh);
                    mouse.Y = MathHelper.Clamp(mouse.Y, thresh, ViewBounds.Height - thresh);
                    Mouse.SetPosition((int)mouse.X, (int)mouse.Y);
                }
            }
            if (!mouseLocked)// edit.active)
            {
                if (editor.edit.active)
                    game1.IsMouseVisible = true;
            }
            wasMouseLocked = mouseLocked;
            wasActive = game1.IsActive;
            Vector3 moveInput = Vector3.Zero;
            Vector3 camForward = Vector3.Transform(Vector3.Forward, playerCam.rotation3D);
            Vector3 camRight = Vector3.Transform(Vector3.Right, playerCam.rotation3D);
            Vector3 camForwardFlat = camForward; camForwardFlat.Y = 0;
            camForwardFlat.Normalize();

            localInput0 = localInput;
            localInput = new FPSInput();
            if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                localInput.forward = true;
            }
            if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.S) && !controlDown)
            {
                localInput.back = true;
            }
            if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                localInput.right = true;
            }
            if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                localInput.left = true;
            }
            if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                localInput.jump = true;
                //bodyState.vel.Y += 5f;
            }
            if (localInput.forward)
            {
                moveInput.Z += 1;
            }
            if (localInput.back)
            {
                moveInput.Z -= 1;
            }
            if (localInput.right)
            {
                moveInput.X += 1;
            }
            if (localInput.left)
            {
                moveInput.X -= 1;
            }
            if (localInput.jump)
            {
                moveInput.Y = 1;
                //bodyState.vel.Y += 5f;
            }
            if (game1.kclick(Microsoft.Xna.Framework.Input.Keys.R))
            {
                if (controlDown)
                {
                    returnLoc = bodyState.pos;
                    bodyState.pos = playerSpawnPoint;
                }
                else if (shiftDown)
                    bodyState.pos = returnLoc;
                else
                    reload();
            }
            float speedboost = playerWalkBoost;
            Action<int, float, int> changeStance = (int spherecount, float speed, int stancelabel) =>
            {
                int delta = spherecount - bodyc;
                bodyc += delta;
                moveSpeed = speed;
                //move up or down for each sphere
                bodyState.pos += (Vector3.Up * (height / 6 * delta)) / 2;
                stance0 = stance;
                stance = stancelabel;
                stanceChanged = true;
            };
            if (game1.kclick(Keys.C) && !controlDown)
            {
                if (stance == 0)//standing
                {
                    //crouch
                    changeStance(sphereBodyCount - 2, crouchSpeed, 1);
                }
                if (stance == 2)
                {
                    //stand
                    changeStance(sphereBodyCount - 2, crouchSpeed, 1);
                }
            }
            if (game1.kheld(Keys.C) && !controlDown)
            {
                if (stance == 0 || stance == 1)
                {
                    if (!stanceChanged || stance0 == 0) //allow stand>crouch>prone but not prone>stand>prone
                    {
                        changeStance(sphereBodyCount - 4, crawlSpeed, 2);
                    }
                }
                stancekeyheld = true;
            }
            if (game1.krelease(Keys.C) && !controlDown)
            {
                if (stance == 1 && !stancekeyheld && !stanceChanged)
                {
                    changeStance(sphereBodyCount, standSpeed, 0);
                }
                stancekeyheld = false;
                stanceChanged = false;
            }
            if (game1.kdown(Keys.LeftShift))
            {
                speedboost = playerRunBoost;
            }
            bool playerTouchingGround = false;
            {
                Vector3 start = bodyState.pos;
                float slop = 0.01f;
                Vector3 toGround = Vector3.Down * ((float)bodyc / 2 * dropValue + slop);
                Vector3 end = start + toGround;
                float groundDist = toGround.Length();
                Ray groundRay = new Ray(start, toGround / groundDist);
                Rectangle zone = getzone(Vector3.Min(start, end), Vector3.Max(start, end));
                List<Box> boxes = getBoxesInZone(zone);
                foreach (Box b in boxes)
                {
                    float? hit = groundRay.Intersects(b.boundingBox);
                    if (hit.HasValue && hit.Value <= groundDist)
                    {
                        playerTouchingGround = true;
                    }
                }
            }
            if(!playerTouchingGround)
            {
                speedboost = playerFlyBoost;
            }
                    Vector3 moveDirection = Vector3.Zero;
            if (moveInput.X != 0 || moveInput.Z != 0)
            {
                moveDirection = Vector3.Normalize(moveInput.X * camRight + moveInput.Z * camForwardFlat);
                //moveDirection = Vector3.Normalize(moveInput.X * camRight + moveInput.Z * camForward);
                playerMoveInputElapsed += playerInputAccelRate * et;
            }else
            {
                playerMoveInputElapsed -= playerInputDecelRate * et;
            }
            playerMoveInputElapsed = MathHelper.Clamp(playerMoveInputElapsed, 0, 1);
            Vector3 movementForce = moveDirection * moveSpeed * speedboost * et * bodyState.mass * playerMoveInputElapsed;
            //if (intank)
            //{
            //    tankps.force += movementForce;
            //    if(game1.kclick(Keys.T))
            //    {
            //        intank = false;
            //    }
            //}
            //else
            //{
            if (editor.edit.active && editor.editOutOfBody)
            {
                //Vector3 nonplanardir = Vector3.Zero;
                //if (direction.LengthSquared() > 0)
                //    nonplanardir = Vector3.Normalize(direction.X * rt + direction.Z * camForward);
                playerCam.pos += moveDirection * moveSpeed * speedboost * et * et;
                float risefall = 1;
                if (game1.kdown(Keys.LeftShift))
                    risefall = -1;
                if (moveInput.Y != 0)
                {
                    playerCam.pos.Y += moveSpeed * et * et * risefall * moveInput.Y;
                }
            }
            else
            {
                if (true)
                {
                    bodyState.force += movementForce;
                    //float preY = bodyState.vel.Y;
                    //bodyState.vel = Vector3.Lerp(bodyState.vel, movementForce / bodyState.mass * et * 50, 0.1f);
                    //bodyState.vel.Y = preY;
                    float jumpImpulse = (-gravity.Y / et) + jumpSpeed;
                    if (moveInput.Y > 0 && bodyState.vel.Y <= 0 && playerTouchingGround)
                    {
                        jumpImpulse = groundJumpSpeed;
                    }
                    bodyState.force += Vector3.Up * jumpImpulse * moveInput.Y * bodyState.mass * et;
                }
                else
                {

                    //bodyState.vel = movementForce / bodyState.mass * et * 50;
                    //if (jumpElapsed < jumpDuration)
                    //{
                    //update jump
                    float jumpImpulse = 2000;
                    float use = 1f;
                    //bodyState.force += Vector3.Up * jumpImpulse * moveInput.Y * bodyState.mass * et; //turn off hack for siege
                    //if (moveInput.Y != 0 && jetfuel > use)
                    //{
                    //    jetfuel -= use;
                    //bodyState.force += Vector3.Up * jumpImpulse * moveInput.Y * bodyState.mass * et; //turn off hack for siege
                    //bodyState.vel.Y = moveInput.Y * 10;
                    //}
                    //jetfuel += jetfuelrechargerate * et;
                    //jetfuel = MathHelper.Clamp(jetfuel, 0, 50);
                    //jumpElapsed += et;
                    //}
                }
            }
            //}
            //update gun input
            if (currentGun != null)
            {
                if (game1.IsActive
                && !editor.edit.active
                && (game1.kclick(Microsoft.Xna.Framework.Input.Keys.Up)
                    || (game1.mouseCurrent.LeftButton == ButtonState.Pressed)
                    //&& game1.mouseOld.LeftButton == ButtonState.Released)
                    //|| game1.krelease(Keys.F)
                    || game1.kdown(Keys.OemCloseBrackets))
                && GraphicsDevice.Viewport.Bounds.Contains(game1.mouseCurrent.Position))
                {
                    localInput.shoot = true;
                }
            }
            //update shoot gun
            chargeShotDelay = 0.1f;
            if (localInput.shoot)
            {
                if (currentGun == chargeShot)
                {
                    chargeShotElapsed += et;
                }
                if (currentGun == chargeShot && chargeShotElapsed >= chargeShotDelay)
                {
                    chargeShotElapsed -= chargeShotDelay;
                    chargeShot.bulletsPerShot =
                        MathHelper.Clamp(chargeShot.bulletsPerShot + 1, 1, chargeShot.bullets.Count);
                }
                if (currentGun != chargeShot && currentGun.TriggerDown(bodyState))
                {
                    //update shoot recoil/kickback
                    //for (int i = 0; i < currentGun.bullets.Length; ++i)
                    //for (int i = 0; i < currentGun.bullets.Count; ++i)
                    //    {
                    //    if (currentGun.bullets[i].t != 0)
                    //        continue;
                    //    bodyState.force -= currentGun.bullets[i].phy.vel * currentGun.bullets[i].phy.mass / et;
                    //}
                    //audioEmitter.Position = currentGun.pos;
                    //SoundEffectInstance instance = click.CreateInstance();
                    //instance.Apply3D(audioListener, audioEmitter);
                    //instance.Play();
                    //var noise = new SignalGenerator() { Type = SignalGeneratorType.White };
                    //var noiseVolume = new VolumeSampleProvider(noise) { Volume = 0.01f };
                    //var noiseAdsr = new AdsrSampleProvider(noiseVolume.ToMono());
                    //SignalGeneratorType shootSignalType = SignalGeneratorType.Sin;

                    //update shoot sound
                    var sin = new SignalGenerator() { Type = shootSound.signalT, Frequency = shootSound.freq, FrequencyEnd = shootSound.freqEnd, SweepLengthSecs = shootSound.sweepL };
                    float totalTime = currentGun.automaticFireDelayS / 2;
                    sin.SweepLengthSecs = totalTime;
                    var sinAdsr = new AdsrSampleProvider(sin.ToMono());
                    sinAdsr.AttackSeconds = totalTime * shootSound.attackP;
                    sinAdsr.ReleaseSeconds = totalTime - sinAdsr.AttackSeconds;
                    //mixer.AddMixerInput(adsr.Take(TimeSpan.FromSeconds(currentGun.automaticFireDelayS/2)));
                    AddMixerInput(sinAdsr.Take(TimeSpan.FromSeconds(totalTime)), "gun being shot", 3);
                    //mixer.AddMixerInput(noiseAdsr.Take(TimeSpan.FromSeconds(noiseAdsr.AttackSeconds + noiseAdsr.ReleaseSeconds)));
                }
                //if (!currentGun.requireTriggerUp)
                //{
                //    Vector3 gunForward = Vector3.Transform(Vector3.Forward, currentGun.rot);
                //    float amt = 3;
                //    float stepAngle = MathHelper.TwoPi / amt;
                //    Vector3 gunRight = Vector3.Transform(Vector3.Right, currentGun.rot);
                //    float screenSpread = 0.015f;
                //    Vector2 screenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                //    Vector3 screenFarCenter = playerCam.ScreenToWorld(
                //        screenSize / 2, 1, GraphicsDevice.Viewport);
                //    Vector3 screenFarRight = playerCam.ScreenToWorld(
                //        screenSize / 2 + new Vector2(screenSpread * screenSize.X / 2, 0), 1, GraphicsDevice.Viewport);
                //    Vector3 spreadArm = screenFarRight - screenFarCenter;
                //    for (int i = 0, j = 0; i < currentGun.bullets.Length && j < amt; ++i)
                //    {
                //        if (!currentGun.bullets[i].off)
                //            continue;
                //        //Vector3 spread = Vector3.Transform(gunRight,
                //        //    Matrix.CreateFromAxisAngle(gunForward, stepAngle * i));
                //        Vector3 spread = Vector3.Transform(spreadArm,
                //            Matrix.CreateFromAxisAngle(gunForward, stepAngle * i));
                //        Vector3 targetFar = screenFarCenter + spread;
                //        Vector3 dir = targetFar - currentGun.bullets[i].phy.pos;
                //        dir.Normalize();
                //        Bullet b = currentGun.bullets[i];
                //        b.off = false;
                //        //b.phy.vel = gunForward * currentGun.bulletSpeed + spread;
                //        b.phy.vel = dir * currentGun.bulletSpeed;
                //        b.t = 0;
                //        j++;
                //    }
                //    currentGun.requireTriggerUp = true;
                //}
                //float stepAngle = MathHelper.TwoPi / 5;
                //Vector3 gunForward = Vector3.Transform(Vector3.Forward, currentGun.rot);
                //Vector3 gunRight = Vector3.Transform(Vector3.Right, currentGun.rot);
                //Matrix originalRotation = currentGun.rot;
                //for (int i = 0; i < 5; ++i)
                //{
                //    Vector3 spread = Vector3.Transform(gunRight,
                //        Matrix.CreateFromAxisAngle(gunForward, stepAngle * i));
                //    currentGun.Shoot(spread * currentGun.bulletSpeed / 2);
                //}
                //tankgun.Shoot(tankps);
            }
            else
            {
                if (currentGun != null)
                {
                    currentGun.TriggerUp();
                }
                if (localInput0.shoot && currentGun == chargeShot && currentGun != null)
                {
                    currentGun.TriggerDown(bodyState);
                    currentGun.TriggerUp();
                    currentGun.bulletsPerShot = 1;
                    chargeShotElapsed = 0;
                }
            }
            //update current gun with middle mouse
            int swd = game1.mouseCurrent.ScrollWheelValue - game1.mouseOld.ScrollWheelValue;
            if (swd != 0 && !editor.edit.active)
            {
                int max = myGuns.Count;
                if(playerCanSwapToEmpty)
                    max = (myGuns.Count + 1);
                if (max > 0)
                {
                    if (swd > 0)
                    {
                        guni = (guni + 1) % max;
                    }
                    if (swd < 0)
                    {
                        guni--;
                        if (guni < 0)
                            guni += max;
                    }
                    if (guni == myGuns.Count)
                    {
                        currentGun = null;
                    }
                    else
                    {
                        currentGun = myGuns[guni];
                    }
                }
            }
            //playerCam.fov_rate = 50;
            //    playerCam.fov_max = 45;
            //    playerCam.fov_min = 35;
            //if(currentGun == null)
            //{
            //    playerCam.zooming = false;
            //}else
            //{
            //    playerCam.zooming = true;
            //}
            if (!editor.edit.active)
            {
                for (int i = 0; i < 10; ++i)
                {
                    int desiredGunIndex = i - 1;
                    if (i == 0) //0 is actually at the end
                        desiredGunIndex = 9;
                    if (desiredGunIndex >= myGuns.Count && desiredGunIndex >= 0)
                        continue;
                    Keys key = Keys.D0 + i;
                    if (game1.kclick(key))
                    {
                        guni = desiredGunIndex;
                        currentGun = myGuns[guni];
                        break;
                    }
                }
            }
            //if (intank)
            //{
            //    currentGun = tankgun;
            //    bodyState.force = Vector3.Zero;
            //    bodyState.vel = Vector3.Zero;
            //    bodyState.pos = tankps.pos;
            //    tankeu = new Vector3(0, camera.Euler.Y, 0);
            //}
            //update player position
            //float airFriction = 8;
            bodyState.force -= bodyState.vel * playerFrictionAir;
            bodyState.force += gravity * bodyState.mass;
            bodyState.Advance(et);
            if(bodyState.vel.LengthSquared() > playerTerminalVelocity * playerTerminalVelocity)
            {
                Vector3 oldVel = bodyState.vel;
                float vel = bodyState.vel.Length();
                bodyState.vel /= vel;
                bodyState.vel *= playerTerminalVelocity;
                bodyState.force += (bodyState.vel - oldVel) * bodyState.mass / et; //preemptive force, penalty
            }

            //targets[playerSelfTarget].Center = bodyState.pos;

            //update koth ai 
            //update koth pathfinding
            //kothPathElapsed += et;
            //if (kothEnemyPath == null || kothPathElapsed > kothPathDuration)
            //{
            //    kothPathElapsed -= kothPathDuration;
            //    kothEnemyPath = pathfinding.findPath(worldToCoord(enemyPlayer.pos),
            //        worldToCoord(kothCenter));
            //    kothPathProgress = 0;
            //}
            ////game1.add3DLine(bodyState.pos, enemyPlayer.pos, Color.Red);
            //Vector3 kothAiForce = Vector3.Zero;
            //if (kothEnemyPath != null && kothPathProgress < kothEnemyPath.Count)
            //{
            //    square next = kothEnemyPath[kothPathProgress];
            //    Vector3 targetPosition = coordToWorld(next.coordinate);
            //    targetPosition.Y = height / 2;
            //    Vector3 toTarget = targetPosition - enemyPlayer.pos;
            //    float dist = toTarget.Length();
            //    float delta = 3 * et;
            //    if (dist > delta)
            //    {
            //        //enemyPlayer.input.
            //        enemyPlayer.pos += (toTarget / dist) * delta;
            //        //float angleToTarget = (float)Math.Atan2(-toTarget.X, -toTarget.Z);
            //        //enemyPlayer.euler.Y = angleToTarget;
            //        //kothAiForce += (toTarget / dist);
            //    }
            //    else
            //    {
            //        enemyPlayer.pos = targetPosition;
            //        kothPathProgress++;
            //    }
            //}

            //update koth offense
            //NetworkPlayer closestPlayer = null;
            //float closestPlayerDist = float.MaxValue;
            //Vector3 toClosestPlayer = Vector3.Zero;
            //foreach(var pair in networkplayerdata)
            //{
            //    if(pair.Value != enemyPlayer)
            //    {
            //        BoundingSphere target = targets[pair.Value.selfTarget];
            //        Vector3 toTarg = target.Center - enemyPlayer.pos;
            //        float dist = toTarg.Length();
            //        if(dist < closestPlayerDist)
            //        {
            //            closestPlayer = pair.Value;
            //            closestPlayerDist = dist;
            //            toClosestPlayer = toTarg / dist;
            //        }
            //    }
            //}
            //if(closestPlayer != null)
            //{
            //    game1.add3DLine(enemyPlayer.gun.pos, enemyPlayer.gun.pos + toClosestPlayer, Color.Red);
            //    //enemyPlayer.euler.Y = (float)Math.Atan2(-toClosestPlayer.X, -toClosestPlayer.Z);
            //    //enemyPlayer.euler.X = (float)Math.Atan2(toClosestPlayer.Y, -toClosestPlayer.Z);
            //    //enemyPlayer.gun.TriggerDown(new PhysicsState(1));
            //    enemyPlayer.gun.rot = Matrix.Invert(Matrix.CreateLookAt(Vector3.Zero, toClosestPlayer, Vector3.Up));
            //    enemyPlayer.input.shoot = true;
            //}

            // update scripted sequence
            //{
            //    Vector3 toPlayer = bodyState.pos - scriptedGun.pos;
            //    toPlayer.Y = 0;
            //    float dist = toPlayer.Length();
            //    float delta = 10 * et;
            //    switch (scriptedSequenceState)
            //    {
            //        case -1:
            //            if(dist < widthm * 2)
            //            {
            //                scriptedSequenceState++;
            //            }
            //                break;
            //        case 0: //move away from player until certain distance
            //            if (dist >= widthm * 7)
            //            {
            //                scriptedSequenceState++;
            //            }
            //            else
            //            {
            //                scriptedGun.pos -= (toPlayer/dist) * delta;
            //                scriptedGun.rot = Matrix.Invert(Matrix.CreateLookAt(
            //                    Vector3.Zero, toPlayer / dist, Vector3.Up));
            //            }
            //            break;
            //        case 1:
            //            if (scriptedSequenceTime < 0)
            //                scriptedSequenceTime = tt;
            //            Vector3 strafeDir = Vector3.Cross(toPlayer, Vector3.Up);
            //            strafeDir.Normalize();
            //            if (scriptedStrafeCount > 0)
            //                strafeDir = -strafeDir;
            //            scriptedGun.pos += strafeDir * delta;
            //            scriptedGun.rot = Matrix.Invert(Matrix.CreateLookAt(
            //                Vector3.Zero, toPlayer / dist, Vector3.Up));
            //            if(tt - scriptedSequenceTime > 0.25f)
            //            {
            //                scriptedSequenceTime = -1;
            //                scriptedStrafeCount = (scriptedStrafeCount + 1) % 2;
            //                scriptedStrafeShootLoopCount++;
            //                if(scriptedStrafeShootLoopCount == 3)
            //                {
            //                    scriptedSequenceState = 3;
            //                    scriptedStrafeShootLoopCount = 0;
            //                }else
            //                {
            //                    scriptedSequenceState++;
            //                }
            //            }
            //            break;
            //        case 2:
            //            if(scriptedGun.TriggerDown(new PhysicsState(1)))
            //            {
            //                scriptedShotsFired++;
            //            }
            //            if(scriptedShotsFired >= 2)
            //            {
            //                scriptedSequenceState = 1;
            //                scriptedShotsFired = 0;
            //            }
            //            break;
            //        case 3: //wait a bit
            //            if (scriptedSequenceTime < 0)
            //                scriptedSequenceTime = tt;
            //            if(tt - scriptedSequenceTime > 0.25f)
            //            {
            //                scriptedSequenceState = 4;
            //                scriptedSequenceTime = -1;
            //            }
            //            break;
            //        case 4:
            //            if (scriptedSequenceTime < 0)
            //            {
            //                scriptedSequenceTime = tt;
            //                scriptedGunRot0 = scriptedGun.rot;
            //            }
            //            float elapsed = (float)(tt - scriptedSequenceTime);
            //            float radians = elapsed * MathHelper.TwoPi;
            //            if(radians <= MathHelper.TwoPi)
            //            {
            //                scriptedGun.rot = Matrix.CreateRotationX(radians) * scriptedGunRot0;
            //            }
            //            else
            //            {
            //                scriptedGun.rot = scriptedGunRot0;
            //                scriptedSequenceTime = -1;
            //                scriptedSequenceState++;
            //            }
            //            game1.add3DLine(
            //                scriptedGun.pos, 
            //                scriptedGun.pos + Vector3.Transform(Vector3.Forward, scriptedGun.rot),
            //                Color.Red);
            //            break;
            //        default:
            //            scriptedSequenceState = -1;
            //            break;
            //    }
            //}

            //update travel

            //update video
            //if(vPlayer.State == MediaState.Stopped)
            //{
            //    vPlayer.Play(video);
            //}

            // update training
            //{
            //    Point startCoord = new Point(1, 3);
            //    Vector3 flyStartAir = coordToWorld(startCoord, 200);
            //    Vector3 flyStartGround = coordToWorld(startCoord, height / 2);
            //    bool inAir = false;
            //    if(game1.kclick(Keys.Home))
            //    {
            //        missionState.ChangeState(0);
            //        gaz.ChangeState(0);
            //        g36State.ChangeState(0);
            //    }
            //    switch (missionState.state)
            //    {
            //        case 0: //wait
            //            inAir = true;
            //            camera.pos = flyStartAir;
            //            myGuns.Clear();
            //            currentGun = null;
            //            if (game1.kclick(Keys.Enter))
            //            {
            //                missionState.Increment();
            //            }
            //            if(filename != "48.txt")
            //            {
            //                filename = "48.txt";
            //                loadlevel();
            //            }
            //            break;
            //        case 1: //fly down
            //            float flyInDuration = 1;
            //            inAir = true;
            //            camera.pos = Vector3.Lerp(flyStartAir, bodyState.pos, 
            //                Math.Min(1, missionState.getElapsed() / flyInDuration));
            //            if (missionState.getElapsed() >= flyInDuration)
            //            {
            //                missionState.Increment();
            //                camera.Euler.X = 0;
            //                inAir = false;
            //                playOutOfBody = false;
            //                gaz.Increment();
            //            }
            //            break;
            //        case 2: //
            //            break;
            //        default:
            //            missionState.ChangeState(0);
            //            break;

            //    }
            //    if(inAir && !edit.active)
            //    {
            //        playOutOfBody = true;
            //        camera.Euler.X = -MathHelper.PiOver2;
            //        camera.Euler.Y = -MathHelper.PiOver2;
            //        bodyState.pos = flyStartGround;
            //    }
            //}
            ////update gaz state
            //{
            //    int q = 0;
            //    switch(gaz.state)
            //    {
            //        case 0:
            //            //wait
            //            gazPosition = coordToWorld(new Point(2, 2), height / 2);
            //            break;
            //        case 1:
            //            dialogText = "Welcome to training Sopa.";
            //            gaz.Increment();
            //            break;
            //        case 2:
            //            if(moveInput.LengthSquared() > 0)
            //            {
            //                gaz.Increment();
            //                dialogText = "Pickup the G36c on the table ahead.";
            //            }
            //            break;
            //        case 3:
            //            float duration = 3;
            //            float f = gaz.getElapsed() / duration;
            //            Vector3 goal = coordToWorld(new Point(2, 15), height);
            //            gazPosition = Vector3.Lerp(gazPosition, goal, f);
            //            break;
            //    }
            //}
            ////update g36c state
            //{
            //    switch(g36State.state)
            //    {
            //        case 0:
            //            if (myGuns.Contains(g36c))
            //                myGuns.Remove(g36c);
            //            g36c.pos = coordToWorld(new Point(7, 5), height);
            //            Vector3 toPlayer = bodyState.pos - g36c.pos;
            //            float dist = toPlayer.Length();
            //            if(dist < widthm * 2)
            //            {
            //                dialogText = "*Press F to Pickup G36c*";
            //                if(game1.kclick(Keys.F))
            //                {
            //                    myGuns.Add(g36c);
            //                    g36State.Increment();
            //                    currentGun = g36c;
            //                }
            //            }
            //            break;
            //        case 1:
            //            break;
            //    }
            //}

            //update mechanisms
            //if (targets[gunMechTarget].Center.Y == targetRestY)
            //{
            //    gunMech.TriggerDown(new PhysicsState(1));
            //}
            ////update reload mechanism
            //if(reloadMechGun != null)
            //{
            //    Vector3 goal = reloadMech.Center;
            //    bool retrieving = false;
            //    Bullet expiredBullet = null;
            //    if (reloadMechBullet != null)
            //    {
            //        goal = reloadMechGun.pos + Vector3.Down * (reloadMechGun.size.Y + reloadMech.Radius);
            //        retrieving = false;
            //        reloadMechBullet.phy.pos = reloadMech.Center + Vector3.Up * reloadMech.Radius;
            //    }
            //    else if (expiredBullets.Count > 0)// && gunMech.getEmpty())
            //    {
            //        //for (int i = 0; i < reloadMechGun.bullets.Length; ++i)
            //        for (int i = 0; i < reloadMechGun.bullets.Count; ++i)
            //        {
            //            Bullet b = reloadMechGun.bullets[i];
            //            if (expiredBullets.Contains(b))
            //            {
            //                expiredBullet = b;
            //                break;
            //            }
            //        }
            //        if (expiredBullet != null)
            //        {
            //            goal = expiredBullet.phy.pos;
            //            retrieving = true;
            //        }
            //    }
            //    //goal.Y = reloadMech.Center.Y;

            //    Vector3 vector = goal - reloadMech.Center;
            //    //game1.add3DLine(reloadMech.Center, goal, Color.Red);
            //    float dist = vector.Length();
            //    bool arrived = false;
            //    float unitsPerSecond = 40;
            //    float delta = unitsPerSecond * et;
            //    if (true)//dist <= delta)
            //    {
            //        arrived = true;
            //        reloadMech.Center = goal;
            //    }
            //    else
            //    {
            //        //if it would take more than a second to get there then go a portion
            //        if (dist > unitsPerSecond)
            //        {
            //            reloadMech.Center += (vector / dist) * unitsPerSecond * 2 * (dist/unitsPerSecond) * et;
            //        }
            //        else
            //        {
            //            reloadMech.Center += (vector / dist) * delta;
            //        }
            //    }

            //    if (arrived)
            //    {
            //        if (retrieving)
            //        {
            //            reloadMechBullet = expiredBullet;
            //        }
            //        else if (reloadMechBullet != null)
            //        {
            //            if(reloadMechBullet.off)
            //                reloadMechBullet = null;
            //        }
            //    }
            //}

            //if(game1.IsActive && game1.kdown(Keys.F) && bullets[0].off)
            //{
            //    cooktime += et;
            //}

            //update bullets
            bulletTrailColor = monochrome(1.0f);
            if (game1.kclick(Keys.H))
            {
                pauseBullets = !pauseBullets;
            }
            if (!pauseBullets)
            {
                for (int b = 0; b < allbullets.Count; ++b)
                {
                    Bullet bul = allbullets[b];
                    if (!bul.off && bul.affectedByGravity)
                        bul.phy.force += gravity * bul.phy.mass;
                    Vector3 previousPos = bul.phy.pos;
                    if (!bul.skipNextAdvance)
                    {
                        //bul.phy.Advance(et - bul.advance);
                        bul.phy.Advance(et);
                        //if (!bul.off)
                        //{
                        //    Vector3 right = Vector3.Cross(bul.direction, Vector3.Up);
                        //    Vector3 ford = Vector3.Cross(right, bul.direction);
                        //    float speed = bul.phy.vel.Length();
                        //    float radius = .1f;
                        //    float rate = 20;
                        //    if (bul.size < .15f)
                        //    {
                        //        radius = 10;
                        //        rate = 10;
                        //    }
                        //    float f = (float)Math.Sin(MathHelper.PiOver2 + bul.t * rate) * radius;
                        //    float g = (float)Math.Cos(MathHelper.PiOver2 + bul.t * rate) * radius;
                        //    bul.phy.vel = Vector3.Normalize(bul.direction + right * f + ford * g) * speed;
                        //}
                    }
                    bul.skipNextAdvance = false;
                    //game1.add3DLine(bul.phy.pos, bul.phy.pos + bul.phy.vel, Color.Black, Color.White);
                    if (!bul.off)
                    {
                        float previousT = bul.t;
                        bul.t += et;
                        //expire bullet
                        if (bul.t >= bul.lifeSpan)
                        {
                            //expire bullet, may benefit more from a flag than a list
                            //if (previousT < bul.lifeSpan)
                            //{
                            //    if (!expiredBullets.Contains(bul))
                            //        expiredBullets.Add(bul);
                            //}
                            bul.off = true;
                            bul.phy.vel = bul.phy.force = Vector3.Zero;
                            //bul.phy.force -= bul.phy.vel;
                        }
                    }
                }
            }

            //update explosion
            //for (int i = 0; i < explosionBullets.Length; ++i)
            //{
            //    //host is off and is not about to be fired
            //    if (explosionHostBullet == null || 
            //        (explosionHostBullet.off && explosionHostBullet != slowGun.bullets[slowGun.bulleti]))
            //    {
            //        for (int j = 0; j < slowGun.bullets.Length; ++j)
            //        {
            //            int test = j + slowGun.bulleti;
            //            if (test >= slowGun.bullets.Length)
            //            {
            //                test -= slowGun.bullets.Length;
            //            }
            //            if (slowGun.bullets[test].off)
            //            {
            //                explosionHostBullet = slowGun.bullets[test];
            //                break;
            //            }
            //        }
            //    }
            //    if (explosionHostBullet == null)
            //        break;
            //    Bullet a = explosionHostBullet;// slowGun.bullets[nextBullet];
            //    Bullet b = explosionBullets[i];
            //    Vector3 dir = explosionBulletLocations[i];
            //    if (a.t < 1 || a.off) //sleep
            //    {
            //        b.phy.pos = a.phy.pos + dir * Math.Max(0.05f, a.size - b.size);
            //        b.off = true;
            //        //b.direction = dir;
            //        b.phy.force = Vector3.Zero;
            //        b.phy.vel = Vector3.Zero;
            //    }
            //    else if (a.t <= 1 + et && b.off) //trigger
            //    {
            //        b.phy.vel = dir * 50;
            //        b.off = false;
            //    }
            //    else
            //    {
            //        b.phy.force -= b.phy.vel * 10; //falloff
            //        b.off = false;
            //    }
            //    //Bullet bomb = bombBullets[i];
            //    //if (bombt == -1)
            //    //{
            //    //    bomb.phy.pos = targets[0].Center + dir * Math.Max(0.05f, targets[0].Radius - bomb.size);
            //    //    bomb.off = true;
            //    //    bomb.phy.vel = Vector3.Zero;
            //    //    bomb.phy.force = Vector3.Zero;
            //    //}
            //    //else
            //    //{
            //    //    if (bombt == 0 && bomb.off)
            //    //    {
            //    //        bomb.phy.vel = dir * 50;
            //    //        bomb.off = false;
            //    //    }
            //    //    if (bombt > 0)
            //    //    {
            //    //        float term = 2;
            //    //        if (bomb.phy.vel.LengthSquared() > term * term)
            //    //            bomb.phy.force -= bomb.phy.vel * 10; //falloff
            //    //        bomb.off = false;
            //    //    }
            //    //    if (bombt > 4)
            //    //    {
            //    //        bombt = -1;
            //    //    }
            //    //    //else
            //    //    //{
            //    //    //}
            //    //}
            //}
            //if (bombt >= 0)
            //    bombt += et;

            //BoundingSphere tanksv = new BoundingSphere(tankps.pos, tankr);
            //BoundingBox tankbb = Game1.MakeBox(tankps.pos, new Vector3(tankr*2));
            //Rectangle tankz = getzone(tankbb);
            //for (int x = tclamp(tankz.Left); x <= tclamp(tankz.Right); ++x)
            //{
            //    for (int z = tclamp(tankz.Top); z <= tclamp(tankz.Bottom); ++z)
            //    {
            //        foreach (Box box in terrain[x, z])
            //        {
            //            BoundingBox bv = Game1.MakeBox(box.position, box.size);
            //            Vector3 rd = Vector3.Zero;
            //            float rp = 0;
            //            if (intersectSphereBox(tanksv, bv, out rd, out rp))
            //            {
            //                resolvePenetration(ref tankps, -rd, rp, 0, 0.03f);
            //            }
            //        }
            //    }
            //}
            //tankps.force += Vector3.Down * 10;
            //Matrix tankrot =
            //    Matrix.CreateRotationY(tankeu.Y) *
            //    Matrix.CreateRotationX(tankeu.X)//MathHelper.PiOver4/2) *
            //    ;
            //tankgun.pos = tankps.pos + Vector3.Transform(Vector3.Forward, 
            //    tankrot
            //    //Matrix.CreateFromYawPitchRoll(tankeu.Y, tankeu.X,tankeu.Z)
            //    ) * tankr;
            //tankgun.rot = tankrot;// Matrix.CreateRotationY(tankeu.Y);
            //tankps.Advance(et);
            float heightPercentage = (float)bodyc / (float)sphereBodyCount;
            float totalHeight = height * heightPercentage;
            Vector3 top = bodyState.pos + (totalHeight / 2) * Vector3.Up;
            //Vector3 top = bodyState.pos + (height / 2) * Vector3.Up;
            //gun2.pos = new Vector3(20, 1, 20);
            //gun2.rot = Matrix.CreateRotationY(MathHelper.Pi);
            //bool touchingTank = false;

            //update player collision
            bool playerMakingContact = false;
            Vector3 randomPlayerBoxContact = Vector3.Zero;
            int bodyshots = 0;
            for (int i = 0; i < bodyc; ++i)
            {
                BoundingSphere sphereVolume = new BoundingSphere(top + drop * i + drop / 2, dropValue / 2);
                //update player hit obb
                //{
                //    Vector3 cp = closestPointSphereOBB(sphereVolume, obbPos, obbSize, obbRot);
                //    Vector3 toCP = cp - sphereVolume.Center;
                //    game1.add3DLine(sphereVolume.Center, cp, Color.Red);
                //    game1.add3DLine(sphereVolume.Center, 
                //        getClosestPoint(sphereVolume.Center, Game1.MakeBox(obbPos,obbSize)) + Vector3.Right * 0.01f, Color.Green);
                //    if (toCP.Length() <= sphereVolume.Radius)
                //    {
                //        float pen = sphereVolume.Radius - toCP.Length();
                //        Vector3 toCPNorm = Vector3.Normalize(toCP);
                //        bodyState.pos -= Vector3.Normalize(toCP) * pen;
                //        float relSpeed = Vector3.Dot(toCPNorm, bodyState.vel);
                //        if (relSpeed > 0)
                //        {
                //            bodyState.vel -= relSpeed * toCPNorm;
                //            bodyState.vel *= 0.03f;
                //                }
                //    }
                //}
                //if (sphereVolume.Intersects(tanksv))
                //{
                //    touchingTank = true;
                //}
                BoundingBox bb = GameMG.MakeBox(sphereVolume.Center, new Vector3(sphereVolume.Radius * 2));
                if (terrainActive)
                {
                    Rectangle zone = getzone(bb);
                    List<Box> boxes = getBoxesInZone(zone);
                    //List<ContactData> allContacts = new List<ContactData>();
                    foreach (Box box in boxes)
                    {
                        //if (box == boxBullet && (bulletBox == null || bulletBox.off))
                        //    continue;
                        BoundingBox bv = GameMG.MakeBox(box.position, box.size);
                        Vector3 resolveDir = Vector3.Zero;
                        float resolvepen = 0;
                        // update player hit box
                        if (intersectSphereBox(sphereVolume, bv, out resolveDir, out resolvepen))
                        {
                            //Vector3 closestPoint = sphereVolume.Center;
                            //closestPoint = Vector3.Max(closestPoint, boxVolume.Min);
                            //closestPoint = Vector3.Min(closestPoint, boxVolume.Max);
                            //Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
                            //float dist = toClosestPoint.Length();
                            //float penetration = sphereVolume.Radius - dist;
                            float penetration = resolvepen;
                            //if (penetration >= 0)
                            //{
                            //Vector3 sphereSurfaceNormal = (toClosestPoint / dist);
                            Vector3 sphereSurfaceNormal = -resolveDir;
                            //float slop = 0.1f;
                            //if (penetration > slop)
                            //{
                            //    bodyState.pos -= sphereSurfaceNormal * (penetration - slop);
                            //}
                            playerMakingContact = true;
                            float speedTowardsBox = Vector3.Dot(sphereSurfaceNormal, bodyState.vel);
                            Vector3 tangent = bodyState.vel - speedTowardsBox * sphereSurfaceNormal;
                            float restitution = 0;// box.restitution;
                            float friction = playerFriction;
                            if (movementForce.LengthSquared() == 0)
                                friction = playerFrictionStopped;
                            if (playerRequestFrictionOverride)
                                friction = playerFrictionOverride;
                            //bool doesPositionResponse = true;
                            //    box.GetMaterialProperties(out friction, out restitution, out doesPositionResponse);
                            if (speedTowardsBox > 0)
                            {
                                randomPlayerBoxContact = sphereVolume.Center + sphereSurfaceNormal * (sphereVolume.Radius - penetration);
                                //if (box.embedsBulletOnImpact)
                                //    friction = 0.3f;
                                bodyState.vel -= (1 + restitution) * sphereSurfaceNormal * speedTowardsBox;
                                if (restitution == 0)
                                {
                                    //bodyState.force -= gravity;
                                }
                                //friction *= playerFriction;
                                bodyState.vel -= tangent * friction;
                            }
                            //if (doesPositionResponse)
                            //{
                            bodyState.pos -= sphereSurfaceNormal * (penetration);
                            //}
                            //allContacts.Add(new ContactData()
                            //{
                            //    norm = resolveDir,
                            //    contact = sphereVolume.Center + sphereSurfaceNormal *
                            //        (sphereVolume.Radius - penetration),
                            //    pen = penetration,
                            //    //restitution = box.restitution
                            //});

                            //Vector3 bot = bodyState.pos + Vector3.Down * (height / 2);
                            //if (i >= bodyc - 3)
                            //{
                            //    Vector3 contact = sphereVolume.Center + sphereSurfaceNormal * (sphereVolume.Radius - penetration);
                            //    Vector3 toBot = bot - contact;
                            //    //float step = Vector3.Dot(sphereSurfaceNormal * penetration, Vector3.Down);
                            //    float step = toBot.Length();
                            //    sphereSurfaceNormal = Vector3.Down;
                            //    penetration = step;
                            //}
                            //bool skipResolve = false;
                            //float stepHeight = 2;// sphereVolume.Radius * 2;
                            //if (i >= bodyc - 2) //bottom
                            //{
                            //    Vector3 contact = sphereVolume.Center +
                            //        sphereSurfaceNormal * (sphereVolume.Radius - penetration);
                            //    Vector3 planarDir = sphereSurfaceNormal;
                            //    planarDir.Y = 0;
                            //    if (planarDir.LengthSquared() > 0)
                            //    {
                            //        planarDir.Normalize();
                            //        Vector3 sampleLoc = contact + planarDir * 0.1f;
                            //        sampleLoc.Y = top.Y;
                            //        Ray stepRay = new Ray(sampleLoc, Vector3.Down);
                            //        float? stepHit = stepRay.Intersects(bv);
                            //        if (stepHit.HasValue && stepHit.Value > 0)
                            //        {
                            //            Vector3 stepContact = stepRay.Position + stepRay.Direction * stepHit.Value;
                            //            if (stepContact.Y < sphereVolume.Center.Y + (stepHeight - sphereVolume.Radius))
                            //            {
                            //                float stepDelta = stepContact.Y - contact.Y;
                            //                bodyState.pos.Y += stepDelta;
                            //                bodyState.vel.Y = (float)Math.Max(0, bodyState.vel.Y);
                            //                skipResolve = true;
                            //            }
                            //        }
                            //    }
                            //}
                            //if (!skipResolve)
                            //{
                            //if(Vector3.Dot(sphereSurfaceNormal, Vector3.Down) > 0.98f)
                            //{
                            //    jumpElapsed = 0;
                            //}
                            //}
                            //}
                            //}
                        }
                    }
                    //if (allContacts.Count > 1)
                    //{
                    //    allContacts.Sort((ContactData a, ContactData b) =>
                    //    {
                    //        if (a.pen > b.pen) return -1;
                    //        if (a.pen < b.pen) return 1;
                    //        return 0;
                    //    });
                    //}
                    //if (allContacts.Count > 0)
                    //{
                    //    ContactData contact = allContacts.First();
                    //    //foreach (ContactData contact in allContacts)
                    //    //{
                    //    //    //float contribution = 1 / (float)allContacts.Count;
                    //    Vector3 sphereSurfaceNormal = -contact.norm;
                    //    float penetration = contact.pen;
                    //    if (sphereSurfaceNormal.Y < 0)
                    //    {
                    //        float abx = Math.Abs(sphereSurfaceNormal.X);
                    //        float abz = Math.Abs(sphereSurfaceNormal.Z);
                    //        if (abx < 0.8f && abz < 0.8f)
                    //        {
                    //            if (abx > 0.2f || abz > 0.2f)
                    //            {
                    //                int nada = 0;
                    //            }
                    //        }
                    //    }
                    //bodyState.pos -= sphereSurfaceNormal * penetration * contribution;
                    //bodyState.pos -= sphereSurfaceNormal * penetration;
                    //float speedTowardsBox = Vector3.Dot(sphereSurfaceNormal, bodyState.vel);
                    //Vector3 tangent = bodyState.vel - speedTowardsBox * sphereSurfaceNormal;
                    //if (speedTowardsBox > 0)
                    //{
                    //    float restitution = 0;// contact.restitution;
                    //    float friction = 0.03f;
                    //    bodyState.vel -= (1 + restitution) * sphereSurfaceNormal * speedTowardsBox;
                    //    if (restitution == 0)
                    //    {
                    //        bodyState.force -= gravity;
                    //    }
                    //    bodyState.vel -= tangent * friction;
                    //}
                    //    if(box != mostRecentBoxHit)
                    //    {
                    //        mostRecentBoxHit = box;
                    //        boxCHI++;
                    //        if(boxCHI >= boxCH.Length)
                    //        {
                    //            boxCHI = 0;
                    //        }
                    //        boxCH[boxCHI] = new ContactData()
                    //        {
                    //            norm = resolveDir,
                    //            contact = sphereVolume.Center + 
                    //                sphereSurfaceNormal * 
                    //                (sphereVolume.Radius - penetration),
                    //            pen = penetration
                    //        };
                    //    }
                    //}
                }
                //update player pickup gun, player hit gun
                if (game1.kclick(Keys.X) && myGuns.Count < myGunLimit)
                {
                    for (int j = 0; j < allguns.Count; ++j)
                    {
                        Gun gun = allguns[j];
                        if (myGuns.Contains(gun)) continue;
                        if (sphereIntersectsOBB(sphereVolume, gun.pos + Vector3.Transform(Vector3.Down * gun.size.Y / 2, gun.rot),
                            gun.size, gun.rot))
                        {
                            //if (myGuns.Count == myGunLimit)
                            //{
                            //int g = guni;
                            //if (g > myGuns.Count - 1)
                            //    g = 0;
                            //myGuns[g].pos = gun.pos;
                            //myGuns.RemoveAt(g);
                            //DropGun(g);
                            //}
                            PickupGun(gun);
                            currentGun = gun;
                            break;
                        }
                    }
                }
                //update player hit bullet, bullet hit player
                for (int j = 0; j < allbullets.Count; ++j)
                {
                    Bullet b = allbullets[j];
                    if (allbullets[j].off)// || expiredBullets.Contains(b))
                        continue;
                    Vector3 bodyNormal;
                    float pen;
                    if (b.isSolid(et))
                    {
                        BoundingSphere bulletSphere = new BoundingSphere(allbullets[j].phy.pos, allbullets[j].size / 2);
                        if (intersectSphereSphere(sphereVolume, bulletSphere, out bodyNormal, out pen))
                        {
                            //bodyState.pos -= bodyNormal * new Vector3(1,0,1) * pen;
                            allbullets[j].phy.vel = Vector3.Reflect(allbullets[j].phy.vel, bodyNormal);
                            bodyshots++;
                        }
                    }
                    if (b.isRay(et))
                    {
                        Vector3 pv = b.phy.vel * et;
                        float pvl = pv.Length();
                        Ray ray = new Ray(b.phy.pos, pv / pvl);
                        float? hit = ray.Intersects(sphereVolume);
                        if (hit.HasValue && hit.Value <= pvl)
                        {
                            Vector3 contact = hit.Value * ray.Direction;
                            bodyNormal = Vector3.Normalize(contact - sphereVolume.Center);
                            b.phy.pos = contact;
                            b.phy.vel = Vector3.Reflect(allbullets[j].phy.vel, bodyNormal);
                            bodyshots++;
                        }
                    }
                }
                ////hack to add destroyable barrier for siege
                //for (int j = breachTargets.Length - 1; j >= 0; j--)
                //{
                //    BoundingSphere targ = targets[breachTargets[j]];
                //    Vector3 toTarg = targ.Center - sphereVolume.Center;
                //    float len = toTarg.Length();
                //    float radSum = targ.Radius + sphereVolume.Radius;
                //    if (len < radSum)
                //    {
                //        float pen = radSum - len;
                //        Vector3 dir = toTarg / len;
                //        float relvel = Vector3.Dot(bodyState.vel, dir);
                //        if (relvel > 0)
                //        {
                //            bodyState.vel -= relvel * dir;
                //        }
                //        bodyState.pos -= dir * pen;
                //        break;
                //    }
                //}

                ////DESIGN: Hard to stand on sphere as a sphere body. No clear effect, fun or not.
                ////update player hit target, player hit sphere
                //for (int j = 0; j < targets.Length; ++j)
                //{
                //    BoundingSphere target = targets[j];
                //    Vector3 toTarget = target.Center - sphereVolume.Center;
                //    float dist = toTarget.Length();
                //    float radiusSum = target.Radius + sphereVolume.Radius;
                //    //player touch target
                //    if (dist <= radiusSum && dist != 0)
                //    {
                //        Vector3 normToTarg = toTarget / dist;
                //        float relativeVelocity = Vector3.Dot(bodyState.vel, normToTarg);
                //        if (relativeVelocity > 0)
                //        {
                //            float restitution = 0;
                //            bodyState.vel -= (1 + restitution) * normToTarg * relativeVelocity;
                //            float penetration = radiusSum - dist;
                //            bodyState.pos -= normToTarg * penetration;
                //        }
                //    }
                //}
            }
            bodyc -= bodyshots;
            if (bodyc < 1) bodyc = 1;
            // update player on collide
            //if (!wasInContact && playerMakingContact)
            //if (playerMakingContact)
            //{
            //    audioEmitter.Position = randomPlayerBoxContact;
            //    SoundEffectInstance instance = click.CreateInstance();
            //    instance.Apply3D(audioListener, audioEmitter);
            //    instance.Play();
            //}
            //wasInContact = playerMakingContact;
            //if(touchingTank)
            //{
            //    if (canEnterTank)
            //    {
            //        intank = true;
            //        canEnterTank = false;
            //    }
            //}else
            //{
            //    canEnterTank = true;
            //}
            //Vector3[] resolveDirs = new Vector3[allbullets.Count];
            //float[] resolvePens = new float[allbullets.Count];
            //update bullet collisions
            for (int i = 0; i < allbullets.Count; ++i)
            {
                if(pauseBullets)
                    break;
                Bullet bul = allbullets[i];
                Vector3 pv = allbullets[i].phy.vel * et; //projected velocity
                BoundingSphere sv = new BoundingSphere(allbullets[i].phy.pos, bul.size / 2);
                if (allbullets[i].off)
                {
                    //bullet hit gun, gun hit bullet
                    //for (int j = 0; j < allguns.Count; ++j)
                    //{
                    //    Gun g = allguns[j];
                    //    if (g.off) continue;
                    //    Vector3 b2g = g.pos - bul.phy.pos; //bullet to gun vector
                    //    b2g.Normalize();
                    //    float relVel = Vector3.Dot(bul.phy.vel, b2g); //make sure bullet is moving towards the gun
                    //    bool hit = false;
                    //    if (bul.isSolid(et))
                    //    {
                    //        hit = sphereIntersectsGun(sv, g);
                    //    }
                    //    if (bul.isRay(et))
                    //    {
                    //        float pvd = pv.Length();
                    //        Vector3 dir = pv / pvd;
                    //        float? rayhit = IntersectRayGun(new Ray(bul.phy.pos, dir), g);
                    //        if (rayhit.HasValue && rayhit.Value < pvd)
                    //        {
                    //            hit = true;
                    //        }
                    //    }
                    //    if (bul.off && hit && !g.bullets.Contains(bul))
                    //    {
                    //        g.bullets.Add(bul);
                    //    }
                    //}
                    continue;
                }

                //for (int j = i + 1; j < allbullets.Count; ++j)
                //{
                //    Bullet bro = allbullets[j];
                //    if (bro.off) continue;
                //    Vector3 toBro = bro.phy.pos - bul.phy.pos;
                //    float radiusSum = bro.size / 2 + bul.size / 2;
                //    if (toBro.LengthSquared() <= radiusSum * radiusSum)
                //    {
                //        float dist = toBro.Length();
                //        Vector3 toBroNrm = toBro / dist;
                //        //float speedBro = Vector3.Dot(-toBroNrm, bro.phy.vel);
                //        //float speedBul = Vector3.Dot(toBroNrm, bul.phy.vel);
                //        //float totalRSpeed = speedBro + speedBul;
                //        //float totalMass = bro.phy.mass + bul.phy.mass;
                //        //float restitutionF = (1+1);
                //        //bro.phy.vel += restitutionF * toBroNrm * totalRSpeed * (bul.phy.mass / totalMass);
                //        //bul.phy.vel -= restitutionF * toBroNrm * totalRSpeed * (bro.phy.mass / totalMass);
                //        bro.phy.force += toBroNrm * 10;
                //        bul.phy.force -= toBroNrm * 10;
                //    }
                //}
                //bul.inContactOld = bul.inContact;
                //bul.inContact = false;
                //resolveDir = Vector3.Zero;
                // BoundingSphere sv = new BoundingSphere(allbullets[i].phy.pos, bul.size / 2);
                bool doSphere = !bul.isTooFast(et);
                BoundingBox bb = GameMG.MakeBox(sv.Center, new Vector3(sv.Radius * 2));
                //Vector3 pv = allbullets[i].phy.vel * et; //projected velocity
                Vector3 start = allbullets[i].phy.pos; //starting point of bullet
                Vector3 end = start + pv; //ending point of bullet
                float minhit = float.MaxValue;
                Vector3 newvel = Vector3.Zero;
                Vector3 newpos = Vector3.Zero;
                Rectangle region = getzone(Vector3.Min(start, end), Vector3.Max(start, end));
                //Vector3 obbPosition = Vector3.Zero;
                //Vector3 obbSize = Vector3.Zero;
                //Matrix obbRotation = new Matrix();
                if (doSphere)
                {
                    region = getzone(bb);
                }
                //else
                //{
                //    Vector3 vel = bul.phy.vel;
                //    Vector3 A = bul.phy.pos;
                //    Vector3 B = A + vel / updatefps;
                //    //volume of sphere = (4/3) * pi * r^3
                //    float volume =
                //        (4.0f / 3.0f) *
                //        MathHelper.Pi *
                //        (float)Math.Pow(bul.size / 2, 3);
                //    //float s = bul.size;
                //    Vector3 BA = B - A;
                //    float l = (B - A).Length();
                //    //volume of cube = w * h * d
                //    //volume / d = w * h
                //    //h = w
                //    //volume / d = w ^ 2
                //    //w = sqrt(volume / d)
                //    float s = (float)Math.Sqrt(volume / l);
                //    if (bul.skipNextAdvance)
                //        l = 0.01f;
                //    Vector3 up = Vector3.Up;
                //    Vector3 dir = B - A;
                //    dir.Normalize();
                //    if (Vector3.Dot(dir, up) > 0.98f)
                //        up = Vector3.Right;
                //    Matrix look = Matrix.CreateLookAt(A, B, up);
                //    obbPosition = A + (BA / 2);
                //    obbSize = new Vector3(s, s, l);
                //    obbRotation = Matrix.Invert(look);
                //}
                List<Box> boxes = getBoxesInZone(region);
                Vector3 resolveDir = Vector3.Zero;
                float resolvepen = 0;
                ContactData minContactData = null;
                Box minBoxHit = null;
                //update bullet hit box
                foreach (Box box in boxes)
                {
                    //if (box == boxBullet)
                    //    continue;
                    //if (box.ignoresBullets)
                    //    continue;
                    BoundingBox bv = box.boundingBox;// Game1.MakeBox(box.position, box.size);
                    if (box.id == 172)
                    {
                        int nada = 0;
                    }
                    if (doSphere)
                    {
                        if (intersectSphereBox(sv, bv, out resolveDir, out resolvepen))
                        {
                            //if (resolvepen > resolvePens[b])
                            //{
                            //resolveDirs[b] = resolveDir;
                            //resolvePens[b] = resolvepen;
                            ContactData hitData = new ContactData()
                            {
                                norm = resolveDir,
                                contact = sv.Center - (sv.Radius - resolvepen) * resolveDir,
                                pen = resolvepen
                            };
                            if (minContactData == null
                || Vector3.DistanceSquared(bul.phy.pos, hitData.contact) <
                    Vector3.DistanceSquared(bul.phy.pos, minContactData.contact))
                            {
                                minContactData = hitData;
                                minBoxHit = box;
                            }
                            //}
                        }
                    }
                    else
                    {
                        Vector3 dir = Vector3.Normalize(pv);
                        Ray ray = new Ray(start, dir);
                        float? hit = ray.Intersects(bv);
                        float delta = pv.Length();
                        Point t = new Point(12, 1);
                        //bullet ray hit box
                        if (hit.HasValue && hit.Value <= delta && hit.Value < minhit)
                        {
                            Vector3 contact = ray.Position + ray.Direction * hit.Value;
                            Vector3 normal;
                            float pen;
                            intersectBoxPoint(contact, bv, out normal, out pen);
                            if (normal.LengthSquared() > 0)
                            {
                                //if (shotIsValid)
                                //{
                                normal.Normalize();
                                minhit = hit.Value;
                                //Vector3 refl = Vector3.Reflect(allbullets[b].direction, normal);
                                Vector3 refl = Vector3.Reflect(Vector3.Normalize(allbullets[i].phy.vel), normal);
                                newvel = refl * allbullets[i].phy.vel.Length();
                                newpos = contact;
                                ContactData hitData = new ContactData()
                                {
                                    norm = normal,
                                    contact = contact,
                                    pen = delta - hit.Value
                                };
                                if (minContactData == null
                    || Vector3.DistanceSquared(bul.phy.pos, hitData.contact) <
                        Vector3.DistanceSquared(bul.phy.pos, minContactData.contact))
                                {
                                    minContactData = hitData;
                                    minBoxHit = box;
                                }
                                //}
                            }
                            else
                            {
                                int nada = 0;
                            }
                        }
                    }
                }
                //if (minhit < float.MaxValue)
                //{
                //    allbullets[b].phy.pos = newpos;
                //    //allbullets[b].phy.vel = newvel;
                //    allbullets[b].phy.vel = Vector3.Zero;
                //    //allbullets[b].off = true;
                //    //allbullets[b].direction = Vector3.Normalize(newvel);
                //    allbullets[b].skipNextAdvance = true;
                //    //bul.t = 1;//EXPLODE
                //}
                //if (doSphere)
                //{
                //    resolveDirs[b] += resolveDir;
                //}

                //bullet hit bullet
                //for(int j = i + 1; j < allbullets.Count; ++j)
                //{
                //    Bullet b2 = allbullets[j];
                //    if (b2.off || bul.off) continue;
                //    BoundingSphere b2s = new BoundingSphere(b2.phy.pos, b2.size / 2);
                //    ContactData contact = new ContactData();
                //    bool hit = intersectSphereSphere(sv, b2s, out contact.norm, out contact.pen);
                //    float length = pv.Length();
                //    float? rhit = new Ray(sv.Center, pv/length).Intersects(b2s);
                //    if(rhit.HasValue && rhit.Value <= length)
                //    {
                //        hit = true;
                //    }
                //    if (hit)
                //    {
                //        bul.off = true;
                //        b2.off = true;
                //        break;
                //        //contact.contact = sv.Center - contact.norm * (sv.Radius - contact.pen);
                //        //if(contact.)
                //    }
                //}

                //bullet hit gun, gun hit bullet
                for (int j = 0; j < allguns.Count; ++j)
                {
                    Gun g = allguns[j];
                    if (g.off) continue;
                    Vector3 b2g = g.pos - bul.phy.pos; //bullet to gun vector
                    b2g.Normalize();
                    float relVel = Vector3.Dot(bul.phy.vel, b2g); //make sure bullet is moving towards the gun
                    bool hit = false;
                    if (bul.isSolid(et))
                    {
                        hit = sphereIntersectsGun(sv, g);
                    }
                    if (bul.isRay(et))
                    {
                        float pvd = pv.Length();
                        Vector3 dir = pv / pvd;
                        float? rayhit = IntersectRayGun(new Ray(bul.phy.pos, dir), g);
                        if (rayhit.HasValue && rayhit.Value < pvd)
                        {
                            hit = true;
                        }
                    }
                    if (!bul.off && hit && relVel >= 0)
                    {
                        //Pickup bullets with gun (scavenging)
                        //if (expiredBullets.Contains(bul))
                        //{
                        //    expiredBullets.Remove(bul);
                        //    bul.off = true;
                        //}
                        //else
                        //{
                        if (currentGun == retractorGun && retractorTarget == null)
                        {
                            retractorTarget = g;
                        }
                        else
                        {
                            g.Shutdown();
                            bul.phy.vel = Vector3.Zero;
                            bul.off = true;
                        }
                        //bul.affectedByGravity = true;
                        //bul.phy.vel = Vector3.Reflect(bul.phy.vel, -b2g);
                        //}
                    }
                }
                //}
                //for (int b = 0; b < allbullets.Count; ++b)
                //{
                Bullet blt = allbullets[i];
                //Vector3 resolveDir = -resolveDirs[b];
                //BoundingSphere sv = new BoundingSphere(blt.phy.pos, blt.size);
                //resolveDir = -resolveDirs[b];
                int minTarget = -1;
                //if (resolveDir.LengthSquared() > 0)
                //{
                //    resolveDir.Normalize();
                //    ContactData hitData = new ContactData()
                //    {
                //        norm = -resolveDir,
                //        contact = sv.Center -resolveDir * (sv.Radius - resolvePens[b]),
                //        pen = resolvePens[b]
                //    };
                //    if(minContactData == null
                //            || Vector3.DistanceSquared(blt.phy.pos, hitData.contact) <
                //                Vector3.DistanceSquared(blt.phy.pos, minContactData.contact))
                //    {
                //        minContactData = hitData;
                //        minTarget = -1;
                //    }
                //    //float speedTowardsBox = Vector3.Dot(resolveDir, blt.phy.vel);
                //    //if (speedTowardsBox > 0)
                //    //{
                //    //    blt.phy.pos -= resolveDir * resolvePens[b];
                //    //    float restitution = 1;
                //    //    blt.phy.vel -= (1 + restitution) * resolveDir * speedTowardsBox;
                //    //    //blt.direction = Vector3.Normalize(blt.phy.vel);
                //    //    //blt.t = 1;//EXPLODE
                //    //}
                //}
                //bool bomb = bombBullets.Contains(blt);
                //update bullet hit targets
                for (int t = 0; t < targets.Length; ++t)
                {
                    if (targets[t].off) continue;
                    //if (t == 0 && bomb)
                    //    continue;
                    //Target targ = targets[t];
                    //Func<Target, bool>hittarget = (Target testsv)=> {
                    Func<BoundingSphere, ContactData> hittarget = (BoundingSphere testsv) =>
                    {
                        //    BoundingSphere targsv = new BoundingSphere(testsv.Center, testsv.Radius);
                        //BoundingBox targbv = Game1.MakeBox(testsv.Center, new Vector3(testsv.Radius*2));
                        if (blt.isRay(et))
                        {
                            Vector3 pvel = blt.phy.vel * et; //projected velocity
                            float delta = pvel.Length(); //projected position delta
                            Ray ray = new Ray(blt.phy.pos, pvel / delta); //bullet ray
                                                                          //if (!targ.isCube)
                                                                          //{
                            float? hit = ray.Intersects(testsv); //ray sphere hit result
                                                                 //float? hit = ray.Intersects(targsv); //ray sphere hit result
                            if (delta > 0 && hit.HasValue && hit.Value >= 0 && hit.Value <= delta)
                            {
                                Vector3 contact = ray.Position + ray.Direction * hit.Value;
                                Vector3 targNormal = contact - testsv.Center;
                                //NOTE: allows you to fire a gun inside a target while not
                                //allowing bullets to collide with targets twice. Does not
                                //detect a gun firing inside a target past the targets center.
                                bool shotIsValid = hit.Value != 0 ||
                                Vector3.Dot(targNormal, pvel) < 0;
                                if (targNormal.LengthSquared() > 0 && shotIsValid)
                                {
                                    targNormal.Normalize();
                                    //if(min)
                                    //blt.phy.vel = targNormal * (delta / et);
                                    //blt.direction = toc;
                                    //blt.phy.pos = contact;
                                    //float portion = hit.Value / delta;
                                    //blt.skipNextAdvance = true;
                                    //blt.phy.vel = Vector3.Normalize(toS) * blt.phy.vel.Length();
                                    return new ContactData()
                                    {
                                        norm = targNormal,
                                        contact = contact,
                                        pen = delta - hit.Value
                                    };
                                    //blt.t = 1;//explode
                                }
                            }
                            //}
                            //else
                            //{
                            //    float? hit = ray.Intersects(targbv);
                            //    if (delta > 0 && hit.HasValue && hit.Value > 0 && hit.Value <= delta)
                            //    {
                            //        Vector3 contact = ray.Position + ray.Direction * hit.Value;
                            //        Vector3 norm = Vector3.Zero;//box normal
                            //        float pen = 0; //ray penetration
                            //        pointToBoxNormal(contact, targbv, out norm, out pen);
                            //        blt.phy.vel = norm * (delta / et);
                            //        blt.phy.pos = contact;
                            //        blt.skipNextAdvance = true;
                            //        return true;
                            //    }
                            //}
                            //else
                            //{
                            //    if( t%2==0 && toS.LengthSquared() < 9)
                            //    {
                            //        Vector3 move = toS;
                            //        move.Y = 0;
                            //        move.Normalize();
                            //        targets[t].Center -= move * 0.09f;
                            //    }
                            //}
                        }
                        if (blt.isSolid(et))
                        {
                            Vector3 toS = sv.Center - testsv.Center;
                            if (sv.Intersects(testsv))
                            //bool hitsphere = !targ.isCube && sv.Intersects(targsv);
                            //bool hitcube = targ.isCube && sv.Intersects(targbv);
                            //if(hitsphere)
                            {
                                float len = toS.Length();
                                Vector3 targNorm = toS / len;
                                //blt.phy.vel = Vector3.Normalize(toS) * blt.phy.vel.Length();
                                //blt.direction = toS;
                                //targets[t].Center.Y = 0;
                                //blt.t = 1;//explode
                                return new ContactData()
                                {
                                    norm = targNorm,
                                    contact = testsv.Center + testsv.Radius * targNorm,
                                    pen = (sv.Radius + testsv.Radius) - len
                                };
                            }
                            //if(hitcube)
                            //{
                            //    Vector3 norm = Vector3.Zero;//box normal
                            //    float pen = 0; //ray penetration
                            //    Vector3 closestPoint = getClosestPoint(sv, targbv);
                            //    pointToBoxNormal(closestPoint, targbv, out norm, out pen);
                            //    blt.phy.vel = norm * blt.phy.vel.Length();
                            //    return true;
                            //}
                        }
                        return null;
                    };

                    //bool hittarg = false;
                    //float hity = 0;
                    //ContactData hitData = hittarget(targets[t]);
                    ContactData hitData = hittarget(new BoundingSphere(targets[t].Center, targets[t].Radius));
                    if (hitData != null
                        && (minContactData == null
                            || Vector3.DistanceSquared(blt.phy.pos, hitData.contact) <
                                Vector3.DistanceSquared(blt.phy.pos, minContactData.contact)))
                    //if (hittarget(targ))
                    {
                        minTarget = t;
                        minBoxHit = null;
                        minContactData = hitData;
                        //hittarg = true;
                    }
                    //if(hittarget(new BoundingSphere(targets[t].Center + Vector3.Down * target2y, target2r)))
                    //{
                    //    hittarg = true;
                    //    hity = target2HitY;
                    //}
                    //targetSpawns[t] = spawns[game1.rand.Next(spawns.Count)];
                    //if(t == 0 && bombt == -1)
                    //{
                    //    bombt = 0;
                    //}
                }
                // update bullet hit something
                float relSpeed = 0;
                if (minContactData != null)
                {
                    relSpeed = Vector3.Dot(minContactData.norm, blt.phy.vel);
                    //bul.inContact = true;
                }

                //update peon

                //if (minContactData != null)
                if (relSpeed < 0)
                {
                    bool wantReturn = false;
                    if (minTarget > -1) //update bullet hit target hit
                    {
                        //OnTargetHit(bul, targets[minTarget], minContactData);
                        float time = 0.1f;
                        var noise = new SignalGenerator()
                        {
                            Type = SignalGeneratorType.Sweep,
                            Frequency = 430,
                            FrequencyEnd = 550,
                            SweepLengthSecs = time
                        };
                        AddMixerInput(noise.ToMono().Take(TimeSpan.FromSeconds(time)), "bullet hitting target", tt + 3);
                        //targets[minTarget].phy.vel = -minContactData.norm;
                        float hity = 0;// targetRestY;
                        float restY = targetRestY;
                        float landY = targetHitY;
                        if (targets[minTarget].Center.Y == targetHitY)
                            hity = targetRestY;
                        else if (targets[minTarget].Center.Y == targetRestY)
                            hity = targetHitY;
                        targets[minTarget].Center.Y = hity;

                        //update capture target
                        //if (!capturedTargets.Contains(minTarget))
                        //{
                        //    capturedTargets.Add(minTarget);
                        //}
                        if (currentGun == retractorGun)
                        {
                            retractorTarget = targets[minTarget];
                        }
                        else if (minTarget == 0)
                        {
                            wantReturn = true;
                        }
                        else
                        {
                            //targets[minTarget].off = true;
                            targets[minTarget].Center.Y = 0;
                        }

                        //if(minTarget == playerSelfTarget)
                        //{
                        //    bodyState.pos = unitSpawn(true, -1);
                        //    bodyState.vel = Vector3.Zero;
                        //    bodyState.force = Vector3.Zero;
                        //}

                        //update koth respawn king of the hill
                        //Vector3 spawn = spawnA;
                        //if (game1.rand.Next(2) == 0)
                        //    spawn = spawnB;
                        //foreach(var pair in networkplayerdata)
                        //{
                        //    if(pair.Value.selfTarget == minTarget)
                        //    {
                        //        pair.Value.pos = spawn;
                        //    }
                        //}

                        //if (//minTarget == targets.Length - 1)
                        //    zombieVTargs.Contains(minTarget))
                        //{
                        //zombies[0].pos = spawnA;
                        //zombies[0].pos = spawn;
                        //difficultyLevel++;
                        //applyDifficulty();

                        //    //hack for ground war
                        //    for (int j = 0; j < zombieVTargs.Length; ++j)
                        //    {
                        //        if (zombieVTargs[j] == minTarget)
                        //        {
                        //            if(j < 4)
                        //                zombies[j].pos = spawnA; //HACK FOR SIEGE
                        //            else
                        //                zombies[j].pos = spawnB; //HACK FOR SIEGE
                        //        }
                        //    }
                        //}
                        //if (minTarget == targets.Length - 2)
                        //if (minTarget == targets.Length - 1)
                        //{
                        //bodyState.pos = spawnB;
                        //bodyState.pos = spawn;
                        //}
                        //if (minTarget == teleportationTarget)
                        //{
                        //    teleportationLocation = spawnB;
                        //    //new Vector3(
                        //    //game1.randf(0, 1) * widthm * terrain.GetLength(0),
                        //    //targetRestY,
                        //    //game1.randf(0, 1) * depthm * terrain.GetLength(1));
                        //    //clevergirl = new Vector3(teleportationLocation.X,
                        //    //    clevergirl.Y,
                        //    //    teleportationLocation.Z);
                        //    //bodyState.pos = new Vector3(teleportationLocation.X,
                        //    //    bodyState.pos.Y,
                        //    //    teleportationLocation.Z);
                        //}
                        //HACK FOR SIEGE to completely remove barrier
                        //if(breachTargets.Contains(minTarget))
                        //{
                        //    targets[minTarget].Center.Y = -100;
                        //}
                    }
                    float velocityCofactor = 1;// 0;// 0.5f;
                    //float friction = 0;
                    //bool doPositionCorrect = true;
                    if (minBoxHit != null) //update bullet hit box
                    {
                        var noise = new SignalGenerator() { Type = SignalGeneratorType.Sin, Frequency = 550 };
                        //var pitch = new SmbPitchShiftingSampleProvider(noise);
                        //pitch.PitchFactor = 1.01f;// 1 + game1.randf(-.1f,.1f);
                        Vector3 toCam = bul.phy.pos - playerCam.pos;
                        float range = 20;
                        float rangesq = range * range;
                        float minVol = 0.1f;
                        float maxVol = 0.5f;
                        float proximity = MathHelper.Clamp((rangesq - toCam.LengthSquared()) / rangesq, 0, 1);
                        float volumeValue = (maxVol - minVol) * proximity + minVol;
                        var volume = new VolumeSampleProvider(noise) { Volume = volumeValue };
                        AddMixerInput(volume.ToMono().Take(TimeSpan.FromSeconds(0.01f)), "bullet hitting box", tt + 3);
                        //minBoxHit.GetMaterialProperties(out friction, out velocityCofactor, out doPositionCorrect);
                        //if(minBoxHit.type == BoxType.STICK)
                        //{
                        //    bul.t = bul.lifeSpan-et;
                        //}

                        //if (minBoxHit.embedsBulletOnImpact)
                        //{
                        //    velocityCofactor = 0.0f;
                        //    allbullets[b].affectedByGravity = false;
                        //}

                        //minContactData.restitution = minBoxHit.restitution;
                    }
                    Vector3 pos0 = bul.phy.pos;
                    if (blt.isSolid(et))
                    {
                        float slop = blt.size / 20;
                        if (minContactData.pen > slop)
                        {
                            blt.phy.pos += minContactData.norm * (minContactData.pen - slop);
                        }
                    }
                    if (blt.isRay(et))
                    {
                        blt.phy.pos = minContactData.contact + minContactData.norm * blt.size / 2;
                        //since we instantly teleport position velocity 
                        //this is like a pseudo physics update, 
                        //another one would be double updating (jk its a hack, kappa)
                        blt.skipNextAdvance = true;
                        //blt.incomingPosition = minContactData.contact;
                    }
                    //update draw bullet ricochet
                    game1.add3DLine(pos0, bul.phy.pos, bulletTrailColor);
                    //if(minTarget == -1)
                    //    blt.phy.vel = Vector3.Zero;
                    // else
                    //if (bul.phy.vel.LengthSquared() < 2) //settle down bullets!
                    //{
                    //    velocityCofactor = 0;
                    //}
                    //blt.phy.vel = Vector3.Reflect(blt.phy.vel, minContactData.norm) * velocityCofactor;
                    //blt.phy.pos -= resolveDir * resolvePens[b];

                    //update bullet friction, update bullet restitution
                    //float restitution = minContactData.restitution;
                    ////if (blt.isRay(et)) restitution = Math.Max( restitution, 0.01f);
                    //float friction = 0;
                    velocityCofactor = 0; //goodbye ricochet, disable ricochet, temporarily
                    if (!wantReturn)
                    {
                        blt.phy.vel = Vector3.Zero;
                    }else
                    {
                        blt.phy.vel = -blt.phy.vel;
                    }
                    blt.affectedByGravity = false;
                    // Vector3 flatVelocity = bul.phy.vel - minContactData.norm * relSpeed;
                    //blt.phy.vel -= (1 + velocityCofactor) * minContactData.norm * relSpeed;
                    //blt.phy.vel -= flatVelocity * friction;
                    ////blt.direction = Vector3.Normalize(blt.phy.vel);
                    ////blt.t = 1;//EXPLODE
                }
                //new contact
                //if(bul.inContact && !bul.inContactOld)
                //{
                //    audioEmitter.Position = minContactData.contact;
                //    SoundEffectInstance clicki = click.CreateInstance();
                //    clicki.Apply3D(audioListener, audioEmitter);
                //    clicki.Play();
                //}
            }
            //udpate retractor target
            if (retractorTarget != null)
            {
                bool travelComplete = false;
                float delta = et * 15;
                Vector3 targetPosition = playerCam.pos + camForwardFlat;
                if (retractorTarget is Target)
                    travelComplete = travel(ref (retractorTarget as Target).Center, targetPosition, delta);
                if (retractorTarget is Gun)
                    travelComplete = travel(ref (retractorTarget as Gun).pos, targetPosition, delta);
                if (travelComplete)
                {
                    retractorTarget = null;
                }
            }
            //update targets
            for (int i = 0; i < targets.Length; ++i)
            {
                Target T = targets[i];
                //if player is near then flee
                Vector3 v = bodyState.pos - T.Center; //to player
                float l2 = v.LengthSquared();
                float r = 5; //radius
                if (l2 < r * r)
                {
                    //move target is somewhere on perimeter of greater circle 
                    Vector2 flee = xz(v);
                    //T.movementTarget = 
                }
                //travel(ref T.Center, )
                //if player is distant move perpendicularly
                //    //update target collision
                //    if (targets[i].off)
                //        targets[i].phy.force += gravity * targets[i].phy.mass;
                //    targets[i].phy.force -= targets[i].phy.vel;
                //    targets[i].phy.Advance(et);
                //    BoundingSphere bs = new BoundingSphere(targets[i].Center, targets[i].Radius);
                //    Rectangle zone = getzone(bs);
                //    List<Box> boxes = getBoxesInZone(zone);
                //    foreach (Box b in boxes)
                //    {
                //        Vector3 cp = getClosestPoint(bs.Center, b.boundingBox); //closest point
                //        Vector3 vec = cp - bs.Center;
                //        float dist = vec.Length();
                //        // update target hit box, update box hit target
                //        if (dist <= bs.Radius)
                //        {
                //            Vector3 boxNormal = Vector3.Zero;
                //            float pen = 0;
                //            if (dist == 0)
                //            {
                //                pointToBoxNormal(cp, b.boundingBox, out boxNormal, out pen);
                //                pen += bs.Radius;
                //            }
                //            else
                //            {
                //                boxNormal = -vec / dist;
                //                pen = bs.Radius - dist;
                //            }
                //            float relSpeed = -Vector3.Dot(targets[i].phy.vel, boxNormal);
                //            if (relSpeed > 0)
                //            {
                //                targets[i].Center += boxNormal * pen;
                //                targets[i].phy.vel += relSpeed * boxNormal;
                //            }
                //            //b.position -= norm * pen / 2;
                //            //RefreshBox(b);
                //            //b.position += norm * pen;

                //        }
                //    }
                //target hit gun, gun hit target
                //for (int j = 0; j < allguns.Count; ++j)
                //{
                //    Gun g = allguns[j];
                //    if (!targets[i].off && g.off && sphereIntersectsGun(bs, g))
                //    {
                //        g.off = false;
                //        targets[i].off = true;
                //    }
                //}
                //}
                //BoundingSphere targ = targets[i];
                //int targetZombieId = -1;
                //for (int j = 0; j < zombieVTargs.Length; ++j)
                //{
                //    if (zombieVTargs[j] == i)
                //    {
                //        targetZombieId = j;
                //    }
                //}
                //int targetTeam = -1;// targetZombieId < 4 ? 0 : 1;
                //if (targetZombieId > -1)
                //{
                //    targetTeam = targetZombieId < 4 ? 0 : 1;
                //}
                //if (coreTargetsTable[0].Contains(i))
                //{
                //    targetTeam = 0;
                //}
                //if (coreTargetsTable[1].Contains(i))
                //{
                //    targetTeam = 1;
                //}
                //if(i == targets.Length - 1)
                //{
                //    targetTeam = 1;
                //}
                //update visibles for zombies
                //for (int j = 0; j < zombies.Length; ++j)
                //{
                //    ZombieState zombitch = zombies[j];
                //    //hear targets
                //    //Vector3 girlToTarg = targ.Center - clevergirl;
                //    //Vector3 girlToTarg = targ.Center - zombitch.pos;
                //    //float len = girlToTarg.Length();
                //    int zombieTeam = j < 4 ? 0 : 1; //hack for level ground war
                //    //    Vector3 sample = samples[i];
                //    //    Vector3 dir = sample - gun.pos;
                //    //    dir.Normalize();
                //    //    Ray ray = new Ray(gun.pos, dir);
                //    //    //game1.add3DLine(ray.Position, closestTarget, monochrome(0.2f));
                //    //    Rectangle zone = getzone(
                //    //        Vector3.Min(ray.Position, closestTarget),
                //    //        Vector3.Max(ray.Position, closestTarget));
                //    //    for (int x = tclamp(zone.Left); x <= tclamp(zone.Right) && !interrupted; ++x)
                //    //    {
                //    //        for (int z = tclamp(zone.Top); z <= tclamp(zone.Bottom) && !interrupted; ++z)
                //    //        {
                //    //if(isSideA && targetIsSideB)
                //    //{
                //    //    int nada = 0;
                //    //}
                //    //test if visibility fails if 
                //    Vector3 zombToTarg = zombitch.pos - targ.Center;
                //    bool visibilityFail = false;
                //    if (zombToTarg.LengthSquared() > 0)
                //    {
                //        visibilityFail = RayHitsBox(targ.Center, zombitch.pos);
                //    }
                //    if (
                //        //len >= hearingRadius ||
                //        targ.Center.Y <= targetHitY
                //        //|| t == teleportationTarget 
                //        //|| t == teleportationTarget2 
                //        //|| t == teleportationTarget3
                //        //|| coreTargetsTable[0].Contains(i)
                //        //|| i == targets.Length - 1
                //        //|| zombieVTargs.Contains(i)
                //        || zombieTeam == targetTeam
                //        || visibilityFail)
                //    {
                //        //if (!visibleTargetsBoy.Contains(t)) //boys like targets on ground
                //        //    visibleTargetsBoy.Add(t);
                //        //if (visibleTargetsGirl.Contains(i))
                //        //    visibleTargetsGirl.Remove(i);
                //        if (zombitch.visibleTargets.Contains(i))
                //            zombitch.visibleTargets.Remove(i);
                //    }
                //    else
                //    {
                //        //if (visibleTargetsBoy.Contains(t))
                //        //    visibleTargetsBoy.Remove(t);
                //        //if (!visibleTargetsGirl.Contains(i)) //girls like targets in air
                //        //    visibleTargetsGirl.Add(i);
                //        if (!zombitch.visibleTargets.Contains(i)) //girls like targets in air
                //        {
                //                zombitch.visibleTargets.Add(i);
                //        }
                //    }
                //}
            }
            Vector3 captureCenter = coordToWorld(new Point(terrain.GetLength(0) / 2, terrain.GetLength(1) / 2), dropValue / 2);
            float separation = dropValue * 1.5f;
            for (int i = 0; i < capturedTargets.Count; ++i)
            {
                int y = (targets.Length - 1) - i;
                int target = capturedTargets[i];
                Vector3 destination = captureCenter + Vector3.Up * separation * (float)y;
                Vector3 toDestination = destination - targets[target].Center;
                float dist = toDestination.Length();
                float rate = 5 * et;
                if (dist <= rate)
                {
                    targets[target].Center = destination;
                }
                else
                {
                    targets[target].Center += toDestination / dist * rate;
                }
            }

            //update units
            //bool gameOver = targets[coreTargetsTable[0].First()].Center.Y == 0 ||
            //    targets[coreTargetsTable[1].First()].Center.Y == 0;
            //for (int i = 0; i < units.Count && !gameOver; ++i)
            //{
            //    Unit u = units[i];
            //    targets[u.selfTarget].Center = u.pos;
            //    u.gun.pos = u.pos;
            //    int j = i - 4;
            //    if(u.target == -1 && j > 1)
            //    {
            //        u.target = playerSelfTarget;
            //    }
            //    if (u.target > -1)
            //    {
            //        BoundingSphere target = targets[u.target];
            //        Vector3 dir = target.Center - u.gun.pos;
            //        float length = dir.Length();
            //        if (length > 0)
            //        {
            //            dir /= length;
            //            u.gun.rot = Matrix.Invert(Matrix.CreateLookAt(Vector3.Zero, dir, Vector3.Up));
            //        }
            //        if (length < widthm * 10)
            //        {
            //            u.gun.TriggerDown(new PhysicsState(1));
            //        }
            //        else if(!u.stationary)
            //        {
            //            u.pos += dir * 5 * et;
            //        }
            //    }
            //}
            //teleportationLocation = bodyState.pos;
            //Action<int> tele = (int t) =>
            //{
            //    Vector3 oldCenter = targets[t].Center;
            //    Vector3 newCenter = teleportationLocation;
            //    newCenter.Y = oldCenter.Y;
            //    Vector3 dir = newCenter - oldCenter;
            //    float len = dir.Length();
            //    dir /= len;
            //    float rate = 100 * et;
            //    if (len < rate)
            //    {
            //        targets[t].Center = newCenter;
            //    }
            //    else
            //    {
            //        targets[t].Center += dir * rate;
            //    }
            //};
            //tele(teleportationTarget);
            //tele(teleportationTarget2);
            //tele(teleportationTarget3);

            //int clevergoalsoutputI = -1;
            //Func<List<int>, Vector3, Gun, int, Vector3> clevergoals = (List<int> visibles, Vector3 cleverone, Gun gun, int nextTarg) =>
            //Func<ZombieState, Vector3> clevergoals = (ZombieState zom) =>
            //{
            //    List<int> visibles = zom.visibleTargets;
            //    Vector3 cleverone = zom.pos;
            //    Gun gun = zom.gun;
            //    //int nextTarg = zom.targ;
            //    gun.pos = cleverone;
            //    //clevergoalsoutputI = -1;
            //    /*if(nextTarg != -1)
            //    {
            //        if (!visibles.Contains(nextTarg))
            //            nextTarg = -1;
            //        else
            //            clevergoalsoutputI = nextTarg;
            //    }*/
            //    //if (visibles.Count > 0)
            //    //{
            //    //    int i = 0;
            //    //    while (i < visibles.Count)
            //    //    {
            //    //        if (pathfinding.walkablePredicate(worldToCoord(targets[visibles[i]].Center)))
            //    //        {
            //    //            nextTarg = visibles[i];
            //    //            break;
            //    //        }
            //    //        ++i;
            //    //    }
            //    //}
            //    //if (clevergoalsoutputI != -1 && nextTarg != clevergoalsoutputI)
            //    //    if (clevergoalsoutputI != -1 && nextTarg != clevergoalsoutputI)
            //    //    {
            //    //    nextTarg = clevergoalsoutputI;
            //    //    //pathfindElapsed = pathfindWaitDurationS;
            //    //    zom.pathfindElapsed = zom.pathfindWaitDurationS;
            //    //}
            //    //if (closestTargetIndex > -1)
            //    //{
            //    //    nextTarg = closestTargetIndex;
            //    //}
            //        //Vector3[] samples = new Vector3[] {
            //        //closestTarget + Vector3.Up * gun.bulletSize/2,
            //        //closestTarget + Vector3.Left * gun.bulletSize / 2,
            //        //closestTarget + Vector3.Right * gun.bulletSize / 2,
            //        //closestTarget + Vector3.Down * gun.bulletSize / 2
            //        //};
            //        //Vector3 gunForward = Vector3.Transform(Vector3.Forward, gun.rot);
            //        //Ray ray = new Ray(cleverone, forward);
            //        //bool interrupted = false;
            //        //for (int i = 0; i < samples.Length; ++i)
            //        //{
            //        //    if (i > 0) break; //HACK TO SPEEDUP ZOMBIES
            //        //    Vector3 sample = samples[i];
            //        //    Vector3 dir = sample - gun.pos;
            //        //    dir.Normalize();
            //        //    Ray ray = new Ray(gun.pos, dir);
            //        //    //game1.add3DLine(ray.Position, closestTarget, monochrome(0.2f));
            //        //    Rectangle zone = getzone(
            //        //        Vector3.Min(ray.Position, closestTarget),
            //        //        Vector3.Max(ray.Position, closestTarget));
            //        //    for (int x = tclamp(zone.Left); x <= tclamp(zone.Right) && !interrupted; ++x)
            //        //    {
            //        //        for (int z = tclamp(zone.Top); z <= tclamp(zone.Bottom) && !interrupted; ++z)
            //        //        {
            //        //            foreach (var box in terrain[x, z])
            //        //            {
            //        //                float? hit = ray.Intersects(Game1.MakeBox(box.position, box.size));
            //        //                if (hit.HasValue && hit.Value <= closestTargetDist)
            //        //                {
            //        //                    interrupted = true;
            //        //                    break;
            //        //                }
            //        //            }
            //        //        }
            //        //    }
            //        //}
            //        if (/*interrupted ||*/ !gun.TriggerDown(new PhysicsState(1)))// || !girlgun.Shoot(new PhysicsState(1)))
            //        {
            //            //List<square> path = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(closestTarget));
            //            //List<square> path = cleverpath;
            //            //List<square> path = zom.path;
            //            //if (path != null)
            //            //{
            //            ////    //if (path.Count > 1 && girlpathprogress < path.Count)
            //            //    if (path.Count > 1 && zom.pathProgress < path.Count)
            //            //    {
            //            //        //Vector3 next = coordToWorld(path[girlpathprogress].coordinate) + Vector3.Up * 2;
            //            //        Vector3 next = coordToWorld(path[zom.pathProgress].coordinate) + Vector3.Up * 2;

            //            //        Vector3 toNext = next - cleverone;
            //            //        float length = toNext.Length();
            //            //        toNext /= length;
            //            //        float rate = moveSpeed * et;
            //            //        if (length < 1)
            //            //        {
            //            //            cleverone = next;
            //            //            //girlpathprogress++;
            //            //            zom.pathProgress++;
            //            //        }
            //            //        else
            //            //        {
            //            //            //cleverone += Vector3.Normalize(toNext) * rate;
            //            //            zom.force += Vector3.Normalize(toNext) * moveSpeed;
            //            //        }
            //            //    }
            //            //}
            //            //if (pathfindElapsed > pathfindWaitDurationS)
            //            //if (zom.pathfindElapsed > zom.pathfindWaitDurationS)
            //            //{
            //            //    //if (path == null || girlpathprogress == path.Count)
            //            //    if (path == null || zom.pathProgress == path.Count)
            //            //    {
            //            //        //pathfindElapsed = 0;// pathfindWaitDuration;
            //            //        //cleverpath = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(closestTarget));
            //            //        //girlpathprogress = 1;
            //            //        zom.pathfindElapsed = 0;// pathfindWaitDuration;
            //            //        zom.path = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(closestTarget));
            //            //        //zom.path = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(kothCenter)); //HACK TO DO KOTH
            //            //        zom.pathProgress = 1;
            //            //    }
            //            //}
            //            //pathfindElapsed += et;
            //            //zom.pathfindElapsed += et;
            //            //ai gun expression
            //            //if (interrupted)
            //            //{
            //            //    gun.rot = Matrix.CreateRotationX(MathHelper.PiOver4) * gun.rot;
            //            //}
            //        }
            //        //else
            //        //{
            //        //}
            //    }
            //zom.targ = nextTarg;
            //return cleverone;
            //};
            //int lastCore = coreTargetsTable[0].First();
            //if (targets[lastCore].Center.Y > targetHitY)
            //{
            //for (int i = 0; i < zombies.Length; ++i)
            //{
            //    ZombieState zom = zombies[i];
            //Vector3 clevergirl = zom.pos;//,
            //    clevergirlvel = zom.vel,
            //    clevergirlforce = zom.force;
            //Vector3 girlMovementExtra = Vector3.Zero;
            //Vector3 offensePosition = clevergoals(visibleTargetsGirl, clevergirl, girlgun, girlnexttarg);
            //Vector3 toOffense = offensePosition - clevergirl;
            //Vector3 offensePosition = clevergoals(visibleTargetsGirl, clevergirl, girlgun, girlnexttarg);
            //bool isSideA = i < 4;
            //if(isSideA) //debug
            //{
            //    int nada = 3;
            //}
            //Vector3 closestTarget = Vector3.Zero;
            //Vector3 toClosestTarget = Vector3.Zero;
            //float closestTargetDist = float.MaxValue;
            //int closestTargetIndex = -1;
            //for (int j = 0; j < zom.visibleTargets.Count; ++j)
            //{
            //    int k = zom.visibleTargets[j]; //visible target index
            //    //if (nextTarg != -1 && j != nextTarg) //comment out hack to enable proximity preference
            //    //    continue;
            //    BoundingSphere target = targets[k];
            //    //Target target = targets[j];
            //    //BoundingSphere sphere = new BoundingSphere(target.position,target.radius);
            //    Vector3 line = target.Center - zom.gun.pos;
            //    float dist = line.Length();
            //    if (dist < closestTargetDist)// && target.Center.Y > targetHitY)
            //    {
            //        //hack to improve siege by allowing shooting against player in crazy spots
            //        //if (pathfinding.walkablePredicate(worldToCoord(target.Center)))
            //        //{
            //        closestTargetDist = dist;
            //        closestTarget = target.Center;// + Vector3.Up;
            //        toClosestTarget = line;
            //        //clevergoalsoutputI = j;
            //        closestTargetIndex = k;
            //        //}
            //    }
            //}
            //if (closestTargetIndex > -1)
            //{
            //    zom.gun.rot = 
            //        Matrix.Invert(
            //            Matrix.CreateLookAt(Vector3.Zero, toClosestTarget, Vector3.Up));
            //    zom.gun.TriggerDown(new PhysicsState(1));
            //}
            //if (zom.path != null)
            //{
            //    //    //if (path.Count > 1 && girlpathprogress < path.Count)
            //    if (zom.path.Count > 1 && zom.pathProgress < zom.path.Count)
            //    {
            //        //Vector3 next = coordToWorld(path[girlpathprogress].coordinate) + Vector3.Up * 2;
            //        Vector3 next = coordToWorld(zom.path[zom.pathProgress].coordinate) + Vector3.Up * 2;

            //        Vector3 toNext = next - zom.pos;
            //        float length = toNext.Length();
            //        toNext /= length;
            //        float rate = moveSpeed * et;
            //        if (length < 1)
            //        {
            //            zom.pos = next;
            //            //girlpathprogress++;
            //            zom.pathProgress++;
            //        }
            //        else
            //        {
            //            //cleverone += Vector3.Normalize(toNext) * rate;
            //            zom.force += Vector3.Normalize(toNext) * moveSpeed;
            //        }
            //    }
            //}
            ////HACK TO GET BETTER PATHFINDING
            //if (zom.pathfindElapsed > zom.pathfindWaitDurationS)
            //{
            //    //    //if (path == null || girlpathprogress == path.Count)
            //    //    //if (zom.path == null || zom.pathProgress == zom.path.Count)
            //    //    //{
            //    //        pathfindElapsed = 0;// pathfindWaitDuration;
            //    //        //cleverpath = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(closestTarget));
            //    //        //girlpathprogress = 1;
            //    zom.pathfindElapsed = 0;// pathfindWaitDuration;
            //                            //                                //zom.path = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(closestTarget));
            //    zom.path = pathfinding.findPath(worldToCoord(zom.pos), worldToCoord(spawnA)); //hack for ground war
            //                                                                                  //    //hack to defend siege core
            //                                                                                  //    //zom.path = pathfinding.findPath(worldToCoord(cleverone), new Point(7,7)); 
            //                                                                                  //    //zom.path = pathfinding.findPath(worldToCoord(cleverone), worldToCoord(kothCenter)); //HACK TO DO KOTH
            //                                                                                  //    zom.pathProgress = 1;
            //                                                                                  //    //}
            //}
            //zom.pathfindElapsed += et;
            //Vector3 toOffense = offensePosition - clevergirl;
            //toOffense.Y = 0;
            //if (toOffense.LengthSquared() > 0)
            //{
            //    toOffense.Normalize();
            //    girlMovementExtra += toOffense;
            //}
            //}
            //update cease fire
            //hack for level ground war
            //if (targets[coreTargetsTable[0].First()].Center.Y <= targetHitY && 
            //    i < 4)
            //{
            //    zom.gun.requireTriggerUp = true;
            //}
            //if (targets[coreTargetsTable[1].First()].Center.Y <= targetHitY &&
            //    i >= 4)
            //{
            //    zom.gun.requireTriggerUp = true;
            //}
            //girlnexttarg = clevergoalsoutputI;
            //cleverboy = clevergoals(visibleTargetsBoy, cleverboy, boygun, boynexttarg);
            //boynexttarg = clevergoalsoutputI;
            //for (int i = 0; i < targets.Length; ++i)
            //{
            //for (int j = targetHistory.GetLength(1)-1; j >= 0; --j)
            //{
            //    Vector3 other = targets[i].Center;
            //    if (j != 0)
            //        other = targetHistory[i, j - 1];
            //    float leash = 0.25f;
            //    Vector3 toOther = other - targetHistory[i, j];
            //    if (toOther.LengthSquared() > leash * leash)
            //    {
            //        float len = toOther.Length();
            //        float delta = toOther.Length() - leash;
            //        targetHistory[i, j] += delta * (toOther / len);
            //    }
            //}
            //Vector3 spawn = targetSpawns[i] + Vector3.Up * targetRestY;
            //Vector3 toSpawn = spawn - targets[i].Center;
            //if (toSpawn.LengthSquared() > 0)
            //{
            //    float len = toSpawn.Length();
            //    float rate = 1;
            //    //if (i == 0)
            //        rate = 10;
            //    float delta = rate * et;
            //    if (len < delta)
            //        targets[i].Center = spawn;
            //    else
            //        targets[i].Center += toSpawn / len * delta;
            //}
            //}
            //for (int b = 0; b < grenadebullets.Length; ++b)
            //{
            //    Bullet B = grenadebullets[b];
            //    BoundingSphere sv = new BoundingSphere(B.p.pos, 0.1f);
            //    for (int t = 0; t < targets.Length; ++t)
            //    {
            //        if (sv.Intersects(targets[t]))
            //        {
            //            Vector3 toS = sv.Center - targets[t].Center;
            //            B.p.vel = Vector3.Normalize(toS) * B.p.vel.Length();
            //            targets[t].Center.Y = 0;
            //        }
            //    }
            //}

            //if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.Down))
            //{
            //    camera.Euler.X = MathHelper.ToRadians(-45);
            //}else
            //{
            //    camera.Euler.X = 0;
            //}

            //clevergirl defense
            //Vector3 girlToPlayer = bodyState.pos - clevergirl;
            //girlToPlayer.Y = 0;
            //float girlToPlayerLen = girlToPlayer.Length();
            //bool awareOfPlayer = false;
            //if(girlToPlayerLen <  hearingRadius)
            //{
            //    awareOfPlayer = true;
            //}
            //girlToPlayer /= girlToPlayerLen;
            //Vector3 girlTangent = Vector3.Cross(girlToPlayer, Vector3.Up);
            //bool girlSafe = false;
            //Vector3 playerGunForward = Vector3.Transform(Vector3.Forward, currentGun.rot);
            //if (!currentGun.getEmpty())
            //{
            //    Ray gunRayToGirl = new Ray(currentGun.pos, -girlToPlayer);
            //    Rectangle gunRayZone = getzone(Vector3.Min(currentGun.pos, clevergirl),
            //        Vector3.Max(currentGun.pos, clevergirl));
            //    for (int x = tclamp(gunRayZone.Left); x <= tclamp(gunRayZone.Right); ++x)
            //    {
            //        for (int z = tclamp(gunRayZone.Top); z <= tclamp(gunRayZone.Bottom); ++z)
            //        {
            //            foreach (Box b in terrain[x, z])
            //            {
            //                float? hit = gunRayToGirl.Intersects(Game1.MakeBox(b.position, b.size));
            //                if (hit.HasValue && hit.Value < girlToPlayerLen)
            //                {
            //                    girlSafe = true;
            //                    break;
            //                }

            //            }
            //        }
            //    }
            //}

            //Rectangle girlZone = getzone(clevergirl, clevergirl);
            //bool wantFlee = false;
            //for (int x = tclamp(girlZone.Left); x <= tclamp(girlZone.Right); ++x)
            //{
            //    for (int z = tclamp(girlZone.Top); z <= tclamp(girlZone.Bottom); ++z)
            //    {
            //        foreach (Box b in terrain[x, z])
            //        {
            //            BoundingBox bb = Game1.MakeBox(b.position, b.size);
            //            //if (bb.Contains(clevergirl) == ContainmentType.Contains)
            //            //{
            //            //game1.
            //            Vector3 cp = clevergirl;
            //            cp = Vector3.Min(cp, bb.Max);
            //            cp = Vector3.Max(cp, bb.Min);
            //            float rad = 0.1f;
            //            if ((cp - clevergirl).LengthSquared() < rad * rad)
            //            {
            //                Vector3 dir;
            //                float pen;
            //                pointToBoxNormal(cp, bb, out dir, out pen);
            //                clevergirl += dir * pen;
            //                float relSpeed = -Vector3.Dot(clevergirlvel, dir);
            //                if (relSpeed > 0)
            //                {
            //                    clevergirlvel += relSpeed * dir;
            //                }
            //                //girlMovementExtra = dir;
            //                //wantFlee = true;
            //                //}
            //            }
            //        }
            //    }
            //}
            //bool wantStrafe = false;
            //Vector3 desiredMove = Vector3.Zero;
            ////game1.add3DLine(clevergirl, currentGun.pos, Color.White);
            ////if (girlSafe) //FAILED ATTEMPT TO DO COVER PEAKING
            ////{
            ////    desiredMove.X = 1;
            ////    if (Vector3.Dot(girlTangent, playerGunForward) < 0)
            ////        desiredMove.X = -1;
            ////}
            //if (!girlSafe
            //    //&& awareOfPlayer
            //    )
            //{
            //    //wantStrafe = true;
            //    Vector3 dangerPoint = playerGunForward * girlToPlayerLen + currentGun.pos;
            //    //if within player view
            //    //if (Vector3.Dot(girlToPlayer, playerGunForward) < -0.98f)
            //    Vector3 girlToDP = dangerPoint - clevergirl;
            //    float dangerDist = widthm * 1;
            //    if (girlToDP.LengthSquared() < dangerDist * dangerDist)
            //    {
            //        //move tangent to the player
            //        wantStrafe = true;
            //    }
            //}
            //float dangerZone = widthm * 2;
            //if (girlToPlayerLen < dangerZone * dangerZone
            //        //&& awareOfPlayer
            //        )
            //{
            //    //clevergirlforce -= Vector3.Normalize(girlToPlayer) * moveSpeed * walkBoost;
            //    wantFlee = true;
            //}
            ////strafe
            //if (false)//wantStrafe)
            //{
            //    desiredMove.X = -1;
            //    if (Vector3.Dot(girlTangent, playerGunForward) < 0)
            //        desiredMove.X = 1;
            //    //clevergirlforce -= girlTangent * moveSpeed * walkBoost;
            //}
            //if (false)//wantFlee)
            //{
            //    desiredMove.Z = -1;
            //    //clevergirlforce -= girlToPlayer * moveSpeed * walkBoost;
            //}
            //if (girlMovementExtra.LengthSquared() > 0)
            //    girlMovementExtra.Normalize();
            //Vector3 movementDir = //desiredMove.X * girlTangent
            //+ desiredMove.Z * girlToPlayer
            //+ 
            //girlMovementExtra
            //;
            //if (movementDir.LengthSquared() > 0)
            //{
            //    movementDir.Normalize();
            //    Vector3 moveTangent = Vector3.Cross(movementDir, Vector3.Up);
            //    moveTangent.Normalize();
            //    //clevergirlforce += movementDir * moveSpeed * walkBoost;
            //    clevergirlforce += movementDir * 33 * walkBoost;
            //    for(int j = 0; j < zombies.Length; ++j)
            //    {
            //        ZombieState zj = zombies[j];
            //        Vector3 toZj = zj.pos - zom.pos;
            //        float len = toZj.Length();
            //        if(len > 0 && len < widthm/2)
            //        {
            //            clevergirlforce -= (toZj / len) * 100;
            //        }
            //    }
            //    //clevergirlforce -= Vector3.Dot(moveTangent, clevergirlvel) * moveTangent * 10;
            //    //clevergirlvel += movementDir * moveSpeed * walkBoost * et;
            //clevergirl += movementDir;
            //}
            ////update clever girl movement
            ////clevergirlvel *= 0.5f;
            //clevergirlvel += clevergirlforce * et;
            //clevergirlforce = Vector3.Zero;
            //clevergirlforce -= clevergirlvel * 10;
            //clevergirl += clevergirlvel * et;
            //clevergirl.X = MathHelper.Clamp(clevergirl.X, 0, widthm * terrain.GetLength(0));
            //clevergirl.Z = MathHelper.Clamp(clevergirl.Z, 0, depthm * terrain.GetLength(1));
            ////targets[targets.Length - 1].Center = new Vector3(clevergirl.X,
            ////    targets[targets.Length - 1].Center.Y,
            ////    clevergirl.Z);
            //zom.force -= zom.vel;
            //PhysicsState.Advance(et, ref zom.pos, ref zom.vel, ref zom.force);
            //targets[zombieVTargs[i]].Center = zom.pos;
            ////update zombie movement
            //zom.pos = clevergirl;
            //zom.vel = clevergirlvel;
            //zom.force = clevergirlforce;
            //}
            //if (targets[coreTargetsTable[0].First()].Center.Y <= targetHitY)
            //{
            //    currentGun.requireTriggerUp = true;
            //}
            //targets[targets.Length - 2].Center = top + drop / 2;
            //targets[targets.Length - 1].Center = top + drop / 2;
            //targets[targets.Length - 1].Center = zombies[0].pos;

            //update camera position
            if ((!editor.edit.active || !editor.editOutOfBody) && !playOutOfBody)
            {
                playerCam.pos = top + drop / 2;
                //3rd person
                if (is3rdPerson)
                {
                    playerCam.pos -= camForward * 4;
                }
            }
            playerCam.Update(gameTime, GraphicsDevice.Viewport);
            player.untransformedProjection = playerCam.GetProjection(playerCam.fov_max, GraphicsDevice.Viewport.AspectRatio);
            Vector2 gvMutable = player.gv;
            if (player.playerLeftHandy)
                gvMutable = Vector2.One - gvMutable;
            Vector3 preTransformFar = CameraState.ScreenToWorld(
                gvMutable * ViewBounds.Size.ToVector2(),
                1,
                GraphicsDevice.Viewport,
                playerCam.view * player.untransformedProjection);
            if (!gameMouseLocked && aimDownSight)
            {

                Vector2 aimPosNorm = CameraState.worldToScreen(preTransformFar, GraphicsDevice.Viewport,
                    playerCam.view * playerCam.projection);
                aimPosNorm /= ViewBounds.Size.ToVector2();
                aimPosNorm = aimPosNorm * 2 - Vector2.One;
                playerCam.projection *= Matrix.CreateTranslation(-aimPosNorm.X, aimPosNorm.Y, 0);
            }
            //camera.fov_max = MathHelper.ToRadians(95);
            //_recoile = Vector3.Lerp(_recoile, recoile, 0.3f);
            //camera.Euler += _recoile;

            //update audio 
            //audioListener.Position = playerCam.pos;
#if ADD_CLOSED_CAPTIONS
            for(int i = 0; i < closedCaptions.Count; ++i)
            {
                var v = closedCaptions.ElementAt(i);
                if(v.Value < tt)
                {
                    closedCaptions.Remove(v.Key);
                    --i;
                }
            }
#endif
            if (!editor.edit.active)
            {
                float attackDelta = 0.05f;
                float freqDelta = 5;
                if (game1.kclickheld(Keys.OemPlus))
                {
                    if (controlDown)
                        shootSound.freq += freqDelta;
                    else
                        shootSound.attackP += attackDelta;
                }
                if (game1.kclickheld(Keys.OemMinus))
                {
                    if (controlDown)
                        shootSound.freq -= freqDelta;
                    else
                        shootSound.attackP -= attackDelta;
                }
                int type = (int)shootSound.signalT;
                int count = Enum.GetValues(typeof(SignalGeneratorType)).Length;
                if (game1.kclick(Keys.OemPeriod))
                {
                    type++;
                    if (type >= count)
                        type -= count;
                }
                if (game1.kclick(Keys.OemComma))
                {
                    type--;
                    if (type < 0)
                        type += count;
                }
                if (game1.kclick(Keys.D0))
                {
                    shootSound.attackP = 0.5f;
                }
                shootSound.signalT = (SignalGeneratorType)type;
                if (game1.kclick(Keys.F2))
                {
                    using (StreamWriter writer = new StreamWriter(File.Create("shoot-sound.csv")))
                    {
                        writer.WriteLine("#Version 0");
                        writer.WriteLine(writeCsvSignalASDRState(shootSound));
                    }
                }
                if (game1.kclick(Keys.F3))
                {
                    shootSound = loadSignalASDRState("shoot-sound.csv");
                }
                if (game1.kclick(Keys.F4))
                {
                    enableSound = !enableSound;
                    mixerVolume.Volume = enableSound ? 1 : 0;
                }
            }

            //camera.Euler -= _recoile;
            //recoile = Vector3.Lerp(recoile, Vector3.Zero, 0.1f);
            //Vector2 gvMutable = new Vector2(0.5f, 0.5f);// new Vector2(0.7f, 0.8f);
            //Vector2 gvMutable = new Vector2(0.7f, 0.8f);
            //if(!edit.active && !mouseLocked)
            //{
            //gvMutable = game1.mouseCurrent.Position.ToVector2() / new Vector2(ViewBounds.Width, ViewBounds.Height);
            //}
            aimDownSight = false;
            if (game1.IsActive &&
                game1.mouseCurrent.RightButton == ButtonState.Pressed && !editor.edit.active)
            {
                aimDownSight = true;
            }
            playerCam.fov_max = 70;
            if (aimDownSight)
            {
                //gvMutable = new Vector2(0.5f, 0.5f);
                playerCam.fov_min = 15;
                playerCam.zooming = true;
            }
            else
            {
                playerCam.zooming = false;
            }
            //update current gun position
            //if (currentGun != null)
            //{
            //    if (!(edit.active && editOutOfBody))
            //    {
            //        UpdateGunFromCam(currentGun, playerCam, gvMutable, GraphicsDevice, aimDownSight, this);
            //    }
            //    else
            //    {
            //        //Vector3 far = playerCam.ScreenToWorld(
            //        //    gvMutable * GraphicsDevice.Viewport.Bounds.Size.ToVector2(),
            //        //    1,
            //        //    GraphicsDevice.Viewport);
            //        Vector3 far = preTransformFar;
            //        currentGun.pos = top + drop * 0.9f + camForward * 0.25f + camRight * 0.1f;// bodyState.pos + Vector3.Up * (dropValue*(float)(bodyc/2-0.25f));// bodyState.pos;
            //        Vector3 gff = far - currentGun.pos;
            //        gff.Normalize();
            //        ////game1.add3DLine(currentGun.pos, currentGun.pos + gff * 10, Color.Red);
            //        if (!(edit.active && editOutOfBody))
            //            currentGun.rot = Matrix.Invert(Matrix.CreateLookAt(Vector3.Zero, gff, Vector3.Up));
            //    }
            //}
            //update ghost
            //ghostGun.off = false;
            //if (ghostFrame < ghostFrames.Length)
            //{
            //    if (currentGun != null)
            //    {
            //        ghostFrames[ghostFrame++] = new GhostFrame()
            //        {
            //            gunPosition = currentGun.pos,
            //            gunRotation = currentGun.rot,
            //            shooting = localInput.shoot
            //        };
            //    }
            //    if(game1.kclick(Keys.Tab))
            //    {
            //        ghostFrame = ghostFrames.Length;
            //    }
            //}else
            //{
            //    if (ghostPlaybackFrame < ghostFrames.Length)
            //    {
            //        GhostFrame f = ghostFrames[ghostPlaybackFrame++];
            //        ghostGun.pos = f.gunPosition;
            //        ghostGun.rot = f.gunRotation;
            //        if (f.shooting)
            //            ghostGun.TriggerDown(new PhysicsState(1));
            //        else
            //            ghostGun.TriggerUp();
            //    }
            //    if (game1.kclick(Keys.Tab))
            //    {
            //        ghostFrame = 0;
            //    }
            //}

            //update my guns
            int altGunCounter = 0;
            for (int i = 0; i < myGuns.Count; ++i)
            {
                Gun G = myGuns[i];
                //if (G != currentGun)
                //{
                //myGuns[g].pos = altGunReference + Vector3.Down * (float)altguncount * (myGuns[g].size.Y + 0.1f);
                //if (!(edit.active && editOutOfBody))
                //    myGuns[g].rot = Matrix.CreateRotationY(playerCam.Euler.Y + MathHelper.PiOver2);// currentGun.rot;
                //if (currentGun != null)
                //{
                //}
                //}
                switch (playerGunHolsterFormation)
                {
                    case playerHolsterOcto:
                        {
                            float f = (float)i;
                            float step = MathHelper.TwoPi / myGuns.Count;
                            float angle = f * step;
                            float half = 0.5f;
                            Vector2 altgv = Vector2.Transform(gvMutable,
                                Matrix.CreateTranslation(-half, -half, 0) *
                                Matrix.CreateScale(G == currentGun ? 0.4f : 1) *
                                Matrix.CreateRotationZ(angle) *
                                Matrix.CreateTranslation(half, half, 0));
                            UpdateGunFromCam(G, playerCam, altgv, GraphicsDevice, aimDownSight, this);
                            //if (G == currentGun)
                            //{
                            //    G.pos += camForwardFlat * 0.5f;
                            //}

                        }
                        break;
                    case playerHolsterAdventurer:
                        {
                            if (G == currentGun)
                            {
                                UpdateGunFromCam(G, playerCam, gvMutable, GraphicsDevice, aimDownSight, this);
                            }
                            else
                            {
                                G.rot = Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateRotationY(playerCam.Euler.Y);
                                G.pos = bodyState.pos - camForwardFlat * (G.size.X * (float)altGunCounter + dropValue / 2);
                            }
                        }
                        break;
                }
                if (G != currentGun)
                {
                    altGunCounter++;
                }
            }

            //update guns
            //bulletBox = slowGun.bullets[0];
            //boxBullet.position = bulletBox.phy.pos;
            //RefreshBox(boxBullet);
            for (int g = 0; g < allguns.Count; ++g)
            {
                Gun G = allguns[g];
                if (myGuns.Contains(allguns[g]) == false)
                {
                    if (!customGuns.Contains(G) || (gunsIveHad.Contains(G) && gunsAutofireGunsIveHad))
                    {
                        G.TriggerDown(new PhysicsState(1));
                        //    Vector3 toPlayerFlat = bodyState.pos - G.pos;
                        //    toPlayerFlat.Y = 0;
                        //    toPlayerFlat.Normalize();
                        //    G.rot = Matrix.Invert(Matrix.CreateLookAt(Vector3.Zero, toPlayerFlat, Vector3.Up));
                    }
                }
                else
                {
                    //int targ = 30000;
                    //if (G.BulletLifespan != targ)
                    //    G.BulletLifespan = targ;
                }
                allguns[g].Update(et);
            }
            //if(game1.kdown(Keys.F))
            //{
            //    meleeing = true;
            //}
            //Vector3 camRight = Vector3.Cross(camForward, Vector3.Up);
            Vector3 camUp = Vector3.Cross(camRight, camForward);
            //Vector3 meleeBulletOffset = -camRight * (dropValue / 2 + meleeBullet.size/2);
            //if (meleeing)
            //{
            //    meleeTime += et;
            //    float meleeProgress = meleeTime / meleeDuration;
            //    Matrix rotation = Matrix.CreateFromAxisAngle(camUp, meleeProgress * -MathHelper.Pi);
            //    float meleeDist = 2;
            //    meleeBulletOffset = Vector3.Transform(-camRight * meleeDist, rotation);
            //    if (meleeTime > meleeDuration)
            //    {
            //        meleeing = false;
            //        meleeTime = 0;
            //    }
            //}
            //meleeBullet.phy.pos = top + drop / 2 + meleeBulletOffset;

            //update dialog

            //update edit state
            editor.Update(GraphicsDevice, game1, bodyState, playerCam, drop, dropValue);

            //update network outgoing
            //if (game1.kdown(Keys.F1)) //PING/CONNECT
            //{
            //    //sendClient.Connect("10.0.0.4", connectPort);
            //    ////perform an empty send to update the 'connected' flag
            //    sendClient.Send(new byte[0], 0, pingAddress, remotePort);
            //}
            //float packetsPerSecond = 10;
            //float secondsPerPacket = 1 / packetsPerSecond;
            //if (packetT > secondsPerPacket)
            //{
            //packetT -= secondsPerPacket;
            //Byte[] sendBytes = Encoding.ASCII.GetBytes(string.Format("p {0} {1} {2}",
            //    csv_writev3(bodyState.pos),
            //    csv_writev3(playerCam.Euler),
            //    csv_writeFPSInput(localInput)));
            //for (int i = 0; i < networkplayerdata.Keys.Count; ++i)
            //{
            //    NetworkPlayer p = networkplayerdata.ElementAt(i).Value;
            //    //if (p.endpoint.Address.ToString() == "10.0.0.4")
            //    //    continue;
            //    if (p.endpoint.Address == IPAddress.None)
            //        continue;
            //    sendClient.Send(sendBytes, sendBytes.Length, p.endpoint.Address.ToString(), remotePort);
            //}
            //packetT += et;

        }//END UPDATE

        //edit update

        //helper audio
        // add mixer input
        public void AddMixerInput(ISampleProvider sampler, string closedCaption, float closedCaptionDuration)
        {
            mixer.AddMixerInput(sampler);
            float targetTime = StateMachine.totalGameTime + 3;
#if ADD_CLOSED_CAPTIONS
            if (closedCaptions.ContainsKey(closedCaption) == false)
                closedCaptions.Add(closedCaption, StateMachine.totalGameTime + 3);
            else
                closedCaptions[closedCaption] = targetTime;
#endif
        }

        //helper edit

        //helper terrain
        public List<Box> getBoxesInZone(Rectangle zone)
        {
            List<Box> returnList = new List<Box>();
            for (int x = zone.Left; x <= zone.Right; ++x)
            {
                if (x < 0) continue;
                if (x >= terrain.GetLength(0)) break;
                for (int z = zone.Top; z <= zone.Bottom; ++z)
                {
                    if (z < 0) continue;
                    if (z >= terrain.GetLength(1)) break;
                    foreach (Box box in terrain[x, z])
                    {
                        if (!returnList.Contains(box))
                            returnList.Add(box);
                    }
                }
            }
            return returnList;
        }
        //add box
        public void AddBox(Box newbox)
        {
            editor.edit.saveNeeded = true;
            newbox.position = editor.snap(newbox.position);
            newbox.size = FPSEditor.snap(newbox.size, editor.editSnapSize * 2);
            allBoxes.Add(newbox);
            //if(game1.rand.Next(5) == 0)
            //{
            //    spawns.Add(newbox.position + newbox.size.Y/2 * Vector3.Up);
            //}
            if (terrainActive)
            {
                BoundingBox bb = newbox.boundingBox;
                int minx = (int)Math.Floor((bb.Min.X + widthm / 2) / widthm);
                int maxx = (int)Math.Floor((bb.Max.X + widthm / 2) / widthm);
                int minz = (int)Math.Floor((bb.Min.Z + depthm / 2) / depthm);
                int maxz = (int)Math.Floor((bb.Max.Z + depthm / 2) / depthm);
                for (int x = minx; x <= maxx && x < terrain.GetLength(0); ++x)
                {
                    if (x < 0) continue;
                    for (int z = minz; z <= maxz && z < terrain.GetLength(1); ++z)
                    {
                        if (z < 0) continue;
                        terrain[x, z].Add(newbox);
                    }
                }
            }
            //newbox.originalPosition = newbox.position;
        }
        public Box AddBox(Vector3 position, Vector3 size, Color color)
        {
            Box newbox = new Box(size, position);
            newbox.color = color;
            AddBox(newbox);
            return newbox;
        }
        //remove box
        public void RemoveBox(Box box)
        {
            editor.edit.saveNeeded = true;
            if (terrainActive)
            {
                BoundingBox bb = box.boundingBox;
                Rectangle zone = getzone(bb);
                int minx = (int)Math.Floor((bb.Min.X + widthm / 2) / widthm);
                int maxx = (int)Math.Floor((bb.Max.X + widthm / 2) / widthm);
                int minz = (int)Math.Floor((bb.Min.Z + depthm / 2) / depthm);
                int maxz = (int)Math.Floor((bb.Max.Z + depthm / 2) / depthm);
                for (int x = minx; x <= maxx && x < terrain.GetLength(0); ++x)
                {
                    if (x < 0) continue;
                    for (int z = minz; z <= maxz && z < terrain.GetLength(1); ++z)
                    {
                        if (z < 0) continue;
                        if (terrain[x, z].Contains(box))
                            terrain[x, z].Remove(box);
                    }
                }
            }
            allBoxes.Remove(box);
        }
        // move box
        public void MoveBox(Vector3 newPosition, Box box)
        {
            editor.edit.saveNeeded = true;
            //Vector3 oldPosition = box.position;
            RemoveBox(box);
            box.position = newPosition;
            //Rectangle newzone = getzone(box.position - box.size / 2, box.position + box.size / 2);
            //Vector3 sizeSlop = new Vector3(0.01f);
            //BoundingBox bb1 = Game1.MakeBox(box.position, box.size - sizeSlop);
            //bool conflictFound = false;
            //for (int x = Math.Max(0, newzone.Left); 
            //    !conflictFound && x <= Math.Min(terrain.GetLength(0) - 1, newzone.Right); 
            //    ++x)
            //{
            //    for (int z = Math.Max(0, newzone.Top); 
            //        !conflictFound && z <= Math.Min(terrain.GetLength(1) - 1, newzone.Bottom); 
            //        ++z)
            //    {
            //        foreach(Box b in terrain[x,z])
            //        {
            //            BoundingBox bb2 = Game1.MakeBox(b.position, b.size);
            //            if(bb1.Intersects(bb2))
            //            {
            //                conflictFound = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            //if(conflictFound)
            //{
            //    box.position = oldPosition;
            //}
            AddBox(box);
        }
        //resize box
        public void ResizeBox(Vector3 newSize, Box box)
        {
            editor.edit.saveNeeded = true;
            //Vector3 oldPosition = box.position;
            RemoveBox(box);
            box.size = newSize;
            //Rectangle newzone = getzone(box.position - box.size / 2, box.position + box.size / 2);
            //Vector3 sizeSlop = new Vector3(0.01f);
            //BoundingBox bb1 = Game1.MakeBox(box.position, box.size - sizeSlop);
            //bool conflictFound = false;
            //for (int x = Math.Max(0, newzone.Left); 
            //    !conflictFound && x <= Math.Min(terrain.GetLength(0) - 1, newzone.Right); 
            //    ++x)
            //{
            //    for (int z = Math.Max(0, newzone.Top); 
            //        !conflictFound && z <= Math.Min(terrain.GetLength(1) - 1, newzone.Bottom); 
            //        ++z)
            //    {
            //        foreach(Box b in terrain[x,z])
            //        {
            //            BoundingBox bb2 = Game1.MakeBox(b.position, b.size);
            //            if(bb1.Intersects(bb2))
            //            {
            //                conflictFound = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            //if(conflictFound)
            //{
            //    box.position = oldPosition;
            //}
            AddBox(box);
        }
        //refresh box (so that the terrain chunks can update)
        public void RefreshBox(Box box)
        {
            editor.edit.saveNeeded = true;
            RemoveBox(box);
            AddBox(box);
        }
        //save level
        public void SaveLevel(string levelFilename)
        {
            if (levelFilename == editor.currentLevelFilename)
                editor.edit.saveNeeded = false;
            using (StreamWriter writer = new StreamWriter(File.Create(levelFilename)))
            {
                writer.WriteLine("playerSpawnPoint " +
                    csvWriteV3(playerSpawnPoint));
                writer.WriteLine("playerSpawnEuler " + //playerSpawnEuler
                    csvWriteV3(playerSpawnEuler));
                for (int i = 0; i < targets.Length; ++i)
                {
                    //BoundingSphere t = targets[i];
                    Target t = targets[i];
                    Vector3 startPosition = targetStarts[i];
                    writer.WriteLine("target " + csvWriteV3(startPosition) + ' ' +
                        t.Radius);
                }
                foreach (Box box in allBoxes)
                {
                    writer.WriteLine(
                        "box " +
                        csvWriteV3(box.position) + ' ' +
                        csvWriteV3(box.size) + ' ' +
                        csvWriteV4(box.color.ToVector4())
                        //+ ' ' + ((int)box.type).ToString()
                        );
                }
                //save gun
                foreach (Gun g in allguns)
                {
                    if (myGuns.Contains(g)) continue;
                    if (customGuns.Contains(g)) continue;
                    writer.WriteLine(csvWriteGun(g));
                }
            }
        }
        //load level
        public void loadlevel()
        {
            editor.levelUnedited = true;
            allBoxes.Clear();
            editor.edit.boxes.Clear();
            capturedTargets.Clear();
            editor.editGun = null;
            editor.edit.boxes.Clear();
            editor.edit.target = -1;
            for (int i = 0; i < allguns.Count; ++i)
            {
                if (!myGuns.Contains(allguns[i]))
                {
                    RemoveGun(allguns[i]);
                    --i;
                }
            }
            //targets = new BoundingSphere[0];
            targets = new Target[0];
            targetStarts = new Vector3[0];
            foreach (var bucket in terrain)
            {
                if (bucket != null)
                    bucket.Clear();
            }
            using (StreamReader reader = new StreamReader(File.Open(editor.currentLevelFilename, FileMode.Open)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    string[] tokens = line.Split(' ');
                    switch (tokens[0])
                    {
                        case "box":
                            Box newbox = new Box(csvParseV3(tokens[2]), csvParseV3(tokens[1]));
                            if (tokens.Length > 3)
                                newbox.color = new Color(csvParseV4(tokens[3]));
                            //newbox.color = monochrome(0.9f);
                            //newbox.type = BoxType.DEFAULT;
                            //if (tokens.Length > 4)
                            //    newbox.type = (BoxType)int.Parse(tokens[4]);
                            //allBoxes.Add(newbox);
                            ////if(game1.rand.Next(5) == 0)
                            ////{
                            ////    spawns.Add(newbox.position + newbox.size.Y/2 * Vector3.Up);
                            ////}
                            //BoundingBox bb = Game1.MakeBox(newbox.position, newbox.size);
                            //int minx = (int)Math.Floor((bb.Min.X + widthm / 2) / widthm);
                            //int maxx = (int)Math.Floor((bb.Max.X + widthm / 2) / widthm);
                            //int minz = (int)Math.Floor((bb.Min.Z + depthm / 2) / depthm);
                            //int maxz = (int)Math.Floor((bb.Max.Z + depthm / 2) / depthm);
                            //for (int x = minx; x <= maxx && x >= 0 && x < terrain.GetLength(0); ++x)
                            //{
                            //    for (int z = minz; z <= maxz && z >= 0 && z < terrain.GetLength(1); ++z)
                            //    {
                            //        terrain[x, z].Add(newbox);
                            //    }
                            //}
                            AddBox(newbox);
                            break;
                        case "target":
                            //BoundingSphere newTarget = new BoundingSphere(
                            //    csvParseV3(tokens[1]), float.Parse(tokens[2]));
                            Target newTarget = new Target(
                                csvParseV3(tokens[1]), float.Parse(tokens[2]));
                            AddTarget(newTarget);
                            break;
                        case "playerSpawnPoint":
                            playerSpawnPoint = csvParseV3(tokens[1]);
                            break;
                        case "playerSpawnEuler":
                            playerSpawnEuler = csvParseV3(tokens[1]);
                            break;
                        case "gun": //load gun
                            Gun g = new Gun(0.05f, 0);
                            csvParseGun(line, g);
                            //g.AddBullets(allbullets);
                            //allguns.Add(g);
                            AddGun(g);
                            break;
                    }
                }
            }
            editor.edit.saveNeeded = false;
            SaveSettings(singlePlayerSaveFileName); //to save filename
        }

        // helper csv
        //either add the value to the csv or pull the value from the csv
        //public static void ialize(bool serialize, ref float value, string csv, int index)
        //{
        //    if(serialize)
        //    {
        //        csv += value;
        //    }else
        //    {
        //        value = float.Parse(csv.Split(',')[index]);
        //    }
        //}
        //public static Gun ialize(bool serialize, Gun gun, string csv, int index)
        //{
        //    ialize(serialize, gun.bulletSpeed, csv, index++);
        //}
        //public static void reload(Gun gun, string text)
        //{
        //    string[] csv = text.Split(',');
        //    gun.bulletSpeed = float.Parse(csv[0]);
        //    gun.BulletSize = float.Parse(csv[1]);
        //    gun.startingBullets = int.Parse(csv[2]);
        //}
        //public static string serialize(Gun gun)
        //{
        //    return string.Format("{0},{1},{2}",
        //        gun.bulletSpeed,
        //        gun.BulletSize, 
        //        gun.startingBullets);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>returns null on fail</returns>
        public static SignalADSRState loadSignalASDRState(string filename)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(filename)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = line.Trim();
                    string[] ssv = line.Split(' ');
                    if (ssv[0][0] == '#') continue; //comment
                    else return parseCSVSignalADSRState(line);
                }
            }
            return null;
        }
        public static string csvString(params string[] entries)
        {
            StringBuilder builder = new StringBuilder();
            Action<string> w = (string text) =>
            {
                if (builder.Length > 0)
                    builder.Append(',');
                builder.Append(text);
            };
            for (int i = 0; i < entries.Length; ++i)
            {
                w(entries.ToString());
            }
            return builder.ToString();
        }
        /// <summary>
        /// Good for any array where the ToString can be Parsed by the data type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string writeObjectArray(Array input)
        {
            //length
            string write = input.Length.ToString() + ',';
            //items
            for (int i = 0; i < input.Length; ++i)
                write += input.GetValue(i).ToString() + ',';
            return write;
        }
        public static float[] readArrayFloat(string csvLine)
        {
            float[] output = null;
            string[] tokens = csvLine.Split(',');
            output = new float[int.Parse(tokens[0])];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = float.Parse(tokens[i+1]);
            }
            return output;
        }
        public static bool[] readArrayBool(string csvLine)
        {
            bool[] output = null;
            string[] tokens = csvLine.Split(',');
            output = new bool[int.Parse(tokens[0])];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = bool.Parse(tokens[i + 1]);
            }
            return output;
        }
        //public static string csvObject(params object[] entries)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    Action<string> w = (string text) =>
        //    {
        //        if (builder.Length > 0)
        //            builder.Append(',');
        //        builder.Append(text);
        //    };
        //    for (int i = 0; i < entries.Length; ++i)
        //    {
        //        w(entries.ToString());
        //    }
        //    return builder.ToString();
        //}
        public static string writeCsvSignalASDRState(SignalADSRState state)
        {
            return string.Format("attackP:{0},signalT:{1}", state.attackP, (int)state.signalT);
        }
        public void csvParseGun(string s, Gun g)
        {
            string[] tokens = s.Split(' ');
            string name = tokens[0];
            Vector3 pos = csvParseV3(tokens[1]);
            Vector4 quaternion = Quaternion.Identity.ToVector4();
            if (tokens.Length > 2)
                quaternion = csvParseV4(tokens[2]);
            //if (tokens.Length > 3)
            for (int i = 3; i < tokens.Length; ++i)
            {
                string[] pair = tokens[i].Split(':');
                switch (pair[0]) //key
                {
                    case "BulletSpeed":
                        g.bulletSpeed = float.Parse(pair[1]);
                        break;
                    case "BulletSize":
                        g.BulletSize = float.Parse(pair[1]);
                        break;
                    case "automaticFireDelayS":
                        g.automaticFireDelayS = float.Parse(pair[1]);
                        break;
                    case "bullets.Count":
                        int ammo = int.Parse(pair[1]);
                        for (int j = 0; j < ammo; ++j)
                        {
                            g.AddBullet();
                        }
                        break;
                    default:
                        if (gunSaveLoadVariables.ContainsKey(pair[0]))
                        {
                            gunSaveLoadVariables[pair[0]].read(g, pair[1]);
                        }
                        break;
                }
            }
            g.pos = pos;
            g.rot = Matrix.CreateFromQuaternion(new Quaternion(quaternion));
        }
        public string csvWriteGun(Gun g)
        {
            string dynamicVars = "";
            foreach (var a in gunSaveLoadVariables)
            {
                dynamicVars += a.Value.name + ':' + a.Value.write(g) + ' ';
            }
            return
                "gun " +
                csvWriteV3(g.pos) + ' ' +
                csvWriteQuaternion(Quaternion.CreateFromRotationMatrix(g.rot)) + ' ' +
                "BulletSpeed:" + g.bulletSpeed + ' ' +
                "BulletSize:" + g.BulletSize + ' ' +
                "automaticFireDelayS:" + g.automaticFireDelayS + ' ' +
                "bullets.Count:" + g.bullets.Count + ' ' +
                dynamicVars;
        }
        public static SignalADSRState parseCSVSignalADSRState(string csv)
        {
            SignalADSRState result = null;
            string[] values = csv.Split(',');
            if (values.Length > 0)
            {
                result = new SignalADSRState();
            }
            for (int i = 0; i < values.Length; ++i)
            {
                string[] tokens = values[i].Split(':');
                switch (tokens[0])
                {
                    case "attackP":
                        result.attackP = float.Parse(tokens[1]);
                        break;
                    case "signalT":
                        result.signalT = (SignalGeneratorType)int.Parse(tokens[1]);
                        break;
                }
            }
            return result;
        }
        public static string csvWriteV2(Vector2 input)
        {
            return string.Format("{0},{1}", input.X, input.Y);
        }
        public static string csvWriteV3(Vector3 input)
        {
            return string.Format("{0},{1},{2}", input.X, input.Y, input.Z);
        }
        public static string csvWriteQuaternion(Quaternion input)
        {
            return csvWriteV4(input.ToVector4());
        }
        public static string csvWriteV4(Vector4 input)
        {
            return string.Format("{0},{1},{2},{3}", input.X, input.Y, input.Z, input.W);
        }
        public static Vector2 csvParseV2(string input)
        {
            string[] c = input.Split(',');
            return new Vector2(float.Parse(c[0]),
                float.Parse(c[1]));
        }
        public static Vector3 csvParseV3(string input)
        {
            string[] c = input.Split(',');
            return new Vector3(float.Parse(c[0]),
                float.Parse(c[1]),
                float.Parse(c[2]));
        }
        public static Vector4 csvParseV4(string input)
        {
            string[] c = input.Split(',');
            return new Vector4(float.Parse(c[0]),
                float.Parse(c[1]),
                float.Parse(c[2]),
                float.Parse(c[3]));
        }
        public static string csv_writeFPSInput(FPSInput fpsi)
        {
            StringBuilder builder = new StringBuilder();
            Action<string> w = (string text) =>
            {
                if (builder.Length > 0)
                    builder.Append(',');
                builder.Append(text);
            };
            if (fpsi.forward)
            {
                w("forward");
            }
            if (fpsi.back)
            {
                w("back");
            }
            if (fpsi.left)
            {
                w("left");
            }
            if (fpsi.right)
            {
                w("right");
            }
            if (fpsi.shoot)
            {
                w("shoot");
            }
            return builder.ToString();
        }
        public static FPSInput csv_parseFPSInput(string csv)
        {
            FPSInput result = new FPSInput();
            string line = csv.Trim();
            string[] tokens = line.Split(',');
            foreach (string token in tokens)
            {
                string s = token.Trim();
                if (string.IsNullOrEmpty(s))

                    continue;
                switch (s)
                {
                    case "forward":
                        result.forward = true;
                        break;
                    case "back":
                        result.back = true;
                        break;
                    case "left":
                        result.left = true;
                        break;
                    case "right":
                        result.right = true;
                        break;
                    case "shoot":
                        result.shoot = true;
                        break;
                }
            }
            return result;
        }
        //public static bool csv_tryParseVec3(string input, out Vector3 v3)
        //{
        //    v3 = Vector3.Zero;
        //    string[] c = input.Split(',');
        //    if (c.Length < 3)
        //        return false;
        //    bool succeeded = true;
        //    if (c.Length < 1)
        //        succeeded &= !float.TryParse(c[0], out v3.X);
        //    if (c.Length < 1)
        //        succeeded &= !float.TryParse(c[0], out v3.X);
        //    if (c.Length < 1)
        //        succeeded &= !float.TryParse(c[0], out v3.X);
        //    return succeeded;
        //}

        
        //helper collsion
        /// <summary>
        /// returns surface normal of A
        /// </summary>
        /// <param name="bsA"></param>
        /// <param name="bsB"></param>
        /// <param name="dir"></param>
        /// <param name="pen"></param>
        /// <returns></returns>
        public bool intersectSphereSphere(BoundingSphere bsA, BoundingSphere bsB, out Vector3 dir, out float pen)
        {
            dir = Vector3.Zero;
            pen = 0;
            Vector3 toB = bsB.Center - bsA.Center;
            float lengthSq = toB.LengthSquared();
            float radiusSum = bsB.Radius + bsA.Radius;
            if (lengthSq <= radiusSum * radiusSum) //cheap
            {
                float length = (float)Math.Sqrt(lengthSq); //expensive
                dir = toB / length;
                pen = length - radiusSum;
                return true;
            }
            return false;
        }
        public static float? IntersectRayGun(Ray ray, Gun G)
        {
            Vector3 up = Vector3.Transform(Vector3.Up, G.rot);
            Vector3 p = G.pos - up * G.size.Y / 2;
            Matrix r = G.rot;
            Matrix rI = Matrix.Invert(r);
            Vector3 s = G.size;
            //get ray positions offset
            Vector3 sW = ray.Position;
            Vector3 sO = sW - p;
            //rotate into local space
            Vector3 sL = Vector3.Transform(sO, rI);
            //rotate direction into local space
            Vector3 dW = ray.Direction;
            Vector3 dL = Vector3.Transform(dW, rI);
            Ray rL = new Ray(sL, dL);
            BoundingBox b = GameMG.MakeBox(Vector3.Zero, G.size);
            float? hit = rL.Intersects(b);
            //if (hit.HasValue && hit.Value > 0)
            //{
            //    return true;
            //}
            return hit;
        }
        //TODO: OPTIMIZE: add 'Point[] getzone(Ray ray)'
        //warn: not prepared for when ray is aligned with cell edge
        //Point[] getzone(Ray ray, float length)
        //{
        //    List<Point> cells = new List<Point>();
        //    Vector2 start = xz(ray.Position);
        //    Vector3 end3d = ray.Position + ray.Direction * length;
        //    Vector2 end = xz(end3d);
        //    Vector2 dir = end - start;
        //    dir.Normalize();
        //    Point startCoord = worldToCoord(ray.Position);
        //    Point endCoord = worldToCoord(end3d);
        //    Point curCoord = startCoord;
        //    Vector2 currentPosition = xz(ray.Position);
        //    while (curCoord != endCoord && !(curCoord.X < 0 || curCoord.Y < 0
        //     || curCoord.X >= terrain.GetLength(0) || curCoord.Y >= terrain.GetLength(1)))
        //    {
        //        cells.Add(curCoord);
        //        Vector3 center = coordToWorld(curCoord);
        //        float right = center.X + widthm / 2;
        //        float bottom = center.Z + depthm / 2;
        //        float remainderX = right - currentPosition.X;
        //        float remainderY = bottom - currentPosition.Y;
        //        if (dir.X < 0)
        //            remainderX = -(widthm - remainderX);
        //        if (dir.Y < 0)
        //            remainderY = -(depthm - remainderY);
        //        float magnitudeY = Math.Abs(remainderY / dir.Y);
        //        float magnitudeX = Math.Abs(remainderX / dir.X);
        //        if (magnitudeX > magnitudeY)
        //        {
        //            curCoord.Y += Math.Sign(dir.Y);
        //            currentPosition += dir * magnitudeY;
        //        }
        //        else if (magnitudeY > magnitudeX)
        //        {
        //            curCoord.X += Math.Sign(dir.X);
        //            currentPosition += dir * magnitudeX;
        //        }
        //        else
        //        {
        //            curCoord += new Point(Math.Sign(dir.X), Math.Sign(dir.Y));
        //            float magnitude = magnitudeX = magnitudeY;
        //            currentPosition += dir * magnitude; //arbitrary
        //        }
        //    }
        //    cells.Add(endCoord);
        //    return cells.ToArray();
        //}
        public Rectangle getzone(BoundingSphere bs)
        {
            return getzone(GameMG.MakeBox(bs.Center, new Vector3(bs.Radius)));
        }
        public Rectangle getzone(Vector3 min, Vector3 max)
        {
            int minx = (int)Math.Floor((min.X + widthm / 2) / widthm);
            int maxx = (int)Math.Floor((max.X + widthm / 2) / widthm);
            int minz = (int)Math.Floor((min.Z + depthm / 2) / depthm);
            int maxz = (int)Math.Floor((max.Z + depthm / 2) / depthm);
            return new Rectangle(minx, minz, maxx - minx, maxz - minz);
        }
        public Rectangle getzone(BoundingBox bb)
        {
            return getzone(bb.Min, bb.Max);
        }
        public static Vector3 getClosestPoint(Vector3 point, BoundingBox boxVolume)
        {
            Vector3 closestPoint = point;
            closestPoint = Vector3.Max(closestPoint, boxVolume.Min);
            closestPoint = Vector3.Min(closestPoint, boxVolume.Max);
            //Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
            return closestPoint;
        }
        Vector3 closestPointSphereGun(BoundingSphere sphereVolume, Gun gun)
        {
            Vector3 gunDown = Vector3.Transform(Vector3.Down, gun.rot);
            Vector3 gunCenter = gun.pos + gunDown * gun.size.Y * 0.5f;
            Vector3 toSphereWorld = sphereVolume.Center - gunCenter;
            Vector3 toSphereLocal = Vector3.Transform(toSphereWorld, Matrix.Invert(gun.rot));
            Vector3 toCPLocal = toSphereLocal; //to closest point local
            BoundingBox boxVolume = GameMG.MakeBox(Vector3.Zero, gun.size);
            toCPLocal = Vector3.Max(toCPLocal, boxVolume.Min);
            toCPLocal = Vector3.Min(toCPLocal, boxVolume.Max);
            //Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
            Vector3 toCPWorld = Vector3.Transform(toCPLocal, gun.rot);
            return gunCenter + toCPWorld;
        }
        bool sphereIntersectsGun(BoundingSphere sphereVolume, Gun gun)
        {
            Vector3 cp = closestPointSphereGun(sphereVolume, gun);
            Vector3 toCP = cp - sphereVolume.Center;
            return toCP.LengthSquared() <= sphereVolume.Radius * sphereVolume.Radius;
        }
        Vector3 closestPointSphereOBB(BoundingSphere sphereVolume, Vector3 obbPosition, Vector3 obbSize, Matrix obbRotation)
        {
            //Vector3 gunDown = vec
            Vector3 toSphereWorld = sphereVolume.Center - obbPosition;
            Vector3 toSphereLocal = Vector3.Transform(toSphereWorld, Matrix.Invert(obbRotation));
            Vector3 toCPLocal = toSphereLocal; //to closest point local
            BoundingBox boxVolume = GameMG.MakeBox(Vector3.Zero, obbSize);
            toCPLocal = Vector3.Max(toCPLocal, boxVolume.Min);
            toCPLocal = Vector3.Min(toCPLocal, boxVolume.Max);
            //Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
            Vector3 toCPWorld = Vector3.Transform(toCPLocal, obbRotation);
            return obbPosition + toCPWorld;
        }
        bool sphereIntersectsOBB(BoundingSphere sphereVolume, Vector3 obbPosition, Vector3 obbSize, Matrix obbRotation)
        {
            Vector3 cp = closestPointSphereOBB(sphereVolume, obbPosition, obbSize, obbRotation);
            Vector3 toCP = cp - sphereVolume.Center;
            return toCP.LengthSquared() <= sphereVolume.Radius * sphereVolume.Radius;
        }
        float penetration(BoundingSphere sphereVolume, BoundingBox boxVolume)
        {
            Vector3 closestPoint = getClosestPoint(sphereVolume.Center, boxVolume);
            Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
            float dist = toClosestPoint.Length();
            float penetration = sphereVolume.Radius - dist;
            return penetration;
        }
        public static bool intersectSphereBox(/*ref PhysicsState sphereState,*/
            BoundingSphere sphereVolume, BoundingBox boxVolume, out Vector3 resolveDir, out float resolvePenetration)
        {
            resolveDir = Vector3.Zero;
            resolvePenetration = 0;
            Vector3 closestPoint = getClosestPoint(sphereVolume.Center, boxVolume);
            Vector3 toClosestPoint = closestPoint - sphereVolume.Center;
            float dist = toClosestPoint.Length();
            float penetration = sphereVolume.Radius - dist;
            Vector3 sphereSurfaceNormal = (toClosestPoint / dist);
            //if (dist == 0)
            //{
            Vector3 boxNormal = -sphereSurfaceNormal;
            if (closestPoint == sphereVolume.Center)
            {
                Vector3 boxSize = boxVolume.Max - boxVolume.Min;
                Vector3 boxCenter = boxVolume.Min + boxSize / 2;
                Vector3 fromBox = closestPoint - boxCenter;
                float maxValue = Math.Max(Math.Abs(fromBox.X), Math.Max(Math.Abs(fromBox.Y), Math.Abs(fromBox.Z)));
                //-1, 0, 1, (min, 0, max)
                Vector3 dir = Vector3.Zero;
                float curr = 0; //current region
                float maxr = float.MinValue; //max region
                float curp = 0; //current penetration
                Func<float, float, float, bool> testaxis = (float v, float min, float max) =>
                   {
                       float range = max - min;
                       float relative = v - min;
                       float normalize = relative / range;
                       float region = normalize * 2 - 1;
                       float absreg = Math.Abs(region);
                       float radiation = region * range / 2;
                       float edge = (float)Math.Sign(region) * range / 2;
                       float halfSize = range / 2;
                       float pen = halfSize - absreg * halfSize;
                       if (absreg > maxr)
                       {
                           maxr = absreg;
                           curr = region;
                           curp = pen;
                           return true;
                       }
                       return false;
                   };
                //TODO: get diagonals when two penetration values are equivalent
                if (testaxis(closestPoint.X, boxVolume.Min.X, boxVolume.Max.X))
                {
                    dir = Vector3.Zero;
                    dir.X = curr;
                    penetration = curp;
                }
                if (testaxis(closestPoint.Y, boxVolume.Min.Y, boxVolume.Max.Y))
                {
                    dir = Vector3.Zero;
                    dir.Y = curr;
                    penetration = curp;
                }
                if (testaxis(closestPoint.Z, boxVolume.Min.Z, boxVolume.Max.Z))
                {
                    dir = Vector3.Zero;
                    dir.Z = curr;
                    penetration = curp;
                }
                //penetration = Vector3.Dot(sphereSurfaceNormal, boxSize / 2) - dist;
                //}
                boxNormal = Vector3.Normalize(dir);
                sphereSurfaceNormal = -boxNormal;
                //game1.add3DLine(boxCenter, boxCenter + boxNormal * 1000, Color.White);
            }

            if (penetration > 0)
            {
                if (dist >= 0)
                {
                    //sphereState.pos += boxNormal * penetration;
                    resolveDir = boxNormal;
                    resolvePenetration = penetration;
                    //float speedTowardsBox = Vector3.Dot(sphereSurfaceNormal, sphereState.vel);
                    //if (speedTowardsBox > 0)
                    //{
                    //    float restitution = 1;
                    //    sphereState.vel -= (1 + restitution) * sphereSurfaceNormal * speedTowardsBox;
                    //}
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="direction">The direction the physics state is moving against</param>
        /// <param name="penetration"></param>
        /// <param name="restitution"></param>
        /// <param name="friction"></param>
        public void resolvePenetration(ref PhysicsState body, Vector3 direction, float penetration, float restitution, float friction)
        {
            body.pos -= direction * penetration;
            float speedTowardsBox = Vector3.Dot(direction, body.vel);
            if (speedTowardsBox > 0)
            {
                body.vel -= (1 + restitution) * direction * speedTowardsBox;
                //if (restitution == 0)
                //{
                //bodyState.force -= gravity;
                //}
                Vector3 tangent = body.vel - speedTowardsBox * direction;
                body.vel -= tangent * friction;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="closestPoint"></param>
        /// <param name="boxVolume"></param>
        /// <param name="dir">Normal of the box surface</param>
        /// <param name="penetration"></param>
        public static void intersectBoxPoint(Vector3 closestPoint, BoundingBox boxVolume, out Vector3 dir, out float penetration)
        {
            float maxDistNorm = 0; //max region
            float curLocalPosNorm = 0; //current region
            float curPen = 0; //current penetration
            dir = Vector3.Zero;
            penetration = 0;
            Func<float, float, float, bool> testaxis = (float v, float min, float max) =>
            {
                float range = max - min; //absolute size of region
                float local = v - min; //local absolute position of v
                float localPolar = 0f;  //local absolute position normalized to [0, 1]
                if (local != 0) //avoid divide by 0
                    localPolar = local / range;
                float localNorm = localPolar * 2 - 1; //local position normalized to [-1, 1]
                float distNorm = Math.Abs(localNorm); //normalized distance
                float dist = localNorm * range / 2; //world distance
                float edge = (float)Math.Sign(localNorm) * range / 2; //local position of closest edge
                float halfSize = range / 2; //half world size
                float pen = halfSize - distNorm * halfSize; //distance from edge
                //pen = halfSize - dist
                if (distNorm >= maxDistNorm) //greater normalized distance
                {
                    maxDistNorm = distNorm; //current greatest normalized distance
                    curLocalPosNorm = localNorm; //current normalized position
                    curPen = pen; //current absolute penetration
                    return true;
                }
                return false;
            };
            //TODO: get diagonals when two penetration values are equivalent
            if (testaxis(closestPoint.X, boxVolume.Min.X, boxVolume.Max.X))
            {
                dir = Vector3.Zero;
                dir.X = curLocalPosNorm;
                penetration = curPen;
            }
            if (testaxis(closestPoint.Y, boxVolume.Min.Y, boxVolume.Max.Y))
            {
                if (penetration != curPen)
                {
                    dir = Vector3.Zero;
                    penetration = curPen;
                }else
                {
                    penetration = (float)Math.Sqrt(curPen * curPen + curPen * curPen);
                }
                dir.Y = curLocalPosNorm;
            }
            if (testaxis(closestPoint.Z, boxVolume.Min.Z, boxVolume.Max.Z))
            {
                if (penetration != curPen)
                {
                    dir = Vector3.Zero;
                    penetration = curPen;
                }
                else
                {
                    penetration = (float)Math.Sqrt(curPen * curPen + curPen * curPen);
                }
                dir.Z = curLocalPosNorm;
            }
            if (dir.LengthSquared() > 0) dir.Normalize();
        }
        // draw
        public override void Draw(GameTime gameTime)
        {//start draw
            float et = (float)gameTime.ElapsedGameTime.TotalSeconds;
            drawfps.Update( et);
            float tt = (float)gameTime.TotalGameTime.TotalSeconds;

            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.BlendState = BlendState.Opaque;

            //draw clear deferred buffers and shadows
            //List<object> clearList = new List<object>();
            //clearList.Add(deferredBuffer0, deferredBufferDepth);
            //clearList.AddRange(cascadeShadowMaps);
            //GraphicsDevice.SetRenderTargets(deferredBuffer0, deferredBufferDepth, cascadeShadowMaps);
            render3d.Clear();


            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            //r:nx g:ny b:nz a:depth
            //setup draw shadows
            Vector3 offset = Vector3.Zero;
            if (editor.edit.active && editor.editOutOfBody)
                offset = bodyState.pos;
            else
                offset = playerCam.pos;
            render3d.PreRenderShadowSetup(offset, playerCam.view, playerCam.near, playerCam.fov);

            render3d.Render(DrawCore, playerCam);
            //draw process

            ////draw 2d
            Rectangle debugWindow = Backpack.percentage(ViewBounds, 0, 0, 0.1f, 0.1f);
            string[] debugText = {
                string.Format("U:{0:n2}/D:{1:n2}", updatefps.fps, drawfps.fps) ,
            editor.currentLevelFilename
            };
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            
            if (game1.kdown(Keys.OemComma))
            {
                spriteBatch.Draw(render3d.deferredBuffer0, ViewBounds, Color.White);
            }
            for (int i = 0; i < debugText.Length; ++i)
            {
                Rectangle r = Backpack.percentage(debugWindow, 0, 0, 1, 0);
                r.Height = 30;
                r.Y = r.Height * i;
                game1.drawString(debugText[i], r, monochrome(1));
            }
            if (game1.kdown(Keys.Home))
            {
                Rectangle preview2Rect = Backpack.percentage(ViewBounds, .8f, 0.8f, 0.2f, 0.2f);
                if (game1.kdown(Keys.OemPeriod))
                    preview2Rect = ViewBounds;
                spriteBatch.Draw(render3d.deferredBufferDepth, preview2Rect, Color.White);
                for (int i = 0; i < Render3D.shadowMapCount; ++i)
                {
                    Rectangle previewRect = Backpack.percentage(ViewBounds, 0, 0.2f + 0.2f * (float)i, 0.2f, 0.2f);
                    spriteBatch.Draw(render3d.cascadeShadowMaps[i], previewRect, Color.White);
                }
            }
            //if (frustum.Contains(Vector3.Zero) == ContainmentType.Contains)
            //{
            //    DrawWorldLabel2D(Vector3.Zero, "ORIGIN", 100, 30);
            //}
            spriteBatch.End();
        }//end draw

        void DrawCore(int i, int pass)
        {
            const int finalPass = Render3D.shadowMapCount;
            //draw boxes
            render3d.instanceCubeIterator = 0;
            foreach (Box b in allBoxes)
            {
                render3d.instanceTransforms[render3d.instanceCubeIterator].position = new Vector4(b.position, 0);
                render3d.instanceTransforms[render3d.instanceCubeIterator++].scale = new Vector4(b.size / 2, 1);
            }

            //Matrix shadowView = playerCam.view;// Matrix.CreateLookAt(bodyState.pos - lightDir * distance / 2, bodyState.pos, Vector3.Up);
            //Matrix shadowProjection = playerCam.projection;// Matrix.CreateOrthographic(100, 100, 1 / distance, distance);

            render3d.SetDeferredParametersGeometry(playerCam, Matrix.Identity, Matrix.Identity);
            render3d.deferfx.CurrentTechnique = render3d.deferfx.Techniques["Instanced"];

            render3d.instanceVertexBuffer.SetData(render3d.instanceTransforms, 0, render3d.instanceCubeIterator);
            GraphicsDevice.SetVertexBuffers(new VertexBufferBinding(render3d.instanceCubeVertexBuffer, 0), new VertexBufferBinding(render3d.instanceVertexBuffer, 0, 1));
            GraphicsDevice.Indices = render3d.instanceCubeIndexBuffer;

            render3d.deferfx.CurrentTechnique.Passes[pass].Apply();
            //        GraphicsDevice.RasterizerState = game1.wireFrameRs;
            GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, 16, render3d.instanceCubeIterator);

            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            //draw guns
            foreach (Gun g in allguns)
            {
                drawgun(g, 1, false, Color.TransparentBlack, pass);
            }

            // draw player
            for (int j = 0; j < bodyc; ++j)
            {
                //break;
                if (!is3rdPerson && j == 0) continue;
                //game1.DrawModel(
                //    game1.sphereModel,
                //    Matrix.CreateScale(dropValue / 2) *
                //    Matrix.CreateTranslation(bodyState.pos + Vector3.Up * (dropValue * (float)bodyc / 2 - dropValue / 2) + drop * j),
                //    playerCam.view,
                //    playerCam.projection,
                //    monochrome(0.5f),
                //    true,
                //    null);
                DrawModel(
                    game1.sphereModel,
                    Matrix.CreateScale(dropValue / 2) *
                    Matrix.CreateTranslation(bodyState.pos + Vector3.Up * (dropValue * (float)bodyc / 2 - dropValue / 2) + drop * j),
                    Matrix.Identity,
                    pass);
            }

            //draw bullets
            render3d.instanceCubeIterator = 0;
            foreach (Bullet b in allbullets)
            {
                if (true)//b.phy.vel.LengthSquared() == 0)
                {
                    render3d.instanceTransforms[render3d.instanceCubeIterator].position = new Vector4(b.phy.pos, 0);
                    render3d.instanceTransforms[render3d.instanceCubeIterator++].scale = new Vector4(new Vector3(b.size / 2), 1);
                    continue;
                }
                Vector3 vel = b.phy.vel;
                Vector3 pvel = vel / updatefps.fps;
                Vector3 A = b.phy.pos;
                Vector3 B = A + pvel;
                //volume of sphere = (4/3) * pi * r^3
                float volume =
                    (4.0f / 3.0f) *
                    MathHelper.Pi *
                    (float)Math.Pow(b.size / 2, 3);
                //float s = bul.size;
                float l = (B - A).Length();
                float s = 0;
                //float amt = 10;
                //float currentRadius = b.size / 2;
                //float capsuleVolume = 0;
                //int iterations = 0;
                //float dif;
                //do
                //{
                ////get volume of capsule currently
                //float factorA = MathHelper.Pi * (currentRadius * currentRadius);
                //float factorB = (4f / 3f) * currentRadius + l;
                //capsuleVolume = factorA * factorB;
                ////check if its within a threshhold of the real volume
                //float vdelta = volume - capsuleVolume;
                //dif = Math.Abs(vdelta);
                ////shrink or grow radius by amount
                //currentRadius += 0.01f * vdelta;
                ////decrease amount
                ////amt *= 0.5f;
                ////if amount is very low than give up
                //iterations++;
                //} while (dif > 0.01f || iterations < 30);
                //s = currentRadius * 2;
                {
                    //volume of cube = w * h * d
                    //volume / d = w * h
                    //h = w
                    //volume / d = w ^ 2
                    //w = sqrt(volume / d)
                    s = (float)Math.Sqrt(volume / l);
                }
                if (b.skipNextAdvance)
                    l = 0.01f;
                Vector3 up = Vector3.Up;
                float pv = pvel.Length();
                Vector3 dir = pvel / pv;
                if (Vector3.Dot(dir, up) > 0.98f)
                    up = Vector3.Right;
                Vector3 start = A;
                Vector3 end = B;
                Matrix look = Matrix.CreateLookAt(start, end, up);
                float length = l;
                Matrix orientation = Matrix.Invert(look);
                Matrix world =
                    Matrix.CreateTranslation(0, 0, -1) *
                    Matrix.CreateScale(s / 2, s / 2, length / 2) *
                    orientation;
                DrawModel(game1.cubeModel, world, orientation, pass);

                //Vector3 scale = new Vector3(s / 2);// - 0.1f);
                //instanceTransforms[instanceCubeIterator].position = new Vector4(start, 0);
                //instanceTransforms[instanceCubeIterator++].scale = new Vector4(scale, 1);
                //instanceTransforms[instanceCubeIterator].position = new Vector4(end, 0);
                //instanceTransforms[instanceCubeIterator++].scale = new Vector4(scale, 1);
            }

            render3d.SetDeferredParametersGeometry(playerCam, Matrix.Identity, Matrix.Identity);
            render3d.deferfx.CurrentTechnique = render3d.deferfx.Techniques["Instanced"];
            ModelMeshPart sphMeshPart = surfaceSphere.Meshes[0].MeshParts[0];
            //ModelMeshPart sphMeshPart = game1.sphereModel.Meshes[0].MeshParts[0];
            render3d.instanceVertexBuffer.SetData(render3d.instanceTransforms, 0, render3d.instanceCubeIterator);
            GraphicsDevice.SetVertexBuffers(new VertexBufferBinding(sphMeshPart.VertexBuffer, 0), new VertexBufferBinding(render3d.instanceVertexBuffer, 0, 1));
            GraphicsDevice.Indices = sphMeshPart.IndexBuffer;
            render3d.deferfx.CurrentTechnique.Passes[pass].Apply();
            GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, sphMeshPart.PrimitiveCount, render3d.instanceCubeIterator);

            if (i == finalPass)
            {
                game1.DrawAll3dLines(playerCam.view, playerCam.projection);
                game1.Flush3dLines();

                //draw targets
                foreach (Target t in targets)
                {
                    game1.DrawModel(game1.sphereModel,
                        Matrix.CreateScale(t.Radius) *
                        Matrix.CreateTranslation(t.Center),
                        playerCam.view, playerCam.projection,
                        monochrome(0.9f),
                        true);
                }

                //draw selected boxes
                if (editor.edit.active)
                {
                    foreach (Box b in editor.edit.boxes)
                    {
                        game1.DrawModel(game1.cubeModel,
                            Matrix.CreateScale(b.size / 2) * Matrix.CreateScale(1.001f) *
                            Matrix.CreateTranslation(b.position),
                            playerCam.view,
                            playerCam.projection,
                            Color.Red,
                            true);
                    }
                    if (editor.editGun != null)
                    {
                        drawgun(editor.editGun, 1.01f, true, Color.Red, 0);
                    }
                }
                // draw wireframe
                GraphicsDevice.RasterizerState = game1.wireFrameRs;

                for (int j = 0; j < Render3D.shadowMapCount; ++j)
                {
                    break;
                    var corners = new BoundingFrustum(render3d.shadowViews[j] * render3d.shadowProjections[j]).GetCorners();
                    for (int k = 0; k < 4; ++k)
                    {
                        game1.add3DLine(corners[k], corners[(k + 1) % 4], Color.White);
                        game1.add3DLine(corners[k + 4], corners[(k + 1) % 4 + 4], Color.Yellow);
                        game1.add3DLine(corners[k], corners[k + 4], Color.White, Color.Yellow);
                    }
                }

                //draw hover box
                if (editor.edit.active)
                {
                    if (editor.edit.hoverbox != null)
                    {
                        game1.DrawModel(
                            game1.cubeModel,
                            Matrix.CreateScale(editor.edit.hoverbox.size / 2) *
                            Matrix.CreateScale(1.002f) *
                            Matrix.CreateTranslation(editor.edit.hoverbox.position),
                            playerCam.view,
                            playerCam.projection,
                            Color.Blue,
                            false);
                    }
                    if (editor.editHoverGun != null)
                    {
                        drawgun(editor.editHoverGun, 1.02f, true, Color.Yellow, 0);
                    }
                }

                for (int j = 0; j < Render3D.shadowMapCount; ++j)
                    game1.DrawModel(game1.sphereModel, Matrix.Invert(render3d.shadowViews[j]), playerCam.view, playerCam.projection, Color.Blue, false);

                GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            }//end if i == 1
        }

        //draw boxes
        void DrawBoxes()
        {
        }
        //public override void Draw(GameTime gameTime)
        //{

        //            //if (!loadContentComplete)
        //            //    return;
        //            base.Draw(gameTime);

        //            //draw time of day
        //            //float sin = (float)(Math.Sin(tt/40f));
        //            //float min = 0;
        //            //float max = 0.98f;
        //            //float range = max - min;
        //            //float time = sin * .5f + .5f;
        //            //GraphicsDevice.Clear(monochrome(min + time * range));

        //            //GraphicsDevice.SetRenderTargets(backbuffrt, depthrt);

        //            //draw 3d, draw opaque
        //GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        //            //draw training
        //            //draw lootz detection
        //            //game1.add3DLine(pvtLootz.position, bodyState.pos, Color.Magenta);
        //            //game1.add3DLine(allBoxes.Last().position, bodyState.pos, Color.Magenta);
        //            ////draw gaz
        //            //game1.DrawModel(game1.cubeModel, Matrix.CreateTranslation(gazPosition),
        //            //    camera.view, camera.projection,
        //            //    monochrome(0.4f));

        //            // draw dialog 3d
        //            //float ratio = (float)dialogBox.Width / (float)dialogBox.Height;
        //            //ApplyBasicEffect(
        //            //    Matrix.CreateScale(ratio, 1, 1) *
        //            //    Matrix.CreateTranslation(0, 2, 0), camera.view, camera.projection, dialogRt);
        //            //DrawScreenQuad();
        //            //Matrix rotation = Matrix.Identity;
        //            //narrativeBoxRotation += et * narrativeBoxExcitement * 10;
        //            //if (narrativeBoxExcitement > 0)
        //            //    rotation = Matrix.CreateRotationY(narrativeBoxRotation);
        //            //game1.DrawModel(game1.cubeModel,
        //            //    Matrix.CreateScale(narrativeBox.size / 2) *
        //            //    rotation *
        //            //    Matrix.CreateTranslation(narrativeBox.position),
        //            //    camera.view,
        //            //    camera.projection,
        //            //    narrativeBox.color);

        //            var be = game1.cubeModel.Meshes[0].Effects[0] as BasicEffect;
        //            be.SpecularColor = monochrome(0.0f).ToVector3();
        //            be.SpecularPower = 1;

        //            game1.DrawAll3dLines(playerCam.view, playerCam.projection);
        //            game1.Flush3dLines();

        //            /*Matrix skyProj = Matrix.CreatePerspectiveFieldOfView(
        //                MathHelper.ToRadians(camera.fov),
        //                GraphicsDevice.Viewport.AspectRatio, 1, 2000);*/
        //            //sky.Draw(playerCam.view, playerCam.projection);
        //            //occlusionCircles.Clear();

        //            Vector3 camRight = Vector3.Transform(Vector3.Right, playerCam.rotation3D);
        //BoundingFrustum frust = new BoundingFrustum(playerCam.view * playerCam.projection);

        //            //draw edit 3d
        //            if (edit.active)
        //            {
        //                //draw player, draw spawn
        //                Matrix playerSpawnRot =
        //                    Matrix.CreateFromYawPitchRoll(playerSpawnEuler.Y,
        //                        playerSpawnEuler.X,
        //                        playerSpawnEuler.Z);
        //                game1.DrawModel(game1.cubeModel,
        //                    Matrix.CreateScale(dropValue / 2, 1, dropValue / 2) *
        //                    playerSpawnRot *
        //                    Matrix.CreateTranslation(playerSpawnPoint),
        //                    playerCam.view,
        //                    playerCam.projection,
        //                    Color.Purple,
        //                    false);
        //                Vector3 forward = Vector3.Transform(Vector3.Forward, playerSpawnRot);
        //                game1.add3DLine(playerSpawnPoint, playerSpawnPoint + forward, Color.Green);

        //                //draw target starting positions
        //                for (int i = 0; i < targets.Length; ++i)
        //                {
        //                    //BoundingSphere t = targets[i];
        //                    Target t = targets[i];
        //                    Vector3 start = targetStarts[i];
        //                    game1.add3DLine(t.Center, start, Color.Green);
        //                    game1.DrawModel(game1.sphereModel,
        //                        Matrix.CreateScale(0.05f) *
        //                        Matrix.CreateTranslation(start),
        //                        playerCam.view,
        //                        playerCam.projection,
        //                        Color.Purple,
        //                        false);
        //                }
        //            }

        //// draw boxes
        ////List<Box> glassBoxes = new List<Box>();
        //Rectangle zone = getzone(Game1.MakeBox(bodyState.pos, new Vector3(50)));
        //List<Box> boxes = getBoxesInZone(zone);
        ////foreach (Box b in allBoxes)
        //foreach (Box b in boxes)
        //{
        //    //if (b.color.A < 0.01f)
        //    //{
        //    //    continue; //not prepared to handle invisibles
        //    //}
        //    Texture2D texture = game1.pixel;
        //    Color color = monochrome(0.9f, 1, 90f / 360f, 0.2f);
        //    bool lightingEnabled = true;
        //    Vector2 uvScale = new Vector2(1.05f);
        //    //basicfx.Parameters["EmissiveColor"].SetValue(Vector3.Zero);
        //    //if (b.embedsBulletOnImpact)
        //    //switch (b.type) {
        //    //    case BoxType.STICK:
        //    //        color = monochrome(.75f);// 9f);// 10.75f);
        //    //        texture = soiltx;
        //    //        uvScale = new Vector2(0.5f);
        //    //        //color = game1.hsl2Rgb(98f/360f, .8f, 0.3f);
        //    //        break;
        //    //    case BoxType.REFLECT:
        //    //        //if(b.restitution == 1)
        //    //        //{
        //    //        //    color = monochrome(0.2f);
        //    //        //    texture = basetx;
        //    //        //    uvScale = new Vector2(0.01f);
        //    //        //    //basicfx.Parameters["EmissiveColor"].SetValue(monochrome(0.5f).ToVector3());
        //    //        //}
        //    //        //else
        //    //        //{
        //    //        color = monochrome(1f);
        //    uvScale = new Vector2(1 / editSnapSize / 16);// 0.25f);
        //                                                 //uvScale = new Vector2(0.25f);
        //                                                 //texture = basetx;
        //    texture = tileTx;
        //    //texture = frameTx;
        //    //texture = checker4Tx;
        //    //texture = game1.pixel;
        //    //texture = metaltx;
        //    //SetBasicHlslWallTx(metaltx, new Vector2(0.5f));
        //    //SetBasicHlslWallTx(tileTx, new Vector2(0.5f));
        //    SetBasicHlslWallTx(game1.pixel, new Vector2(0.5f));
        //    //        //color = game1.hsl2Rgb(0, 0, 0.9f);
        //    //        //}
        //    //        break;
        //    //    case BoxType.FLATTEN:
        //    //        color = monochrome(0.5f);
        //    //        texture = diagonalTx;
        //    //        break;
        //    //    case BoxType.SLOW:
        //    //        color = monochrome(1.0f, 0.2f);
        //    //        break;
        //    //    case BoxType.SLIDE:
        //    //        texture = metaltx;
        //    //        color = monochrome(0.3f, 0.4f);
        //    //        break;
        //    //    case BoxType.DEFAULT:
        //    //        color = monochrome(0.9f);
        //    //        uvScale = new Vector2(0.5f);
        //    //        texture = metaltx;
        //    //        break;
        //    //    default:
        //    //        color = Color.Magenta;
        //    //        break;
        //    //}
        //    //texture = game1.pixel;
        //    //if (color.A < 255)
        //    //{
        //    //    glassBoxes.Add(b);
        //    //    b.color = color;
        //    //    continue;
        //    //}
        //    //if (game1.kdown(Keys.OemComma))
        //    //{
        //    //    //float minExtent = Math.Min(b.size.X, Math.Min(b.size.Y, b.size.Z)) / 2;
        //    //    //Vector3 screenPos = Vector3.Transform(b.position, )
        //    //    //float radius = Vector3
        //    //    //if (frust.Intersects(Game1.MakeBox(b.position, b.size)))
        //    //    //{
        //    //game1.DrawModel(cubeModel,
        //    //Matrix.CreateScale(b.size / 2) *
        //    //Matrix.CreateTranslation(b.position), playerCam.view, playerCam.projection,
        //    //color, lightingEnabled,
        //    //texture);
        //    //    //}
        //    //}
        //    //else /*if(false)*/
        //    //{
        //    //    //GraphicsDevice.RasterizerState = game1.wireFrameRs;
        //    //    //    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //    DrawModel(cubeModel,
        //    Matrix.CreateScale(b.size / 2) *
        //    Matrix.CreateTranslation(b.position),
        //    //Matrix.Identity,
        //    playerCam.view, playerCam.projection,
        //    color,
        //    texture,
        //    null, uvScale, lightingEnabled);// normaltx);
        //    //}
        //    //else
        //    //{
        //    //    instanceTransforms[instanceIterator++] =
        //    //    Matrix.CreateScale(b.size / 2) *
        //    //    Matrix.CreateTranslation(b.position);
        //    //}
        //}
        ////DrawInstances(game1.cubeModel, Matrix.Identity, playerCam.view, playerCam.projection, Color.White, tileTx, null, new Vector2(0.5f), true);
        //SetBasicHlslWallTx(game1.pixel, new Vector2(0.5f));

        //draw obb
        //GraphicsDevice.RasterizerState = game1.wireFrameRs;
        //obbSize = new Vector3(1, 1, 10);
        //obbRot = Matrix.CreateRotationX(MathHelper.PiOver2 / 2);// (float)Math.Sin(tt) * MathHelper.PiOver2/2);
        //game1.DrawModel(game1.cubeModel,
        //    Matrix.CreateScale(obbSize/2) *
        //    obbRot *
        //    Matrix.CreateTranslation(obbPos), playerCam.view, playerCam.projection, monochrome(0.2f));
        //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        //draw network players
        //foreach (var pair in networkplayerdata)
        //{
        //    //Matrix scale = Matrix.CreateScale(dropValue / 2);
        //    Vector3 pos = pair.Value.pos;
        //    Vector3 headOffset = Vector3.Up * (height / 2 - dropValue / 2);
        //    Vector3 headPos = pos + headOffset;
        //    //Vector3 footPos = pos - headOffset;
        //    bool lighting = true;
        //    game1.DrawModel(game1.cubeModel,
        //        Matrix.CreateScale(dropValue/2, dropValue/2, dropValue/8) *
        //        Matrix.CreateTranslation(0, 0, -dropValue/3) *
        //        pair.Value.gun.rot *
        //        Matrix.CreateTranslation(headPos),
        //        playerCam.view, playerCam.projection, monochrome(0.75f), lighting);
        //    //game1.DrawModel(game1.cubeModel,
        //    //    scale *
        //    //    Matrix.CreateRotationY(pair.Value.euler.Y) *
        //    //    Matrix.CreateTranslation(pos),
        //    //    camera.view, camera.projection, monochrome(0.25f), lighting);
        //    //game1.DrawModel(game1.sphereModel,
        //    //    scale *
        //    //    Matrix.CreateRotationY(pair.Value.euler.Y) *
        //    //    Matrix.CreateTranslation(footPos),
        //    //    camera.view, camera.projection, monochrome(0.25f), lighting);
        //    //game1.DrawModel(game1.cubeModel,
        //    //    scale *
        //    //    Matrix.CreateRotationY(-tt) *
        //    //    Matrix.CreateTranslation(pair.Value.pos - Vector3.Up * 0.5f),
        //    //    camera.view, camera.projection, monochrome(0.25f), false);
        //    Vector3 tiptop = pair.Value.pos + (height / 2) * Vector3.Up;
        //    for (int i = bodyc - 1; i >= 0; --i)
        //    {
        //        BoundingSphere sv = new BoundingSphere(tiptop + drop * i + drop / 2, dropValue / 2);
        //        game1.DrawModel(game1.sphereModel,
        //            Matrix.CreateScale(sv.Radius) *
        //            Matrix.CreateTranslation(sv.Center),
        //            playerCam.view, playerCam.projection, monochrome(1.0f, 1.0f),
        //            true,
        //            basetx);
        //    }
        //}

        //            //Matrix personr = Matrix.CreateFromYawPitchRoll(person.euler.Y, person.euler.X, person.euler.Z);
        //            //Matrix personw = personr * Matrix.CreateTranslation(person.pos);// + person.offset);
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(0.3f) *
        //            //    personw,
        //            //    //Matrix.CreateTranslation(person.pos),
        //            //    camera.view, camera.projection, monochrome(0.5f));
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(0.1f) *
        //            //    Matrix.CreateTranslation(0, 0, -0.3f) *
        //            //    personw,
        //            //Matrix.CreateTranslation(person.pos),
        //            //camera.view, camera.projection, monochrome(0.5f));

        //            // draw guns
        //            for (int g = 0; g < allguns.Count; ++g)
        //            {
        //                Gun gun = allguns[g];
        //                //game1.add3DLine(gun.pos, bodyState.pos, Color.Magenta);
        //                drawgun(allguns[g]);
        //            }
        //            if (slowGun != null)
        //            {
        //                //Bullet b = slowGun.bullets[0];
        //                if (slowGun.bullets.Count > 0)
        //                {
        //                    Bullet b = slowGun.bullets[0];
        //                    if (!b.off)
        //                    {
        //                        game1.add3DLine(b.phy.pos, b.phy.pos + Vector3.Normalize(b.phy.vel) * 50, Color.Red);
        //                    }
        //                }
        //            }

        ////draw bullets
        //for (int b = 0; b < allbullets.Count; ++b)
        //{
        //    Bullet bul = allbullets[b];
        //    Matrix r = Matrix.Identity;
        //    Texture2D texture = game1.pixel;
        //    if (allbullets[b].off)
        //        r = playerCam.rotation3D;
        //    Color color = bulletTrailColor;// monochrome(0.5f);
        //    if (bul.off)
        //        color.A = 150;
        //    if (allbullets[b].isSolid(et) && frust.Intersects(new BoundingSphere(bul.phy.pos, bul.size / 2)))
        //    {
        //        //game1.DrawModel(game1.sphereModel,
        //        //    Matrix.CreateScale(bul.size / 2) * r * Matrix.CreateTranslation(allbullets[b].phy.pos),
        //        //    playerCam.view, playerCam.projection, color, true, 
        //        //    texture);
        //        DrawModel(game1.sphereModel,
        //            Matrix.CreateScale(bul.size / 2) * r * Matrix.CreateTranslation(allbullets[b].phy.pos),
        //            playerCam.view, playerCam.projection, color, true,
        //            texture);
        //    }
        //    if (allbullets[b].isRay(et)) //draw fluid bullet
        //    {
        //        Vector3 vel = allbullets[b].phy.vel;
        //        Vector3 A = allbullets[b].phy.pos;
        //        Vector3 B = A + vel / updatefps;
        //        //volume of sphere = (4/3) * pi * r^3
        //        float volume =
        //            (4.0f / 3.0f) *
        //            MathHelper.Pi *
        //            (float)Math.Pow(allbullets[b].size / 2, 3);
        //        //float s = bul.size;
        //        float l = (B - A).Length();
        //        //volume of cube = w * h * d
        //        //volume / d = w * h
        //        //h = w
        //        //volume / d = w ^ 2
        //        //w = sqrt(volume / d)
        //        float s = (float)Math.Sqrt(volume / l);
        //        if (allbullets[b].skipNextAdvance)
        //            l = 0.01f;
        //        Vector3 up = Vector3.Up;
        //        Vector3 dir = B - A;
        //        dir.Normalize();
        //        if (Vector3.Dot(dir, up) > 0.98f)
        //            up = Vector3.Right;
        //        Matrix look = Matrix.CreateLookAt(A, B, up);
        //        //game1.DrawModel(game1.cubeModel,
        //        //    Matrix.CreateTranslation(0, 0, -1) *
        //        //    Matrix.CreateScale(s / 2, s / 2, l / 2) *
        //        //    Matrix.Invert(look),
        //        //    playerCam.view, playerCam.projection, color, false,
        //        //    texture);
        //        DrawModel(game1.cubeModel,
        //            Matrix.CreateTranslation(0, 0, -1) *
        //            Matrix.CreateScale(s / 2, s / 2, l / 2) *
        //            Matrix.Invert(look),
        //            playerCam.view, playerCam.projection, color, false,
        //            texture);
        //        //game1.add3DLine(allbullets[b].phy.pos, allbullets[b].phy.pos + allbullets[b].phy.vel * et, monochrome(1.0f));
        //    }
        //}

        //            // draw video 3d
        //            //screenQuadEffect.LightingEnabled = false;
        //            //screenQuadEffect.Alpha = 1.0f;
        //            //screenQuadEffect.DiffuseColor = new Vector3(1);
        //            //screenQuadEffect.World = 
        //            //    Matrix.CreateRotationY(MathHelper.Pi) *
        //            //    Matrix.CreateTranslation(0,0,-1.01f) *
        //            //    videoTutorialRot *
        //            //    Matrix.CreateTranslation(videoTutorialPosition);
        //            //screenQuadEffect.View = camera.view;
        //            //screenQuadEffect.Projection = camera.projection;
        //            //screenQuadEffect.TextureEnabled = true;
        //            //screenQuadEffect.Texture = vPlayer.GetTexture();
        //            //screenQuadEffect.CurrentTechnique.Passes[0].Apply();
        //            //DrawScreenQuad();

        //            Action<Vector3, float> ds = (Vector3 pos, float rad) =>
        //            {
        //                DrawModel(game1.sphereModel,
        //                    Matrix.CreateScale(rad) *
        //                    Matrix.CreateTranslation(pos),
        //                    playerCam.view, playerCam.projection, Color.White, true, game1.pixel);
        //            };
        //            Action<Vector3, Vector3> dc = (Vector3 pos, Vector3 halfSize) =>
        //            {
        //                DrawModel(game1.cubeModel,
        //                    Matrix.CreateScale(halfSize) *
        //                    Matrix.CreateTranslation(pos),
        //                    playerCam.view, playerCam.projection, Color.White, true, game1.pixel);
        //            };

        //            // draw mechanisms
        //            //Vector3 gunConnectorPos = gunMech.pos + Vector3.Down * gunMech.size;
        //            //dc(gunConnectorPos, new Vector3(0.06f));
        //            //if (currentGun != null && sphereIntersectsGun(new BoundingSphere(gunConnectorPos, 0.06f), currentGun))
        //            //{
        //            //    reloadMechGun = currentGun;
        //            //}
        //            //Vector3 reloadCenter = reloadMech.Center + Vector3.Up * reloadMech.Radius;
        //            ////reloadCenter.Y = reloadMech.Radius;
        //            //dc(reloadCenter, new Vector3(0.06f));
        //            ////ds(reloadMech.Center, reloadMech.Radius);

        //            //draw clevergirl
        //            //dc(clevergirl, new Vector3(0.1f));
        //            //foreach(ZombieState z in zombies)
        //            //{
        //            //    dc(z.pos, new Vector3(0.1f));
        //            //}
        //            //ds(cleverboy, 0.1f);
        //            //Vector3 antennaA = Vector3.Normalize(Vector3.Up + Vector3.Forward + Vector3.Right);
        //            //Vector3 antennaB = Vector3.Normalize(Vector3.Up + Vector3.Forward + Vector3.Left);
        //            //game1.add3DLine(clevergirl, clevergirl + antennaA * 2, monochrome(1.0f));
        //            //game1.add3DLine(clevergirl, clevergirl + antennaB * 2, monochrome(1.0f));
        //            //Point p = worldToCoord(clevergirl);
        //            //var path = pathfinding.findPath(p, worldToCoord(bodyState.pos));
        //            //if (path != null)
        //            //{
        //            //    foreach (var s in path)
        //            //    {
        //            //        ds(new Vector3(s.coordinate.X * widthm, 2, s.coordinate.Y * depthm), 0.5f);
        //            //    }
        //            //}
        //            //draw targets
        //            //Li
        //            float markerStepAngle = MathHelper.TwoPi / (float)targets.Length;
        //            for (int t = 0; t < targets.Length; ++t)
        //            {
        //                float r = targets[t].Radius;
        //                Vector3 c = targets[t].Center;
        //                Model model = game1.sphereModel;
        //                Color color = monochrome(0.9f);
        //                if (targets[t].off)
        //                {
        //                    color = monochrome(0.6f, 0.2f);
        //                }
        //                Texture2D texture = basetx;
        //                bool lightingEnabled = true;
        //                //if (t == teleportationTarget)
        //                //{
        //                //    color = monochrome(0.10f);
        //                //}
        //                //if (t == teleportationTarget2)
        //                //{
        //                //    color = monochrome(0.5f);
        //                //}
        //                //if(t == teleportationTarget3)
        //                //    color = monochrome(0.5f);
        //                //if (targets[t].isCube)
        //                //    model = game1.cubeModel;
        //                //game1.DrawModel(model,
        //                //    Matrix.CreateScale(targets[t].Radius) *
        //                //    Matrix.CreateTranslation(targets[t].Center),
        //                //    playerCam.view, playerCam.projection, color, lightingEnabled,
        //                //    texture);
        //                DrawModel(model,
        //                    Matrix.CreateScale(targets[t].Radius) *
        //                    Matrix.CreateTranslation(targets[t].Center),
        //                    playerCam.view, playerCam.projection, color,
        //                    texture, null, lightingEnabled);
        //                Plane plane = new Plane(Vector3.Down, 0.05f);
        //                Matrix shadow = Matrix.CreateShadow(Vector3.Down, plane);
        //                bool doShadow = false;
        //                //for(int j = 0; j < coreTargetsTable.Length; ++j)
        //                //{
        //                //    if(coreTargetsTable[j].Contains(t) && t != coreTargetsTable[j].Last())
        //                //    {
        //                //        doShadow = false;
        //                //    }
        //                //}
        //                if (doShadow)
        //                {
        //                    game1.DrawModel(model,
        //                        Matrix.CreateScale(targets[t].Radius * (1 + 0.5f * Math.Max(0, targets[t].Center.Y - plane.D) / (height - dropValue / 2))) *
        //                        Matrix.CreateTranslation(targets[t].Center) * shadow,
        //                        playerCam.view, playerCam.projection, monochrome(0.25f, 0.5f), false);
        //                }
        //                //ds(c + Vector3.Down * target2y, target2r);
        //                //Vector3 body = c + Vector3.Down;
        //                //float l = 0.5f;
        //                //for(int j = 0; j < targetHistory.GetLength(1); ++j)
        //                //{
        //                //    ds(targetHistory[t, j], r / 1.5f);
        //                //}
        //                //ds(c + Vector3.Down, r * 2);
        //                //ds(body + Vector3.Down * l, r);
        //                //ds(body + Vector3.Down * l * 2, r);
        //                //ds(body + Vector3.Right, r);
        //                //ds(body + Vector3.Left, r);

        //                //DESIGN: how can the markers be interactable?
        //                //draw target marker
        //                //    Vector3 markerCenter = Vector3.Zero;
        //                //    Matrix markerRotation = Matrix.CreateRotationY(markerStepAngle * (float)t);
        //                //    Vector3 markerArm = Vector3.Transform(Vector3.Forward, markerRotation);
        //                //game1.DrawModel(Matrix.CreateScale(targets[t].Radius),
        //            }
        //            //BoundingFrustum frust = new BoundingFrustum(Matrix.Invert(personw) * Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(65),
        //            //    1, 1, 30));
        //            //var frustc = frust.GetCorners();
        //            //for (int i = 0; i < frustc.Length; ++i)
        //            //{
        //            //    Vector3 a = frustc[i];
        //            //    for (int i2 = i + 1; i2 < frustc.Length; ++i2)
        //            //    {
        //            //        if (i2 == i) continue;
        //            //        Vector3 b = frustc[i2];
        //            //        game1.add3DLine(a, b, monochrome(1.0f));
        //            //    }
        //            //}
        //            //draw unoccluded 3d
        //            if (edit.active)
        //            {
        //                GraphicsDevice.RasterizerState = game1.wireFrameRs;
        //                //if(!edit.occludeTargets)
        //                GraphicsDevice.DepthStencilState = DepthStencilState.None;
        //                //for (int t = 0; t < targets.Length; ++t)
        //                //{
        //                //    //if (targets[t].isCube)
        //                //    //    dc(targets[t].Center, new Vector3(targets[t].Radius));
        //                //    //else
        //                //    ds(targets[t].Center, targets[t].Radius);
        //                //}
        //                for (int i = 0; i < allguns.Count; ++i)
        //                {
        //                    drawgun(allguns[i]);
        //                }
        //                GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //                //if (!edit.occludeTargets)
        //                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        //            }
        //            //game1.DrawAll3dLines(playerCam.view, playerCam.projection);
        //            game1.Flush3dLines();
        //            //draw transparent
        //            if (edit.active)
        //            {
        //                //draw terrain boundaries
        //                GraphicsDevice.RasterizerState = game1.wireFrameRs;
        //                Vector3 terrainSize = new Vector3(widthm * terrain.GetLength(0),
        //                    500, depthm * terrain.GetLength(1));
        //                game1.DrawModel(game1.cubeModel,
        //                    Matrix.CreateTranslation(1, 0, 1) *
        //                    Matrix.CreateScale(terrainSize / 2) *
        //                    Matrix.CreateTranslation(-widthm / 2, 0, -depthm / 2),
        //                    playerCam.view, playerCam.projection, new Color(0, 1, 0, 0.3f), true);
        //                GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        //draw edit 3d, draw edit boxes, draw selection boxes, draw selection targets, draw hover 3d
        //foreach (Box b in edit.boxes)
        //{
        //    game1.DrawModel(cubeModel,
        //        Matrix.CreateScale(b.size / 2) * Matrix.CreateScale(1.0015f) *
        //        Matrix.CreateTranslation(b.position), playerCam.view, playerCam.projection,
        //        new Color(b.color.ToVector4() * Color.Red.ToVector4()), true);
        //}
        //if (edit.target > -1)
        //{
        //    //BoundingSphere t = targets[edit.target];
        //    Target t = targets[edit.target];
        //    game1.DrawModel(game1.sphereModel,
        //        Matrix.CreateScale(t.Radius) * Matrix.CreateScale(1.001f) *
        //        Matrix.CreateTranslation(t.Center), playerCam.view, playerCam.projection,
        //        Color.Red, true);
        //}
        //if (editGun != null)
        //{
        //    Gun g = editGun;
        //    DrawModel(cubeModel,
        //        Matrix.CreateScale(g.size / 2) * Matrix.CreateScale(1.001f) *
        //        Matrix.CreateTranslation(0, -g.size.Y / 2, 0) *
        //        //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
        //        g.rot * Matrix.CreateTranslation(g.pos),
        //        //Matrix.CreateTranslation(camera.Position3D), 
        //        playerCam.view, playerCam.projection, Color.Red, true, game1.pixel);
        //}
        //GraphicsDevice.RasterizerState = game1.wireFrameRs;
        //Box e = edit.hoverbox;
        //if (e != null)
        //{
        //    game1.DrawModel(cubeModel,
        //        Matrix.CreateScale(e.size / 2) * Matrix.CreateScale(1.002f) *
        //        Matrix.CreateTranslation(e.position), playerCam.view, playerCam.projection,
        //        Color.Yellow, false);
        //}
        //if (edit.hoverTarget > -1)
        //{
        //    //BoundingSphere t = targets[edit.hoverTarget];
        //    Target t = targets[edit.hoverTarget];
        //    game1.DrawModel(game1.sphereModel,
        //        Matrix.CreateScale(t.Radius) * Matrix.CreateScale(1.02f) *
        //        Matrix.CreateTranslation(t.Center), playerCam.view, playerCam.projection,
        //        Color.Yellow, false);
        //}
        //if (editHoverGun != null)
        //{
        //    Gun g = editHoverGun;
        //    DrawModel(cubeModel,
        //        Matrix.CreateScale(g.size / 2) * Matrix.CreateScale(1.02f) *
        //        Matrix.CreateTranslation(0, -g.size.Y / 2, 0) *
        //        //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
        //        g.rot * Matrix.CreateTranslation(g.pos),
        //        //Matrix.CreateTranslation(camera.Position3D), 
        //        playerCam.view, playerCam.projection, Color.Yellow, true, game1.pixel);
        //}
        //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //draw edit 3d, draw clipboard
        //foreach (Box b in clipboardBoxes)
        //{
        //    game1.DrawModel(cubeModel,
        //        Matrix.CreateScale(b.size / 2) * Matrix.CreateScale(1.01f) *
        //        Matrix.CreateTranslation(b.position), playerCam.view, playerCam.projection,
        //        new Color(b.color.ToVector4() * new Vector4(0,0,1,0.3f)), false);
        //}

        //                //draw selection brush 3d
        //                if (editUseSelectionBrush)
        //                {
        //                    game1.DrawModel(game1.sphereModel,
        //                        Matrix.CreateScale(editSelectionBrushRadius) *
        //                        Matrix.CreateTranslation(edit.hoverboxContact),
        //                        playerCam.view, playerCam.projection, new Color(0, 1, 0, 0.3f), false);
        //                }
        //            }
        //            //draw boxes transparent
        //            glassBoxes.Sort((Box a, Box b) =>
        //            {
        //                if ((playerCam.pos - a.position).LengthSquared() <
        //                (playerCam.pos - b.position).LengthSquared())
        //                {
        //                    return 1;
        //                }
        //                return -1;
        //            });
        //            foreach (Box b in glassBoxes)
        //            {
        //                for (int i = 0; i < 2; ++i)
        //                {
        //                    if (i == 0) GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //                    else GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //                    DrawModel(cubeModel,
        //                        Matrix.CreateScale(b.size / 2) *
        //                        Matrix.CreateTranslation(b.position), playerCam.view, playerCam.projection,
        //                        b.color, metaltx, null, new Vector2(0.5f), false);
        //                }
        //            }
        //            //for (int i = 0; i < 2; ++i)
        //            //{
        //            //    if (i == 0) GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //            //    else GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //            //}
        //            //draw contact history
        //            //for(int i = 0; i < boxCH.Length;++i)
        //            //{
        //            //    int j = boxCHI - i;
        //            //    if (j < 0)
        //            //        j += boxCH.Length;
        //            //    ContactData data = boxCH[j];
        //            //    if (data == null)
        //            //        continue;
        //            //    game1.add3DLine(data.contact, data.contact + data.norm * 1, monochrome(0.2f));
        //            //    //DrawModel(game1.cubeModel,
        //            //    //    Matrix.CreateScale())
        //            //}
        //            //draw player
        //            float heightPercentage = (float)bodyc / (float)sphereBodyCount;
        //            float totalHeight = height * heightPercentage;
        //            Vector3 top = bodyState.pos + (totalHeight / 2) * Vector3.Up;
        //            //Vector3 top = bodyState.pos + (height / 2) * Vector3.Up;
        //            for (int i = bodyc - 1; i >= 0; --i)
        //            {
        //                BoundingSphere sv = new BoundingSphere(top + drop * i + drop / 2, dropValue / 2);
        //                game1.DrawModel(game1.sphereModel,
        //                    Matrix.CreateScale(sv.Radius) *
        //                    Matrix.CreateTranslation(sv.Center),
        //                    playerCam.view, playerCam.projection, monochrome(1.0f, 0.1f));
        //            }
        //            //draw units
        //            //for (int i = 0; i < units.Count; ++i)
        //            //{
        //            //    ds(units[i].pos, 0.01f);
        //            //}
        //            //draw hearing
        //            //GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //            //game1.DrawModel(
        //            //    game1.sphereModel,
        //            //    Matrix.CreateScale(hearingRadius) *
        //            //    Matrix.CreateTranslation(clevergirl),
        //            //    camera.view, camera.projection,
        //            //    monochrome(1.0f, 0.55f),
        //            //    false);

        //            //draw koth sphere
        //            //game1.DrawModel(
        //            //    game1.sphereModel,
        //            //    Matrix.CreateScale(kothRadius) *
        //            //    Matrix.CreateTranslation(kothCenter),
        //            //    camera.view, camera.projection,
        //            //    monochrome(1.0f, 0.25f),
        //            //    false);
        //            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //            //game1.DrawModel(
        //            //    game1.sphereModel,
        //            //    Matrix.CreateScale(kothRadius) *
        //            //    Matrix.CreateTranslation(kothCenter),
        //            //    camera.view, camera.projection,
        //            //    monochrome(1.0f, 0.25f),
        //            //    false);

        //            //doffx.Parameters["diffusetx"].SetValue();
        //            //GraphicsDevice.SetRenderTargets(blurrt);
        //            //if (blurfx.Parameters["diffusetx"] != null)
        //            //    blurfx.Parameters["diffusetx"].SetValue(backbuffrt);
        //            //blurfx.Parameters["drawmode"].SetValue(0);
        //            //blurfx.Parameters["screenWidth"].SetValue((float)GraphicsDevice.Viewport.Width);
        //            //blurfx.Parameters["screenHeight"].SetValue((float)GraphicsDevice.Viewport.Height);
        //            //blurfx.CurrentTechnique.Passes[0].Apply();
        //            //DrawScreenQuad();

        //            //GraphicsDevice.SetRenderTargets(null);
        //            //if (doffx.Parameters["diffusetx"] != null)
        //            //    doffx.Parameters["diffusetx"].SetValue(backbuffrt);
        //            //if (doffx.Parameters["depthtx"] != null)
        //            //    doffx.Parameters["depthtx"].SetValue(depthrt);
        //            //if (doffx.Parameters["blurtx"] != null)
        //            //    doffx.Parameters["blurtx"].SetValue(blurrt);
        //            //doffx.Parameters["screenWidth"].SetValue((float)GraphicsDevice.Viewport.Width);
        //            //doffx.Parameters["screenHeight"].SetValue((float)GraphicsDevice.Viewport.Height);
        //            //doffx.Parameters["near"].SetValue(camera.near);
        //            //doffx.Parameters["far"].SetValue(camera.far / (camera.far - camera.near));
        //            //doffx.Parameters["range"].SetValue(100.0f);
        //            //doffx.Parameters["distance"].SetValue(0.0f);
        //            //doffx.Parameters["drawmode"].SetValue(2);
        //            //doffx.CurrentTechnique.Passes[0].Apply();
        //            //DrawScreenQuad();
        //            //GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //            //Color tankc = monochrome(1.0f, .4f);
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(tankr) * Matrix.CreateTranslation(tankps.pos),
        //            //    camera.view,
        //            //    camera.projection,
        //            //    tankc);
        //            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(tankr) * Matrix.CreateTranslation(tankps.pos),
        //            //    camera.view,
        //            //    camera.projection,
        //            //    tankc);
        //            //Vector3 personf = Vector3.Transform(Vector3.Forward, personw) - Vector3.Transform(Vector3.Zero, personw);
        //            //personf.Normalize();
        //            //BoundingSphere eyesv = new BoundingSphere(person.pos, 15f);
        //            //person.detected = false;
        //            //    for (int i = 0; i < bodyc; ++i)
        //            //{
        //            //    BoundingSphere sv = new BoundingSphere(top + drop * i + drop / 2, dropValue / 2);
        //            //    //if (frust.Intersects(sv))
        //            //    //{
        //            //    //    person.detected = true;
        //            //    //    break;
        //            //    //}
        //            //    if (eyesv.Intersects(sv))
        //            //    {
        //            //        person.detected = true;
        //            //        break;
        //            //    }
        //            //}
        //            ////person.offset = Vector3.Zero;
        //            //if(person.detected)
        //            //{
        //            //    Vector3 dir = camera.pos - person.pos;
        //            //    person.euler.Y = (float)Math.Atan2(-dir.X, -dir.Z);
        //            //    //person.dt += et;
        //            //    //float t = person.dt * 4;
        //            //    //float sin = (float)Math.Sin(t) / 4;
        //            //    //float cos = (float)Math.Cos(t) / 4;
        //            //    //if (t < MathHelper.TwoPi)
        //            //    //{
        //            //    //    person.offset += Vector3.Normalize(Vector3.Cross(personf, Vector3.Up)) * cos * sin;
        //            //    //    person.offset += Vector3.Up * sin;
        //            //}
        //            //}
        //            //else
        //            //{
        //            //    person.dt = 0;
        //            //}
        //            //GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(eyesv.Radius) *
        //            //    Matrix.CreateTranslation(eyesv.Center),
        //            //    //Matrix.CreateTranslation(person.pos),
        //            //    camera.view, camera.projection, monochrome(1.5f, .3f),
        //            //    true);

        //            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //            //game1.DrawModel(game1.sphereModel,
        //            //    Matrix.CreateScale(eyesv.Radius) *
        //            //    Matrix.CreateTranslation(eyesv.Center),
        //            //    //Matrix.CreateTranslation(person.pos),
        //            //    camera.view, camera.projection, monochrome(1.5f, .3f),
        //            //    true);
        //            //{
        //            //    Vector3 p = gunpos + Vector3.Transform(Vector3.Forward, camera.rotation3D) * 5;
        //            //    for (int t = 0; t < 4; ++t)
        //            //    {
        //            //        game1.DrawModel(game1.sphereModel,
        //            //            Matrix.CreateTranslation(p + Vector3.Forward * t),
        //            //            camera.view,
        //            //            camera.projection,
        //            //            monochrome(1.0f));
        //            //    }
        //            //}
        //            //{
        //            //    Vector3 gunf = Vector3.Transform(Vector3.Forward, gunrot);
        //            //    float s = 0.1f;
        //            //    bool wasExploding = grenadexploding;
        //            //    Color color = monochrome(0.1f);
        //            //    float start = 2 - cooktime;
        //            //    float duration = 0.25f;
        //            //    float linger = 0.5f;
        //            //    float end = start + duration;
        //            //    float lingerend = end + linger;
        //            //    if (bullets[0].off || bullets[0].t < start)
        //            //    {
        //            //        grenadepos = bullets[0].p.pos;
        //            //        grenadexploding = false;
        //            //        bullets[0].p.force += Vector3.Down * 10;
        //            //        if(wasExploding)
        //            //            cooktime = 0;
        //            //    }
        //            //    else
        //            //    {
        //            //        //if (bullets[0].t < lingerend)
        //            //        bullets[0].p.vel = Vector3.Zero;
        //            //        if (bullets[0].t < end)
        //            //        {
        //            //            grenadexploding = true;
        //            //            //float a = 1 - (end - bullets[0].t) / duration;
        //            //            //float b = lingerend - bullets[0].t / linger;
        //            //            //float f = a;
        //            //            //if (a > 1)
        //            //            //    f = b;
        //            //            //s = MathHelper.Lerp(s, 5, f);
        //            //            s = 10;
        //            //            //color = Color.Lerp(color, monochrome(1, 0.2f), f);
        //            //            color = monochrome(1, 0.2f);
        //            //            BoundingSphere sv = new BoundingSphere(grenadepos, s);
        //            //            for (int i = 0; i < targets.Length; ++i)
        //            //            {
        //            //                if (targets[i].Intersects(sv))
        //            //                {
        //            //                    targets[i].Center.Y = 0;
        //            //                }
        //            //            }
        //            //        }
        //            //    }
        //            //    Action dgren = () =>
        //            //    {
        //            //        game1.DrawModel(game1.sphereModel,
        //            //            Matrix.CreateScale(s) *
        //            //            Matrix.CreateTranslation(grenadepos),
        //            //            camera.view, camera.projection,
        //            //            color);
        //            //    };
        //            //    GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
        //            //    dgren();
        //            //    GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        //            //    dgren();
        //            //    //BoundingBox bb = Game1.MakeBox(c, new Vector3(s));
        //            //    //for(int i = 0; i < grenadebullets.Length; ++i)
        //            //    //{
        //            //    //    Vector3 g = c + gbp[i] * (s + bsz);
        //            //    //    if (grenadexploding && !wasExploding)
        //            //    //    {
        //            //    //        Vector3 d = g - c; //to bullet
        //            //    //        d.Normalize();
        //            //    //        grenadebullets[i].p.vel = d * 10;
        //            //    //        grenadebullets[i].off = false;
        //            //    //    }
        //            //    //    if(!grenadexploding)
        //            //    //    {
        //            //    //        grenadebullets[i].p.pos = g;
        //            //    //        grenadebullets[i].p.vel = Vector3.Zero;
        //            //    //        grenadebullets[i].off = true;
        //            //    //    }
        //            //    //    grenadebullets[i].p.force -= grenadebullets[i].p.vel * 0.1f;
        //            //    //    grenadebullets[i].p.Advance(et);
        //            //    //    Vector3 p = grenadebullets[i].p.pos;
        //            //    //    game1.DrawModel(game1.sphereModel,
        //            //    //        Matrix.CreateScale(bsz) * Matrix.CreateTranslation(p),
        //            //    //        camera.view, camera.projection, Color.White);
        //            //    //}
        //            //}

        // draw 2d, draw spritebatch
        //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        ////draw pause 2d overlay
        //if (paused)
        //{
        //    game1.drawTexture(metaltx,
        //        Vector2.Zero,
        //        monochrome(0.3f, 0.5f),
        //        0,
        //        GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        //}

        //draw ghost ui
        //game1.drawString(ghostFrame < ghostFrames.Length ? "recording" : "playback",
        //    Backpack.percentage(ViewBounds, 0, 0, .3f, .1f), monochrome(1.0f));

        //game1.drawTexture(game1.pixel,
        //    Vector2.Zero,
        //    monochrome(time, 0.1f),
        //    0,
        //    GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        //draw bullets 2d
        //for (int i = 0; i < allbullets.Count; ++i)
        //{
        //    Bullet b = allbullets[i];
        //    if (b.off) continue;
        //    Func<Plane, Vector3, Vector3> flatten = (Plane pl, Vector3 pt) =>
        //    {
        //        //Vector3 planeCenter = pl.D * pl.Normal;
        //        pl.Normalize(); //whyyyyyy isnt the plane normalized?
        //        //pl.D = Vector3.Dot(pl.Normal, planeCenter);
        //        //float dot = pl.DotCoordinate(pt);
        //        //float dot = -Vector3.Dot(pl.Normal, pt);
        //        //float realD = -pl.D;
        //        //float rel = dot - realD;
        //        //float pen = rel - pl.D;
        //        game1.add3DLine(pt, pt + pl.Normal, Color.Purple, Color.Yellow);
        //        //Vector3 n = Vector3.Normalize(pl.Normal);
        //        if (PlaneHelper)
        //            pt -= pl.Normal * rel;
        //        return pt;
        //    };
        //    Vector3 start3d = b.phy.pos;
        //    Vector3 end3d = b.phy.pos + b.phy.vel;
        //for (int j = 0; j < 1; ++j)
        //{
        //    Vector3 vec = start3d;
        //    if (j == 1) vec = end3d;
        //    //vec = flatten(frust.Left, vec);
        //    //vec = flatten(frust.Right, vec);
        //    //vec = flatten(frust.Near, vec);
        //    //vec = flatten(frust.Far, vec);
        //    vec = flatten(frust.Top, vec);
        //    vec = flatten(frust.Bottom, vec);
        //    if (j == 1) end3d = vec;
        //    else start3d = vec;
        //}
        //game1.add3DLine(start3d, end3d, Color.Blue);
        //Vector2 start = playerCam.worldToScreen(start3d, GraphicsDevice.Viewport);
        //Vector2 end = playerCam.worldToScreen(end3d, GraphicsDevice.Viewport);
        //game1.drawLine(start, end, Color.Red, 1);
        //}
        //float alpha = 0.0f;
        //float alpha2 = 0.0f;
        //if (game1.kdown(Microsoft.Xna.Framework.Input.Keys.Enter))
        //{
        //    alpha = 0.98f;
        //    alpha2 = 0.5f;
        //}
        //spriteBatch.Draw(traceImage, GraphicsDevice.Viewport.Bounds, monochrome(1.0f, alpha));
        //Frame ammoFrame = new Frame(Backpack.percentageEdges(GraphicsDevice.Viewport.Bounds, 0.75f, 0.97f, 0.95f, 0.98f));
        //float width = ammoFrame.bounds.Width / 7;
        //for (int i = 0; i < 7; ++i)
        //{
        //    Rectangle r = ammoFrame.bounds;
        //    r.Width = (int)width - 10;
        //    r.X += (int)width * i;
        //    game1.drawSquare(r, monochrome(1.0f, alpha2), 0);
        //}
        //draw video 2d
        //game1.drawTexture(vPlayer.GetTexture(),
        //    new Vector2(0, 0), Color.White, 0, 400, 300);
        // draw dialog 2d
        //Rectangle dialogBox = Backpack.percentageEdges(GraphicsDevice.Viewport.Bounds,
        //    0.2f, 0.9f, 0.8f, 0.95f);
        //game1.drawSquare(dialogBox, monochrome(0.2f, 0.7f), 0);
        //game1.drawString(dialogText, dialogBox, monochrome(0.95f, 0.9f));

        //draw audio 2d
        //            if (false)
        //            {
        //                Rectangle frame = Backpack.percentage(ViewBounds, 0.02f, 0.7f, 0.2f, 0.2f);
        //                game1.drawSquare(frame, monochrome(0.0f, 0.4f), 0);
        //                game1.drawFrame(frame, monochrome(1.0f, 0.8f), 1, 0, true);
        //                float width = frame.Width;
        //                Vector2 bottomLeft = new Vector2(frame.Left, frame.Bottom);
        //                Vector2 rawPeak = new Vector2(frame.Left + width * shootSound.attackP, frame.Top);
        //                Vector2 peakVec = rawPeak - bottomLeft;
        //                float riseOverRun = peakVec.Y / peakVec.X;
        //                float run = MathHelper.Clamp(peakVec.X, 0, width);
        //                float rise = riseOverRun * run;
        //                Vector2 peak = bottomLeft + new Vector2(run, rise);
        //                Color lineColor = monochrome(1.0f);
        //                game1.drawLine(bottomLeft, peak, lineColor, 1);
        //                Vector2 bottomRight = new Vector2(frame.Right, frame.Bottom);
        //                game1.drawLine(peak, bottomRight, lineColor, 1);
        //                var types = Enum.GetNames(typeof(SignalGeneratorType));
        //                for (int i = 0; i < types.Length; ++i)
        //                {
        //                    float percentage = ((float)i + 0.5f) / (float)types.Length;
        //                    Vector2 pos = bottomLeft + new Vector2(width * percentage, 12);
        //                    game1.drawSquare(pos, monochrome(1.0f), 0, 2, 2);
        //                    if (i == (int)shootSound.signalT)
        //                    {
        //                        Rectangle signalRect = game1.centeredRect(pos + new Vector2(0, 16), 128, 24);
        //                        game1.drawSquare(signalRect, monochrome(0.0f, 0.3f), 0);
        //                        game1.drawString(types[i], signalRect, monochrome(1.0f), new Vector2(0.5f, 0.0f), true);
        //                    }
        //                }
        //                Rectangle rect = frame;
        //                rect.X += rect.Width;
        //                rect.Height = 32;
        //                game1.drawString("Sound On: " + enableSound, rect, monochrome(1));
        //                rect.Y += rect.Height;
        //                game1.drawString("Frequency: " + shootSound.freq, rect, monochrome(1));
        //#if ADD_CLOSED_CAPTIONS
        //                            Rectangle ccRect = game1.centeredRect(Backpack.percentageLocation(GraphicsDevice.Viewport.Bounds, 0.5f, 0.9f), 128, 32);
        //                            if(closedCaptions.Count > 0)
        //                            {
        //                                for(int i = 0; i < closedCaptions.Count && i < 3; ++i)
        //                                {
        //                                    var pair = closedCaptions.ElementAt(i);
        //                                    game1.drawSquare(ccRect, monochrome(0, 0.3f), 0);
        //                                    game1.drawFrame(ccRect, monochrome(1, 0.5f), 1);
        //                                    game1.drawString(pair.Key, ccRect, monochrome(1.0f));
        //                                    ccRect.Y -= ccRect.Height;
        //                                }
        //                            }
        //#endif
        //            }

        //            //draw edit 2d, draw edit ui
        //            mouseUi = null;
        //            if (edit.active)
        //            {
        //                Color filenameColor = monochrome(1.0f);
        //                if (edit.saveNeeded) filenameColor = Color.Red;
        //                Rectangle filenameRect = Backpack.percentageEdges(GraphicsDevice.Viewport.Bounds, 0.01f, 0.01f, 0.3f, 0.1f);
        //                if (game1.restrictedInput)
        //                {
        //                    Rectangle filenameFrameRect = filenameRect;
        //                    filenameFrameRect.Inflate(2, 2);
        //                    game1.drawSquare(filenameFrameRect, game1.hsl2Rgb(182f / 360f, 0.2f, 0.61f, 0.3f), 0);
        //                    game1.drawFrame(filenameFrameRect,
        //                        monochrome(1.0f, 0.6f), 1);
        //                }
        //                game1.drawString("File:" + currentLevelFilename, filenameRect,
        //                    filenameColor);
        //                game1.drawString("pos: " + bodyState.pos,
        //                    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.1f, 0.49f, 0.05f),
        //                    monochrome(1.0f));
        //                game1.drawString("camera pos: " + playerCam.pos,
        //                    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.15f, 0.49f, 0.05f),
        //                    monochrome(1.0f));
        //                game1.drawString("draw: " + drawfps,
        //                    Backpack.percentageEdges(GraphicsDevice.Viewport.Bounds, 0.01f, 0.2f, 0.5f, 0.25f),
        //                    monochrome(1.0f));
        //                game1.drawString("update: " + updatefps,
        //                    Backpack.percentageEdges(GraphicsDevice.Viewport.Bounds, 0.01f, 0.25f, 0.5f, 0.3f),
        //                    monochrome(1.0f));
        //                game1.drawString("coord: " + worldToCoord(bodyState.pos),
        //                    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.3f, 0.49f, 0.05f),
        //                    monochrome(1.0f));
        //                game1.drawString("occlude targets: " + edit.occludeTargets,
        //                    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.35f, 0.49f, 0.05f),
        //                    monochrome(1.0f));
        //                game1.drawString("out of body: " + editOutOfBody,
        //                    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.4f, 0.49f, 0.05f),
        //                    monochrome(1.0f));
        //                if (edit.hoverbox != null)
        //                {
        //                    game1.drawString("hover box coord: " + worldToCoord(edit.hoverbox.position),
        //                        Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.45f, 0.49f, 0.05f),
        //                        monochrome(1.0f));
        //                }

        //                if (frust.Contains(playerSpawnPoint) == ContainmentType.Contains)
        //                {
        //                    DrawWorldLabel2D(playerSpawnPoint, "player spawn", 128, 32);
        //                }
        //                //draw selection 2d, draw selection labels, draw box labels
        //                //for(int i = 0; i <= edit.boxes.Count; ++i)
        //                {
        //                    Box box = edit.hoverbox;
        //                    //if(i < edit.boxes.Count)
        //                    //{
        //                    //    box = edit.boxes[i];
        //                    //}
        //                    //if(box == null)
        //                    //{
        //                    //    continue;
        //                    //}
        //                    if (box != null && frust.Contains(box.position) == ContainmentType.Contains)
        //                    {
        //                        Vector2 size = new Vector2(40, 20);
        //                        if (box == edit.hoverbox)
        //                            size = new Vector2(64, 32);
        //                        DrawWorldLabel2D(box.position, box.id.ToString(), size.X, size.Y);
        //                    }
        //                }

        //                //draw bullet labels
        //                for (int i = 0; i < allbullets.Count; ++i)
        //                {
        //                    break;
        //                    Bullet b = allbullets[i];
        //                    if (frust.Contains(b.phy.pos) == ContainmentType.Contains)
        //                    {
        //                        DrawWorldLabel2D(b.phy.pos, b.id.ToString(), 64, 32);
        //                    }
        //                }

        //                ////draw box 2d, draw box ui
        //                //if (edit.boxes.Count > 0)
        //                //{
        //                //    Frame incrementerFrame = new Frame(Backpack.percentage(GraphicsDevice.Viewport.Bounds,
        //                //        0.8f, 0.1f, 0.1f, 0.1f));
        //                //    if(incrementerFrame.bounds.Contains(game1.mouseCurrent.Position))
        //                //    {
        //                //        //notify others
        //                //        mouseUi = incrementerFrame.bounds;
        //                //    }else
        //                //    {
        //                //        //notify others
        //                //        mouseUi = null;
        //                //    }
        //                //    DrawFilledRect(incrementerFrame.bounds);
        //                //    Rectangle type = incrementerFrame.relativeRect(0, 0, 0.5f, 1);
        //                //    Box box = edit.boxes[0];
        //                //    game1.drawString(Enum.GetName(typeof(BoxType), box.type), type, monochrome(1.0f));
        //                //    Rectangle btn1 = incrementerFrame.relativeRect(0.5f, 0, 1.0f, 0.5f);
        //                //    DrawFilledRect(btn1);
        //                //    Rectangle btn2 = btn1;
        //                //    btn2.Y += btn2.Height;
        //                //    DrawFilledRect(btn2);
        //                //    if (game1.lmouse && !game1.lmouseOld)
        //                //    {
        //                //        if(btn1.Contains(game1.mouseCurrent.Position))
        //                //        {
        //                //            box.type++;
        //                //        }
        //                //        if (btn2.Contains(game1.mouseCurrent.Position))
        //                //        {
        //                //            box.type--;
        //                //        }

        //                //        box.type = (BoxType)(int)game1.wrap((int)box.type, 0, (int)BoxType.COUNT - 1);
        //                //    }
        //                //    for(int i = 0; i < edit.boxes.Count; ++i)
        //                //    {
        //                //        edit.boxes[i].type = box.type;
        //                //    }
        //                //    game1.drawString("Mouse In UI: " + mouseUi.HasValue, Backpack.percentage(ViewBounds, 0.8f, 0.3f, 0.1f, 0.1f), monochrome(1.0f));
        //                //}

        //                //draw ui element
        //                //for (int i = 0; i < allUIElements.Count;++i)
        //                //{
        //                //    UIDrawElement(allUIElements[i]);
        //                //}

        //                //draw gun ui
        //                if (editGun != null)
        //                {

        //                    Gun g = editGun;
        //                    Frame root = new Frame(Backpack.percentageEdges(ViewBounds, 0.6f, 0.2f, 0.95f, 0.9f));
        //                    float y = 0;
        //                    //for (int i = 0; i < gunFields.Length; ++i)
        //                    //{
        //                    //    FieldInfo f = gunFields[i];
        //                    //    Rectangle rect = Backpack.percentage(root.bounds, 0, y, 1, 0.1f);
        //                    //    if (f.FieldType == typeof(float))
        //                    //    {
        //                    //        y += 0.1f;
        //                    //        float value = (float)f.GetValue(editGun);
        //                    //        value += drawIncrementer(rect, f.Name + ": " + value);
        //                    //        f.SetValue(editGun, value);
        //                    //    }
        //                    //    else if (f.FieldType == typeof(int))
        //                    //    {
        //                    //        y += 0.1f;
        //                    //        int value = (int)f.GetValue(editGun);
        //                    //        value += drawIncrementer(rect, f.Name + ": " + value);
        //                    //        f.SetValue(editGun, value);
        //                    //    }
        //                    //    else if (f.FieldType == typeof(Vector3))
        //                    //    {
        //                    //        y += 0.1f;
        //                    //        Vector3 value = (Vector3)f.GetValue(editGun);
        //                    //        value += DrawVectorButton(rect, f.Name, value);
        //                    //        f.SetValue(editGun, value);
        //                    //    }
        //                    //    //else
        //                    //    //{
        //                    //    //    y += 0.1f;
        //                    //    //    drawlabel(rect, f.Name);
        //                    //    //}
        //                    //}
        //                    DrawFilledRect(root.bounds);
        //                    if (root.bounds.Contains(game1.mouseCurrent.Position))
        //                        mouseUi = root.bounds;
        //                    //if (drawButton(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), "Reload"))
        //                    //{
        //                    //    //for (int i = 0; i < editGun.bullets.Length; ++i)
        //                    //    for (int i = 0; i < editGun.bullets.Count; ++i)
        //                    //    {
        //                    //        Bullet b = editGun.bullets[i];
        //                    //        editGun.bullets[i].off = true;
        //                    //        if (expiredBullets.Contains(editGun.bullets[i]))
        //                    //        {
        //                    //            expiredBullets.Remove(editGun.bullets[i]);
        //                    //        }
        //                    //    }
        //                    //}
        //                    int ammoDelta = drawIncrementer(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), "Ammo Count: " + editGun.bullets.Count);
        //                    y += 0.1f;
        //                    if (ammoDelta > 0)
        //                    {
        //                        //for (int i = 0; i < editGun.bullets.Length; ++i)
        //                        editGun.AddBullet(allbullets);
        //                    }
        //                    if (ammoDelta < 0 && editGun.bullets.Count > 0)
        //                    {
        //                        Bullet b = editGun.bullets[editGun.bullets.Count - 1];
        //                        editGun.bullets.Remove(b);
        //                        allbullets.Remove(b);
        //                        editGun.bulleti = MathHelper.Clamp(editGun.bulleti, 0, editGun.bullets.Count - 1);
        //                    }
        //                    Rectangle bsr = Backpack.percentage(root.bounds, 0, y, 1, 0.1f);
        //                    float sizeDelta = drawIncrementer(bsr, "Bullet Size: " + editGun.BulletSize);
        //                    if (sizeDelta > 0 || sizeDelta < 0)
        //                    {
        //                        g.BulletSize += sizeDelta * 0.01f;
        //                    }
        //                    y += 0.1f;
        //                    float speedDelta = drawIncrementer(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), "Launch Speed: " + editGun.bulletSpeed);
        //                    g.bulletSpeed += speedDelta * 5;
        //                    y += 0.1f;
        //                    //if (drawButton(Backpack.percentage(root.bounds, 0, 0.4f, 1, 0.1f), "Filename:" + editGun.filename))
        //                    //{
        //                    //    RequestText(gunFiletxt);
        //                    //}
        //                    float fireRateDelta = drawIncrementer(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), "Fire Rate: " + editGun.automaticFireDelayS);
        //                    g.automaticFireDelayS += fireRateDelta * 0.05f;
        //                    y += 0.1f;
        //                    if (drawButton(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), g.off ? "Off" : "On", false))
        //                    {
        //                        g.off = !g.off;
        //                    }
        //                    //float massDelta = drawIncrementer(Backpack.percentage(root.bounds, 0, y, 1, 0.1f), "Mass: " + editGun.bullets[0].phy.mass);
        //                    //for (int i = 0; i < g.bullets.Count; ++i)
        //                    //{
        //                    //    g.bullets[i].phy.mass += massDelta * 0.1f;
        //                    //}

        //                    //if (drawButton(Backpack.percentage(root.bounds, 0, 0.9f, 0.5f, 0.1f), "Save"))
        //                    //{
        //                    //    using (StreamWriter writer = new StreamWriter(File.Create(editGun.filename)))
        //                    //    {
        //                    //        writer.WriteLine(serialize(editGun));
        //                    //    }
        //                    //}
        //                    //if (drawButton(Backpack.percentage(root.bounds, 0.5f, 0.9f, 0.5f, 0.1f), "Load"))
        //                    //{
        //                    //    using (StreamReader reader = new StreamReader(File.Open(editGun.filename, FileMode.Open)))
        //                    //    {
        //                    //        reload(editGun, reader.ReadLine());
        //                    //    }
        //                    //}
        //                    //g.size += 0.01f * DrawVectorButton(Backpack.percentage(root.bounds, 0f, 0.6f, 1, 0.1f), "size", g.size);
        //                }

        //                //draw grid
        //                if (game1.kdown(Keys.OemComma))
        //                {
        //                    for (float x = 0; x <= 1; x += 0.1f)
        //                    {
        //                        for (float y = 0; y <= 1; y += 0.1f)
        //                        {
        //                            Vector2 a = Backpack.percentageLocation(ViewBounds, x, y);
        //                            if (x == 0)
        //                            {
        //                                game1.drawLine(a, Backpack.percentageLocation(ViewBounds, 1, y), Color.White, 1);
        //                            }
        //                            if (y == 0)
        //                            {
        //                                game1.drawLine(a, Backpack.percentageLocation(ViewBounds, x, 1), Color.White, 1);
        //                            }
        //                            game1.drawString(string.Format("({0}, {1})", x, y), Backpack.percentage(ViewBounds, x, y, 0.1f, 0.1f), Color.White);
        //                        }
        //                    }
        //                }
        //                //draw network 2d information
        //                //game1.drawString(string.Format("listen: {0}, send: {1}, conn: {2}", listenPort, sendPort, remotePort),
        //                //    Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.5f, 0.49f, 0.05f),
        //                //    monochrome(1.0f));
        //                //float f = 0;
        //                //foreach (var pair in networkplayerdata)
        //                //{
        //                //    game1.drawString(string.Format("{0}: {1}", pair.Key, pair.Value.endpoint.Port),
        //                //        Backpack.percentage(GraphicsDevice.Viewport.Bounds, 0.01f, 0.55f + f * 0.05f, 0.49f, 0.5f),
        //                //        monochrome(1.0f));
        //                //    f++;
        //                //}
        //            }
        //            if (mouseUi.HasValue)
        //            {
        //                game1.drawFrame(mouseUi.Value, monochrome(1.0f, 0.8f), 4, 0, false);
        //            }
        //            //if(pauseMenuShowing)
        //            //{
        //            //    game1.drawSquare(GraphicsDevice.Viewport.Bounds, monochrome(0.0f, 0.3f), 0);
        //            //    for(int i = 0; i < pauseTexts.Length; ++i)
        //            //    {
        //            //        game1.drawSquare(pauseRects[i], monochrome(0.2f), 0);
        //            //        game1.drawString(pauseTexts[i], pauseRects[i], monochrome(1.0f));
        //            //    }
        //            //}
        //            spriteBatch.End();
        //} //end draw
        //helper ui
        //void UIFollow(UIElement child, UIElement parent, float x, float y, float w, float h)
        //{
        //    child.x = parent.x + parent.width * x;
        //    child.y = parent.y + parent.height * y;
        //    child.width = parent.width * w;
        //    child.height = parent.height * h;
        //}
        //void UIAddElement(UIElement uie)
        //{
        //    allUIElements.Add(uie);
        //}
        //void UICaptureMouse(UIElement uie, Vector2 mouse)
        //{
        //    if (uie.minimized)
        //        return;
        //    if (mouse.X > uie.x && mouse.X < uie.x + uie.width &&
        //        mouse.Y > uie.y && mouse.Y < uie.y + uie.height)
        //    {
        //        uiMouseHost = uie;
        //    }
        //}
        Vector3 DrawVectorButton(Rectangle rect, string label, Vector3 vector)
        {
            Rectangle lr = Backpack.percentage(rect, 0, 0, 0.25f, 1);
            Rectangle xr = Backpack.percentage(rect, 0.25f, 0, 0.25f, 1);
            Rectangle yr = Backpack.percentage(rect, 0.50f, 0, 0.25f, 1);
            Rectangle zr = Backpack.percentage(rect, 0.75f, 0, 0.25f, 1);
            drawlabel(lr, label);
            Vector3 result = Vector3.Zero;
            result.X = drawIncrementer(xr, vector.X.ToString());
            result.Y = drawIncrementer(yr, vector.Y.ToString());
            result.Z = drawIncrementer(zr, vector.Z.ToString());
            return result;
        }
        int drawIncrementer(Rectangle buttonRect, string label)
        {
            Rectangle ammoRect = Backpack.percentage(buttonRect, 0, 0, 0.8f, 1);
            DrawFilledRect(ammoRect, monochrome(1, 0.3f), monochrome(0.5f, 1));
            game1.drawString(label, ammoRect, monochrome(1.0f));
            Rectangle increment = Backpack.percentage(buttonRect, 0.8f, 0, 0.2f, 0.5f);
            int change = 0;
            if (drawButton(increment, "+", true))
            {
                change++;
            }
            increment.Y += increment.Height;
            if (drawButton(increment, "-", true))
            {
                change--;
            }
            return change;
        }
        bool drawButton(Rectangle buttonRect, string label, bool fitString = false)
        {
            float frame = 1;
            float fill = 0.5f;
            if (mouseHover(buttonRect))
            {
                editor.mouseUi = buttonRect;
                fill = 0.6f;
                if (game1.lmouse)
                {
                    fill = 0.4f;
                }
                if (game1.ltap || game1.lheld())
                {
                    fill = 0.3f;
                    return true;
                }
            }
            DrawFilledRect(buttonRect, monochrome(frame, 0.3f), monochrome(fill, 1));
            game1.drawString(label, buttonRect, monochrome(1.0f), Vector2.Zero, fitString);
            return false;
        }
        void drawlabel(Rectangle rect, string label)
        {
            float frame = 1;
            float fill = 0.5f;
            DrawFilledRect(rect, monochrome(frame, 0.3f), monochrome(fill, 1));
            game1.drawString(label, rect, monochrome(1.0f));
        }
        //helper ui, helper draw
        //void UIDrawElement(UIElement uie)
        //{
        //    if (uie.minimized)
        //        return;
        //    game1.drawSquare(
        //        new Vector2(uie.x, uie.y) +
        //            new Vector2(uie.width, uie.height) / 2,
        //        monochrome(1.0f, 0.3f), 0, uie.width, uie.height);
        //    if (!String.IsNullOrEmpty(uie.labelText))
        //    {
        //        game1.drawString(uie.labelText, new Vector2(uie.width, uie.height), new Vector2(uie.x, uie.y), Color.White);
        //    }
        //}
        //helper draw
        void DrawModel(Model m, Matrix w, Matrix orientation, int pass)
        {
            render3d.SetDeferredParametersGeometry(playerCam, w, orientation);
            render3d.deferfx.CurrentTechnique = render3d.deferfx.Techniques["Default"];
            render3d.deferfx.CurrentTechnique.Passes[pass].Apply();
            ModelMeshPart meshPart = m.Meshes[0].MeshParts[0];
            GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer);
            GraphicsDevice.Indices = meshPart.IndexBuffer;
            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPart.VertexOffset, meshPart.StartIndex, meshPart.PrimitiveCount);
        }
        Rectangle ViewBounds
        {
            get { return GraphicsDevice.Viewport.Bounds; }
        }
        void DrawFilledRect(Rectangle bounds, Color frameColor, Color fillColor)
        {
            game1.drawSquare(bounds, fillColor, 0);
            game1.drawFrame(bounds, frameColor, 1);
        }
        void DrawFilledRect(Rectangle bounds)
        {
            game1.drawSquare(bounds, monochrome(0.0f, 0.3f), 0);
            game1.drawFrame(bounds, monochrome(1.0f, 0.6f), 1);
        }
        void DrawWorldLabel2D(Vector3 position, string text, float width, float height)
        {
            Vector2 screen = playerCam.worldToScreen(position, GraphicsDevice.Viewport);
            Rectangle rect = game1.centeredRect(screen, width, height);
            game1.drawSquare(rect, monochrome(0.0f, 0.3f), 0);
            game1.drawString(text, rect, monochrome(1.0f), new Vector2(0.5f, 0.5f), true);
        }
        void ApplyBasicEffect(Matrix world, Matrix view, Matrix projection, Texture2D texture)
        {
            screenQuadEffect.LightingEnabled = false;
            screenQuadEffect.Alpha = 1.0f;
            screenQuadEffect.DiffuseColor = new Vector3(1);
            screenQuadEffect.World = world;
            screenQuadEffect.View = view;
            screenQuadEffect.Projection = projection;
            screenQuadEffect.TextureEnabled = true;
            screenQuadEffect.Texture = texture;
            screenQuadEffect.CurrentTechnique.Passes[0].Apply();
        }
        Point worldToCoord(Vector3 pos)
        {
            return new Point((int)(pos.X / widthm + 0.5f),
                (int)(pos.Z / depthm + 0.5f));
        }
        Vector3 coordToWorld(Point coord, float y = 0)
        {
            return new Vector3(
                (float)coord.X * widthm,
                y,
                (float)coord.Y * depthm);
        }
        public void drawgun(Gun g, float scale, bool useXnaEffect, Color xnaColor, int pass)
        {
            Matrix world =
                //Matrix.CreateTranslation(1,-1,-1) *
                g.rot *
                //camera.rotation3D *
                Matrix.CreateTranslation(g.pos);
            //Color color = monochrome(0.7f);
            //if (g.off)
            //    color = monochrome(0.1f, 1.0f);
            Color color = xnaColor;
            if (useXnaEffect)
            {
                game1.DrawModel(cubeModel,
                    Matrix.CreateScale(g.size / 2) *
                    Matrix.CreateTranslation(0, -g.size.Y / 2, 0) *
                    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
                    world,
                    //Matrix.CreateTranslation(camera.Position3D), 
                    playerCam.view, playerCam.projection, color);
            }
            else
            {
                DrawModel(cubeModel,
                    Matrix.CreateScale(g.size / 2) *
                    Matrix.CreateScale(scale) *
                    Matrix.CreateTranslation(0, -g.size.Y / 2, 0) *
                    world, g.rot, pass);
            }
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size / 2) *
            //    Matrix.CreateTranslation(0, -g.size.Y / 2, 0) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            float gpradius = 0.01f + (g.bulletSpeed / 200) * 0.01f;
            int gpc = (int)Math.Ceiling(g.bulletSpeed / 10);
            for (int i = 0; i < gpc; ++i)
            {
                //float step = g.size.Z/2 / (float) gpc;
                //float p = (float)i / (float)gpc;
                float zoff = (gpradius * 1.5f) * (float)i * 2;
                float z = -g.size.Z / 2 + zoff;
                //DrawModel(game1.cubeModel,
                //    Matrix.CreateScale(gpradius) *
                //    Matrix.CreateTranslation(g.size.X / 2 + gpradius, 0, z) *
                //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
                //    world,
                //    //Matrix.CreateTranslation(camera.Position3D), 
                //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            }
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size.X, 0.005f, g.size.Z/8) *
            //    Matrix.CreateTranslation(0, 0, -g.size.Z/2) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size.X / 10, g.size.Y / 2, g.size.Z / 2) *
            //    Matrix.CreateTranslation(g.size.X, -g.size.Y / 2, 0) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size.X / 10, g.size.Y / 2, g.size.Z / 2) *
            //    Matrix.CreateTranslation(-g.size.X, -g.size.Y / 2, 0) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size.X / 10, g.size.Y / 2, g.size.Z / 2) *
            //    Matrix.CreateTranslation(g.size.X * 2, -g.size.Y / 2, 0) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);
            //DrawModel(cubeModel,
            //    Matrix.CreateScale(g.size.X / 10, g.size.Y / 2, g.size.Z / 2) *
            //    Matrix.CreateTranslation(-g.size.X * 2, -g.size.Y / 2, 0) *
            //    //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    playerCam.view, playerCam.projection, color, true, game1.pixel);

            //for (float i = 0; i < 0.3f; i+=0.01f)
            //{
            //    DrawModel(cubeModel,
            //        Matrix.CreateScale(0.001f, 0.001f, 0.05f) *
            //        Matrix.CreateTranslation(0, i, -g.size.Z / 2) *
            //        //Matrix.CreateTranslation(0, 1-g.bulletSize/2, 0) *
            //        world,
            //        //Matrix.CreateTranslation(camera.Position3D), 
            //        playerCam.view, playerCam.projection, Color.Red, true, game1.pixel);
            //}
            //Vector3 sightSize = new Vector3(0.01f, 0.08f, 0.01f);
            //Matrix sightw = Matrix.CreateTranslation(g.size.X / 2 - sightSize.X / 2, 0, g.size.Z / 3) *
            //                g.rot *
            //                Matrix.CreateTranslation(g.pos);
            //Vector3 sightshift = new Vector3();
            //if (aimDownSight)
            //{
            //    float unitDistance = 60;
            //    Func<float, float> cos2 = (float angle) =>
            //     {
            //         return (float)Math.Pow(Math.Cos(angle), 2);
            //     };
            //    float x = unitDistance;
            //    Vector3 forward = Vector3.Transform(Vector3.Forward, g.rot);
            //    forward.Normalize();
            //    float v = g.bulletSpeed + Vector3.Dot(bodyState.vel, forward);
            //    float gr = 10;
            //    if (g.bulletAffectedByGravity)
            //        gr = 0;
            //    Vector3 flatForward = forward;
            //    flatForward.Y = 0;
            //    flatForward.Normalize();
            //    float theta = (float)Math.Atan2(forward.Y, Vector3.Dot(flatForward, forward));
            //    //http://www.softschools.com/formulas/physics/trajectory_formula/162/
            //    float y = x * (float)Math.Tan(theta) - (gr * (x * x)) / (2 * (v * v) * cos2(theta));
            //    Vector3 startPosition = g.pos + g.size.Z / 2 * forward;
            //    Vector3 endPosition = startPosition + Vector3.Up * y + flatForward * x;
            //    Vector2 endScreen = camera.worldToScreen(endPosition, GraphicsDevice.Viewport);
            //    Ray ray = camera.ScreenToRay(endScreen, GraphicsDevice.Viewport);
            //    Plane plane = Plane.Transform(new Plane(Vector3.Forward, 0), sightw);
            //    //Plane plane = Plane.Transform(new Plane(Vector3.Down, 0), sightw);
            //    float?hit = ray.Intersects(plane);
            //    Vector3 sightpos = Vector3.Transform(Vector3.Zero, sightw);
            //    if(hit.HasValue)
            //    {
            //        Vector3 contact = ray.Position + ray.Direction * hit.Value;
            //        sightshift.Y = contact.Y - sightpos.Y;
            //        //sightshift = Vector3.Dot(contact - sightpos, forward) * forward;
            //        game1.DrawModel(game1.sphereModel,
            //            Matrix.CreateScale(0.001f) * Matrix.CreateTranslation(contact),
            //            camera.view, camera.projection, monochrome(0.5f), false);
            //    }
            //}
            //Matrix.CreateTranslation(g.size.X / 2 - sightSize.X / 2, sightSize.Y / 2, g.size.Z / 3) *
            //              g.rot *
            //            Matrix.CreateTranslation(g.pos);//,
            //Matrix.CreateTranslation(sightpos);
            //Vector3 finalpos = Vector3.Transform(Vector3.Zero, sightworld);
            //Plane plane = Plane.Transform(new Plane(Vector3.Forward, 0), sightworld);

            //game1.DrawModel(cubeModel,
            //    Matrix.CreateTranslation(0,-1,0) *
            //    Matrix.CreateScale(sightSize/2) *
            //    sightw *
            //    Matrix.CreateTranslation(sightshift),
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    camera.view, camera.projection, monochrome(0.5f));
            //GraphicsDevice.RasterizerState = game1.wireFrameRs;
            //game1.DrawModel(cubeModel,
            //    world,
            //    //Matrix.CreateTranslation(camera.Position3D), 
            //    camera.view, camera.projection, monochrome(0.5f));
            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
        //struct DrawModelParameters
        //{
        //    public Model m;
        //    public Matrix w, v, p;
        //    public Color color;
        //    public bool lightingEnabled;
        //    public Texture2D floortx, walltx, normaltx;
        //}
        ////draw model
        //public void ApplyBasicHlsl(DrawModelParameters p)
        //{
        //    //ApplyCustomFx(w, v, p, color, texture, normal);
        //    basicfx.Parameters["World"].SetValue(p.w);
        //    basicfx.Parameters["View"].SetValue(p.v);
        //    basicfx.Parameters["Projection"].SetValue(p.p);
        //    basicfx.Parameters["DiffuseColor"].SetValue(p.color.ToVector4());
        //    basicfx.Parameters["AmbientColor"].SetValue(new Vector3(0.1f, 0.1f, 0.1f));// new Vector3(0.053f, 0.099f, 0.181f));
        //    //basicfx.Parameters["AmbientIntensity"].SetValue(0.0f);
        //    basicfx.Parameters["depthfactor"].SetValue(2.2f);
        //    basicfx.Parameters["uvscale"].SetValue(p.uvScale);
        //    basicfx.Parameters["specPow"].SetValue(1.0f);
        //    basicfx.Parameters["l"].SetValue(
        //        Vector3.Normalize(new Vector3(-.5265408f, -.5265408f, -.6275069f)));
        //    basicfx.Parameters["l2"].SetValue(
        //        Vector3.Normalize(new Vector3(.719f, .342f, .604f)));
        //    basicfx.Parameters["l3"].SetValue(
        //        Vector3.Normalize(new Vector3(.454f, -.766f, .454f)));
        //    basicfx.Parameters["colorA"].SetValue(monochrome(0.9f).ToVector3());// new Vector3(1, .961f, .808f));
        //    basicfx.Parameters["colorB"].SetValue(monochrome(0.65f).ToVector3());// new Vector3(.965f, .761f, .408f));
        //    basicfx.Parameters["colorC"].SetValue(monochrome(0.35f).ToVector3());// new Vector3(.323f, .361f, .394f));
        //    basicfx.Parameters["eyePos"].SetValue(Vector3.Transform(Vector3.Zero, Matrix.Invert(v)));
        //    int drawMode = 0; //diffuse
        //    if (!p.lightingEnabled)
        //        drawMode = 5;//tinted albedo
        //    basicfx.Parameters["drawmode"].SetValue(drawMode);
        //    bool lightA = true && p.lightingEnabled;
        //    bool lightB = true && p.lightingEnabled;
        //    bool lightC = true && p.lightingEnabled;
        //    basicfx.Parameters["enableA"].SetValue(lightA);
        //    basicfx.Parameters["enableB"].SetValue(lightB);
        //    basicfx.Parameters["enableC"].SetValue(lightC);
        //    if (basicfx.Parameters["diffusetx"] != null)
        //        basicfx.Parameters["diffusetx"].SetValue(p.floortx);
        //    if (basicfx.Parameters["normaltx"] != null)
        //        basicfx.Parameters["normaltx"].SetValue(p.normal);
        //    basicfx.CurrentTechnique.Passes[0].Apply();
        //}
        //public void DrawModel(Model m, Matrix w, Matrix v, Matrix p, Color color, bool lightingEnabled, Texture2D texture)
        //{
        //    DrawModel(m, w, v, p, color, texture, null, new Vector2(1.05f), lightingEnabled);
        //}
        //public void DrawModel(Model m, Matrix w, Matrix v, Matrix p, Color color, Texture2D texture, Texture2D normal, bool lightingEnabled = true)
        //{
        //    DrawModel(m, w, v, p, color, texture, normal, new Vector2(1.05f), lightingEnabled);
        //}
        //public void SetBasicHlslWallTx(Texture2D texture, Vector2 uv)
        //{
        //    basicfx.Parameters["walluv"].SetValue(uv);
        //    basicfx.Parameters["walltx"].SetValue(texture);
        //}
        //public void DrawModel(Model m, Matrix w, Matrix v, Matrix p, Color color, Texture2D texture, Texture2D normal, Vector2 uvScale, bool lightingEnabled = true)
        //{
        //    //game1.DrawModel(m, w, v, p, color, lightingEnabled, texture);
        //    //return;
        //    //ApplyCustomFx(w, v, p, color, texture, normal);
        //    basicfx.Parameters["World"].SetValue(w);
        //    basicfx.Parameters["View"].SetValue(v);
        //    basicfx.Parameters["Projection"].SetValue(p);
        //    basicfx.Parameters["DiffuseColor"].SetValue(color.ToVector4());
        //    basicfx.Parameters["AmbientColor"].SetValue(new Vector3(0.1f, 0.1f, 0.1f));// new Vector3(0.053f, 0.099f, 0.181f));
        //    //basicfx.Parameters["AmbientIntensity"].SetValue(0.0f);
        //    basicfx.Parameters["depthfactor"].SetValue(2.2f);
        //    basicfx.Parameters["uvscale"].SetValue(uvScale);
        //    basicfx.Parameters["specPow"].SetValue(1.0f);
        //    basicfx.Parameters["l"].SetValue(
        //        Vector3.Normalize(new Vector3(-.5265408f, -.5265408f, -.6275069f)));
        //    basicfx.Parameters["l2"].SetValue(
        //        Vector3.Normalize(new Vector3(.719f, .342f, .604f)));
        //    basicfx.Parameters["l3"].SetValue(
        //        Vector3.Normalize(new Vector3(.454f, -.766f, .454f)));
        //    basicfx.Parameters["colorA"].SetValue(monochrome(0.9f).ToVector3());// new Vector3(1, .961f, .808f));
        //    basicfx.Parameters["colorB"].SetValue(monochrome(0.65f).ToVector3());// new Vector3(.965f, .761f, .408f));
        //    basicfx.Parameters["colorC"].SetValue(monochrome(0.35f).ToVector3());// new Vector3(.323f, .361f, .394f));
        //    basicfx.Parameters["eyePos"].SetValue(Vector3.Transform(Vector3.Zero, Matrix.Invert(v)));
        //    int drawMode = 0; //diffuse
        //    if (!lightingEnabled)
        //        drawMode = 5;//tinted albedo
        //    basicfx.Parameters["drawmode"].SetValue(drawMode);
        //    bool lightA = true && lightingEnabled;
        //    bool lightB = true && lightingEnabled;
        //    bool lightC = true && lightingEnabled;
        //    basicfx.Parameters["enableA"].SetValue(lightA);
        //    basicfx.Parameters["enableB"].SetValue(lightB);
        //    basicfx.Parameters["enableC"].SetValue(lightC);
        //    if (basicfx.Parameters["diffusetx"] != null)
        //        basicfx.Parameters["diffusetx"].SetValue(texture);
        //    if (basicfx.Parameters["normaltx"] != null)
        //        basicfx.Parameters["normaltx"].SetValue(normal);
        //    basicfx.CurrentTechnique.Passes[0].Apply();
        //    foreach (ModelMesh mesh in m.Meshes)
        //    {
        //        foreach (ModelMeshPart part in mesh.MeshParts)
        //        {
        //            GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
        //            GraphicsDevice.Indices = part.IndexBuffer;
        //            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
        //                part.VertexOffset, part.StartIndex, part.PrimitiveCount);
        //        }
        //    }
        //}
        //public void ApplyMyBasicFx(Model m, Matrix w, Matrix v, Matrix p, Color color, Texture2D texture, Texture2D normal, Vector2 uvScale, bool lightingEnabled = true)
        //{
        //    //ApplyCustomFx(w, v, p, color, texture, normal);
        //    basicfx.Parameters["World"].SetValue(w);
        //    basicfx.Parameters["View"].SetValue(v);
        //    basicfx.Parameters["Projection"].SetValue(p);
        //    basicfx.Parameters["DiffuseColor"].SetValue(color.ToVector4());
        //    basicfx.Parameters["AmbientColor"].SetValue(new Vector3(0.1f, 0.1f, 0.1f));// new Vector3(0.053f, 0.099f, 0.181f));
        //    //basicfx.Parameters["AmbientIntensity"].SetValue(0.0f);
        //    basicfx.Parameters["depthfactor"].SetValue(2.2f);
        //    basicfx.Parameters["uvscale"].SetValue(uvScale);
        //    basicfx.Parameters["specPow"].SetValue(1.0f);
        //    basicfx.Parameters["l"].SetValue(
        //        Vector3.Normalize(new Vector3(-.5265408f, -.5265408f, -.6275069f)));
        //    basicfx.Parameters["l2"].SetValue(
        //        Vector3.Normalize(new Vector3(.719f, .342f, .604f)));
        //    basicfx.Parameters["l3"].SetValue(
        //        Vector3.Normalize(new Vector3(.454f, -.766f, .454f)));
        //    basicfx.Parameters["colorA"].SetValue(monochrome(0.9f).ToVector3());// new Vector3(1, .961f, .808f));
        //    basicfx.Parameters["colorB"].SetValue(monochrome(0.65f).ToVector3());// new Vector3(.965f, .761f, .408f));
        //    basicfx.Parameters["colorC"].SetValue(monochrome(0.35f).ToVector3());// new Vector3(.323f, .361f, .394f));
        //    basicfx.Parameters["eyePos"].SetValue(Vector3.Transform(Vector3.Zero, Matrix.Invert(v)));
        //    int drawMode = 0; //diffuse
        //    if (!lightingEnabled)
        //        drawMode = 5;//tinted albedo
        //    basicfx.Parameters["drawmode"].SetValue(drawMode);
        //    bool lightA = true && lightingEnabled;
        //    bool lightB = true && lightingEnabled;
        //    bool lightC = true && lightingEnabled;
        //    basicfx.Parameters["enableA"].SetValue(lightA);
        //    basicfx.Parameters["enableB"].SetValue(lightB);
        //    basicfx.Parameters["enableC"].SetValue(lightC);
        //    if (basicfx.Parameters["diffusetx"] != null)
        //        basicfx.Parameters["diffusetx"].SetValue(texture);
        //    if (basicfx.Parameters["normaltx"] != null)
        //        basicfx.Parameters["normaltx"].SetValue(normal);
        //    basicfx.CurrentTechnique.Passes[0].Apply();
        //}
        //public void DrawModel(Model m, Matrix w)
        //{
        //    deferfx.Parameters["wvp"].SetValue(w * playerCam.view * playerCam.projection);
        //    deferfx.CurrentTechnique.Passes[0].Apply();
        //    foreach (ModelMesh mesh in m.Meshes)
        //    {
        //        foreach (ModelMeshPart part in mesh.MeshParts)
        //        {
        //            GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
        //            GraphicsDevice.Indices = part.IndexBuffer;
        //            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
        //                part.VertexOffset, part.StartIndex, part.PrimitiveCount);
        //        }
        //    }
        //}

        //draw instances
        //public void DrawInstances(Model m, Matrix w)//, Matrix v, Matrix p, Color color, Texture2D texture, Texture2D normal, Vector2 uvScale, bool lightingEnabled = true)
        //{
        //    deferfx.Parameters["wvp"].SetValue(w * playerCam.view * playerCam.projection);
        //    deferfx.CurrentTechnique = deferfx.Techniques["Instanced"];
        //    deferfx.CurrentTechnique.Passes[0].Apply();

        //    instanceVertexBuffer.SetData(instanceTransforms, 0, instanceIterator);

        //    foreach (ModelMesh mm in m.Meshes)
        //    {
        //        foreach (ModelMeshPart mmp in mm.MeshParts)
        //        {
        //            GraphicsDevice.SetVertexBuffers(new VertexBufferBinding(mmp.VertexBuffer, 0), new VertexBufferBinding(instanceVertexBuffer, 0, 1));
        //            GraphicsDevice.Indices = mmp.IndexBuffer;
        //            //ApplyMyBasicFx(null, w, v, p, color, texture, normal, uvScale, lightingEnabled);
        //            GraphicsDevice.DrawInstancedPrimitives(
        //                PrimitiveType.TriangleList,
        //                mmp.VertexOffset, mmp.StartIndex,
        //                mmp.PrimitiveCount,
        //                instanceIterator);
        //        }
        //    }

        //    instanceIterator = 0;
        //}
    }
}