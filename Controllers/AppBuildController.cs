#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppBuild.Models;
using DockerModel;
using Terminal;
namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppBuildController : ControllerBase
    {
        private readonly AppBuildContext _context;

        public AppBuildController(AppBuildContext context)
        {
            _context = context;
        }

        // GET: api/AppBuild
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppModel>>> GetAppModels()
        {
            return await _context.AppModels.ToListAsync();
        }

        // GET: api/AppBuild/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppModel>> GetAppModel(int id)
        {
            var appModel = await _context.AppModels.FindAsync(id);
            var environmentModel = await _context.Envs.Where(e => e.AppModelId == id).ToListAsync();

            if (appModel == null)
            {
                return NotFound();
            }

            appModel.Envs = environmentModel;
            return appModel;
        }

        // PUT: api/AppBuild/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppModel(int id, AppModel appModel)
        {
            if (id != appModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(appModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppBuild
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppModel>> PostAppModel(AppModel appModel)
        {
            _context.AppModels.Add(appModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppModel", new { id = appModel.Id }, appModel);
        }

        // DELETE: api/AppBuild/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppModel(int id)
        {
            var appModel = await _context.AppModels.FindAsync(id);
            if (appModel == null)
            {
                return NotFound();
            }

            _context.AppModels.Remove(appModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppModelExists(int id)
        {
            return _context.AppModels.Any(e => e.Id == id);
        }


        // edit environment for app
        [HttpPut("/api/AppBuild/{id}/environment")]
        public async Task<IActionResult> PutEnvironment(int id, Envs environment)
        {
            if (id != environment.AppModelId)
            {
                return BadRequest();
            }

            _context.Entry(environment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // insert environment for app
        [HttpPost("/api/AppBuild/{id}/environment")]
        public async Task<ActionResult<Envs>> PostEnvironment(int id, Envs environment)
        {
            if (id != environment.AppModelId)
            {
                return BadRequest();
            }

            _context.Envs.Add(environment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnvironment", new { id = environment.Id }, environment);
        }

        // get list of images from docker registry, http://localhost:5000/v2/_catalog
        [HttpGet("/api/AppBuild/{id}/registry")]
        public async Task<ActionResult<DockerModel.DockerModel>> GetImages(int id)
        {
            // get the name of the app
            var appModel = await _context.AppModels.FindAsync(id);
            if (appModel == null)
            {
                return NotFound();
            }

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5000/v2/{appModel.AppName}/admin/tags/list");
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseString = await response.Content.ReadAsStringAsync();
            var dockerModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DockerModel.DockerModel>(responseString);
            return dockerModel;



        }

        // get the repository url and build the docker image based on the repository url
        [HttpPost("/api/AppBuild/{id}/{version}/build")]
        public async Task<IActionResult> BuildDocker(int id, string version)
        {
            // get repository url by id
            var environment = await _context.AppModels.FindAsync(id);
            if (environment == null)
            {
                return NotFound();
            }
            var repositoryUrl = environment.RepositoryUrl;
            var appName = environment.AppName;
            var goToBuildPath = $"cd /home/paulc";
            var doesAppFolderExist = $"if [ -d /home/paulc/{appName} ]; then echo 'true'; else echo 'false'; fi";
            var folderExists = await Terminal.RunTerminalCommand.RunCommand(doesAppFolderExist);

            var gitInstall = $"git clone {repositoryUrl} {appName}";
            var goToRepoPath = $"cd {appName}";
            if (folderExists == "true\n")
            {
                gitInstall = $"cd /home/paulc/{appName} && git pull";
                goToRepoPath = $"cd /home/paulc/{appName}";
            }
            var buildCommand = $"docker build -t {appName}:{version} .";
            var tagImage = $"docker tag {appName}:{version} localhost:5000/{appName}:{version}";
            var pushImage = $"docker push localhost:5000/{appName}:{version}";
            var runAllCommands = $"{goToBuildPath} && {gitInstall} && {goToRepoPath} && {buildCommand} && {tagImage} && {pushImage}.";
            // run all commands in terminal
            var buildCommandResult = await Terminal.RunTerminalCommand.RunCommand(runAllCommands);

            return NoContent();

        }

    }
}
