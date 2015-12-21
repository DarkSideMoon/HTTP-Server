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
        private EventHandler resetTokenEvent;

        private string _tokenString = string.Empty;

        public Token()
        {
            this.InitializeTimer();
            this._tokenString = this.GenerateToken(10);
            this.resetTokenEvent = new EventHandler(this.OnResetToken);
        }

        public delegate void DispatcherTokenTimer();

        public string TokenString
        {
            get { return this._tokenString; }
        }

        public long GetDuration
        {
            get { return this.dispatcherTimer.Interval.Ticks; }
        }

        private void InitializeEvent()
        {
            //this.OnResetToken += new EventHandler(ResetToken);
        }

        private void InitializeTimer()
        {
            this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.dispatcherTimer.Tick += new EventHandler(this.ResetToken);
            this.dispatcherTimer.Interval = this.timerInterval;
            this.dispatcherTimer.Start();
        }

        private void ResetToken(object sender, EventArgs e)
        {
            this._tokenString = null;

            this.OnResetToken(null, null);

            this.dispatcherTimer.Stop();
            this.dispatcherTimer.Interval = new TimeSpan();
        }

        private void OnResetToken(object param, EventArgs e)
        {
            Console.WriteLine("Token changed");
        }

        private string GenerateToken(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
