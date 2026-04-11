using System;
using System.Security.Cryptography.X509Certificates;
using Stateless;
namespace BugPro
{
    public enum StatesOfBug
    {
        Opened,
        BeingAnalysed,
        InProcess,
        Postponed,
        Solved,
        Rejected,
        Returning
    }
    public enum BugActions
    {
        Beginning,
        Accept,
        Do,
        Reject,
        TryToSolve,
        WasOk,
        WasBad,
        Return
    }
    public class Bug
    {
        StateMachine<StatesOfBug, BugActions> sm;
        public Bug()
        {
            sm = new StateMachine<StatesOfBug, BugActions>(StatesOfBug.Opened);
            sm.Configure(StatesOfBug.Opened).Permit(BugActions.Beginning, StatesOfBug.BeingAnalysed);
            sm.Configure(StatesOfBug.BeingAnalysed).Permit(BugActions.Accept, StatesOfBug.InProcess);
            sm.Configure(StatesOfBug.BeingAnalysed).Permit(BugActions.Do, StatesOfBug.Postponed);
            sm.Configure(StatesOfBug.BeingAnalysed).Permit(BugActions.Reject, StatesOfBug.Rejected);
            sm.Configure(StatesOfBug.BeingAnalysed).Permit(BugActions.WasOk, StatesOfBug.Solved);
            sm.Configure(StatesOfBug.BeingAnalysed).Permit(BugActions.WasBad, StatesOfBug.Returning);
            sm.Configure(StatesOfBug.Postponed).Permit(BugActions.Beginning, StatesOfBug.BeingAnalysed);
            sm.Configure(StatesOfBug.InProcess).Permit(BugActions.TryToSolve, StatesOfBug.BeingAnalysed);
            sm.Configure(StatesOfBug.Returning).Permit(BugActions.Accept, StatesOfBug.InProcess);
            sm.Configure(StatesOfBug.Returning).Permit(BugActions.Reject, StatesOfBug.Rejected);
            sm.Configure(StatesOfBug.Rejected).Permit(BugActions.Return, StatesOfBug.BeingAnalysed);
            sm.Configure(StatesOfBug.Solved).Permit(BugActions.Accept, StatesOfBug.Returning); 
        }
        public StatesOfBug GetState()
        {
            return sm.State;
        }
        public void Run()
        {
            sm.Fire(BugActions.Beginning);
            Console.WriteLine("Machine has been started");
        }
        public void Accept()
        {
            sm.Fire(BugActions.Accept);
            Console.WriteLine("Accepted");
        }
        public void Solve()
        {
            sm.Fire(BugActions.TryToSolve);
            Console.WriteLine("Machine is solving");
        }
        public void Do()
        {
            sm.Fire(BugActions.Do);
            Console.WriteLine("Machine is doing smth");
        }
        public void Reject()
        {
            sm.Fire(BugActions.Reject);
            Console.WriteLine("Machine rejected");
        }
        public void Accomplished()
        {
            sm.Fire(BugActions.TryToSolve);
            Console.WriteLine("Machine solved");
        }
        public void WhetherOk()
        {
            sm.Fire(BugActions.WasOk);
            Console.WriteLine("OK");
        }
        public void WhetherBad()
        {
            sm.Fire(BugActions.WasBad);
            Console.WriteLine("Not OK");
        }
        public void Return()
        {
            sm.Fire(BugActions.Return);
            Console.WriteLine("Machine returned");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Bug bug = new Bug();
            bug.Run(); bug.Accept(); bug.Accomplished();
            bug.WhetherBad(); bug.Accept(); bug.Accomplished();
            bug.WhetherOk();
            Console.WriteLine(bug.GetState());      
        }
    }
}
