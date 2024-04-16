using Grpc.Core;

namespace AuthMicroservice.Infrastructure.Common.Exceptions
{
    public class AlreadyExistsRpcException : RpcException
    {
        public AlreadyExistsRpcException(string message) 
            : base(new Status(StatusCode.AlreadyExists, message)) {}

        public AlreadyExistsRpcException(string message, Metadata trailers)
            : base(new Status(StatusCode.AlreadyExists, message), trailers) {}
    }
}