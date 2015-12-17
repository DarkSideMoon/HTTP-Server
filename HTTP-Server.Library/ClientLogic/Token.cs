using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace HttpServer.Library.ClientLogic
{
    public class Token
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private TimeSpan timerInterval = new TimeSpan(0, 0, 30);

        private string _tokenString = string.Empty;
        public string TokenString
        {
            get { return _tokenString; }
        }

        public long GetDuration
        {
            get { return dispatcherTimer.Interval.Ticks; }
        }

        public delegate void DispatcherTokenTimer();
        public event DispatcherTokenTimer OnResetToken;

        public Token()
        {
            this.InitializeTimer();
            this._tokenString = this.GenerateToken(10);
        }

        private void InitializeEvent()
        {
            //this.OnResetToken += new EventHandler(ResetToken);
        }

        private void InitializeTimer()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(ResetToken);
            dispatcherTimer.Interval = this.timerInterval;
            dispatcherTimer.Start();
        }

        private void ResetToken(object sender, EventArgs e)
        {
            this._tokenString = null;

            OnResetToken();

            dispatcherTimer.Stop();
            dispatcherTimer.Interval = new TimeSpan();
        }

        private string GenerateToken(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
