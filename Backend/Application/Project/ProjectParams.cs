using Application.Core;

namespace Application.Project
{
    public class ProjectParams : PagingParams
    {
        public bool ProjectIsActive { get; set; } = true;
    }
}