﻿
using MongoDB.Bson.Serialization.Attributes;
using DataAccessLayer;
using Models;

namespace DataAccessLayerTests
{
    [CollectionName("AnimalsTest")]
    [BsonKnownTypes(typeof(Bird), typeof(Dog))]
    public abstract class Animal : Entity { }

    [CollectionName("Catlikes")]
    [BsonKnownTypes(typeof(Lion), typeof(Cat))]
    public abstract class CatLike : Animal { }

    [CollectionName("Birds")]
    [BsonKnownTypes(typeof(Parrot))]
    public class Bird : Animal { }

    public class Lion : CatLike { }

    public class Cat : CatLike { }

    public class Dog : Animal { }

    public class Parrot : Bird { }

    public class Macaw : Parrot { }

    public class Whale : Entity { }
}
