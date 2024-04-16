namespace AuthMicroservice.Domain.Abstracts.Fields
{
    public abstract class SaltGenerator
    {
        public virtual string Salt => BCrypt.Net.BCrypt.GenerateSalt();
    }
}