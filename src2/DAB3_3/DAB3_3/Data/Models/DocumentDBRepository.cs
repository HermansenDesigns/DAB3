﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DAB3_3.Data.Models
{
    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }
        public static async Task<IEnumerable<T>> Read(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }
        public static async Task<IHttpActionResult> Update(Person person)
        {
            try
            {
                await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, person.Id), person);
                return new OkResult(new HttpRequestMessage());
            }
            catch (Exception)
            {
                return new NotFoundResult(new HttpRequestMessage());
            }
        }
        public static async Task<IHttpActionResult> Create(Person person)
        {
            try
            {
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, person.Id));
                return new BadRequestResult(new HttpRequestMessage());
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), person);
                    return new OkResult(new HttpRequestMessage());
                }

                throw;
            }

        }
        public static async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                return new OkResult(new HttpRequestMessage());
            }
            catch (Exception)
            {
                return new NotFoundResult(new HttpRequestMessage());
            }
        }
    }
}