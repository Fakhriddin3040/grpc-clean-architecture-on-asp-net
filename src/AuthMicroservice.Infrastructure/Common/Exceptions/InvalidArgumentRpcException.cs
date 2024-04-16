using Grpc.Core;

namespace AuthMicroservice.Infrastructure.Common.Exceptions
{
    public class InvalidArgumentRpcException : RpcException
    {
        public InvalidArgumentRpcException(string message)
            : base(new Status(StatusCode.InvalidArgument, message)) {}

        public InvalidArgumentRpcException(string message, Metadata trailers)
            : base(new Status(StatusCode.InvalidArgument, message), trailers) {}
    }
}