using System;
using System.Threading;

namespace P2_598_Aletto_Doyal
{

    class GlobalVariables
    {
        const Int32 MaxBookQuanity = 500;  // max number of books allowed in the store
        //static Int32 StoreInventory = 250;  // initial book inventory set at 500.  Max Books = 500.
        //bool NeedDemandThread = true; // variable to start/stop demand thread
        static Int32  CurrentPrice = 50;

        public Int32 getCurrentPrice()
        {
            return CurrentPrice;
        }
    }
    class Program
    {
        public static GlobalVariables GV = new GlobalVariables();
        public static bool BookStoreThreadRunning = true;
        public static void TestBookStore()
        {
            Random rnd = new Random();

            Bookstore bs = new Bookstore();
            // Start the BookStore Thread

            Thread BookStoreThread = new Thread(new ThreadStart(bs.BookStoreFunc));
            BookStoreThread.Name = "1";
            BookStoreThread.Start();

            // test the bookstore demand thread.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(" i={0} : inventory = {1}", i, bs.myDemand.getStoreInventory());
                bs.myDemand.addStoreInventory(rnd.Next(100, 500));

                System.Threading.Thread.Sleep(1500);
            }
            BookStoreThreadRunning = false;
            bs.myDemand.TurnDemandOff();
        }


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            TestBookStore();

        }
        
    }
}  
