﻿using System;

namespace Rivets
{
	public class AppLinks
	{
		public AppLinks ()
		{
		}

		static AppLinks()
		{
			Navigator = new AppLinkNavigator ();
		}

		public const string Version = "1.0";
		public const string UserAgent = "Rivets 1.0";

		public static IAppLinkNavigation Navigator { get; set; }
	}
}

