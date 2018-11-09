using IDataAccessLayer;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public class Repository<T, TKey> : IRepository<T, TKey>
       where T : IEntity<TKey>
    {

        protected internal MongoCollection<T> collection;

        public Repository()
            : this(Util<TKey>.GetDefaultConnectionString())
        {
        }

         public Repository(string connectionString)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }


        public Repository(string connectionString, string collectionName)
        {
            this.collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public Repository(MongoUrl url)
        {
            this.collection = Util<TKey>.GetCollectionFromUrl<T>(url);
        }


        public Repository(MongoUrl url, string collectionName)
        {
            this.collection = Util<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }


        public MongoCollection<T> Collection
        {
            get { return this.collection; }
        }

        public string CollectionName
        {
            get { return this.collection.Name; }
        }

        public virtual T GetById(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                return this.GetById(new ObjectId(id as string));
            }

            return this.collection.FindOneByIdAs<T>(BsonValue.Create(id));
        }

        public virtual T GetById(ObjectId id)
        {
            return this.collection.FindOneByIdAs<T>(id);
        }

        public virtual T Add(T entity)
        {
            this.collection.Insert<T>(entity);

            return entity;
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
        }

        public virtual T Update(T entity)
        {
            this.collection.Save<T>(entity);

            return entity;
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.collection.Save<T>(entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                this.collection.Remove(Query.EQ("_id", new ObjectId(id as string)));
            }
            else
            {
                this.collection.Remove(Query.EQ("_id", BsonValue.Create(id)));
            }
        }

        public virtual void Delete(ObjectId id)
        {
            this.collection.Remove(Query.EQ("_id", id));
        }


        public virtual void Delete(T entity)
        {
            this.Delete(entity.Id);
        }


        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(predicate))
            {
                this.Delete(entity.Id);
            }
        }

        public virtual void DeleteAll()
        {
            this.collection.RemoveAll();
        }


        public virtual long Count()
        {
            return this.collection.Count();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.collection.AsQueryable<T>().Any(predicate);
        }


        #region IQueryable<T>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }


        public virtual Type ElementType
        {
            get { return this.collection.AsQueryable<T>().ElementType; }
        }

        public virtual Expression Expression
        {
            get { return this.collection.AsQueryable<T>().Expression; }
        }

        public virtual IQueryProvider Provider
        {
            get { return this.collection.AsQueryable<T>().Provider; }
        }
        #endregion
    }

    public class Repository<T> : Repository<T, string>, IRepository<T>
        where T : IEntity<string>
    {
              public Repository()
            : base() { }


        public Repository(MongoUrl url)
            : base(url) { }

        public Repository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }

        public Repository(string connectionString)
            : base(connectionString) { }

        public Repository(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}
