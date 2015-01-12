using System;

namespace RexUpdate.Exceptions
{
    public class InvalidCheckResponseException:Exception
    {
        public InvalidCheckResponseException(string msg, params object[] args):base(string.Format(msg,args)){}
        public InvalidCheckResponseException(string msg, Exception innerException) : base(msg,innerException) { }
    }
}
