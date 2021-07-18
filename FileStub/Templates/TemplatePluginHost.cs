
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using NLog;

namespace FileStub.Templates.PluginHost
{
    //Code copypasted from RTCV and refitted for filestub
    //Sorry everyone, I had to make a separate plugin class for filestub as opposed to just making templates RTCV plugins as you HAVE to load a template BEFORE you can connect filestub to rtcv

    //public enum RTCSide
    //{
    //    Server,
    //    Client,
    //    Both
    //} //the only side for filestub plugins is filestub
    public interface IFileStubPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        Version Version { get; }
        //RTCSide SupportedSide { get; } //


        bool Start();
    }
    public class Host : IDisposable
    {
#pragma warning disable CS0649 //plugins are assigned by MEF, so "never assigned to" warning doesn't apply
        [ImportMany(typeof(IFileStubPlugin))]
        private IEnumerable<IFileStubPlugin> plugins;
        private List<IFileStubPlugin> _loadedPlugins;
        public IReadOnlyList<IFileStubPlugin> LoadedPlugins => _loadedPlugins;


        private CompositionContainer _container;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool initialized = false;

        public Host()
        {
            _loadedPlugins = new List<IFileStubPlugin>();
            AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private void initialize(string[] pluginDirs)
        {
            var catalog = new AggregateCatalog();
            foreach (var dir in pluginDirs)
            {
                if (Directory.Exists(dir))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(dir));
                }
            }
            _container = new CompositionContainer(catalog);

            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                logger.Error(compositionException, "Composition failed in plugin initialization");
            }
        }

        public void Start(string[] pluginDirs)
        {
            if (pluginDirs == null)
            {
                throw new ArgumentNullException(nameof(pluginDirs));
            }

            if (initialized)
            {
                logger.Error(new InvalidOperationException("Host has already been started."));
            }

            initialize(pluginDirs);

            foreach (var p in plugins)
            {
                if (/*p.SupportedSide == side || p.SupportedSide == RTCSide.Both*/true)
                {
                    logger.Info($"Loading {p.Name}");


                    //Hack Hack Hack. If we're in attached, we're both sides. We can't thin-client this, we're actually both sides.
                    //Start both sides and leave it up to the plugin dev to handle it for now if they're devving in attached mode (sorry Narry) //Narry 3-22-20


                    /*if (side == RTCSide.Both)
                    {
                        if (p.Start(RTCSide.Client))
                        {
                            logger.Info("Loaded {pluginName} as client successfully", p.Name);
                        }

                        if (p.Start(RTCSide.Server))
                        {
                            logger.Info("Loaded {pluginName} as server successfully", p.Name);
                        }

                        _loadedPlugins.Add(p);
                    }
                    else */if (p.Start())
                    {
                        logger.Info("Loaded {pluginName} successfully", p.Name);
                        _loadedPlugins.Add(p);
                    }
                    else
                    {
                        logger.Error("Failed to load {pluginName}", p.Name);
                    }
                }
            }
            initialized = true;
        }
        public void Shutdown()
        {
        }

        public void Dispose()
        {
            _container?.Dispose();
        }

        //We want people to be able to pack their plugins into singular binaries and Costura Fody seems like a good option
        //Costura doesn't play nicely with loading assemblies via reflection so we need Costura to register any of the loaded assemblies
        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Assembly assembly = null;
            if (args.LoadedAssembly.IsDynamic)
            {
                assembly = args.LoadedAssembly;
            }
            else
            {
                assembly = Assembly.LoadFile(args.LoadedAssembly.Location);
            }

            var assemblyLoaderType = assembly.GetType("Costura.AssemblyLoader", false);
            var attachMethod = assemblyLoaderType?.GetMethod("Attach", BindingFlags.Static | BindingFlags.Public);
            attachMethod?.Invoke(null, new object[] { });
        }
    }
}
