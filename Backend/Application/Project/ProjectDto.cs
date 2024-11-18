namespace Application.Project
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool ProjectIsActive { get; set; }
        public ICollection<Domain.Seller> Sellers { get; set; }
    }
}