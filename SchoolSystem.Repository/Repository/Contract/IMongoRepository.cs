using StudentSystem.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolSystem.Repository.Repository.Contract
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        List<TDocument> GetAllDocuments();
        void InsertDocument(TDocument document);
        TDocument UpdateDocument(TDocument document);
        TDocument DeleteDocument(TDocument document);
        void DeleteAllDocuments();
    }
}
