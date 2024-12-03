using RentalApp.ViewModels;
using System.Numerics;

namespace RentalApp.Services.QuoteServices
{
    public class QuoteTask
    {
        //Task 1. Calculate discount(%) according to Term Duration.
        public static double Discount(double TermDuration, int TermID)
        {
            double discount = 0;
            switch (TermID) 
            {
                
                case 1:  //Year
                    if (TermDuration == 2)
                    {
                        discount = 0.10;
                    }
                    else if (TermDuration == 5)
                    {
                        discount = 0.30;
                    }
                    else if (TermDuration ==3 ||  TermDuration == 4)
                    {
                        discount = 0.20;
                    }
                    else
                    {
                        discount = 0;
                    }
                    break;
                case 2:  // Month
                    if (TermDuration == 2)
                    {
                        discount = 0.10;
                    }
                    else if (TermDuration == 6)
                    {
                        discount = 0.30;
                    }
                    else if (TermDuration >= 3 && TermDuration <= 5)
                    {
                        discount = 0.20;
                    }
                    else
                    {
                        discount = 0;
                    }
                    break;
                case 3:  //Week
                    if (TermDuration == 10)
                    {
                        discount = 0.10;
                    }
                    else if (TermDuration == 24)
                    {
                        discount = 0.30;
                    }
                    else if (TermDuration >= 11 && TermDuration <= 23)
                    {
                        discount = 0.20;
                    }
                    else
                    {
                        discount = 0;
                    }
                    break;
                case 4:  //Day
                    if (TermDuration == 60)
                    {
                        discount = 0.10;
                    }
                    else if (TermDuration == 90)
                    {
                        discount = 0.30;
                    }
                    else if (TermDuration >= 61 && TermDuration <= 89)
                    {
                        discount = 0.20;
                    }
                    else
                    {
                        discount = 0;
                    }
                    break;
                default:
                       discount = 0;
                    break;
            }
            return discount;
            
        }

        //Task 2. Calculate new price according to discount(%). 
        public static double NewPrice(double price, double discount)
        {
            var newPrice = price - (price* discount);
            return newPrice;
        }

        //Task 3. Calculate Security deposit according to new price. 
        public static double SecurityDeposit(double newPrice)
        {
            double securityDeposit = newPrice + (newPrice * 0.5);
            
            return securityDeposit;
        }

        //Task 4. Calculate Late Fee according to new price.

        public static double LateFee(double newPrice)
        {
            double lateFee = newPrice * 0.5;

            return lateFee;
        }

        //Task 5. Calculate Insurance according to new price.
        public static double Insurance(double newPrice)
        {

            double insurance = newPrice * 0.25;

            return insurance;
        }

        //Task 6. Calculate Date Expiration.
        public static DateTime Expiration()
        {
            DateTime currentDateTime = DateTime.UtcNow;

            DateTime expiresBy = currentDateTime.AddMonths(2); 

            return expiresBy;
        }

        public static void RunTask(ref QuoteViewModel quoteViewModel)
        {
            var price = quoteViewModel.Price;
            var termDuration = quoteViewModel.TermDuration;
            var termId = quoteViewModel.TermID;

            //Task 1. Calculate discount(%) according to Term Duration.
            var discountTask = Task<double>.Run(() => Discount(termDuration, termId));
            var firstTask = new Task[1];
            firstTask[0] = discountTask;
            var discount = discountTask.Result;

            //Task 2. Calculate new price according to discount(%).
            var newPriceTask = Task<double>.Run(() => NewPrice(price,discount));   
            var secondTask = new Task[1];
            secondTask[0] = newPriceTask;
            

            Task<double> securityDeposit = null; //Task 3. Calculate Security deposit according to new price. 
            Task<double> lateFee = null; //Task 4. Calculate Late Fee according to new price.
            Task<double> insurance = null; //Task 5. Calculate Insurance according to new price.
            Task<DateTime> expiration = null; //Task 6. Calculate Date Expiration.

            var thirdTasks = new Task[4];

            // if indexF and indexS are 0 then these Tasks are done.
            int indexF = Task.WaitAny(firstTask);
            int indexS = Task.WaitAny(secondTask);
            if (indexF == 0 && indexS == 0)
            {
                var newPrice = newPriceTask.Result;
                thirdTasks[0] = securityDeposit = Task.Run(() => SecurityDeposit(newPrice));
                thirdTasks[1] = lateFee = Task.Run(() => LateFee(newPrice));
                thirdTasks[2] = insurance = Task.Run(() => Insurance(newPrice));
                thirdTasks[3] = expiration = Task.Run(() => Expiration());

            }

            // Validate that all tasks are done before continue
            Task.WaitAll(thirdTasks);

            quoteViewModel.DiscountPercent = discountTask.Result*100;
            quoteViewModel.DiscountPrice = newPriceTask.Result;
            quoteViewModel.SecurityDeposit = securityDeposit.Result;
            quoteViewModel.LateFee = lateFee.Result;
            quoteViewModel.Insurance = insurance.Result;
            quoteViewModel.ExpiresBy = expiration.Result;
            
        }
    }
}
