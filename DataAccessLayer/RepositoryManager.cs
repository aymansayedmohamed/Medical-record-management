using IDataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Models;

namespace DataAccessLayer
{
    public class RepositoryManager<T, TKey> : IRepositoryManager<T, TKey>
        where T : IEntity<TKey>
    {

        private MongoCollection<T> collection;

              public RepositoryManager()
            : this(Util<TKey>.GetDefaultConnectionString())
        {
        }

        public RepositoryManager(string connectionString)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }

        public RepositoryManager(string connectionString, string collectionName)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

         public virtual bool Exists
        {
            get { return this.collection.Exists(); }
        }

          public virtual string Name
        {
            get { return this.collection.Name; }
        }

         public virtual void Drop()
        {
            this.collection.Drop();
        }

          public virtual bool IsCapped()
        {
            return this.collection.IsCapped();
        }

          public virtual void DropIndex(string keyname)
        {
            this.DropIndexes(new string[] { keyname });
        }

        public virtual void DropIndexes(IEnumerable<string> keynames)
        {
            this.collection.DropIndex(keynames.ToArray());
        }

        public virtual void DropAllIndexes()
        {
            this.collection.DropAllIndexes();
        }

        public virtual void EnsureIndex(string keyname)
        {
            this.EnsureIndexes(new string[] { keyname });
        }

        public virtual void EnsureIndex(string keyname, bool descending, bool unique, bool sparse)
        {
            this.EnsureIndexes(new string[] { keyname }, descending, unique, sparse);
        }

        public virtual void EnsureIndexes(IEnumerable<string> keynames)
        {
            this.EnsureIndexes(keynames, false, false, false);
        }

        public virtual void EnsureIndexes(IEnumerable<string> keynames, bool descending, bool unique, bool sparse)
        {
            var ixk = new IndexKeysBuilder();
            if (descending)
            {
                ixk.Descending(keynames.ToArray());
            }
            else
            {
                ixk.Ascending(keynames.ToArray());
            }

            this.EnsureIndexes(
                ixk,
                new IndexOptionsBuilder().SetUnique(unique).SetSparse(sparse));
        }

        public virtual void EnsureIndexes(IMongoIndexKeys keys, IMongoIndexOptions options)
        {
            this.collection.CreateIndex(keys, options);
        }

        public virtual bool IndexExists(string keyname)
        {
            return this.IndexesExists(new string[] { keyname });
        }

        public virtual bool IndexesExists(IEnumerable<string> keynames)
        {
            return this.collection.IndexExists(keynames.ToArray());
        }

         public virtual void ReIndex()
        {
            this.collection.ReIndex();
        }

         public virtual ValidateCollectionResult Validate()
        {
            return this.collection.Validate();
        }

         public virtual CollectionStatsResult GetStats()
        {
            return this.collection.GetStats();
        }

        public virtual GetIndexesResult GetIndexes()
        {
            return this.collection.GetIndexes();
        }
    }

    public class RepositoryManager<T> : RepositoryManager<T, string>, IRepositoryManager<T>
        where T : IEntity<string>
    {
        public RepositoryManager()
            : base() { }

        public RepositoryManager(string connectionString)
            : base(connectionString) { }

        public RepositoryManager(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}
