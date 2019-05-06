using System;

namespace TEAMModelOS.SDK.Context.Exception
{
    public class BizException : System.Exception
    {
        public string message { get; set; } = "error";
        public int code { get; set; } = 1;
        public string devMessage { get; set; }
        public BizException(int code, String message, string stackTrace) : base(message)
        {
            if (string.IsNullOrEmpty(stackTrace))
            {
                this.devMessage = this.StackTrace;
            }
            else
            {
                this.devMessage = stackTrace;
            }
            this.message = message;
            this.code = code;
        }
        public BizException(int code, String message) : base(message)
        {
            this.devMessage = this.StackTrace;
            this.message = message;
            this.code = code;
        }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class.
        public BizException() : base()
        {
            this.devMessage = this.StackTrace;
            this.message = "error";
            this.code = 1;
        }

        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message.
        //
        // 参数:
        //   message:
        //     The message that describes the error.
        public BizException(string message) : base(message)
        {
            this.message = message;
            this.devMessage = this.StackTrace;
            this.code = 1;
        }
    }
}
