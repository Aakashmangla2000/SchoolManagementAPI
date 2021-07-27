using MongoDB.Driver;
using SchoolSystem.Abstractions;
using SchoolSystem.Repository.Repository.Contract;
using StudentSystem.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolSystem.Repository.Repository.Concrete
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        /// <summary>
        /// Retrieves the collection from database
        /// </summary>
        /// <param name="settings"></param>
        public MongoRepository(IStudentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute),true).FirstOrDefault())?.CollectionName;
        }


        /// <summary>
        /// Gives an Enumerable of documents based on the filter expression
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        /// <summary>
        /// Gets all documents from the collection
        /// </summary>
        /// <returns>List of documents</returns>
        public List<TDocument> GetAllDocuments()
        {
            return _collection.Find(document => true).ToList();
        }

        /// <summary>
        /// Inserts document into the collection
        /// </summary>
        /// <param name="document"></param>
        public void InsertDocument(TDocument document)
        {
            _collection.InsertOne(document);
        }

        /// <summary>
        /// Updates the document in the collection
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Updated document</returns>
        public TDocument UpdateDocument(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(student => student.Id, document.Id);
            return _collection.FindOneAndReplace(filter, document);
        }

        /// <summary>
        /// Deletes document from collection
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public TDocument DeleteDocument(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(student => student.Id, document.Id);
            return _collection.FindOneAndDelete(filter);
        }

        /// <summary>
        /// Deletes all the documents in the collection
        /// </summary>
        public void DeleteAllDocuments()
        {
            var filter = Builders<TDocument>.Filter.Empty;
            _collection.DeleteMany(filter);
        }
    }
}
