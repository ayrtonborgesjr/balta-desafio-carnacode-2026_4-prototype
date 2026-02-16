using DocumentTemplate.Console.Interfaces;

namespace DocumentTemplate.Console.Models;

public class ApprovalWorkflow : IPrototype<ApprovalWorkflow>
{
    public List<string> Approvers { get; set; } = new();
    public int RequiredApprovals { get; set; }
    public int TimeoutDays { get; set; }
    
    public ApprovalWorkflow Clone()
    {
        return new ApprovalWorkflow
        {
            Approvers = new List<string>(Approvers),
            RequiredApprovals = RequiredApprovals,
            TimeoutDays = TimeoutDays
        };
    }
}