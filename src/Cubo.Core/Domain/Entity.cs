using System;

namespace Cubo.Core.Domain
{
    public class Entity
    {
         public Guid Id { get; protected set; }

         protected Entity()
         {
           Id = new Guid();  
         }
    }
}