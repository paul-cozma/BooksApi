// generate app model, that contains app name, list of environments and repository url

namespace AppBuild.Models
{
    public class AppModel
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public ICollection<Envs> Envs { get; set; }
        public string RepositoryUrl { get; set; }
    }

    public class Envs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int AppModelId { get; set; }
    }
}