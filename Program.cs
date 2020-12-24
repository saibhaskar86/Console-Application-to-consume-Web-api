using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsumeDemoService
{
    public class Program
    {
        static HttpClient htpc = new HttpClient();


        static void Main(string[] args)
        {
        Start:

            Console.WriteLine("Enter 1 For Manager List");
            Console.WriteLine("Enter 2 For Client List");
            Console.WriteLine("Enter 3 For AddManager");
            Console.WriteLine("Enter 4 For AddClient");
            Console.WriteLine("Enter 5 For Delete Client");
            Console.WriteLine("Enter 6 For Delete Manger");
            Console.WriteLine("Enter 7 For  ManagerlistWithClients");
            Console.WriteLine("=======================");
            Console.Write("Enter Number --");
            int input = int.Parse(Console.ReadLine());




            switch (input)
            {
                case 1:

                    Program p = new Program();
                    p.ShowManagerlist();
                    break;

                case 2:
                    Program P = new Program();
                    P.ShowClients();
                    break;

                case 3:
                    Program prom = new Program();
                    prom.AddManager();
                    break;
                case 4:
                    Program procli = new Program();
                    procli.AddClient();
                    break;

                case 5:
                    Program prodelcli = new Program();
                    prodelcli.DeleteClient();
                    break;
                case 6:
                    Program promdel = new Program();
                    promdel.DeleteManager();
                    break;

                case 7:
                    Program mancli = new Program();
                    mancli.ShowManagerlistWithClients();
                    break;

                default:
                    Console.WriteLine("Your Input {0} is Invalid", input);
                    break;
            }

        Decide:
            Console.WriteLine("Do You Want To Use TectoraService Again Yes or  No?");
            string userinput = Console.ReadLine();

            switch (userinput)
            {
                case "Yes":
                    goto Start;
                case "No":
                    break;
                default:
                    Console.WriteLine("Your Input {0} is Invalid", userinput);
                    goto Decide;
            }

            Console.WriteLine("Thanks For Using This Service");
            Console.Read();

        }

        private void ShowManagerlist()
        {
            IEnumerable<Manager> Managers;
            HttpResponseMessage webResponse = htpc.GetAsync("http://localhost:54039/api/ManagerList").Result;
            Managers = webResponse.Content.ReadAsAsync<IEnumerable<Manager>>().Result;

            Console.WriteLine("List Of Manager");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (Manager manager in Managers)
            {
                Console.WriteLine("ManagerId -" + manager.MgrId + "  " + "ManagerName -" + manager.ManagerName + "  " + "Email -" + manager.Email + "  " + "Position -" + manager.Position);
            }
            Console.Read();
        }

        private void ShowClients()
        {

            IEnumerable<Client> Clients;
            HttpResponseMessage webResponse = htpc.GetAsync("http://localhost:54039/api/Client").Result;
            Clients = webResponse.Content.ReadAsAsync<IEnumerable<Client>>().Result;

            Console.WriteLine("List Of ClientName With Manager");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (Client client in Clients)
            {
                Console.WriteLine("ClientId -" + client.ClientId + "  " + "ClientName -" + client.ClientName + " " + "ClientName" + client.ClientMail + "  " + "ManagerName -" + client.ManagerName);
            }
            Console.Read();

        }

        private void AddManager()
        {
            Console.WriteLine("Enter ManagerName :");
            string ManagerName = Console.ReadLine();
            Console.WriteLine("Email :");
            string Email = Console.ReadLine();
            Console.WriteLine("Position :");
            string Position = Console.ReadLine();

            var Manager = new Manager()
            {
                ManagerName = ManagerName,
                Email = Email,
                Position = Position

            };

            var postTask = htpc.PostAsJsonAsync("http://localhost:54039/api/Manager", Manager);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsAsync<Manager>();
                readTask.Wait();

                var insertedManager = readTask.Result;

                Console.WriteLine("Manager {0} inserted  Successfully with id: {1}", insertedManager.ManagerName, insertedManager.MgrId);
            }
            else
            {
                Console.WriteLine(result.StatusCode);
            }
            Console.ReadLine();
        }

        private void AddClient()
        {
            Console.WriteLine("Enter Name :");
            string ClientName = Console.ReadLine();
            Console.WriteLine("Enter Mail :");
            string Mail = Console.ReadLine();
            Console.WriteLine("Enter ManagerName :");
            string ManagerName = Console.ReadLine();


            var Client = new Client()
            {
                ClientName = ClientName,
                ClientMail = Mail,
                ManagerName = ManagerName
            };

            var postTask = htpc.PostAsJsonAsync("http://localhost:54039/api/Client", Client);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadAsAsync<Client>();
                readTask.Wait();

                var insertedManager = readTask.Result;

                Console.WriteLine("Client {0} inserted  Successfully with id: {1}", insertedManager.ClientName, insertedManager.ClientId);
            }
            else
            {
                Console.WriteLine(result.StatusCode);
            }
            Console.ReadLine();
        }


        public void DeleteClient()
        {
            Console.Write("Enter id to delete---");
            int id = Convert.ToInt32(Console.ReadLine());
            HttpResponseMessage httpResponse = htpc.DeleteAsync("http://localhost:54039/api/Client" + id).Result;
            if (httpResponse.IsSuccessStatusCode)
            {



                Console.WriteLine("Client With Id {0} Deleted ", id);
            }
            else
            {
                Console.WriteLine(httpResponse.StatusCode);
            }
            Console.ReadLine();
        }

        public void DeleteManager()
        {
            Console.Write("Enter id to delete---");
            int id = Convert.ToInt32(Console.ReadLine());
            HttpResponseMessage httpResponse = htpc.DeleteAsync("http://localhost:54039/api/Manager/" + id).Result;
            if (httpResponse.IsSuccessStatusCode)
            {



                Console.WriteLine("Manager With Id {0} Deleted ", id);
            }
            else
            {
                Console.WriteLine(httpResponse.StatusCode);
            }
            Console.ReadLine();
        }

        private void ShowManagerlistWithClients()
        {
            IEnumerable<ManagerwithClient> ManagerswithClient;
            HttpResponseMessage webResponse = htpc.GetAsync("http://localhost:54039/api/Manager").Result;
            ManagerswithClient = webResponse.Content.ReadAsAsync<IEnumerable<ManagerwithClient>>().Result;

            Console.WriteLine("List Of ManagerWithClients");
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (ManagerwithClient manager in ManagerswithClient)
            {
                Console.WriteLine("ManagerName -" + manager.ManagerName + "  " + "Email -" + manager.Email + "  " + "Position -" + manager.Position + " " + "ClientName-" + manager.ClientName);
            }
            Console.Read();
        }
    }
}
     

    
      
      
    

