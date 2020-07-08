using System;

namespace Alpaki.CrossCutting.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException(long id): base($"Can't find entity {typeof(T).Name} with Id = {id}") { }
    }
}
