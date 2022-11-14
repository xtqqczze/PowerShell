// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Runtime.Serialization;

namespace System.Management.Automation
{
    /// <summary>
    /// This is a wrapper for exception class
    /// <see cref="System.NotImplementedException"/>
    /// which provides additional information via
    /// <see cref="System.Management.Automation.IContainsErrorRecord"/>.
    /// </summary>
    /// <remarks>
    /// Instances of this exception class are usually generated by the
    /// Monad Engine.  It is unusual for code outside the Monad Engine
    /// to create an instance of this class.
    /// </remarks>
    [Serializable]
    public class PSNotImplementedException
            : NotImplementedException, IContainsErrorRecord
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the PSNotImplementedException class.
        /// </summary>
        /// <returns>Constructed object.</returns>
        public PSNotImplementedException()
            : base()
        {
        }

        #region Serialization
        /// <summary>
        /// Initializes a new instance of the PSNotImplementedException class
        /// using data serialized via
        /// <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        /// <returns>Constructed object.</returns>
        protected PSNotImplementedException(SerializationInfo info,
                                              StreamingContext context)
                : base(info, context)
        {
            _errorId = info.GetString("ErrorId");
        }

        /// <summary>
        /// Serializer for <see cref="System.Runtime.Serialization.ISerializable"/>
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new PSArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
            info.AddValue("ErrorId", _errorId);
        }
        #endregion Serialization

        /// <summary>
        /// Initializes a new instance of the PSNotImplementedException class.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Constructed object.</returns>
        public PSNotImplementedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PSNotImplementedException class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <returns>Constructed object.</returns>
        public PSNotImplementedException(string message,
                        Exception innerException)
                : base(message, innerException)
        {
        }
        #endregion ctor

        /// <summary>
        /// Additional information about the error.
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// Note that ErrorRecord.Exception is
        /// <see cref="System.Management.Automation.ParentContainsErrorRecordException"/>.
        /// </remarks>
        public ErrorRecord ErrorRecord
        {
            get
            {
                _errorRecord ??= new ErrorRecord(
                    new ParentContainsErrorRecordException(this),
                    _errorId,
                    ErrorCategory.NotImplemented,
                    null);

                return _errorRecord;
            }
        }

        private ErrorRecord _errorRecord;
        private readonly string _errorId = "NotImplemented";
    }
}
