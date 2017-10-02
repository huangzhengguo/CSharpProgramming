using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter10.QueueSample
{
    public class ProcessDocuments
    {
        private DocumentManager documentManager;
        public static void Start(DocumentManager dm)
        {
            Task.Factory.StartNew(new ProcessDocuments(dm).Run);
        }

        public ProcessDocuments(DocumentManager dm)
        {
            if (dm == null)
            {
                throw new ArgumentException("dm");
            }

            this.documentManager = dm;
        }

        protected void Run()
        {
            while (true)
            {
                if (documentManager.IsDocumentAvailable)
                {
                    Document doc = documentManager.GetDocument();
                    Console.WriteLine("Processing document: {0}", doc.Title);
                }

                Thread.Sleep(new Random().Next(20));
            }
        }
    }
}
