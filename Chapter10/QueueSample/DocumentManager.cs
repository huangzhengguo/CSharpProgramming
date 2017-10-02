using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.QueueSample
{
    public class DocumentManager
    {
        private readonly Queue<Document> documentQueue = new Queue<Document>();

        public void AddDocument(Document document)
        {
            lock (this)
            {
                documentQueue.Enqueue(document);
            }
        }

        public Document GetDocument()
        {
            Document document = null;
            lock (this)
            {
                document = documentQueue.Dequeue();
            }

            return document;
        }

        public bool IsDocumentAvailable
        {
            get
            {
                return documentQueue.Count > 0;
            }
        }
    }
}
