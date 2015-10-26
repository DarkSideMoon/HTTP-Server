using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public class Session
    {
        public Session()
        {
            this.Objects = new Dictionary<string, object>();
            this.UpdateLastConnectionTime();
        }

        public DateTime LastConnection { get; set; }
        public bool Authenticated { get; set; }

        private Dictionary<string, object> Objects { get; set; }

        // Indexer for accessing session objects.  If an object isn't found, null is returned.
        public object this[string objectKey]
        {
            get
            {
                object val = null;
                this.Objects.TryGetValue(objectKey, out val);

                return val;
            }
            set
            {
                this.Objects[objectKey] = value;
            }
        }

        /// <summary>
        /// Returns true if the last request exceeds the specified expiration time in seconds.
        /// </summary>
        public bool IsExpired(int expirationInSeconds)
        {
            return (DateTime.Now - this.LastConnection).TotalSeconds > expirationInSeconds;
        }

        /// <summary>
        /// De-authorize the session.
        /// </summary>
        public void Expire()
        {
            this.Authenticated = false;
            // Don't remove the validation token, as we still essentially have a session, we just want the user to log in again.
        }

        private void UpdateLastConnectionTime()
        {
            this.LastConnection = DateTime.Now;
        }
    }
}
