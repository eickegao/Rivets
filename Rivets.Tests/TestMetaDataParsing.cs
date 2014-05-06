﻿using System;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using NUnit.Framework;
using Owin;

namespace Rivets.Tests
{
	[TestFixture ]
	public class TestMetaDataParsing
	{
		const string HOST_BASE = "http://localhost:4477/";

		[SetUp]
		public void Setup()
		{
			var root = System.IO.Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Html");
			var options = new FileServerOptions
			{
				EnableDirectoryBrowsing = true, 
				FileSystem = new PhysicalFileSystem(root)
			};

			WebApp.Start(HOST_BASE, builder => builder.UseFileServer(options));            
		}

		[Test]
		public void SimpleAndroidMetaDataTest ()
		{
			var resolver = new HttpClientAppLinkResolver ();
			var appLinks = resolver.ResolveAppLinks (HOST_BASE + "SimpleAndroidMetaData.html").Result;

			Assert.IsNotNull (appLinks);
			Assert.Greater (appLinks.Targets.Count, 0);
		}

		[Test]
		public void SimpleiOSMetaDataTest ()
		{
			var resolver = new HttpClientAppLinkResolver ();
			var appLinks = resolver.ResolveAppLinks (HOST_BASE + "SimpleiOSMetaData.html").Result;

			Assert.IsNotNull (appLinks);
			Assert.Greater (appLinks.Targets.Count, 0);

			Assert.IsTrue (appLinks.Targets [0] is IOSAppLinkTarget);
			var t = (IOSAppLinkTarget)appLinks.Targets [0];
			Assert.AreEqual (t.Url.ToString(), "example://products/?id=widget");
			Assert.AreEqual (t.AppStoreId, "12345");
			Assert.AreEqual (t.AppName, "Example Store");
		}

		[Test]
		public void SimpleiPhoneMetaDataTest ()
		{
			var resolver = new HttpClientAppLinkResolver ();
			var appLinks = resolver.ResolveAppLinks (HOST_BASE + "SimpleiPhoneMetaData.html").Result;

			Assert.IsNotNull (appLinks);
			Assert.Greater (appLinks.Targets.Count, 0);

			Assert.IsTrue (appLinks.Targets [0] is IPhoneAppLinkTarget);
			var t = (IOSAppLinkTarget)appLinks.Targets [0];
			Assert.AreEqual (t.Url.ToString(), "example://products/?id=widget");
			Assert.AreEqual (t.AppStoreId, "12345");
			Assert.AreEqual (t.AppName, "Example Store");
		}

		[Test]
		public void SimpleiPadMetaDataTest ()
		{
			var resolver = new HttpClientAppLinkResolver ();
			var appLinks = resolver.ResolveAppLinks (HOST_BASE + "SimpleiPadMetaData.html").Result;

			Assert.IsNotNull (appLinks);
			Assert.Greater (appLinks.Targets.Count, 0);

			Assert.IsTrue (appLinks.Targets [0] is IPadAppLinkTarget);
			var t = (IOSAppLinkTarget)appLinks.Targets [0];
			Assert.AreEqual (t.Url.ToString(), "example://products/?id=widget");
			Assert.AreEqual (t.AppStoreId, "12345");
			Assert.AreEqual (t.AppName, "Example Store");
		}

		[Test]
		public void MultiTargetsMetaData ()
		{
			var resolver = new HttpClientAppLinkResolver ();
			var appLinks = resolver.ResolveAppLinks (HOST_BASE + "MultiTargetsMetaData.html").Result;

			Assert.IsNotNull (appLinks);
			Assert.AreEqual (appLinks.Targets.Count, 4);
		}
	}
}

