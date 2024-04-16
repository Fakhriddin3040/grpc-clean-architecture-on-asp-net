using Grpc.Core;

namespace AuthMicroservice.Infrastructure.Common.Exceptions
{
    public class UnauthenticatedRpcException : RpcException
    {
        public UnauthenticatedRpcException(string message) 
            : base(new Status(StatusCode.Unauthenticated, message)) {}

        public UnauthenticatedRpcException(string message, Metadata trailers)
            : base(new Status(StatusCode.Unauthenticated, message), trailers) {}
    }
}