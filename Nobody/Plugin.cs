using System;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events;
using Handlers = Exiled.Events.Handlers;

namespace Nobody
{
	public class Plugin : Exiled.API.Features.Plugin<Nobody.Config>
	{
		//Instance variable for eventhandlers
		public EventHandlers EventHandlers;

		public override string Author { get; } = "gamehunt";
		public override string Name { get; } = "Nobody";
		public override string Prefix { get; } = "Nobody";
		public override Version Version { get; } = new Version(1, 0, 0);
		public override Version RequiredExiledVersion { get; } = new Version(2, 1, 3);

		public System.Random Random { get; } = new System.Random();

		public static Plugin Instance { get; private set; }
		public override void OnEnabled()
		{
			try
			{

				Instance = this;

				Log.Debug("Initializing event handlers..");
				//Set instance varible to a new instance, this should be nulled again in OnDisable
				EventHandlers = new EventHandlers();
				//Hook the events you will be using in the plugin. You should hook all events you will be using here, all events should be unhooked in OnDisabled
				Handlers.Server.RoundStarted += EventHandlers.OnRoundStart;

				Log.Info($"SerpentsHand plugin loaded. @gamehunt");
			}
			catch (Exception e)
			{
				//This try catch is redundant, as EXILED will throw an error before this block can, but is here as an example of how to handle exceptions/errors
				Log.Error($"There was an error loading the plugin: {e}");
			}
		}

		public override void OnDisabled()
		{
			Handlers.Server.RoundStarted -= EventHandlers.OnRoundStart;

			EventHandlers = null;
			Instance = null;
		}

		public override void OnReloaded()
		{
			//This is only fired when you use the EXILED reload command, the reload command will call OnDisable, OnReload, reload the plugin, then OnEnable in that order. There is no GAC bypass, so if you are updating a plugin, it must have a unique assembly name, and you need to remove the old version from the plugins folder
		}
	}
}