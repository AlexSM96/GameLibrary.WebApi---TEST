namespace GameLibrary.Application.Exceptions
{
    public class IsAlreadyExistException : Exception
    {
        public IsAlreadyExistException(string name) 
            : base($"Entity {name} is already exist") { }   
    }
}
