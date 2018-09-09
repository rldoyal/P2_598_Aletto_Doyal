using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


// Project 2 ASU CSE 598  Fall 2018
// Team Aletto Doyal 598
// All code in this directory was developed by Roy Doyal

namespace P2_598_Aletto_Doyal
{

    // Encoder -- turns OrderObject into a string
    public class Bookstore
    {

        public SupplyDemand myDemand = new SupplyDemand();

        public Bookstore()
        {
            // setup and start the demand thread
            Thread demandThread = new Thread(new ThreadStart(myDemand.DemandFunc));
            demandThread.Name = "1";
            demandThread.Start();
            

        }

        // functional thread for the bookstore.  It will generate an order every 2 seconds.
        public void BookStoreFunc()
        {
            while (Program.BookStoreThreadRunning)
            {
                OrderObject newOrder = CreateOrder("2");
                Console.WriteLine("\t\t\t New Order Created... {0},{1},{2},{3},{4},{5}",
                   newOrder.getSenderId(), newOrder.getCardNo(), newOrder.getReceiverId(), newOrder.getAmount(),
                   newOrder.getUnitPrice(), newOrder.getTimeStamp());
                Thread.Sleep(2000); // sleep for 2 seconds
            }

        }

 
        OrderObject CreateOrder( string publisherName )
        {
            string SenderID = Thread.CurrentThread.Name;
            Int32 CardNo = 5000;
            string ReceiverID = publisherName;
            Int32 amountBooks = myDemand.BooksNeeded();
            Int32 unitPrice = Program.GV.getCurrentPrice();          
            DateTime timeStamp = DateTime.Now;
            OrderObject myObj = new OrderObject( SenderID, CardNo, ReceiverID, amountBooks, unitPrice, timeStamp );
            return myObj;
        }

        // Encoder -- turns OrderObject into a CSV string
        string Encorder(OrderObject order)
        {
            string orderStr = null;
            // build CSV String
            orderStr = order.getSenderId();
            orderStr += "," + order.getCardNo().ToString();
            orderStr += "," + order.getReceiverId();
            orderStr += "," + order.getAmount().ToString();
            orderStr += "," + order.getUnitPrice().ToString();
            orderStr += "," + order.getTimeStamp();

            return orderStr; // return the encoded string just created.
        }


    }

    public class SupplyDemand
    {
        const Int32 MaxBookQuanity = 500;  // max number of books allowed in the store
        static Int32 StoreInventory = 250;  // initial book inventory set at 500.  Max Books = 500.
        bool NeedDemandThread = true; // variable to start/stop demand thread
        public SupplyDemand()
        {

        }
        // function to act as demand... 
        // it will be a thread that consumes a random number of books every 1 second
        public void DemandFunc()
        {
            Random rnd = new Random();
            Int32 RemoveBooks;
            while (NeedDemandThread)
            {
                Thread.Sleep(1000); // sleep for 1 second (1000 ms)
                RemoveBooks = rnd.Next(5, 250);
                StoreInventory -= RemoveBooks;  // allow no more than 250 max books per demand
                Console.WriteLine(" Removed {0} books...", RemoveBooks);
                if (StoreInventory < 0)
                    StoreInventory = 25; // don't let the book store get less than 25 books
            }
        }
        public void TurnDemandOff()
        {
            NeedDemandThread = false;
        }

        public Int32 getStoreInventory()
        {
            return StoreInventory;
        }

        public void addStoreInventory(Int32 newBooks)
        {
            Console.WriteLine("Adding {0} new books.", newBooks);
            StoreInventory += newBooks;
        }

        public Int32 BooksNeeded()
        {
            // TODO :  Update this method to include pricing but for now, just do basic
            Int32 booksWanted = 0;

            // for now, just return the bookstore back to 500 books
            booksWanted = Math.Abs(500 - StoreInventory);
            return booksWanted;
        }

        

    }

}
