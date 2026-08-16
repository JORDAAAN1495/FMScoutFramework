using System.IO;
using System.Reflection;

namespace FM.Pitchside.Core
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
#if MAC
            return;
#else
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
#endif
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
#if MAC
            return;
#else
            try
            {
                txtWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine(":{0}", logMessage);
            }
            catch (Exception ex)
            {
            }
#endif
        }
    }
}