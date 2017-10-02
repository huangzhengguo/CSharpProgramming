using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.LinkedListSample
{
    public class PriorityDocumentManager
    {
        private readonly LinkedList<Documents> documentList;

        // priorities 0.0
        private readonly List<LinkedListNode<Documents>> priorityNodes;

        public PriorityDocumentManager()
        {
            documentList = new LinkedList<Documents>();

            priorityNodes = new List<LinkedListNode<Documents>>(10);
            for (int i=0; i < 10; i++)
            {
                priorityNodes.Add(new LinkedListNode<Documents>(null));
            }
        }

        public void DisplayAllNodes()
        {
            foreach(Documents doc in documentList)
            {
                Console.WriteLine("priority {0}, title {1}", doc.Priority, doc.Title);
            }
        }

        public Documents GetDocuments()
        {
            Documents doc = documentList.First.Value;
            documentList.RemoveFirst();

            return doc;
        }

        public void AddDocument(Documents d)
        {
            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Documents doc, int priority)
        {
            if (priority < 0 || priority > 9)
            {
                throw new ArgumentException("Invalid Argument!");
            }

            if (priorityNodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    // chekc for the next lower priority
                    AddDocumentToPriorityNode(doc, priority);
                }
                else
                {
                    documentList.AddLast(doc);
                    priorityNodes[doc.Priority] = documentList.Last;
                }
            }
            else
            {
                LinkedListNode<Documents> linkedListNode = priorityNodes[priority];
                if (priority == doc.Priority)
                {
                    documentList.AddAfter(linkedListNode, doc);
                    priorityNodes[priority] = linkedListNode.Next;
                }
                else
                {
                    LinkedListNode<Documents> firstPrioNode = linkedListNode;
                    while(firstPrioNode.Previous != null && firstPrioNode.Previous.Value.Priority == linkedListNode.Value.Priority)
                    {
                        firstPrioNode = firstPrioNode.Previous;
                        linkedListNode = firstPrioNode;
                    }

                    documentList.AddBefore(firstPrioNode, doc);
                    priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }
        }
    }
}
