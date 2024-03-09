using System.Collections.Generic;
using System.Text;

namespace SparkNest.Common.Base.Services
{
    public interface ISharedIdentityService
    {
        /// <summary>
        /// Accessing the current user's identity
        /// </summary>
        public string UserId { get;}
    }
}
