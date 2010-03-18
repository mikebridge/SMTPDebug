using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections;

namespace SMTPDebug
{
	/// <summary>
	/// Collection of Addresses
	/// </summary>
	[Serializable]
	public class AddressBook : CollectionBase
	{
		public AddressBook()
		{
		}

		public Address this[ int index ]  
		{
			get  
			{
				return( (Address) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}

		public int Add( Address value )  
		{
			return( List.Add( value ) );
		}

		public int IndexOf( Address value )  
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, Address value )  
		{
			List.Insert( index, value );
		}

		public void Remove( Address value )  
		{
			List.Remove( value );
		}

		#region Load
		public static AddressBook Load() 
		{
            String filename = EnsureUserFile();

            FileInfo thefile = new FileInfo(filename);
            if (!thefile.Exists)
            {
                throw new ApplicationException(String.Format("Can't open {0}", filename));
            }

			TextReader reader = null;
			AddressBook coll=new AddressBook();
			try 
			{
				reader=new StreamReader(filename);
				XmlSerializer serializer = coll.GetXmlSerializer();
				coll=(AddressBook) serializer.Deserialize(reader);
			}
			finally 
			{
				reader.Close();
			}
			return coll;
		} 
		#endregion

        private static String EnsureUserFile()
        {
            String userFileLocation = GetSmtpServerConfigFile();
            if (!File.Exists(userFileLocation))
            {
                CreateUserFile(userFileLocation);
            }
            return userFileLocation;
        }

        public static String GetSmtpServerConfigFile()
        {
            //return System.IO.Path.Combine(Application.StartupPath,"AddressBook.xml");
            return System.IO.Path.Combine(Application.UserAppDataPath, "AddressBook.xml");
        }

        /// <summary>
        /// Creates the user file from the default.  If the default is missing, an
        /// error is thrown.
        /// </summary>
        /// <param name="destination"></param>
        private static void CreateUserFile(String destination)
        {
            const string demofilename = "AddressBook.xml.demo";
            bool found = false;
            foreach (String dir in new[] 
                {
                    Application.StartupPath
                }
                )
            {
                FileInfo origfile = new FileInfo(System.IO.Path.Combine(dir, demofilename));
                if (origfile.Exists)
                {
                    found=true;
                    origfile.CopyTo(destination);
                }
            }
            if (!found)
            {
                throw new ApplicationException(String.Format("Could not find the {0} file.", demofilename));
            }
        }

		#region Save
		public void Save(String filename) 
		{
			//StringBuilder sb=new StringBuilder("");
			//StringWriter sw=null;			
			StreamWriter sw=null;
			try 
			{
				XmlSerializer serializer=GetXmlSerializer();
				sw=new StreamWriter(filename);
				XmlSerializerNamespaces ns=new XmlSerializerNamespaces();
				ns.Add("", "");
				serializer.Serialize(sw, this, ns);
			}
			finally 
			{
				sw.Close();
			}
		}
		#endregion

		#region GetXmlSerializer
		private XmlSerializer GetXmlSerializer() 
		{			
			
			XmlSerializer xmlserializer=new XmlSerializer(typeof(AddressBook), new System.Xml.Serialization.XmlRootAttribute("addressbook"));
			return xmlserializer;
		}
		#endregion

	}
}
