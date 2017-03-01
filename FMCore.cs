using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Entities;
using FMScoutFramework.Core.Entities.InGame;

namespace FMScoutFramework.Core
{
    public class FMCore : IDisposable
    {
        public GameManager gameManager = null;
        public ObjectManager objectManager = null;
        public static LogWriter logger = new LogWriter("FMSF Private v1.0.0 Initialising...\n");

        public DatabaseModeEnum DatabaseMode { get; private set; }

        public event Action GameLoaded = () => { };

        public FMCore (DatabaseModeEnum databaseMode)
        {
            DatabaseMode = databaseMode;
        }

        public FMCore ()
        {
            DatabaseMode = DatabaseModeEnum.Realtime;
        }

        #region Objects
        public IEnumerable<Award> Awards { get { return GetListFromStore<Award>(); } }
        public IEnumerable<Continent> Continents { get { return GetListFromStore<Continent> (); } }
        public IEnumerable<City> Cities { get { return GetListFromStore<City> (); } }
        public IEnumerable<Club> Clubs { get { return GetListFromStore<Club> (); } }
        public IEnumerable<Nation> Nations { get { return GetListFromStore<Nation> (); } }
        public IEnumerable<Player> Players { get { return GetListFromStore<Player> (); } }
        public IEnumerable<Staff> Staff { get { return GetListFromStore<Staff> (); } }
        public IEnumerable<PlayerStaff> PlayerStaff { get { return GetListFromStore<PlayerStaff> (); } }
        public IEnumerable<Team> Teams { get { return GetListFromStore<Team>(); } }

        private IQueryable<T> GetListFromStore<T> ()
        {
            return ((Dictionary<Int64, T>)objectManager.ObjectStore [typeof (T)]).Values.AsQueryable ();
        }
        #endregion

        public void LoadData ()
        {
            LoadData (false);
        }

        public void LoadData (bool refreshPersonCache)
        {
            if (CheckProcessAndGame ()) {
                LoadDataForCheckedGame(refreshPersonCache);
            }

            GameLoaded();
        }

        public bool CheckProcessAndGame ()
        {
            if (gameManager == null) { 
                gameManager = new GameManager ();
                gameManager.logger = logger;
                gameManager.FMLoading = true;
                logger.LogWrite("Searching for FM Process...");
                gameManager.findFMProcess();
                logger.LogWrite("FM Process search finished.");
                gameManager.FMLoading = false;
            }

            logger.LogWrite("Load result is " + gameManager.FMLoaded.ToString());

            return gameManager.FMLoaded;
        }

        public void LoadDataForCheckedGame (bool refreshPersonCache)
        {
            if (objectManager == null)
                objectManager = new ObjectManager (gameManager, DatabaseMode);

            objectManager.Load (refreshPersonCache);
        }

        public void ReloadGameData() {
            // Clean up
            this.Dispose();

            this.LoadData();
        }

        private MetaDataCls _metaData;
        public MetaDataCls MetaData {
            get {
                if (_metaData == null) {
                    _metaData = new MetaDataCls(this, this.objectManager, gameManager);
                }

                return _metaData;
            }
            set {
                if (_metaData != value) {
                    _metaData = value;
                }
            }
        }

        #region IDisposable Members
        public void Dispose () {
            objectManager.ClearObjectStore();

            gameManager = null;
            objectManager = null;
            _metaData = null;
        }
        #endregion
    }

    public class MetaDataCls
    {
        private readonly Global glob;
        private readonly GameManager gameManager;

        internal MetaDataCls (FMCore fmCore, ObjectManager objectManager, GameManager gameManager)
        {
            this.gameManager = gameManager;
            glob = new Global (gameManager.Version);
        }

        public string CurrentVersion {
            get { return gameManager.Version.Description; }
        }

        public DateTime InGameDate {
            get { return glob.InGameDate; }
        }

        public int ActiveObjectID {
            get { return glob.ActiveObjectID; }
        }
    }
}
