//Copyright (c) Telerik.  All rights reserved.
//
// usage example:
//
// // Get ObjectScope from ObjectScopeProvider
// IObjectScope scope = ObjectScopeProvider1.ObjectScope();
// // start transaction
// scope.Transaction.Begin();
// // create new persistent object person and add to scope
// Person p = new Person();
// scope.Add(p);
// // commit transction
// scope.Transaction.Commit();
//

using Telerik.OpenAccess;
using Telerik.OpenAccess.Util;

namespace saibabacharityreceiptorDL
{
	/// <summary>
	/// This class provides an object context for connected database access.
	/// </summary>
	/// <remarks>
	/// This class can be used to obtain an IObjectScope instance required for a connected database
	/// access.
	/// </remarks>
	public class ObjectScopeProvider1 : IObjectScopeProvider
	{
		private Database MyDatabase;
		private IObjectScope MyScope;

		static private ObjectScopeProvider1 TheObjectScopeProvider1;

	    /// <summary>
		/// Adjusts for dynamic loading when no entry assembly is available/configurable.
		/// </summary>
		/// <remarks>
        /// When dynamic loading is used, the configuration path from the
        /// applications entry assembly to the connection setting might be broken.
        /// This method makes up the necessary configuration entries.
        /// </remarks>
        static public void AdjustForDynamicLoad()
        {
            if( TheObjectScopeProvider1 == null )
                TheObjectScopeProvider1 = new ObjectScopeProvider1();

            if( TheObjectScopeProvider1.MyDatabase == null )
            {
                string assumedInitialConfiguration =
                           "<openaccess>" +
                               "<references>" +
                                   "<reference assemblyname='PLACEHOLDER' configrequired='True'/>" +
                               "</references>" +
                           "</openaccess>";
                System.Reflection.Assembly dll = TheObjectScopeProvider1.GetType().Assembly;
                assumedInitialConfiguration = assumedInitialConfiguration.Replace(
                                                    "PLACEHOLDER", dll.GetName().Name);
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(assumedInitialConfiguration);
                Database db = Telerik.OpenAccess.Database.Get("DatabaseConnection1",
                                            xmlDoc.DocumentElement,
                                            new[] { dll } );

                TheObjectScopeProvider1.MyDatabase = db;
            }
        }

		/// <summary>
		/// Returns the instance of Database for the connectionId
		/// specified in the Enable Project Wizard.
		/// </summary>
		/// <returns>Instance of Database.</returns>
		/// <remarks></remarks>
		static public Database Database()
		{
			if( TheObjectScopeProvider1 == null )
				TheObjectScopeProvider1 = new ObjectScopeProvider1();

			if( TheObjectScopeProvider1.MyDatabase == null )
				TheObjectScopeProvider1.MyDatabase = Telerik.OpenAccess.Database.Get( "DatabaseConnection1" );

			return TheObjectScopeProvider1.MyDatabase;
		}

		/// <summary>
		/// Returns the instance of ObjectScope for the application.
		/// </summary>
		/// <returns>Instance of IObjectScope.</returns>
		/// <remarks></remarks>
		static public IObjectScope ObjectScope()
		{
			Database();

			if( TheObjectScopeProvider1.MyScope == null )
				TheObjectScopeProvider1.MyScope = GetNewObjectScope();

			return TheObjectScopeProvider1.MyScope;
		}

		/// <summary>
		/// Returns the new instance of ObjectScope for the application.
		/// </summary>
		/// <returns>Instance of IObjectScope.</returns>
		/// <remarks></remarks>
		static public IObjectScope GetNewObjectScope()
		{
			Database db = Database();

			IObjectScope newScope = db.GetObjectScope();
			return newScope;
		}
	}
}