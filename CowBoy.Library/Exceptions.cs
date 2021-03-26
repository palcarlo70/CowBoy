using System;
using System.Runtime.Serialization;

namespace CowBoy.Library
{
    [Serializable]
    public abstract class BaseException : ApplicationException
    {
        public BaseException()
            : base()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

    [Serializable]
    public class SecurityException : BaseException
    {
        public SecurityException()
            : base()
        {
        }

        public SecurityException(string message)
            : base(message)
        {
        }

        public SecurityException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected SecurityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }

    [Serializable]
    public class ProcessException : BaseException
    {
        public ProcessException()
            : base()
        {
        }

        public ProcessException(string message)
            : base(message)
        {
        }

        public ProcessException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected ProcessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class BusinessException : BaseException
    {
        public BusinessException()
            : base()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, Exception exception) :
            base(message, exception)
        {
        }


        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class DataException : BaseException
    {

        public DataException()
            : base()
        {
        }

        public DataException(string message)
            : base(message)
        {
        }

        public DataException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

    }


    [Serializable]
    public class WarningException : BaseException
    {
        public WarningException()
            : base()
        {
        }

        public WarningException(string message)
            : base(message)
        {
        }

        public WarningException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected WarningException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class UIException : BaseException
    {
        public UIException()
            : base()
        {
        }

        public UIException(string message)
            : base(message)
        {
        }

        public UIException(string message, Exception exception) :
            base(message, exception)
        {
        }

        protected UIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}