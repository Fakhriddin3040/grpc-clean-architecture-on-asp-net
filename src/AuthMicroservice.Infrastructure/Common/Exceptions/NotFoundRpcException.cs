using Grpc.Core;

namespace AuthMicroservice.Infrastructure.Common.Exceptions
{
    public class NotFoundRpcException : RpcException
    {
        public NotFoundRpcException(string message) 
            : base(new Status(StatusCode.NotFound, message)) {}

        public NotFoundRpcException(string message, Metadata trailers)
            : base(new Status(StatusCode.NotFound, message), trailers) {}
    }
}