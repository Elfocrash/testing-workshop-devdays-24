using System;

namespace LegacyApp.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }

        public static void ProveAddUser(string[] args)
        {
            /*
			 *	DO NOT CHANGE THIS FILE AT ALL
        	*/

            var userService = new UserService();
            var addResult = userService.AddUser("Nick", "Chapsas", "nick.chapsas@gmail.com", new DateTime(1993, 1, 1), 4);
            Console.WriteLine("Adding Nick Chapsas was " + (addResult ? "successful" : "unsuccessful"));
        }
    }
}
