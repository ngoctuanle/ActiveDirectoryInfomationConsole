using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAD2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (stringDomainName != null)
                {
                    string[] info = TextProcessing.login();
                    while (true)
                    {
                        if (validateUser(info[0], info[1]))
                        {
                            goto PROCESS;
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Invalid username or password. Try Again. (y/n): ");
                            CHOOSE:
                            string key = Console.ReadLine().ToUpper();
                            if (key.Equals("Y") || key.Equals("YES"))
                            {
                                info = TextProcessing.login();
                            }
                            else if (key.Equals("N") || key.Equals("NO"))
                            {
                                goto END;
                            }
                            else
                            {
                                goto CHOOSE;
                            }
                        }
                    }
                PROCESS:
                    Console.Clear();
                    Console.WriteLine("Welcome!");
                    while (true)
                    {
                        Console.Write("Please select feature:\n[1]Show OU\n[2]Show User\n[3]Show Computer\n[4]Exit\n:");
                        string choose = Console.ReadLine();
                        switch (choose)
                        {
                            case "1":
                                OU.Clear();
                                OU = getOU();
                                foreach(string nameOU in OU)
                                {
                                    Console.WriteLine(nameOU);
                                }
                                break;
                            case "2":
                                Users.Clear();
                                Users = getUsers();
                                OU.Clear();
                                OU = getOU();
                                foreach(User user in Users)
                                {
                                    Console.WriteLine(user.SAMAccountName+" - "+user.commonName+" - "+user.ou);
                                }
                                break;
                            case "3":
                                break;
                            case "4":
                                Console.WriteLine("====================");
                                goto END;
                            default:
                                break;
                        }
                        Console.WriteLine("====================");
                    }
                END:
                    Console.WriteLine("Goodbye!"); 
                }
                else
                {
                    Console.WriteLine("Your computer is not a member of domain");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static DirectoryEntry dEntry = null;
        private static string stringDomainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        private static List<string> OU = new List<string>();
        private static List<User> Users = new List<User>();

        private static bool validateUser(string username, string password)
        {
            bool result = true;
            try
            {
                dEntry = new DirectoryEntry("LDAP://" + stringDomainName, username, password);
                object nativeObject = dEntry.NativeObject;
            } catch (Exception e)
            {
                dEntry.Dispose();
                result = false;
            }
            return result;
        }

        private static void showUsers()
        {
            DirectorySearcher dSearcher = new DirectorySearcher(dEntry);

            dSearcher.Filter = "(&(objectClass=user)(SAMAccountName=tuanle2))";

            foreach (SearchResult sResult in dSearcher.FindAll())
            {
                //Console.WriteLine(TextProcessing.getProperty(sResult, "cn") + " - " + TextProcessing.getProperty(sResult, "SAMAccountName"));
                var name = sResult.Properties["distinguishedname"][0];
                Console.WriteLine(TextProcessing.getProperty(sResult, "distinguishedname"));
                /*foreach (string key in sResult.Properties.PropertyNames)
                {
                    foreach (Object myColl in sResult.Properties[key])
                    {
                        Console.WriteLine(key + ": " + myColl);
                    }
                }*/
            }
            dSearcher.Dispose();
        }

        private static List<User> getUsers()
        {
            List<User> list = new List<User>();
            DirectorySearcher dSearcher = new DirectorySearcher(dEntry);
            dSearcher.Filter = "(&(objectClass=user))";
            foreach(SearchResult result in dSearcher.FindAll())
            {
                list.Add(new User {
                    SAMAccountName = TextProcessing.getProperty(result, "SAMAccountName"),
                    commonName = TextProcessing.getProperty(result, "cn"),
                    ou = TextProcessing.getOU(TextProcessing.getProperty(result, "distinguishedname"))
                    //distinguishedname = TextProcessing.getProperty(result, "distinguishedname")
                });
            }
            return list;
        }

        private static List<string> getOU()
        {
            List<string> list = new List<string>();
            DirectorySearcher dSearcher = new DirectorySearcher(dEntry);
            dSearcher.Filter = "(&(objectClass=organizationalUnit))";
            foreach(SearchResult result in dSearcher.FindAll())
            {
                list.Add(TextProcessing.getProperty(result, "ou"));
            }
            dSearcher.Dispose();
            return list;
        }
    }
}
