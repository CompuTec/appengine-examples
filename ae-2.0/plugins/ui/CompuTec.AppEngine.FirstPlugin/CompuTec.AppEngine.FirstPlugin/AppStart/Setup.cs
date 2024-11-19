﻿using System;
using System.Collections.Generic;
using System.Text;
using CompuTec.AppEngine.Base.Infrastructure.Plugins;
using CompuTec.AppEngine.FirstPlugin.API;
using CompuTec.AppEngine.FirstPlugin.Setup.Tables;

namespace CompuTec.AppEngine.FirstPlugin.Plugin.AppStart
{
	///Plugin Setup Implementation 
	///here you can implement all logic of plugin installation on specific database
	public class Setup : PluginSetup
	{
		public override bool CheckUpdate(Version currentVersion)
		{
			return currentVersion < new Version(FirstPluginInfo.NameVersion);
		}

		public override Version Update(string token)
		{

			var info = new FirstPluginInfo();

			Console.WriteLine("Update");

			List<CompuTec.Core2.DI.Setup.UDO.Model.ICustomField> customUdoFieldList = CustomUDOFields.getCustomFields();
			CompuTec.Core2.DI.Setup.UDO.Setup setup = new CompuTec.Core2.DI.Setup.UDO.Setup(token, customUdoFieldList, false, System.Reflection.Assembly
				.GetAssembly(typeof(FirstPlugin.Setup.Tables.ToDo.ToDoTable)), "CompuTec.AppEngine.FirstPlugin.Setup.Tables", "CompuTec.AppEngine.FirstPlugin.Setup.Tables",
				"CompuTec.AppEngine.FirstPlugin.Setup.Tables", "CompuTec.AppEngine.FirstPlugin.Setup.Tables", "CompuTec.AppEngine.FirstPlugin.Setup.Tables");

			setup.BaseLibInformation = info;

			if (setup.IsUpdateRequiredNew(true))
			{
				Console.WriteLine("Instaling...");
				var updateResult = setup.Update();

				if (!updateResult.Success)
				{
					var message = new StringBuilder();

					updateResult.Errors.ForEach(e =>
					{
						message.Append(e.Message);
					});

					throw new Exception(message.ToString());
				}


				Console.WriteLine(updateResult.ToString());
			}

			Console.WriteLine("Install finish");

			return Version;
		}

		public override Version Version => new Version(FirstPluginInfo.NameVersion);
	}
}