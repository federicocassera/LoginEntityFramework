using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string options = "";
            var ctx = new ordersEntities1();

            using (ctx)
            {
                while (options != "exit")
                {
                    checkEmpty(ctx);
                    Console.WriteLine("Benvenuto eseguire il login");
                    Login(ctx);
                    Console.WriteLine("Benvenuto nel menu");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("selezionare una procedura");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("scrivere utente per creare un utente");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("scrivere list per visualizzare la lista degli ordini");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("scrivere detail per visualizzare il dettaglio dell'ordine");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("scrivere neworder per creare un nuovo ordine");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("scrivere exit per uscire");
                    Console.WriteLine("-----------------------------------------");
                    
                    options = Console.ReadLine();
                    switch (options)
                    {
                        case "utente":
                            createUser(ctx);
                            break;
                        case "list":
                            listOrder(ctx);
                            break;
                        case "detail":
                            detailOrder(ctx);
                            break;
                        case "neworder":
                            createOrder(ctx);
                            break;
                        case "exit":
                            return;
                        default:
                            Console.WriteLine("sscrivere una parola valida");
                            Console.WriteLine("-----------------------------------------");
                            break;
                    }
                }
            }

            Console.ReadLine();
        }

        public static void checkEmpty(ordersEntities1 ctx)
        {
            int i = 0;
            foreach (var u in ctx.users)
            {
                i += 1;
            }
            if (i == 0)
            {
                var newUser = new users() { login = "admin", password = "admin" };
                ctx.users.Add(newUser);
                Console.WriteLine("Nuovo utente admin registrato");
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                return;
            }
            ctx.SaveChanges();
        }

        public static void Login(ordersEntities1 ctx)
        {
            Console.WriteLine("Inserire il nome utente");
            string username = Console.ReadLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire la password");
            string password = Console.ReadLine();
            Console.WriteLine("-----------------------------------------");
            try
            {
                foreach (var cust in ctx.users)
                {
                    if (cust.login == username && cust.password == password)
                    {
                        Console.WriteLine($"Benvenuto: {username}");
                        Console.WriteLine("--------------------------------------");
                    }
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
                Console.WriteLine("username o password errate");
            }
            
            ctx.SaveChanges();
        }

        public static void readUsers(ordersEntities1 ctx)
        {
            foreach (var user in ctx.users)
            {
                Console.WriteLine($"utente: {user.login} {user.password}");
                Console.WriteLine("--------------------------------------");
            }
            ctx.SaveChanges();
        }

        public static void createUser(ordersEntities1 ctx)
        {
            Console.WriteLine("Inserire il nome utente");
            string username = Console.ReadLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire la password");
            string password = Console.ReadLine();
            Console.WriteLine("-----------------------------------------");

            var newUser = new users() { login = username, password = password }; 
            ctx.users.Add(newUser);
            new User(username, password);
            Console.WriteLine("Nuovo utente registrato");
            Console.WriteLine("--------------------------------------");
            ctx.SaveChanges();
        }

        public static void listOrder(ordersEntities1 ctx)
        {
            Console.WriteLine($"orders : ");
            Console.WriteLine($"orderid\tcustomer\tcountry");
            foreach (var o in ctx.orders)
            {
                Console.WriteLine($"{o.orderid}:\t{o.customers.customer}\t{o.customers.country}");
                Console.WriteLine("--------------------------------------------------");
            }
            ctx.SaveChanges();
        }

        public static void detailOrder(ordersEntities1 ctx)
        {
            int ord = 0;
            Console.WriteLine("inseire id dell'ordine");
            try
            {
                ord = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("inserire un valore numerico");
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"orders : ");
            Console.WriteLine($"orderid\tcustomer\tcountry");
            
            foreach (orders o in ctx.orders)
            {                
                if (o.orderid == ord)
                {
                    Console.WriteLine($"{o.orderid}:\t{o.customers.customer}\t{o.customers.country}");
                    Console.WriteLine($"---------------------------------------------------");
                    Console.WriteLine($"orderid\titem\t\tqty\tprice");
                    foreach (orderitems oi in o.orderitems)     //ciclo tabella orderitems
                    {
                        Console.WriteLine($"{oi.orderid}\t{oi.item}\t{oi.qty}\tx{oi.price}");
                        Console.WriteLine($"------------------------------------------------");

                    }
                    Console.WriteLine($"----------------------------");
                }
                else
                {
                    Console.WriteLine("Id dell'ordine non valido o ordine non esistente ... ! ");
                    Console.WriteLine($"---------------------------------");
                }
            }
            ctx.SaveChanges();
        }

        public static void createOrder(ordersEntities1 ctx)
        {
            string prodotto = "";
            int quantità = 0;
            int prezzo = 0;
            string colore = "";
            DateTime date = DateTime.Now;
            Console.WriteLine("Aggiunta nuovo prodotto ... ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire il prodotto");
            try
            {
                prodotto = Console.ReadLine();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                Console.WriteLine("inserire il nome del prodotto!");
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire la quantità");
            try
            {
                quantità = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("inserire la quantità del prodotto!");
            }
            
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire il prezzo");
            try
            {
                prezzo = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("inserire il prezzo del prodotto!");
            }
            
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Inserire il colore del prodotto");
            try
            {
                colore = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("inserire il colore del prodotto!");
            }            
            Console.WriteLine("-----------------------------------------");

            int i = 0;
            foreach (orders o in ctx.orders) 
            {
                i += 1;
            }

            var newCustomer = new customers() { customer = User.username, country = "Italy" };
            ctx.customers.Add(newCustomer);
            Console.WriteLine("Nuovo customer registrato");
            Console.WriteLine("--------------------------------------");
            var newOrder = new orders() { orderid = i+1, customer = User.username, orderdate = date };
            ctx.orders.Add(newOrder);
            Console.WriteLine("Nuovo ordine registrato");
            Console.WriteLine("--------------------------------------");
            var newOrderitems = new orderitems() { orderid = i + 1, item = $"{prodotto}", 
                qty = quantità , price = prezzo};
            ctx.orderitems.Add(newOrderitems);
            Console.WriteLine("Nuovo ordineitem registrato");
            Console.WriteLine("--------------------------------------");
            var newItem = new items() {item = $"{prodotto}", color = $"{colore}" };
            ctx.items.Add(newItem);
            Console.WriteLine("Nuovo item registrato");
            Console.WriteLine("--------------------------------------");
            ctx.SaveChanges();
        }

        public class User
        {
            public static string username { get; set; }
            public static string password { get; set; }

            public User(string Username, string Password)
            {
                username = Username;
                password = Password;    
            }
        }
    }
}
