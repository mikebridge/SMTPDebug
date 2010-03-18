using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

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
		public static SmtpServerAddressCollection Load(String filename) 
		{
			FileInfo thefile=new FileInfo(filename);
			if (!thefile.Exists) 
			{
				FileInfo origfile=new FileInfo(filename+".orig");
				origfile.CopyTo(filename);
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
				reader.Close();
			}
			return coll;
		} 
		#endregion
    
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

		private XmlSerializer GetXmlSerializer() 
		{			
			
			XmlSerializer xmlserializer=new XmlSerializer(typeof(SmtpServerAddressCollection), new System.Xml.Serialization.XmlRootAttribute("smtpservers"));
			return xmlserializer;
		}


		
		

	}
}
