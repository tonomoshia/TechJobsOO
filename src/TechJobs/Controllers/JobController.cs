using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job Ajob = jobData.Find(id);
            return View(Ajob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            //Utilize the isValid method of ModelState class. check if it is indeed valid
            if (ModelState.IsValid)
            //if it is valid . .
            {
                //Create an instance of job passing in appropriate parameters
                Job newJob = new Job
                //Add a new job to the Jobs property of the jobData instance
                {
                    Name = newJobViewModel.Name,
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)
                };
                jobData.Jobs.Add(newJob);
                //then if it is still true, return a redirect to its appropriate place

                return Redirect($"/job?id={newJob.ID}");
            }
            //if its not valid . . .
            //do what I'm doing below
            return View(newJobViewModel);
        }
    }
}
