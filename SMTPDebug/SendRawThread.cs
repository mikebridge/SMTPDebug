using System;
using System.Threading;

using DotNetOpenMail;
using DotNetOpenMail.Logging;

namespace SMTPDebug
{
	/// <summary>
	/// Summary description for SendRawThread.
	/// </summary>
	public class SendRawThread
	{
		private EmailAddress _envelopefrom;
		private EmailAddress[] _mailto;
		private String _rawtext; 
		private SmtpServer _smtpserver;
		private Thread _thread;
		private LogWindow _logwindow;

		public SendRawThread(EmailAddress envelopefrom, EmailAddress[] mailto, String rawtext, SmtpServer smtpserver, LogWindow logwindow)
		{
			this._envelopefrom=envelopefrom;
			this._mailto=mailto;
			this._rawtext=rawtext;
			this._smtpserver=smtpserver;
			this._logwindow=logwindow;
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
				RawEmailMessage message=new RawEmailMessage();

				message.MailFrom=_envelopefrom;
				foreach (EmailAddress to in _mailto) 
				{
					message.RcptToAddresses.Add(to);
				}
				message.Content=_rawtext;
				
				_smtpserver.LogSmtpWrite+=new SmtpServer.LogHandler(_logwindow.LogSmtpWrite);

				_smtpserver.LogSmtpReceive+=new SmtpServer.LogHandler(_logwindow.LogSmtpReceive);

				_smtpserver.LogSmtpCompleted+=new SmtpServer.LogHandler(_logwindow.LogSmtpCompleted);
								
				_logwindow.LogInfo("Sending Email to "+_smtpserver.ToString()+"\r\n");

				message.Send(_smtpserver);
			}
			catch (Exception ex) 
			{
				_logwindow.LogError(ex.Message+"\r\n");
			}

		}


	}
}
