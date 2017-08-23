namespace AuditSandbox.Models
{
    public interface ITestModel
    {
        int Id { get; set; }
    }

    public class TestModel : ITestModel
    {
        public int Id { get; set; }
        public string Test { get; set; }
    }
}