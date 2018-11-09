
using System;
using IDataAccessLayer;
using MongoDB.Bson.Serialization.Attributes;
using Models;
using DataAccessLayer;

namespace DataAccessLayerTests
{
    public class CustomIDEntity : IEntity<string>
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    [CollectionName("MyTestCollection")]
    public class CustomIDEntityCustomCollection : CustomIDEntity { }
}
