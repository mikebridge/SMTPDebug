using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SMTPDebug
{

    [Serializable]
	public class SmtpServerAddressCollection : System.Collections.CollectionBase
	{		
		public SmtpServerAddress this[ int index ]  
		{
			get  
			{
				return( (SmtpServerAddress) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}

		public int Add( SmtpServerAddress value )  
		{
			return( List.Add( value ) );
		}

		public int IndexOf( SmtpServerAddress value )  
		{
			return( List.IndexOf( value ) );
		}

		public void Insert( int index, SmtpServerAddress value )  
		{
			List.Insert( index, value );
		}

		public void Remove( SmtpServerAddress value )  
		{
			List.Remove( value );
		}

		#region Load
		public static SmtpServerAddressCollection Load() 
		{
            String filename = EnsureUserFile();

            FileInfo thefile=new FileInfo(filename);
			if (!thefile.Exists) 
			{
                throw new ApplicationException(String.Format("Can't open {0}", filename));
			}
			TextReader reader = null;

			SmtpServerAddressCollection coll=new SmtpServerAddressCollection();

			try 
			{
				reader=new StreamReader(filename);
				XmlSerializer serializer = coll.GetXmlSerializer();
				coll=(SmtpServerAddressCollection) serializer.Deserialize(reader);
			}
			finally 
			{
                if (reader!=null)
                {
                    reader.Close();
                }
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

        #region GetSmtpServerConfigFile
        public static String GetSmtpServerConfigFile()
        {
            //return System.IO.Path.Combine(Application.StartupPath,"SmtpServers.xml");
            return System.IO.Path.Combine(Application.UserAppDataPath, "SmtpServers.xml");
        }

        /// <summary>
        /// Creates the user file from the default.  If the default is missing, an
        /// error is thrown.
        /// </summary>
        /// <param name="destination"></param>
        private static void CreateUserFile(String destination)
        {
            const string demofilename = "SmtpServers.xml.demo";
            bool found = false;
            String dirstried = "";
            foreach (String dir in new[]
                                       {
                                           Application.StartupPath
                                           //Application.ExecutablePath
                                       }
                )
            {
                dirstried += " " + dir;
                FileInfo origfile = new FileInfo(System.IO.Path.Combine(dir, demofilename));
                if (origfile.Exists)
                {
                    origfile.CopyTo(destination);
                    found = true;
                    //return destination;
                }
            }
            if (!found)
            {

                throw new ApplicationException(String.Format("Could not find the {0} file in {1}.", demofilename,
                                                             dirstried));
            }
        }

        #endregion



		#region Save
		public void Save() 
		{
            String filename = GetSmtpServerConfigFile();
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

		private XmlSerializer GetXmlSerializer() 
		{			
			
			XmlSerializer xmlserializer=new XmlSerializer(typeof(SmtpServerAddressCollection), new System.Xml.Serialization.XmlRootAttribute("smtpservers"));
			return xmlserializer;
		}


		
		

	}
}
