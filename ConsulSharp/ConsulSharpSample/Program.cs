﻿using System;
using System.Collections;
using System.Text;
using ConsulSharp;
using ConsulSharp.ACL;
using ConsulSharp.Agent;
using ConsulSharp.Agent.Check;
using ConsulSharp.Agent.Service;
using ConsulSharp.Event;
namespace ConsulSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamLogs();
            while (true)
            {
                Console.WriteLine("1、Agent  2、Catalog  3、Health 4、ACL  5、Event  按e退出");
                switch (Console.ReadLine())
                {
                    case "1":
                        AgentManage();
                        break;
                    case "2":
                        CatalogManage();
                        break;
                    case "3":
                        HealthManage();
                        break;
                    case "4":
                        ACLManage();
                        break;
                    case "5":
                        EventManage();
                        break;
                    case "e":
                        return;
                }
            }
        }

        private static void EventManage()
        {
            throw new NotImplementedException();
        }

        private static void ACLManage()
        {
            throw new NotImplementedException();
        }

        private static void HealthManage()
        {
            throw new NotImplementedException();
        }

        private static void CatalogManage()
        {
            throw new NotImplementedException();
        }

        #region Agent
        private static void AgentManage()
        {
            Console.WriteLine("1、Check Manage  2、Service Manage  3、List Members  4、Read Configuration  5、Reload Agent 6、Enable Maintenance Mode  7、View Metrics  8、Stream Logs     按e退出");
            switch (Console.ReadLine())
            {
                case "1":
                    CheckManage();
                    break;
                case "2":
                    ServiceManage();
                    break;
                case "3":
                    ListMembers();
                    break;
                case "4":
                    ReadConfiguration();
                    break;
                case "5":
                    ReloadAgent();
                    break;
                case "6":
                    AgentEnableMaintenanceMode();
                    break;
                case "7":
                    ViewMetrics();
                    break;
                case "8":
                    StreamLogs();
                    break;
                case "e":
                    return;
            }
        }
        /// <summary>
        /// List Members
        /// </summary>
        private static void ListMembers()
        {
            var agentGovern = new AgentGovern();
            var list = agentGovern.ListMembers(new ListMembersParmeter { Wan = true }).GetAwaiter().GetResult();
            foreach (var item in list)
            {
                Console.WriteLine(EntityToString(item));
                Console.WriteLine("-----------------------------------------------");
            }
        }
        /// <summary>
        /// Read Configuration
        /// </summary>
        private static void ReadConfiguration()
        {
            var agentGovern = new AgentGovern();
            var list = agentGovern.ReadConfiguration().GetAwaiter().GetResult();
            Console.WriteLine(EntityToString(list));
        }
        /// <summary>
        /// Reload Agent
        /// </summary>
        private static void ReloadAgent()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.ReloadAgent().GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}"); ;
        }

        /// <summary>
        /// Enable Maintenance Mode
        /// </summary>
        private static void AgentEnableMaintenanceMode()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.AgentEnableMaintenanceMode(new EnableMaintenanceModeParmeter
            {
                Enable = true,
                Reason = "abc"
            }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}"); ;
        }

        /// <summary>
        /// View Metrics
        /// </summary>
        private static void ViewMetrics()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.ViewMetrics().GetAwaiter().GetResult();
            Console.WriteLine(result);
        }
        /// <summary>
        /// Stream Logs
        /// </summary>
        private static void StreamLogs()
        {
            var agentGovern = new AgentGovern();
            agentGovern.WriteLog += delegate (string log)
            {
                Console.WriteLine(log);
            };
            agentGovern.StreamLogs().GetAwaiter().GetResult();
        }

        #endregion

        #region Service
        private static void ServiceManage()
        {
            while (true)
            {
                Console.WriteLine("1、List Services   2、Register Service   3、Deregister Service  4、Enable Maintenance Mode   5、  6、  7、  按e退出");
                switch (Console.ReadLine())
                {
                    case "1":
                        ListServices();
                        break;
                    case "2":
                        RegisterService();
                        break;
                    case "3":
                        DeregisterService();
                        break;
                    case "4":
                        EnableMaintenanceMode();
                        break;
                    case "5":

                        break;
                    case "6":

                        break;
                    case "7":

                        break;
                    case "e":
                        return;
                }
            }
        }

        /// <summary>
        /// Register Service
        /// </summary>
        private static void RegisterService()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.RegisterServices(new RegisterServiceParmeter
            {
                ID = "test0001",
                Name = "test0001",
                Address = "http://www.baiduc.om",
                Port = 80,
                Tags = new string[] { "baidu", "百度" }
            }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// Deregister Service
        /// </summary>
        private static void DeregisterService()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.DeregisterServices("test0001").GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// List Services
        /// </summary>
        static void ListServices()
        {
            var agentGovern = new AgentGovern();
            var list = agentGovern.ListServices().GetAwaiter().GetResult();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Key}");
                Console.WriteLine(EntityToString(item.Value));
                Console.WriteLine("-----------------------------------------------");
            }
        }
        static void EnableMaintenanceMode()
        {

            var agentGovern = new AgentGovern();
            var result = agentGovern.EnableMaintenanceMode(new EnableMaintenanceModeParmeter { ServiceID = "lisapi001", Enable = true, Reason = "abc" }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }

        #endregion

        #region Check
        static void CheckManage()
        {
            while (true)
            {
                Console.WriteLine("1、List Checks   2、Register Check   3、Deregister Check  4、TTL Check Pass   5、TTL Check Warn   6、TTL Check Fail   7、TTL Check Update  按e退出");
                switch (Console.ReadLine())
                {
                    case "1":
                        ListChecks();
                        break;
                    case "2":
                        RegisterCheck();
                        break;
                    case "3":
                        DeregisterCheck();
                        break;
                    case "4":
                        TTLCheckPass();
                        break;
                    case "5":
                        TTLCheckWarn();
                        break;
                    case "6":
                        TTLCheckFail();
                        break;
                    case "7":
                        TTLCheckUpdate();
                        break;
                    case "e":
                        return;
                }
            }
        }

        /// <summary>
        /// List Checks
        /// </summary>
        static void ListChecks()
        {
            var agentGovern = new AgentGovern();
            var list = agentGovern.ListChecks().GetAwaiter().GetResult();
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Key}");
                Console.WriteLine(EntityToString(item.Value));
                Console.WriteLine("-----------------------------------------------");
            }
        }
        /// <summary>
        /// Register Check
        /// </summary>
        static void RegisterCheck()
        {
            var agentGovern = new AgentGovern();
            var registerCheck = new RegisterCheckParmeter()
            {
                ID = "test001",
                Name = "test" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Interval = "10s",
                HTTP = "https://example.com",
                Method = "POST",
            };

            var result = agentGovern.RegisterCheck(registerCheck).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// Deregister Check
        /// </summary>
        static void DeregisterCheck()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.DeregisterCheck("test001").GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }


        /// <summary>
        /// TTL Check Pass
        /// </summary>
        static void TTLCheckPass()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.TTLCheckPass(new TTLCheckPassParmeter { Check_ID = "lisapicheck001", Note = "n1" }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// TTL Check Warn
        /// </summary>
        static void TTLCheckWarn()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.TTLCheckWarn(new TTLCheckPassParmeter { Check_ID = "lisapicheck001", Note = "n1" }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// TTL Check Fail
        /// </summary>
        static void TTLCheckFail()
        {
            var agentGovern = new AgentGovern();
            var result = agentGovern.TTLCheckFail(new TTLCheckPassParmeter { Check_ID = "lisapicheck001", Note = "n1" }).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        /// <summary>
        /// TTL Check Update
        /// </summary>
        static void TTLCheckUpdate()
        {
            var agentGovern = new AgentGovern();
            var registerCheck = new TTLCheckUpdateParmeter()
            {
                Check_ID = "lisapicheck001",
                Status = "passing",
                Output = "abc"
            };
            var result = agentGovern.TTLCheckUpdate(registerCheck).GetAwaiter().GetResult();
            Console.WriteLine($"result={result.result}");
            Console.WriteLine($"back content={result.backJson}");
        }
        #endregion

        static string EntityToString(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


    }
}
