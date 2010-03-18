using System;
using System.Threading;
using DotNetOpenMail;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for SendThread.
	/// </summary>
	public class SendThread
	{
		LogWindow _logwindow;
		Thread _thread;

		EmailAddress _fromaddress;
		EmailAddress[] _toaddresses;

		String _subject=null;
		String _htmlcontent=null;
		String _textcontent=null;
		EmailAddress _envelopefrom=null;
		SmtpServer _smtpserver=null;

		public SendThread(
			EmailAddress fromaddress, 
			EmailAddress[] toaddresses,
			EmailAddress envelopefrom, 
			String subject, 
			String htmlcontent, 
			String textcontent,
			SmtpServer smtpserver,
			LogWindow logwindow)
		{
			_fromaddress=fromaddress;
			_toaddresses=toaddresses;
			_envelopefrom=envelopefrom;
			_subject=subject;
			_htmlcontent=htmlcontent;
			_textcontent=textcontent;
			_smtpserver=smtpserver;
			_logwindow=logwindow;

		}

		public void Run() 
		{	

			_thread=new Thread(new ThreadStart(StartThread));
			_thread.Start();

		}

		private void StartThread() 
		{
			try 
			{
				EmailMessage message=new EmailMessage();

				message.Subject=_subject;
				message.FromAddress=_fromaddress;

				EmailAddress[] toaddresses=_toaddresses;

				for (int i=0; i<toaddresses.Length; i++) 
				{
					message.AddToAddress(toaddresses[i]);
				}

				if (this._envelopefrom!=null) 
				{
					message.EnvelopeFromAddress=_envelopefrom;
				}
				bool hashtml=false;
				if (_htmlcontent!=null && _htmlcontent.Trim()!="") 
				{
					hashtml=true;
					message.HtmlPart=new HtmlAttachment(_htmlcontent);
				}
				
				if (_textcontent!=null && _textcontent.Trim()!="") 
				{
					if (hashtml) 
					{
						message.TextPart=new TextAttachment(_textcontent);
					}
					else 
					{
						message.BodyText=_textcontent;
					}
				}
				//			LogWindow logwindow=new LogWindow();
				//			logwindow.ShowDialog(this);

				_smtpserver.LogSmtpWrite+=new SmtpServer.LogHandler(_logwindow.LogSmtpWrite);

				_smtpserver.LogSmtpReceive+=new SmtpServer.LogHandler(_logwindow.LogSmtpReceive);

				_smtpserver.LogSmtpCompleted+=new SmtpServer.LogHandler(_logwindow.LogSmtpCompleted);
								
				_logwindow.LogInfo("Sending to "+_smtpserver.ToString()+"\r\n");
				message.Send(_smtpserver);
			}
			catch (Exception ex) 
			{
				_logwindow.LogError(ex.Message+"\r\n");
			}

		}
	}
}
