using DocumentTemplate.Console.Models;

namespace DocumentTemplate.Tests.Models;

public class ApprovalWorkflowTests
{
    [Fact]
    public void Clone_ShouldCreateIndependentCopy()
    {
        // Arrange
        var original = new ApprovalWorkflow
        {
            Approvers = new List<string> { "manager@example.com", "legal@example.com" },
            RequiredApprovals = 2,
            TimeoutDays = 5
        };

        // Act
        var clone = original.Clone();

        // Assert
        Assert.NotNull(clone);
        Assert.NotSame(original, clone);
        Assert.NotSame(original.Approvers, clone.Approvers);
        Assert.Equal(original.RequiredApprovals, clone.RequiredApprovals);
        Assert.Equal(original.TimeoutDays, clone.TimeoutDays);
        Assert.Equal(original.Approvers.Count, clone.Approvers.Count);
    }

    [Fact]
    public void Clone_ModifyingCloneApprovers_ShouldNotAffectOriginal()
    {
        // Arrange
        var original = new ApprovalWorkflow
        {
            Approvers = new List<string> { "manager@example.com", "legal@example.com" },
            RequiredApprovals = 2,
            TimeoutDays = 5
        };

        // Act
        var clone = original.Clone();
        clone.Approvers.Add("ceo@example.com");
        clone.RequiredApprovals = 3;
        clone.TimeoutDays = 10;

        // Assert
        Assert.Equal(2, original.Approvers.Count);
        Assert.Equal(3, clone.Approvers.Count);
        Assert.Equal(2, original.RequiredApprovals);
        Assert.Equal(3, clone.RequiredApprovals);
        Assert.Equal(5, original.TimeoutDays);
        Assert.Equal(10, clone.TimeoutDays);
    }

    [Fact]
    public void ApprovalWorkflow_ShouldInitializeWithEmptyApprovers()
    {
        // Arrange & Act
        var workflow = new ApprovalWorkflow();

        // Assert
        Assert.NotNull(workflow.Approvers);
        Assert.Empty(workflow.Approvers);
        Assert.Equal(0, workflow.RequiredApprovals);
        Assert.Equal(0, workflow.TimeoutDays);
    }
}

